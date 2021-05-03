using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace SSM.Pages.SSM_GUI.Servers.CLI
{
    /// <summary>
    /// Interaction logic for ServerUI.xaml
    /// </summary>
    public partial class ServerUI : UserControl
    {
        public ServerUI()
        {
            InitializeComponent();

            ServerInfo.cmd.StartInfo.FileName = "cmd.exe";
            ServerInfo.cmd.StartInfo.RedirectStandardInput = true;
            ServerInfo.cmd.StartInfo.RedirectStandardOutput = true;
            ServerInfo.cmd.StartInfo.CreateNoWindow = true;
            ServerInfo.cmd.StartInfo.UseShellExecute = false;
            ServerInfo.cmd.StartInfo.Arguments = Convert.ToString("/k " + AppDomain.CurrentDomain.BaseDirectory);
            ServerInfo.cmd.Start(); //Runs the server


            ServerInfo.cmd.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data)) { Application.Current.Dispatcher.Invoke(new Action(() => { ServerOutput.AppendText("\n" + e.Data); })); }
                Application.Current.Dispatcher.Invoke(new Action(() => { ServerOutput.ScrollToEnd(); }));
            });

            ServerInfo.cmd.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data)) { Application.Current.Dispatcher.Invoke(new Action(() => { ServerOutput.AppendText("\nERROR: " + e.Data); })); }
                Application.Current.Dispatcher.Invoke(new Action(() => { ServerOutput.ScrollToEnd(); }));
            });



            ServerInfo.cmd.StandardInput.WriteLine("cd Servers");
            ServerInfo.cmd.StandardInput.Flush();
            ServerOutput.Text += "Cd'd";
            ServerInfo.cmd.StandardInput.WriteLine("cd " + ServerInfo.ServerLabel);
            ServerInfo.cmd.StandardInput.Flush();

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
        }

        public void SetQuickCommands()
        {
            switch (ServerInfo.ServerGame)
            {
                case "Minecraft Bedrock": QuickCommands.Content = new Minecraft_Java.QuickCommands();  break;
                case "Minecraft Java": QuickCommands.Content = new Minecraft_Java.QuickCommands(); break;
            }
        }

        public static void BaseInitalisation()
        {
            ServerInfo.cmd.StartInfo.FileName = "cmd.exe";
            ServerInfo.cmd.StartInfo.RedirectStandardInput = true;
            ServerInfo.cmd.StartInfo.RedirectStandardOutput = true;
            ServerInfo.cmd.StartInfo.CreateNoWindow = false;
            ServerInfo.cmd.StartInfo.UseShellExecute = false;
            ServerInfo.cmd.StartInfo.Arguments = Convert.ToString("/k " + AppDomain.CurrentDomain.BaseDirectory);
        }

        public static void MountServerFolder()
        {
            ServerInfo.cmd.StandardInput.WriteLine("cd Servers");
            ServerInfo.cmd.StandardInput.Flush();
            ServerInfo.cmd.StandardInput.WriteLine("cd " + ServerInfo.ServerLabel);
            ServerInfo.cmd.StandardInput.Flush();
        }

        public static void OpenServer()
        {
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
        }

    }
}
