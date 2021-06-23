using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RSM.Server
{
    public partial class Manager : UserControl
    {
        public string URL;
        public string Version;
        public Manager()
        {
            AvaloniaXamlLoader.Load(this);

            this.Find<TextBlock>("ServerName").Text = ServerInfo.Name;
            this.Find<TextBlock>("ServerVersion").Text = ServerInfo.Game + " " + ServerInfo.Version + " (" + ServerInfo.Variant + ")";
            
            SetupInfomation();
            CheckForUpdates();
            ConfigureButtons();
            SetupConfigFile();
        }

        void CheckForUpdates()
        {
            switch (ServerInfo.Game) //This switch just downloads the correct file to the server directory named as Update
            {
                case "Minecraft Java": LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerFiles/Minecraft/Paper", ServerInfo.Dir, "Update"); break;
                case "Minecraft Bedrock": LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerFiles/Minecraft/bedrock", ServerInfo.Dir, "Update");  break;
                case "Terraria": LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerFiles/Terraria/Tshock", ServerInfo.Dir, "Update"); break;
            }

            Button UpdateButton = this.Find<Button>("UpdateButton");
            if (ServerInfo.Game != "Factorio" && ServerInfo.Game != "Mindustry" && ServerInfo.Variant != "Modded")
            {
                string[] UpdateFile = File.ReadAllLines(ServerInfo.Dir + "Update");
                if (ServerInfo.Version != UpdateFile[0]) //If version is missmatched
                {
                    UpdateButton.IsEnabled = true;
                    UpdateButton.Content = "Update available!";
                }
                else
                {
                    UpdateButton.IsEnabled = false;
                    UpdateButton.Content = "This server is up to date";
                }
            }
            else
            {
                UpdateButton.IsEnabled = false;
                UpdateButton.Content = "This server cannot be updated";
            }

        }

        void ConfigureButtons() //Configures the buttons shown in the options tab
        {
            if (Directory.Exists(ServerInfo.Dir + "//Mods//"))
            {
                this.Find<Button>("Mods").IsEnabled = true;
                this.Find<Button>("Mods").Opacity = 1;
            }

            if (Directory.Exists(ServerInfo.Dir + "//World//Datapacks//"))
            {
                this.Find<Button>("Datapacks").IsEnabled = true;
                this.Find<Button>("Datapacks").Opacity = 1;
            }

            if (Directory.Exists(ServerInfo.Dir + "//Plugins//") || Directory.Exists(ServerInfo.Dir + "//ServerPlugins//"))
            {
                this.Find<Button>("Plugins").IsEnabled = true;
                this.Find<Button>("Plugins").Opacity = 1;
            }

            ComboBox BackupFrequency = this.Find<ComboBox>("BackupFrequency");
            if (ServerInfo.BackupFrequency == "Never") { BackupFrequency.SelectedIndex = 0; }
            else if (ServerInfo.BackupFrequency == "On Launch") { BackupFrequency.SelectedIndex = 1; }
            else if (ServerInfo.BackupFrequency == "Monthly") { BackupFrequency.SelectedIndex = 3; }
            else { BackupFrequency.SelectedIndex = 2; }
        }

        void SetupConfigFile() //Sets up the config pannel
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java": this.Find<TextBox>("ConfigFile").Text = File.ReadAllText(ServerInfo.Dir + "Server.properties"); break;
                case "Minecraft Bedrock":
                    try
                    {
                        this.Find<TextBox>("ConfigFile").Text = File.ReadAllText(ServerInfo.Dir + "Server.properties"); break;

                    }
                    catch {this.Find<TextBox>("ConfigFile").Opacity = 0; this.Find<TextBox>("ConfigFile").IsEnabled = false; }
                    break;
                default: this.Find<TextBox>("ConfigFile").Opacity = 0; this.Find<TextBox>("ConfigFile").IsEnabled = false; break;

            }
        }

        void SetupInfomation()
        {
            this.Find<TextBlock>("InfoBox").Text = "Server name: " + ServerInfo.Name + "\nServer game: " + ServerInfo.Game + "\nServer Version " + ServerInfo.Version + "\nServer variant: " + ServerInfo.Variant + "\nBackup frequency: " + ServerInfo.BackupFrequency + "\nLast backup: " + ServerInfo.Lastbackup;

            if (ServerInfo.Game + " " + ServerInfo.Variant == "Minecraft Java Modded") { this.Find<TextBlock>("InfoBox").Text += "\nUpdates supported:" + " No"; }
            else { this.Find<TextBlock>("InfoBox").Text += "\nUpdates supported:" + " Yes"; }

            DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//"); //POV You are going in LibRarisma
            long KBSize = dir.EnumerateFiles("*.*", SearchOption.AllDirectories).Sum(fi => fi.Length) / 1024;
            this.Find<TextBlock>("InfoBox").Text += "\nServer filesize: " + KBSize / 1024 + "MB";

            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    this.Find<TextBlock>("InfoBox").Text += "\nRAM Allocated: " + ServerInfo.RAM;
                    break;

                case "Terraria":
                    this.Find<TextBlock>("InfoBox").Text += "\nWorld Size: " + ServerInfo.WorldSize;
                    this.Find<TextBlock>("InfoBox").Text += "\nDiffculty: " + ServerInfo.Difficulty;
                    break;
            }
        }

        //Handles opening folders, depending on the button name
        private void OpenFolder(object sender, RoutedEventArgs e)
        {
            try //In try catch to prevent crash if folder doesnt exist
            {
                switch ((sender as Button).Name)
                {
                    case "Plugins":
                        switch (ServerInfo.Game)
                        {
                            case "Terraria": LibRarisma.Utils.OpenFolder(ServerInfo.Dir + "//ServerPlugins//"); break;
                            default: LibRarisma.Utils.OpenFolder(ServerInfo.Dir +  "//plugins//"); break;
                        }
                        break;

                    case "Datapacks": LibRarisma.Utils.OpenFolder(ServerInfo.Dir + "\\world\\datapacks"); break;
                    case "Mods": LibRarisma.Utils.OpenFolder(ServerInfo.Dir + "\\mods\\"); break;
                }
            }
            catch { }
        }

        private void Updater(object sender, RoutedEventArgs e)
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//Server.jar");
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
            Utilities.MakeINI();
            Global.Display.Content = new Manager();
        }

        void Backup()
        {
            LibRarisma.Utils.DirectoryCopy(ServerInfo.Dir, AppDomain.CurrentDomain.BaseDirectory + "//Backups//" + ServerInfo.Name + " - " + Convert.ToString((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds) + "//", true);
            ServerInfo.Lastbackup = DateTime.Now.ToString("dd/MM/yyyy");
            Utilities.MakeINI();
        }

        public void Launcher(object sender, RoutedEventArgs e)
        {
            Int64 daysbetween = Convert.ToInt64((DateTime.Now - Convert.ToDateTime(ServerInfo.Lastbackup)).TotalDays) - 1;

            if (daysbetween >= 7 && ServerInfo.BackupFrequency == "Weekly") { Backup(); }
            else if (daysbetween >= 30 && ServerInfo.BackupFrequency == "Monthly") { Backup(); }
            else if (ServerInfo.BackupFrequency == "On Launch") { Backup(); }

            Global.Display.Content = new CLI();
        }

        private void Save(object sender, RoutedEventArgs e) 
        {
            string[] File;
            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    File = this.Find<TextBox>("ConfigFile").Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    System.IO.File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//Server.properties", File);
                    break;
                case "Minecraft Bedrock":
                    File = this.Find<TextBox>("ConfigFile").Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    System.IO.File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//Server.properties", File);
                    break;
            }

            if (this.Find<ComboBox>("BackupFrequency").SelectedIndex == 0) { ServerInfo.BackupFrequency = "Never"; }
            else if (this.Find<ComboBox>("BackupFrequency").SelectedIndex == 1) { ServerInfo.BackupFrequency = "On Launch"; }
            else if (this.Find<ComboBox>("BackupFrequency").SelectedIndex == 3) { ServerInfo.BackupFrequency = "Monthly"; }
            else { ServerInfo.BackupFrequency = "Weekly"; }

            Utilities.MakeINI();
            Utilities.ReadINI(ServerInfo.Name);
            Global.Display.Content = new Manager();
        }

        private void GoBack(object sender, RoutedEventArgs e) { Global.Display.Content = new UI.Welcome(); }

    }
}
