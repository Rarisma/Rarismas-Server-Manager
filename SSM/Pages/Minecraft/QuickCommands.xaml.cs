using System;
using System.Collections.Generic;
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

namespace SSM.Pages.Minecraft_Java
{
    /// <summary>
    /// Interaction logic for QuickCommands.xaml
    /// </summary>
    public partial class QuickCommands : UserControl
    {
        public QuickCommands()
        {
            InitializeComponent();
         
            if (ServerInfo.ServerGame == "Minecraft Bedrock")
            {
                SeedButton.IsEnabled = false; //Disables seed button as /seed isn't supported on bedrock
                SeedButton.Opacity = 0;
                SaveButton.IsEnabled = false;
                SaveButton.Opacity = 0;
            }
        }

        private void WeatherClear(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("weather clear");
            ServerInfo.cmd.StandardInput.Flush();
        }

        private void TimeSetDay(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("time set day");
            ServerInfo.cmd.StandardInput.Flush();
        }

        private void TimeSetNight(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("time set night");
            ServerInfo.cmd.StandardInput.Flush();
        }

        private void Peaceful(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("difficulty peaceful");
            ServerInfo.cmd.StandardInput.Flush();
        }

        private void Easy(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("difficulty easy");
            ServerInfo.cmd.StandardInput.Flush();
        }

        private void Normal(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("difficulty normal");
            ServerInfo.cmd.StandardInput.Flush();
        }

        private void Hard(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("difficulty hard");
            ServerInfo.cmd.StandardInput.Flush();
        }

        private void Seed(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("seed");
            ServerInfo.cmd.StandardInput.Flush();
        }

        private void OpenServer(object sender, RoutedEventArgs e)
        {
            /*if (ServerInfo.IsServerRunning == false) { LaunchServer(); }
            else { ModernWpf.MessageBox.Show("ServerInfo is already running."); }*/
        }

        private void StopServer(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("stop");
            ServerInfo.cmd.StandardInput.Flush();
            ServerInfo.IsServerRunning = false;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("save-all");
            ServerInfo.cmd.StandardInput.Flush();
        }
    }
}
