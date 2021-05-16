using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
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
                case "Paper": ModButton.IsEnabled = false; Config.IsEnabled = true; break;
                case "Forge": PluginButton.IsEnabled = false; Config.IsEnabled = true; break;
                case "Normal": ModButton.IsEnabled = false; Config.IsEnabled = false;  break;
                default:
                    PluginButton.IsEnabled = false;
                    ModButton.IsEnabled = false;
                    Config.IsEnabled = false;
                    break;
            }

        }

        void Backup()
        { //If you care, the server that was used to test this was called solitaire, which is a reference to a song (Its a banger!)
            Int32 stagingfolder = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Utilities.DirectoryCopy(AppDomain.CurrentDomain.BaseDirectory + "Servers//" + ServerInfo.Label + "//", AppDomain.CurrentDomain.BaseDirectory + "//Backups//" + ServerInfo.Label + " - " + stagingfolder + "//", true);
        }


        public void Launcher(object sender, RoutedEventArgs e)
        {
            DateTime time = DateTime.Parse(ServerInfo.Lastbackup);
            Int64 daysbetween = Convert.ToInt64(Convert.ToString(Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")) - time).Replace(".00:00:00",""));

            if (daysbetween >= 7 && ServerInfo.BackupFrequency == "Weekly") { Backup(); }
            else if (daysbetween >= 30 && ServerInfo.BackupFrequency == "Monthly") { Backup(); }
            else if (ServerInfo.BackupFrequency == "On Launch") { Backup(); }

            switch (ServerInfo.Game)
            {
                default: ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new CLI(); break;
            }
        }

        private void ConfigServer(object sender, RoutedEventArgs e) { ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new ServerConfig(); }

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

        private void GoBack(object sender, RoutedEventArgs e) { ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new LaunchPage(); }

        private void OpenPluginFolder(object sender, RoutedEventArgs e) 
        {
            switch (ServerInfo.Game)
            {
                case "Terraria": Utilities.OpenFolder(AppDomain.CurrentDomain.BaseDirectory + "Servers\\" + ServerInfo.Label + "\\ServerPlugins\\"); break;
                default: Utilities.OpenFolder(AppDomain.CurrentDomain.BaseDirectory + "Servers\\" + ServerInfo.Label + "\\plugins\\"); break;
            }

        }

        private void ConnectionHelp(object sender, RoutedEventArgs e)
        {
            Window ConnectionHelper = new ConnectionHelper();
            ConnectionHelper.Show();
        }
    }
}
