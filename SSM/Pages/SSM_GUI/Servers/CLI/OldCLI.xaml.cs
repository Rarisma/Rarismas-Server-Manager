using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//So if anyone ever reads these, heres an update on the steamlink stuff I talked about
//So I went, and put a 24hrs of silence vid to prevent sleeping
//I got there and decided to sleep my pc
//So I called a person up and got them to turn it back on
//it happened again later that night but they were out so I guess I couldn't do anything
//Gonna figure out what SSH is exactly and use a RPi to send the packets if I can
namespace SSM.Pages.SSM_GUI.Servers.CLI
{
    /// <summary>
    /// Interaction logic for OldCLI.xaml
    /// </summary>
    public partial class OldCLI : Page
    {
        public OldCLI()
        {
            InitializeComponent();
            Initalise(); //Setups command prompt
            ServerInfo.IsServerRunning = true;
            LaunchServer();
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
                    ServerInfo.cmd.BeginOutputReadLine();

                    //This mounts the server
                    ServerInfo.cmd.StandardInput.WriteLine("cd Servers");
                    ServerInfo.cmd.StandardInput.Flush();
                    ServerInfo.cmd.StandardInput.WriteLine("cd " + ServerInfo.ServerLabel);
                    ServerInfo.cmd.StandardInput.Flush();
                    break;
            }
        }

        private void LaunchServer() //Handles actually launching the server and setting quick commands
        {
            switch (ServerInfo.ServerGame)
            {
                case "Minecraft Bedrock":
                    QuickCommandsFrame.Content = new Minecraft_Java.QuickCommands();
                    ServerInfo.cmd.StandardInput.WriteLine("bedrock_server.exe");
                    ServerInfo.cmd.StandardInput.Flush();
                    break;

                case "Minecraft Java":
                    QuickCommandsFrame.Content = new Minecraft_Java.QuickCommands();
                    ServerInfo.cmd.StandardInput.WriteLine("java -Xms" + ServerInfo.RAM + "M -jar Server.jar nogui");
                    ServerInfo.cmd.StandardInput.Flush();
                    break;

                case "Terraria":
                    QuickCommandsFrame.Content = new Terraria.QuickCommands();
                    ServerInfo.cmd.StandardInput.WriteLine("TerrariaServer.exe -autocreate 3 -world C:\\Users\\Rarisma\\Documents\\My Games\\Terraria\\Worlds\\Test.wrld");
                    ServerInfo.cmd.StandardInput.Flush();
                    break;
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