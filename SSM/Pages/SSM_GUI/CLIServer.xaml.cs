using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace SSM.Pages.SSM_GUI
{
    /// <summary>
    /// Interaction logic for CLIServer.xaml
    /// </summary>
    public partial class CLIServer : UserControl
    {
        public CLIServer()
        {
            InitializeComponent();
            ServerName.Text = ServerInfo.ServerLabel;
            Status.Text = "Current status: Not Running.";
            RAMUsage.Text = "RAM Usage / " + ServerInfo.RAM;

            Server.cmd.StartInfo.FileName = "cmd.exe";
            Server.cmd.StartInfo.RedirectStandardInput = true;
            Server.cmd.StartInfo.RedirectStandardOutput = true;
            Server.cmd.StartInfo.CreateNoWindow = true;
            Server.cmd.StartInfo.UseShellExecute = false;
            Server.cmd.StartInfo.Arguments = Convert.ToString("/k " + AppDomain.CurrentDomain.BaseDirectory);
            LaunchServer();
        }

        private void LaunchServer()
        {
            if(Server.Initalised == false)
            {
                Server.cmd.Start();
                ServerInfo.IsServerRunning = true;
                Server.cmd.BeginOutputReadLine();

                Server.cmd.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data)) { Application.Current.Dispatcher.Invoke(new Action(() => { ServerConsole.AppendText("\n" + e.Data); })); }
                    Application.Current.Dispatcher.Invoke(new Action(() => { ServerConsole.ScrollToEnd(); }));
                });

                Server.cmd.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data)) { Application.Current.Dispatcher.Invoke(new Action(() => { ServerConsole.AppendText("\nERROR: " + e.Data); })); }
                    Application.Current.Dispatcher.Invoke(new Action(() => { ServerConsole.ScrollToEnd(); }));
                });

                Server.MountFolder();
                Server.Initalised = true;
            }

            Status.Text = "Current status: Running!";

            switch (ServerInfo.ServerGame)
            {
                case "Minecraft Java":
                    Server.cmd.StandardInput.WriteLine("java -Xms" + ServerInfo.RAM + "M -jar Server.jar nogui");
                    Server.cmd.StandardInput.Flush();
                    break;
                case "Minecraft Bedrock":
                    Server.cmd.StandardInput.WriteLine("bedrock_server.exe");
                    Server.cmd.StandardInput.Flush();
                    SeedButton.IsEnabled = false; //Disables seed button as /seed isn't supported on bedrock
                    SeedButton.Opacity = 0;
                    SaveButton.IsEnabled = false; //Disables seed button as /seed isn't supported on bedrock
                    SaveButton.Opacity = 0;
                    break;
            }
        }

        private void OpenServer(object sender, RoutedEventArgs e)
        {
            if (ServerInfo.IsServerRunning == false) { LaunchServer(); }
            else { ModernWpf.MessageBox.Show("Server is already running."); }
        }

        private void StopServer(object sender, RoutedEventArgs e)
        {
            Server.cmd.StandardInput.WriteLine("stop");
            Server.cmd.StandardInput.Flush();
            ServerInfo.IsServerRunning = false;
            Status.Text = "Current status: Not Running.";
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            Server.cmd.StandardInput.WriteLine("save-all");
            Server.cmd.StandardInput.Flush();
        }

        private void WeatherClear(object sender, RoutedEventArgs e)
        {
            Server.cmd.StandardInput.WriteLine("weather clear");
            Server.cmd.StandardInput.Flush();
        }

        private void TimeSetDay(object sender, RoutedEventArgs e)
        {
            Server.cmd.StandardInput.WriteLine("time set day");
            Server.cmd.StandardInput.Flush();
        }
        
        private void TimeSetNight(object sender, RoutedEventArgs e)
        {
            Server.cmd.StandardInput.WriteLine("time set night");
            Server.cmd.StandardInput.Flush();
        }

        private void Peaceful(object sender, RoutedEventArgs e)
        {
            Server.cmd.StandardInput.WriteLine("difficulty peaceful");
            Server.cmd.StandardInput.Flush();
        }

        private void Easy(object sender, RoutedEventArgs e)
        {
            Server.cmd.StandardInput.WriteLine("difficulty easy");
            Server.cmd.StandardInput.Flush();
        }

        private void Normal (object sender, RoutedEventArgs e)
        {
            Server.cmd.StandardInput.WriteLine("difficulty Normal");
            Server.cmd.StandardInput.Flush();
        }

        private void Hard(object sender, RoutedEventArgs e)
        {
            Server.cmd.StandardInput.WriteLine("difficulty Hard");
            Server.cmd.StandardInput.Flush();
        }

        private void Seed(object sender, RoutedEventArgs e)
        {
            Server.cmd.StandardInput.WriteLine("seed");
            Server.cmd.StandardInput.Flush();
        }

        private void SendInput(object sender, RoutedEventArgs e)
        {
            Server.cmd.StandardInput.WriteLine(Input.Text);
            Server.cmd.StandardInput.Flush();
        }
    }

    class Server 
    {
        static public Process cmd = new();
        static public bool Initalised = false;
        public static void MountFolder()
        {
            Server.cmd.StandardInput.WriteLine("cd Servers");
            Server.cmd.StandardInput.Flush();
            Server.cmd.StandardInput.WriteLine("cd " + ServerInfo.ServerLabel);
            Server.cmd.StandardInput.Flush();
        }
    }


}
