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

namespace SSM.Pages.Terraria
{
    /// <summary>
    /// Interaction logic for TerrariaServer.xaml
    /// </summary>
    public partial class TerrariaServer : UserControl
    {
        bool Initalised = false;
        public TerrariaServer()
        {
            InitializeComponent();
            ServerName.Text = ServerInfo.ServerLabel;
            Status.Text = "Current status: Not Running.";

            LaunchServer();
        }

        private void LaunchServer() //Handles actually launching the server
        {
            if (Initalised == false)
            {
                ServerInfo.cmd.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//TerrariaServer.exe";
                ServerInfo.cmd.StartInfo.RedirectStandardInput = true;
                ServerInfo.cmd.StartInfo.RedirectStandardOutput = true;
                ServerInfo.cmd.StartInfo.RedirectStandardError = true;
                ServerInfo.cmd.StartInfo.CreateNoWindow = true;
                ServerInfo.cmd.StartInfo.UseShellExecute = false;
                ServerInfo.cmd.StartInfo.Arguments = "-autocreate " + ServerInfo.ServerWorldSize + " -world  " + AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//world.wld";
                ServerInfo.cmd.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//";
                ServerInfo.cmd.Start();
                ServerInfo.IsServerRunning = true;
                ServerInfo.InputStream = ServerInfo.cmd.StandardInput;


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

                ServerInfo.cmd.BeginOutputReadLine();
                ServerInfo.cmd.BeginErrorReadLine();
                Initalised = true;
                Status.Text = "Current status: Running!";

               // ServerInfo.cmd.Close();

            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("save");
            ServerInfo.cmd.StandardInput.Flush();
        }

        private void SendInput(object sender, RoutedEventArgs e)
        {
            string command = Input.Text;
            SendCommand(Input.Text);
            Input.Text = "";
        }

        public static void SendCommand(string command)
        {
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.ServerLabel + "\\SSM\\SSM.txt", command);
        }


        private void OpenServer(object sender, RoutedEventArgs e)
        {
            if (ServerInfo.IsServerRunning == false) { LaunchServer(); }
            else { ModernWpf.MessageBox.Show("Server is already running."); }
        }

        private void StopServer(object sender, RoutedEventArgs e)
        {
            ServerInfo.InputStream.WriteLine("stop");
            ServerInfo.InputStream.Flush();
            ServerInfo.IsServerRunning = false;
            Status.Text = "Current status: Not Running.";
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string command = Input.Text;
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.ServerLabel + "\\SSM\\SSM.txt", command);
            //Input.Text = "";
        }
    }
}
