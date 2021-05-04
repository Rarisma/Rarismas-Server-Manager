using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Diagnostics;
//A temporary forever.
namespace SSM.Pages.SSM_GUI
{
    /// <summary>
    /// Interaction logic for ServerManager.xaml
    /// </summary>
    public partial class ServerManager : UserControl
    {
        public ServerManager()
        {
            InitializeComponent();
            ServerVersion.Text = ServerInfo.ServerGame + " " + ServerInfo.ServerVersion + " (" + ServerInfo.ServerVariant + ")";
            ServerName.Text = ServerInfo.ServerLabel;

            switch (ServerInfo.ServerVariant) //adjusts visible buttons to users so /mods/ and /plugins/ can't be accessed on server variants that dont support them
            {
                case "Paper":    ModButton.Opacity = 0; ModButton.IsEnabled = false; break;
                case "Forge":    PluginButton.Opacity = 0; PluginButton.IsEnabled = false; break;
                case "Normal":   ModButton.Opacity = 0; ModButton.IsEnabled = false; break;
                default:
                    PluginButton.Opacity = 0; PluginButton.IsEnabled = false;
                    ModButton.Opacity = 0; ModButton.IsEnabled = false;
                    break;
            }

        }

        public void Launcher(object sender, RoutedEventArgs e)
        {
            switch (ServerInfo.ServerGame)
            {
                default: ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new CLIServer(); break;
            }
        }

        private void ConfigServer(object sender, RoutedEventArgs e)
        {
            switch (ServerInfo.ServerGame)
            {
                case "Minecraft Java":
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.ServerLabel + "\\server.properties")) { ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new Pages.Minecraft_Java.ServerPropitiesEditor(); }
                    else { ModernWpf.MessageBox.Show("Failed to find server propities file, try launching the server and comming back here"); }
                    break;
            }
        }
        
        private void DeleteServer(object sender, RoutedEventArgs e)
        {
            if (ModernWpf.MessageBox.Show("Are you sure you want to delete this server?", "Confirm deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//", true);
                ModernWpf.MessageBox.Show("Server deleted");
                ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new Welcome();
            }
        }
        
        private void Mods(object sender, RoutedEventArgs e) { SSMGeneric.OpenFolder(AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.ServerLabel + "\\mods\\"); }
        
        private void GoBack(object sender, RoutedEventArgs e) { ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new Welcome(); }

        private void OpenPluginFolder(object sender, RoutedEventArgs e) { SSMGeneric.OpenFolder(AppDomain.CurrentDomain.BaseDirectory + "Servers\\" + ServerInfo.ServerLabel + "\\plugins\\"); }

        private void ConnectionHelp(object sender, RoutedEventArgs e)
        {
            Window ConnectionHelper = new ConnectionHelp();
            ConnectionHelper.Show();
        }
    }
}
