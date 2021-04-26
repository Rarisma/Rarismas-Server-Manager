using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
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
            switch (ServerInfo.ServerGame)
            {
                case "Minecraft Bedrock":
                    ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new CLIServer();
                    break;

                case "Minecraft Java":
                    ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new CLIServer();
                    break;

                case "Terraria":
                    ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new Terraria.TerrariaServer();
                    break;
            }
        }


        private void ConfigServer(object sender, RoutedEventArgs e)
        {
            switch (ServerInfo.ServerGame)
            {
                case "Minecraft Java":
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.ServerLabel + "\\server.properties")) { ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new Pages.Minecraft_Java.ServerPropitiesEditor(); }
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
        
        private void GoBack(object sender, RoutedEventArgs e) { ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new Pages.SSM_GUI.Welcome(); }
        
    }
}
