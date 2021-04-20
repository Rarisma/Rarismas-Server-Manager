using System;
using System.Windows;
using System.Windows.Controls;
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
        }

        public void Launcher(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new CLIServer();
        }


        private void ConfigServer(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new Pages.Minecraft_Java.ServerPropitiesEditor();
        }
        
        private void DeleteServer(object sender, RoutedEventArgs e)
        {
            if (ModernWpf.MessageBox.Show("Are you sure you want to delete this server?", "Confirm deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                System.IO.Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//", true);
                ModernWpf.MessageBox.Show("Server deleted");
                ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new SSM_GUI.Welcome();
            }
        }
        
        private void GoBack(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new Pages.SSM_GUI.Welcome();
        }
        
    }
}
