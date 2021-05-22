using System;
using System.Collections.Generic;
using System.Windows;
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
            List<String> ServerReader = new();
            
            switch (ServerInfo.Game) 
            {
                case "Minecraft Java":
                    ConfigFile.Content = new PerGameSettings.MinecraftText();
                    if (ServerInfo.Variant == "Forge") { ServerVer.IsEnabled = false; ServerVer.Opacity = 0; }
                    else 
                    {
                        LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/Paper", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Paper");
                        ServerReader.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Paper"));
                    }
                    break;

                case "Minecraft Bedrock":
                    ConfigFile.Content = new PerGameSettings.Minecraft();
                    LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerFiles/Minecraft/bedrock", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Bedrock");
                    ServerReader.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Bedrock"));
                    break;
                
                case "Terraria":
                    ConfigFileTab.IsEnabled = false;
                    Backup.IsSelected = true;
                    LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerFiles/Terraria/Tshock", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "TShock");
                    ServerReader.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//TShock"));
                    break;

                default:
                    ServerReader.Add("You shouldn't see this. DO NOT CLICK UPDATE.");
                    ServerReader.Add("This shouldn't happen. EVER.");
                    break;
            }

            Version = ServerReader[0];
            URL = ServerReader[1];
            LatestVersion.Text = "Latest Version: " + Version;
            if (Version == ServerInfo.Version) { UpdateButton.IsEnabled = false; UpdateButton.Content = "Your server is up to date!"; }
            else { UpdateNotice.Opacity = 1; }
            switch (ServerInfo.BackupFrequency)
            {
                case "Disabled": Disabled.IsChecked = true; break;
                case "Monthly": Monthly.IsChecked = true; break;
                case "On Launch": OnLaunch.IsChecked = true; break;
                default: Weekly.IsChecked = true; break;
            }
        }

        private void FrequencyOnLaunch(object sender, RoutedEventArgs e) { ServerInfo.BackupFrequency = "On Launch"; Utilities.Make_INI_File(); }
        private void FrequencyWeekly(object sender, RoutedEventArgs e)   { ServerInfo.BackupFrequency = "Weekly"; Utilities.Make_INI_File(); }
        private void FrequencyMonthly(object sender, RoutedEventArgs e)  { ServerInfo.BackupFrequency = "Monthly"; Utilities.Make_INI_File(); }
        private void FrequencyDisabled(object sender, RoutedEventArgs e) { ServerInfo.BackupFrequency = "Disabled"; Utilities.Make_INI_File(); }

        private void Updater(object sender, RoutedEventArgs e)
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//Server.jar");
                    LibRarisma.IO.DownloadFile(URL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//", "Server.jar");
                    break;
                case "Minecraft Bedrock":
                    LibRarisma.IO.DownloadFile(URL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//", "Server.zip");
                    System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//Server.zip", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//", true);
                    break;
                case "Terraria":
                    LibRarisma.IO.DownloadFile(URL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//", "Server.zip");
                    System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//Server.zip", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//", true);
                    break;
            }

            ServerInfo.Version = Version;
        }

        private void GoBack(object sender, RoutedEventArgs e) { ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new ServerManger(); }
    }
}
