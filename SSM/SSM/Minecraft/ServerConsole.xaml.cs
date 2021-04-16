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

//It took me 12 hours to make this
//Massive thanks to Stack underflow and the CHADs at the C# Discord for helping me understand how to make this magic work
//Living with determination.


namespace SSM.Minecraft
{
    /// <summary>
    /// Interaction logic for ServerConsole.xaml
    /// </summary>
    /// 

    class Server
    {
        static public Process cmd = new();

    }

    public partial class ServerConsole : Page
    {
        public ServerConsole()
        {
            Application.Current.MainWindow.Width = 800;
            Application.Current.MainWindow.Height = 800;
            InitializeComponent();
            Init();
        }
    
        public void Init()
        {
            ServerStatus.Content = MinecraftCreatorData.ServerName + " is currently running";

            Server.cmd.StartInfo.FileName = "cmd.exe";
            Server.cmd.StartInfo.RedirectStandardInput = true;
            Server.cmd.StartInfo.RedirectStandardOutput = true;
            Server.cmd.StartInfo.CreateNoWindow = true;
            Server.cmd.StartInfo.UseShellExecute = false;
            Server.cmd.StartInfo.Arguments = Convert.ToString("/k " + (char)34 + MinecraftCreatorData.ManagerFilepath + (char)34);

            Server.cmd.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data)) { Application.Current.Dispatcher.Invoke(new Action(() => { ServerOutput.AppendText("\n" + e.Data); })); }
                Application.Current.Dispatcher.Invoke(new Action(() => { ServerOutput.ScrollToEnd(); }));
            });

            Server.cmd.ErrorDataReceived += new DataReceivedEventHandler((sender, e) => 
            {
                if (!String.IsNullOrEmpty(e.Data)) { Application.Current.Dispatcher.Invoke(new Action(() => { ServerOutput.AppendText("\nERROR: " + e.Data); })); }
                Application.Current.Dispatcher.Invoke(new Action(() => { ServerOutput.ScrollToEnd(); }));
            });

            Server.cmd.Start();
            MinecraftCreatorData.IsServerRunning = true;
            Server.cmd.BeginOutputReadLine();

            Server.cmd.StandardInput.WriteLine("cd Servers");
            Server.cmd.StandardInput.Flush();
            Server.cmd.StandardInput.WriteLine("cd " + MinecraftCreatorData.ServerName);
            Server.cmd.StandardInput.Flush();

            if (MinecraftCreatorData.ServerType == "Bedrock") { Server.cmd.StandardInput.WriteLine("bedrock_server.exe"); }
            else { Server.cmd.StandardInput.WriteLine("java -Xms" + MinecraftCreatorData.AllocatedRAM + "M -jar Server.jar nogui"); }
            Server.cmd.StandardInput.Flush();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Server.cmd.StandardInput.WriteLine("stop");
            Server.cmd.StandardInput.Flush();
            ServerStatus.Content = MinecraftCreatorData.ServerName + " isn't running.";
            MinecraftCreatorData.IsServerRunning = false;
        }
    }
}
