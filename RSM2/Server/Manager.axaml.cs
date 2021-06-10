using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.IO;
//So while writing RSM2.0 I watched season 7 and 8 of RWBY (Which I hadn't watched since 2018) and I watched Konosuba S2 and the movie
//I listened to bullet hell 1 and 2 (By RicharddEB) + Scaled and Icy (The outside is the best song easy fucking fight me)
namespace RSM2.Server
{
    public partial class Manager : UserControl
    {
        public Manager()
        {
            AvaloniaXamlLoader.Load(this);
            TextBlock ServerVersion = this.Find<TextBlock>("ServerVersion");
            TextBlock ServerName = this.Find<TextBlock>("ServerName");
            ServerVersion.Text = ServerInfo.Game + " " + ServerInfo.Version + " (" + ServerInfo.Variant + ")";
            ServerName.Text = ServerInfo.Name;

        }

        void Backup()
        { 
            Int32 stagingfolder = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Utilities.DirectoryCopy(AppDomain.CurrentDomain.BaseDirectory + "Servers//" + ServerInfo.Name + "//", AppDomain.CurrentDomain.BaseDirectory + "//Backups//" + ServerInfo.Name + " - " + stagingfolder + "//", true);
            ServerInfo.Lastbackup = DateTime.Now.ToString("dd/MM/yyyy");
            Utilities.MakeINI();
        }


        public void Launcher(object sender, RoutedEventArgs e)
        {
            Int64 daysbetween = Convert.ToInt64((DateTime.Now - Convert.ToDateTime(ServerInfo.Lastbackup)).TotalDays) - 1;

            if (daysbetween >= 7 && ServerInfo.BackupFrequency == "Weekly") { Backup(); }
            else if (daysbetween >= 30 && ServerInfo.BackupFrequency == "Monthly") { Backup(); }
            else if (ServerInfo.BackupFrequency == "On Launch") { Backup(); }

            ServerInfo.MainWindow.Content = new CLI();
        }

        private void ConfigServer(object sender, RoutedEventArgs e) { ServerInfo.MainWindow.Content = new Server.Configuration(); }

        private void GoBack(object sender, RoutedEventArgs e) { ServerInfo.MainWindow.Content = new UI.Welcome(); }
    }
}
