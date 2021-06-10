using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RSM2.Server
{
    public partial class Configuration : UserControl
    {
        public string URL;
        public string Version;
        public Configuration()
        {
            AvaloniaXamlLoader.Load(this);
            this.Find<TextBlock>("ServerName").Text = ServerInfo.Name + " configuration";
            ContentControl ConfigFile = this.Find<ContentControl>("ConfigFile");
            TabItem ConfigFileTab = this.Find<TabItem>("ConfigFileTab");
            TextBlock UpdateNotice = this.Find<TextBlock>("UpdateNotice");
            Button UpdateButton = this.Find<Button>("UpdateButton");
            Button Mod = this.Find<Button>("Mod");
            Button Plugin = this.Find<Button>("Plugin");
            ComboBox BackupFrequency = this.Find<ComboBox>("BackupFrequency");
            List<String> ServerReader = new();

            if (ServerInfo.BackupFrequency == "Never") { BackupFrequency.SelectedIndex = 0; }
            else if (ServerInfo.BackupFrequency == "On Launch") { BackupFrequency.SelectedIndex = 1; }
            else if (ServerInfo.BackupFrequency == "Monthly") { BackupFrequency.SelectedIndex = 3; }
            else { BackupFrequency.SelectedIndex = 2; }


            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    ConfigFile.Content = new ConfigFiles.Minecraft();
                    if (ServerInfo.Variant == "Modded") 
                    {
                        Mod.IsEnabled = true; 
                        Plugin.Opacity=0; 
                        ServerReader = new List<string> { "UNSUPPORTED", "127.0.0.1" };
                        this.Find<Button>("Datapacks").IsEnabled = true;
                    }
                    else
                    {
                        Plugin.IsEnabled = true;
                        this.Find<Button>("Datapacks").IsEnabled = true;
                        Mod.Opacity = 0;
                        LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/Paper", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Paper");
                        ServerReader.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Paper"));
                    }
                    break;

                case "Minecraft Bedrock":
                    Plugin.Opacity = 0;
                    Mod.Opacity = 0;
                    ConfigFile.Content = new ConfigFiles.Minecraft();
                    LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerFiles/Minecraft/bedrock", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Bedrock");
                    ServerReader.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Bedrock"));
                    break;

                case "Terraria":
                    Plugin.IsEnabled = true;
                    ConfigFileTab.IsEnabled = false;
                    LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerFiles/Terraria/Tshock", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "TShock");
                    ServerReader.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//TShock"));
                    break;

                default: ServerReader.Add("Error"); ServerReader.Add("Error"); break;
            }

            Version = ServerReader[0];
            URL = ServerReader[1];
            if (Version == "UNSUPPORTED") { UpdateButton.Content = "Not supported."; UpdateButton.IsEnabled = false; }
            else if (Version == ServerInfo.Version && Version != "UNSUPPORTED") { UpdateButton.IsEnabled = false; UpdateButton.Content = "Up to date!"; }
            else { UpdateButton.Content = "Update available!"; }

            InitaliseInfomation();
        }

        //This sets up the infomation shown in the Infomation Section
        void InitaliseInfomation()
        {
            this.Find<Label>("InfoName").Content += ServerInfo.Name;
            this.Find<Label>("InfoGame").Content += ServerInfo.Game;
            this.Find<Label>("InfoVariant").Content += ServerInfo.Variant;
            this.Find<Label>("InfoFreq").Content += ServerInfo.BackupFrequency;
            this.Find<Label>("InfoLastBackup").Content += ServerInfo.Lastbackup;
            this.Find<Label>("InfoImported").Content += "Created in RSM";
            this.Find<Label>("InfoUpdateStatus").Content += " Supported.";

            DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//" ); //POV You are going in LibRarisma
            long KBSize = dir.EnumerateFiles("*.*", SearchOption.AllDirectories).Sum(fi => fi.Length) / 1024;
            this.Find<Label>("InfoFileSize").Content += KBSize / 1024 + "MB";

            //This just blanks the ones out that arent supported 
            this.Find<Label>("InfoSize").Content += "Not supported.";
            this.Find<Label>("InfoDifficulty").Content += "Not supported.";
            this.Find<Label>("InfoRAM").Content += "Not supported.";

            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    this.Find<Label>("InfoRAM").Content = "RAM Allocated: " + ServerInfo.RAM;
                    if (ServerInfo.Variant == "Modded") { this.Find<Label>("InfoUpdateStatus").Content = "Update status: This server cannot be updated by RSM"; }
                    break;

                case "Terraria":
                    this.Find<Label>("InfoSize").Content = "World Size: " + ServerInfo.WorldSize;
                    this.Find<Label>("InfoDifficulty").Content = "World Size: " + ServerInfo.Difficulty;
                    break;

            }
        }


        //Shows the helpw window
        private void ShowHelp(object sender, RoutedEventArgs e) { Window ConnectionHelper = new UI.ConnectionHelper(); ConnectionHelper.Show(); }

        private void Plugins(object sender, RoutedEventArgs e)
        {
            switch (ServerInfo.Game)
            {
                case "Terraria": LibRarisma.Utils.OpenFolder(AppDomain.CurrentDomain.BaseDirectory + "Servers\\" + ServerInfo.Name + "\\ServerPlugins\\"); break;
                default: LibRarisma.Utils.OpenFolder(AppDomain.CurrentDomain.BaseDirectory + "Servers\\" + ServerInfo.Name + "\\plugins\\"); break;
            }
        }

        private void Datapacks(object sender, RoutedEventArgs e)
        {
            LibRarisma.Utils.OpenFolder(AppDomain.CurrentDomain.BaseDirectory + "Servers\\" + ServerInfo.Name + "\\world\\datapacks");
        }

        private void Mods(object sender, RoutedEventArgs e)
        {
            switch (ServerInfo.Game)
            {
                default: LibRarisma.Utils.OpenFolder(AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.Name + "\\mods\\"); break;
            }
        }

        private void Updater(object sender, RoutedEventArgs e)
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//Server.jar");
                    LibRarisma.IO.DownloadFile(URL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//", "Server.jar");
                    break;
                case "Minecraft Bedrock":
                    LibRarisma.IO.DownloadFile(URL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//", "Server.zip");
                    System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//Server.zip", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//", true);
                    break;
                case "Terraria":
                    LibRarisma.IO.DownloadFile(URL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//", "Server.zip");
                    System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//Server.zip", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//", true);
                    break;
            }

            ServerInfo.Version = Version;
        }

        private void GoBack(object sender, RoutedEventArgs e) { ServerInfo.MainWindow.Content = new Manager(); }
        private void Save(object sender, RoutedEventArgs e) 
        {
            ComboBox Frequency = this.Find<ComboBox>("BackupFrequency");
            ServerInfo.BackupFrequency = Frequency.SelectedItem.ToString();
            Utilities.MakeINI();

            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    string[] File = ServerInfo.ConfigFileTextBox.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    System.IO.File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//Server.properties",File); 
                    break;
            }
        }
    }
}
