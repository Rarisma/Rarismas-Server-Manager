using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SSM.Pages.SSM_GUI
{
    /// <summary>
    /// Interaction logic for CLIServer.xaml
    /// </summary>
    public partial class CLIServer : Page
    {
        public CLIServer()
        {
            InitializeComponent();
            Initalise(); //Setups command prompt
            ServerInfo.IsServerRunning = true;
            ServerUtils.LaunchServer();
            SetQuickCommands();
        }

        private void Initalise() //This runs all the server configuration eg mounting folders and redirection
        {
            ServerName.Text = ServerInfo.ServerLabel;

            //This opens the command line and sets up the Output/Error redirection
            ServerInfo.cmd.StartInfo.RedirectStandardInput = true;
            ServerInfo.cmd.StartInfo.RedirectStandardOutput = true;
            ServerInfo.cmd.StartInfo.RedirectStandardError = true;
            ServerInfo.cmd.StartInfo.CreateNoWindow = true;
            SetServerType();

            ServerInfo.cmd.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data)) { Application.Current.Dispatcher.Invoke(new Action(() => { ServerConsole.AppendText("\n" + e.Data); })); }
                Application.Current.Dispatcher.Invoke(new Action(() => { ServerConsole.ScrollToEnd(); }));
            });

            ServerInfo.cmd.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data)) { Application.Current.Dispatcher.Invoke(new Action(() => { ServerConsole.AppendText("\nERROR: " + e.Data); })); }
                Application.Current.Dispatcher.Invoke(new Action(() => { ServerConsole.ScrollToEnd(); }));
            });
        }

        private void SetServerType()
        {
            switch (ServerInfo.ServerGame)
            {
                case "Terraria":
                    ServerInfo.cmd.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//TerrariaServer.exe";
                    ServerInfo.cmd.StartInfo.Arguments = "-autocreate " + ServerInfo.ServerWorldSize + " -world  " + AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//world.wld";
                    ServerInfo.cmd.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//";
                    ServerInfo.IsServerRunning = true;
                    ServerInfo.cmd.Start();
                    ServerInfo.cmd.BeginOutputReadLine();
                    break;

                default:
                    ServerInfo.cmd.StartInfo.FileName = "cmd.exe";
                    ServerInfo.cmd.StartInfo.Arguments = Convert.ToString("/k " + AppDomain.CurrentDomain.BaseDirectory);
                    ServerInfo.cmd.Start();
                    try { ServerInfo.cmd.BeginOutputReadLine(); } catch { }

                    //This mounts the server
                    ServerInfo.cmd.StandardInput.WriteLine("cd Servers");
                    ServerInfo.cmd.StandardInput.Flush();
                    ServerInfo.cmd.StandardInput.WriteLine("cd " + ServerInfo.ServerLabel);
                    ServerInfo.cmd.StandardInput.Flush();
                    break;
            }
        }

        private void SetQuickCommands() //Sets Quick command frame
        {
            switch (ServerInfo.ServerGame)
            {
                case "Minecraft Bedrock": QuickCommandsFrame.Content = new Minecraft_Java.QuickCommands(); break;
                case "Minecraft Java": QuickCommandsFrame.Content = new Minecraft_Java.QuickCommands(); break;
                case "Terraria": QuickCommandsFrame.Content = new Terraria.QuickCommands(); break;
            }
        }

        private void SendInput(object sender, RoutedEventArgs e)
        {
            switch (ServerInfo.ServerGame)
            {
                case "Terraria":
                    string command = Input.Text;
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.ServerLabel + "\\SSM\\SSM.txt", Input.Text);
                    Input.Text = "";
                    break;
                default:
                    ServerInfo.cmd.StandardInput.WriteLine(Input.Text);
                    ServerInfo.cmd.StandardInput.Flush();
                    Input.Text = "";
                    break;
            }

        }

    }
}
