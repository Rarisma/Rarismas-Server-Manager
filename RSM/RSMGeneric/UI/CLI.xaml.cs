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
//hope for tomorrow is slim
namespace RSM.RSMGeneric.UI
{
    public partial class CLI : Page
    {
        public CLI()
        {
            InitializeComponent();
            Initalise(); //Setups command prompt
            ServerInfo.Running = true;
            Utilities.LaunchServer();
            SetQuickCommands();
        }

        private void Initalise() //This runs all the server configuration eg mounting folders and redirection
        {
            ServerName.Text = ServerInfo.Label;

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
            switch (ServerInfo.Game)
            {
                case "Terraria":
                    ServerInfo.cmd.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//TerrariaServer.exe";
                    ServerInfo.cmd.StartInfo.Arguments = "-autocreate " + ServerInfo.WorldSize + " -world World.wld -difficulty " + ServerInfo.Difficulty;
                    ServerInfo.cmd.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//";
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
                    ServerInfo.cmd.StandardInput.WriteLine("cd " + ServerInfo.Label);
                    ServerInfo.cmd.StandardInput.Flush();
                    break;
            }
        }

        private void SetQuickCommands() //Sets Quick command frame
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Bedrock": QuickCommandsFrame.Content = new QuickCommands.Minecraft(); break;
                case "Minecraft Java": QuickCommandsFrame.Content = new QuickCommands.Minecraft(); break;
                case "Terraria": QuickCommandsFrame.Content = new QuickCommands.Terraria(); break;
            }
        }

        private void SendInput(object sender, RoutedEventArgs e)
        {
            switch (ServerInfo.Game)
            {
                case "Terraira":
                    
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
