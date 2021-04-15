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

            var cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.Arguments = Convert.ToString("/k " + (char)34 + MinecraftCreatorData.ManagerFilepath + (char)34);

            cmd.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data)) { Application.Current.Dispatcher.Invoke(new Action(() => { ServerOutput.AppendText("\n" + e.Data); })); }
                Application.Current.Dispatcher.Invoke(new Action(() => { ServerOutput.ScrollToEnd(); }));
            });

            cmd.ErrorDataReceived += new DataReceivedEventHandler((sender, e) => 
            {
                if (!String.IsNullOrEmpty(e.Data)) { Application.Current.Dispatcher.Invoke(new Action(() => { ServerOutput.AppendText("\nERROR: " + e.Data); })); }
                Application.Current.Dispatcher.Invoke(new Action(() => { ServerOutput.ScrollToEnd(); }));
            });

            cmd.Start();
            cmd.BeginOutputReadLine();

            cmd.StandardInput.WriteLine("cd Servers");
            cmd.StandardInput.Flush();
            cmd.StandardInput.WriteLine("cd " + MinecraftCreatorData.ServerName);
            cmd.StandardInput.Flush();

            cmd.StandardInput.WriteLine("java -Xms" + MinecraftCreatorData.AllocatedRAM + "M -jar Server.jar nogui");
            cmd.StandardInput.Flush();



        }


    }

}
