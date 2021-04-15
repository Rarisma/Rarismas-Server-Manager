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

namespace SSM.Minecraft
{
    /// <summary>
    /// Interaction logic for ServerConsole.xaml
    /// </summary>
    public partial class ServerConsole : Page
    {
        public ServerConsole()
        {
            InitializeComponent();
            Init();
        }
    
        public async void Init()
        {
            var cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.Arguments = Convert.ToString("/k " + (char)34 + MinecraftCreatorData.ManagerFilepath + (char)34);

            cmd.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                // Prepend line numbers to each line of the output.
                if (!String.IsNullOrEmpty(e.Data))
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => { ServerOutput.Text +=  "\n" + e.Data; }));
                }
            });

            cmd.Start();
            cmd.BeginOutputReadLine();

            cmd.StandardInput.WriteLine("cd Servers");
            cmd.StandardInput.Flush();
            cmd.StandardInput.WriteLine("cd EasterEgg");
            cmd.StandardInput.Flush();

            cmd.StandardInput.WriteLine("java Xms8000M Server.jar -nogui");
            cmd.StandardInput.Flush();
        }


    }

}
