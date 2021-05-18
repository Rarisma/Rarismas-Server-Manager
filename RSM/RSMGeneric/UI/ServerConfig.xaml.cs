using System;
using System.Collections.Generic;
using System.Windows.Controls;
//If you ain't in on the RSM train, you shoulda came
namespace RSM.RSMGeneric.UI
{

    public partial class ServerConfig : Page
    {
        public string URL;
        public string Version;
        public ServerConfig()
        {
            InitializeComponent();
            ServerName.Text = ServerInfo.Label + " configuration";
            ServerVersion.Text = "Server Version: " + ServerInfo.Version;

            switch (ServerInfo.Game) 
            {
                case "Minecraft Java":
                    if (ServerInfo.Variant == "Forge") { ServerVer.IsEnabled = false; ServerVer.Opacity = 0; }
                    else 
                    {
                        LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/Paper", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Paper");
                        List<String> ServerReader = new();
                        ServerReader.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Paper"));
                        Version = ServerReader[0];
                        URL = ServerReader[1];
                        LatestVersion.Text = "Latest Version:" + Version;
                    }
                    break;
            }

            switch (ServerInfo.BackupFrequency)
            {
                case "Disabled": Disabled.IsChecked = true; break;
                case "Monthly": Monthly.IsChecked = true; break;
                case "On Launch": OnLaunch.IsChecked = true; break;
                default: Weekly.IsChecked = true; break;
            }

        }

        private void FrequencyOnLaunch(object sender, System.Windows.RoutedEventArgs e) { ServerInfo.BackupFrequency = "On Launch"; Utilities.Make_INI_File(); }
        private void FrequencyWeekly(object sender, System.Windows.RoutedEventArgs e) { ServerInfo.BackupFrequency = "Weekly"; Utilities.Make_INI_File(); }
        private void FrequencyMonthly(object sender, System.Windows.RoutedEventArgs e) { ServerInfo.BackupFrequency = "Monthly"; Utilities.Make_INI_File(); }
        private void FrequencyDisabled(object sender, System.Windows.RoutedEventArgs e) { ServerInfo.BackupFrequency = "Disabled"; Utilities.Make_INI_File(); }

        private void Updater(object sender, System.Windows.RoutedEventArgs e)
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//Server.jar");
                    LibRarisma.IO.DownloadFile(URL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//", "Server.jar");
                    break;
                case "Terraria":
                    //LibRarisma.IO.DownloadFile(URL)
                    break;
            }

            ServerInfo.Version = Version;
        }
    }
}
