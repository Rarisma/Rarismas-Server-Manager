using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Microsoft.UI.Dispatching;
using Mono.Nat;
using RSMUltra;
using RSMUltra.Manager;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RSMUltra.Manager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Server : Page
    {
        public Server()
        {
            this.InitializeComponent();
            CmdBar.IsOpen = true;
            Name.Text = $"{ServerInfo.Name}";
            Game.Text = $"{ServerInfo.Game} {ServerInfo.Version} ({ ServerInfo.Variant})";

            //This section controls the buttons that appears in LinkButtons
            switch (ServerInfo.Game)
            {
                case "Minecraft Java Edition":
                    Button modButton = new() { Content = "Open Mods folder", HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(20) };
                    modButton.Click += Mods();
                    LinkPanels.Children.Add(modButton);
                    Button DatapacksButton = new() { Content = "Open Datapacks folder", HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(20) };
                    DatapacksButton.Click += Plugins();
                    LinkPanels.Children.Add(DatapacksButton);
                    Button PluginsButton = new() { Content = "Open Plugins folder", HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(20) };
                    PluginsButton.Click += Datapacks();
                    LinkPanels.Children.Add(PluginsButton);
                    break;
            }
        }

        //Shows CMDBar when pointer has entered
        private void CmdBarFix(object? sender, object e) { CmdBar.IsOpen = true; }

        //Hides CMDBar when pointer has left
        private void CmdBarUnfix(object sender, PointerRoutedEventArgs e) { CmdBar.IsOpen = false; }

        private void StartServer(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = false;
            Stop.IsEnabled = true;

            ServerConsole.Text += "[WARNING] This server is using RSM and has Project CloudSpotter enabled.\nIf you are reading this for debugging/error infomation see the following https://github.com/Rarisma/Rarismas-Server-Manager/wiki/Project-Cloudspotter";

            ServerInfo.Server.StartInfo.CreateNoWindow = false; //Might want to set this to true when debugging to see if the server is actually running
            ServerInfo.Server.StartInfo.WorkingDirectory = Global.ServerDir;

            switch (ServerInfo.Game)
            {
                case "Minecraft Java Edition":
                    if (ServerInfo.Variant == "Forge")
                    {
                        ServerInfo.Server.StartInfo.FileName = Global.Java8; //Forge doesn't work on Java16
                    }
                    else
                    {
                        ServerInfo.Server.StartInfo.FileName = Global.Java16; //Everything else is cool with Java16
                    }
                    ServerInfo.Server.StartInfo.Arguments = "-Xmx" + ServerInfo.AllocatedRAM + "M -jar \"" + Global.ServerDir + "\\Server.jar" + "\" nogui";
                    break;
            }

            //Setups handling for output
            ServerInfo.Server.StartInfo.RedirectStandardInput = true; 
            ServerInfo.Server.StartInfo.RedirectStandardError = true;
            ServerInfo.Server.StartInfo.RedirectStandardOutput = true;
            ServerInfo.Server.OutputDataReceived += new DataReceivedEventHandler((sender, e) => { if (!String.IsNullOrEmpty(e.Data)) { OutputRecieved(e.Data); }});
            ServerInfo.Server.ErrorDataReceived += new DataReceivedEventHandler((sender, e) => { if (!String.IsNullOrEmpty(e.Data)) { OutputRecieved(e.Data); }});

            ServerInfo.Server.Start(); //Starts the server running


            ServerInfo.Server.BeginErrorReadLine(); //Tells RSM to start reading any errors
            ServerInfo.Server.BeginOutputReadLine(); //Tells RSM to start reading the output

            //Starts the search for the router to Port Forward to via UPNP
            NatUtility.DeviceFound += ServerUtils.Forward;
            NatUtility.StartDiscovery();
        }

        public async Task OutputRecieved(string Data) //Actually displays output.
        {
            DispatcherQueue.TryEnqueue(
                () =>
                {
                    ServerConsole.Text += "\n" + ServerUtils.Filter(Data);
                    ServerConsole.SelectionStart = int.MaxValue;
                });
        }

        private void Reboot(object sender, RoutedEventArgs e)
        {
            ServerUtils.StopServer();
            StartServer(null,null);
        }

        private async void Cease(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => ServerUtils.StopServer());

            //Ends read operations to prevent crashes if Global.Server is needed again
            ServerInfo.Server.CancelErrorRead(); //Ends error read
            ServerInfo.Server.CancelOutputRead(); //Ends output read
            Start.IsEnabled = true;
            Stop.IsEnabled = false;
        }

        private void ClearConsole(object sender, RoutedEventArgs e) { ServerConsole.Text = ""; } //Deletes server log (Does not delete server logs if server makes them itself.)

        private RoutedEventHandler Mods()
        {
            return null;
        }
        private RoutedEventHandler Plugins()
        {
            return null;
        }
        private RoutedEventHandler Datapacks()
        {
            return null;
        }

        private void DeleteServer(object sender, RoutedEventArgs e)
        {
            Directory.Delete(Global.ServerDir, true);
            MainWindow.Frame.Content = new UltraUI.Main();
        }

    }
}
