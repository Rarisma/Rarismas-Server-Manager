using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SSM.Pages.SSM_GUI
{
    /// <summary>
    /// Interaction logic for CLIServer.xaml
    /// </summary>
    public partial class CLIServer : UserControl
    {
        bool Initalised = false;

        public CLIServer()
        {
            InitializeComponent();
            ServerName.Text = ServerInfo.ServerLabel;
            Status.Text = "Current status: Not Running.";
            ServerInfo.cmd.StartInfo.FileName = "cmd.exe";
            ServerInfo.cmd.StartInfo.RedirectStandardInput = true;
            ServerInfo.cmd.StartInfo.RedirectStandardOutput = true;
            ServerInfo.cmd.StartInfo.CreateNoWindow = true;
            ServerInfo.cmd.StartInfo.UseShellExecute = false;
            ServerInfo.cmd.StartInfo.Arguments = Convert.ToString("/k " + AppDomain.CurrentDomain.BaseDirectory);
            LaunchServer();
        }

        private void LaunchServer() //Handles actually launching the server
        {
            if (Initalised == false)
            {
                ServerInfo.cmd.Start();
                ServerInfo.IsServerRunning = true;
                ServerInfo.cmd.BeginOutputReadLine();


                ServerInfo.cmd.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data)) { Application.Current.Dispatcher.Invoke(new Action(() => { ServerConsole.AppendText("\n" + e.Data); })); }
                    Application.Current.Dispatcher.Invoke(new Action(() => { ServerConsole.ScrollToEnd(); }));
                });

                ServerInfo.cmd.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data)) { Application.Current.Dispatcher.Invoke(new Action(() => { ServerConsole.AppendText("\nERROR: " + e.Data); }));}
                    Application.Current.Dispatcher.Invoke(new Action(() => { ServerConsole.ScrollToEnd(); }));
                });

                ServerInfo.cmd.StandardInput.WriteLine("cd Servers");
                ServerInfo.cmd.StandardInput.Flush();
                ServerInfo.cmd.StandardInput.WriteLine("cd " + ServerInfo.ServerLabel);
                ServerInfo.cmd.StandardInput.Flush();
                Initalised = true;

                switch (ServerInfo.ServerGame)
                {
                    case "Minecraft Bedrock":
                        SaveButton.IsEnabled = false; //Disables save since it cannot be manually invoked
                        SaveButton.Opacity = 0;
                        QuickCommandsFrame.Content = new SSM.Pages.Minecraft_Java.QuickCommands();
                        break;

                    case "Minecraft Java":
                        QuickCommandsFrame.Content = new SSM.Pages.Minecraft_Java.QuickCommands();
                        break;
                }

               /* Commented out to prevent CLIServer not loading, CLIServer is gonna need a rewrite to implement safer shutdowns
                * and RAM/CPU Meters are gonnna be moved to a frame elsewhere
                * Also gonna move the server shutdown, open ect, to frames 
                * TerrariaServer.cs/xaml is gonna be merged
                * All coming either later or tomorrow depneding if my SteamLink works
                * Yes I own a steam link, I think I got one for about £11 just before they got EOL'd
                * They are now like £100 when you can do it with an RPi or an android, though I've never had much luck getting an RPi to be stable
                * Tbf tho I never really needed one until now
                * Anyway I gotta go, push this to github and portforward the correct stuff
                * 
                * - Rarisma, Screaming into the void since 2019
                PerformanceCounter RAM = new();
                RAM.CategoryName = "Process";
                RAM.CounterName = "Working Set - Private";
                RAM.InstanceName = ServerInfo.cmd.ProcessName;

                while (ServerInfo.IsServerRunning != false)
                {
                    RAMUsage.Text = Convert.ToString(Convert.ToInt32(RAM.NextValue() / 1024));
                }
                RAM.Close();
                RAM.Dispose();*/
            }

            switch (ServerInfo.ServerGame)
            {
                case "Minecraft Java":
                    ServerInfo.cmd.StandardInput.WriteLine("java -Xms" + ServerInfo.RAM + "M -jar Server.jar nogui");
                    ServerInfo.cmd.StandardInput.Flush();
                    break;
                case "Minecraft Bedrock":
                    ServerInfo.cmd.StandardInput.WriteLine("bedrock_server.exe");
                    ServerInfo.cmd.StandardInput.Flush();
                    break;
                case "Terraria":
                    ServerInfo.cmd.StandardInput.WriteLine("TerrariaServer.exe -autocreate 3 -world C:\\Users\\Rarisma\\Documents\\My Games\\Terraria\\Worlds\\Test.wrld");
                    ServerInfo.cmd.StandardInput.Flush();
                    break;

            }
            Status.Text = "Current status: Running!";
            
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            PerformanceCounter RAM = new();
            RAM.CategoryName = "Process";
            RAM.CounterName = "Working Set - Private";
            RAM.InstanceName = ServerInfo.cmd.ProcessName;

            while (ServerInfo.IsServerRunning != false)
            {
                (sender as BackgroundWorker).ReportProgress(Convert.ToInt32(RAM.NextValue()));
            }


            RAM.Close();
            RAM.Dispose();
        }



        private void Save(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("save-all");
            ServerInfo.cmd.StandardInput.Flush();
        }

        private void SendInput(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine(Input.Text);
            ServerInfo.cmd.StandardInput.Flush();
            Input.Text = "";
        }

        private void OpenServer(object sender, RoutedEventArgs e)
        {
            if (ServerInfo.IsServerRunning == false) { LaunchServer(); }
            else { ModernWpf.MessageBox.Show("ServerInfo is already running."); }
        }

        private void StopServer(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("stop");
            ServerInfo.cmd.StandardInput.Flush();
            ServerInfo.IsServerRunning = false;
            Status.Text = "Current status: Not Running.";
        }
    }
}
