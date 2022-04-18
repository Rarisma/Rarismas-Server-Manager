using System;
using System.Diagnostics;
using System.IO;
using Windows.UI;
using ABI.Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Color = ABI.Windows.UI.Color;
using Colors = Microsoft.UI.Colors;

namespace RSM;

public sealed partial class WideNodes : Page
{
    public Servers.Server Server;
    string NewData;
    DispatcherTimer timer = new();

    public WideNodes(ref Servers.Server server)
    {
        InitializeComponent();
        Server = server;
        timer.Interval = new(0, 0, 0, 0, 500);
        timer.Tick += Update;
        timer.Start();
    }

    private async void Update(object? sender, object e)
    {
        DiskStatus.Text = Server.GetDiskUsage() + " MB of Space used";
        try
        {
            if (Server.ServerProcess != null && Server.ServerProcess.StartInfo.FileName != ""){ Server.IsOn = !Server.ServerProcess.HasExited; }
        }
        catch (InvalidOperationException ex){}
        if (Server.IsOn)
        {
            try
            {
                //Console updating
                ConsoleBox.Text += NewData;
                NewData = "";

                //Deals with power status
                PowerIcon.Label = "Power off";
                PowerStatus.Text = $"Running on {Data.PublicIP}:{Server.ActualPort}";
                PowerStatus.Foreground = new SolidColorBrush(Colors.SeaGreen);

                //RAM status
                Server.ServerProcess.Refresh();
                RAMStatus.Foreground = new SolidColorBrush(Colors.LightGreen);
                long RamUsage = Server.ServerProcess.WorkingSet64 / 1024 / 1024;
                double RAMDec = (double) RamUsage / (double) Server.MaxRAMLimit;
                RAMDec = (double)Math.Round(RAMDec, 1);
                if (Server.ShowRAM)
                {
                    RAMStatus.Text = $"{RamUsage} MB of {Server.MaxRAMLimit} MB RAM used";
                    RAMStatus.Foreground = new SolidColorBrush(Colors.LightGreen);
                    if (RAMDec >= 0.4) { RAMStatus.Foreground = new SolidColorBrush(Colors.Green); }
                    if (RAMDec >= 0.6) { RAMStatus.Foreground = new SolidColorBrush(Colors.Yellow); }
                    if (RAMDec >= 0.8) { RAMStatus.Foreground = new SolidColorBrush(Colors.Red); }
                    if (RAMDec >= 0.9) { RAMStatus.Foreground = new SolidColorBrush(Colors.DarkRed); }
                }
                else
                {
                    RAMStatus.Text = $"{RamUsage} MB of RAM used";
                    if (RAMDec >= 0.4) { RAMStatus.Foreground = new SolidColorBrush(Colors.Green); }
                    if (RAMDec >= 0.6) { RAMStatus.Foreground = new SolidColorBrush(Colors.Yellow); }
                    if (RAMDec >= 0.8) { RAMStatus.Foreground = new SolidColorBrush(Colors.Red); }
                    if (RAMDec >= 0.9) { RAMStatus.Foreground = new SolidColorBrush(Colors.DarkRed); }
                }

                //CPU Usage
                double CPUDec = Math.Round(await Server.GetCPUUsage(), 2);
                CPUStatus.Text = CPUDec + "% CPU Usage";
                CPUStatus.Foreground = new SolidColorBrush(Colors.LightGreen);
                if (CPUDec >= 50) { CPUStatus.Foreground = new SolidColorBrush(Colors.Green); }
                if (CPUDec >= 60) { CPUStatus.Foreground = new SolidColorBrush(Colors.Yellow); }
                if (CPUDec >= 80) { CPUStatus.Foreground = new SolidColorBrush(Colors.Red); }
                if (CPUDec >= 90) { CPUStatus.Foreground = new SolidColorBrush(Colors.DarkRed); }
                Scroller.ScrollToVerticalOffset(Scroller.ScrollableHeight);
            }
            catch {/*Do nothing since it will just get sorted next update*/ } //This usually happens when switching between power states.
        }
        else
        {
            PowerStatus.Foreground = new SolidColorBrush(Colors.White);
            RAMStatus.Foreground = new SolidColorBrush(Colors.White);
            CPUStatus.Foreground = new SolidColorBrush(Colors.White);
            PowerIcon.Label = "Power on";
            PowerStatus.Text = "Powered off";
            RAMStatus.Text = "0 MB of RAM used";
            CPUStatus.Text = "0% CPU Usage";
        }
    }

    public void DeleteServer(object sender, RoutedEventArgs e)
    {
        if (Server.IsOn) {Server.ServerProcess.Kill();}
        (Data.Content.Content as StackPanel).Children.RemoveAt(Data.InstalledServers.IndexOf(Server));
        Data.InstalledServers.Remove(Server);
        Directory.Delete(Server.ParentDirectory, true);
    }

    private void GetRAMUsage()
    {
        // System.Diagnostics
    }

    public void Render()
    {
        StackPanel Stak = new();
        foreach (Servers.Server Listing in Data.InstalledServers)
        {
            var l = Listing;
            var Node = new WideNodes(ref l);
            Stak.Children.Add(Node);
        }
        Data.Content.Content = Stak;
    }

    private async void ShowInfoMenu(object sender, RoutedEventArgs e)
    {
        ContentDialog Info = new();
        Info.Title = $"Server Configuration for {Server.Name}";
        Info.SecondaryButtonText = "Close";
        Info.Content = new Info(ref Server);
        Info.XamlRoot = XamlRoot;
        await Info.ShowAsync();
        Server.ReadINI(Path.Combine(Server.ParentDirectory, "RSM.ini"));
    }
    private void TogglePower(object sender, RoutedEventArgs e)
    {
        Server.IsOn = !Server.IsOn;
        if (Server.IsOn)
        {
            Server.ServerLog = "";
            Server.ServerProcess = new();
            if (Server.EntryPoint == "cmd.exe")
            {
                Server.ServerProcess.StartInfo.FileName = "cmd.exe";
                Server.ServerProcess.StartInfo.Arguments = "/k " + Server.LaunchCommand.Replace("$RAM", Server.MaxRAMLimit.ToString()).Replace("$LAUNCHFILE", "\"" + Path.Combine(Server.ParentDirectory, Server.LaunchFile) + "\" disable-commands");
            }
            else
            {
                Server.ServerProcess.StartInfo.FileName = Path.Combine(Data.ToolChains, Server.EntryPoint);
                Server.ServerProcess.StartInfo.Arguments = Server.LaunchCommand.Replace("$RAM", Server.MaxRAMLimit.ToString()).Replace("$LAUNCHFILE", "\"" + Path.Combine(Server.ParentDirectory, Server.LaunchFile) + "\"");
            }
            Server.ServerProcess.StartInfo.WorkingDirectory = Server.ParentDirectory;
            Server.ServerProcess.StartInfo.RedirectStandardOutput = true;
            Server.ServerProcess.StartInfo.RedirectStandardError = true;
            Server.ServerProcess.StartInfo.CreateNoWindow = true;
            Server.ServerProcess.StartInfo.RedirectStandardInput = true;
            Server.ServerProcess.OutputDataReceived += (_, e) => { if (!string.IsNullOrEmpty(e.Data)) { NewData += "\n" + e.Data; } };
            Server.ServerProcess.ErrorDataReceived += (_, e) => { if (!string.IsNullOrEmpty(e.Data)) { NewData += "\n" + e.Data; } };
            Server.ServerProcess.Start();
            Server.ServerProcess.BeginErrorReadLine();
            Server.ServerProcess.BeginOutputReadLine();
            AutoBar.Items.Clear();
            foreach (var Suggestions in Server.QuickCommands.Split(",")) { AutoBar.Items.Add(Suggestions); }
            if (!Data.OpenPorts.Contains(Server.IdealPort)) { Data.OpenPorts.Add(Server.IdealPort); Server.ActualPort = Server.IdealPort; }
            AutoBar.IsEnabled = true;
        }
        else
        {
            Server.ServerProcess.CancelOutputRead();
            Server.ServerProcess.CancelOutputRead();
            Server.ServerProcess.Kill(true);
            AutoBar.IsEnabled = true;

        }
    }

    private void SendCommand(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        Server.ServerProcess.StandardInput.WriteLine(AutoBar.Text);
        Server.ServerProcess.StandardInput.Flush();
        AutoBar.Text = "";
    }

    private void Reboot(object sender, RoutedEventArgs e)
    {
        TogglePower(null,null);
        TogglePower(null, null);
    }

    /// <summary>
    /// This clears the console
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ClearConsole(object sender, RoutedEventArgs e) {  ConsoleBox.Text = "";  }

    private void OpenServerFolder(object sender, PointerRoutedEventArgs e)
    {
        Process.Start("explorer.exe", "/select, " + Server.ParentDirectory + "\\");
    }
}
