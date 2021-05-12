using System;
using System.Collections.Generic;
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
//K-Rino Flow Sessions are legendary
namespace RSM.RSMGeneric.UI
{
    /// <summary>
    /// Interaction logic for ServerManger.xaml
    /// </summary>
    public partial class ServerManger : Page
    {
        public ServerManger()
        {
            InitializeComponent();
            ServerVersion.Text = ServerInfo.Game + " " + ServerInfo.Version + " (" + ServerInfo.Variant + ")";
            ServerName.Text = ServerInfo.Label;

            switch (ServerInfo.Variant) //adjusts visible buttons to users so /mods/ and /plugins/ can't be accessed on server variants that dont support them
            {
                case "Paper": ModButton.Opacity = 0; ModButton.IsEnabled = false; break;
                case "Forge": PluginButton.Opacity = 0; PluginButton.IsEnabled = false; break;
                case "Normal": ModButton.Opacity = 0; ModButton.IsEnabled = false; break;
                default:
                    PluginButton.Opacity = 0; PluginButton.IsEnabled = false;
                    ModButton.Opacity = 0; ModButton.IsEnabled = false;
                    break;
            }

        }

        public void Launcher(object sender, RoutedEventArgs e)
        {
            switch (ServerInfo.Game)
            {
                default: ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new CLI(); break;
            }
        }

        private void ConfigServer(object sender, RoutedEventArgs e)
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.Label + "\\server.properties")) { ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new Creator.MinecraftServerPropitiesEditor(); }
                    else { ModernWpf.MessageBox.Show("Failed to find server propities file, try launching the server and comming back here"); }
                    break;
            }
        }

        private void DeleteServer(object sender, RoutedEventArgs e)
        {
            if (ModernWpf.MessageBox.Show("Are you sure you want to delete this server?", "Confirm deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//", true);
                ModernWpf.MessageBox.Show("Server deleted");
                ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new UI.LaunchPage();
            }
        }

        private void Mods(object sender, RoutedEventArgs e) { Utilities.OpenFolder(AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.Label + "\\mods\\"); }

        private void GoBack(object sender, RoutedEventArgs e) { ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new ServerManger(); }

        private void OpenPluginFolder(object sender, RoutedEventArgs e) { Utilities.OpenFolder(AppDomain.CurrentDomain.BaseDirectory + "Servers\\" + ServerInfo.Label + "\\plugins\\"); }

        private void ConnectionHelp(object sender, RoutedEventArgs e)
        {
            Window ConnectionHelper = new ConnectionHelper();
            ConnectionHelper.Show();
        }
    }
}
