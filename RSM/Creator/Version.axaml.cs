using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.IO;

namespace RSM.Creator
{
    class Servers { public static List<String> URLs = new(); }
    public partial class Version : UserControl
    {
        public Version()
        {
            AvaloniaXamlLoader.Load(this);
            List<String> VersionSorter = new();
            List<String> AvailableVersions = new();
            ComboBox Version = this.Find<ComboBox>("Version");

            if (ServerInfo.Variant == "Vanilla" && ServerInfo.Game == "Minecraft Java")
            {
                LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/Paper", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Paper");
                VersionSorter.AddRange(File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Paper"));
            }
            else if (ServerInfo.Variant == "Modded" && ServerInfo.Game == "Minecraft Java")
            {
                LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/Forge", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Forge");
                VersionSorter.AddRange(File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Forge"));
            }

            VersionSorter.RemoveAll(str => string.IsNullOrEmpty(str)); //Update to use LibRarisma.Utils.Clean();
            for (int i = 0; i <= (VersionSorter.Count - 1); i++)
            {
                if (Convert.ToString(VersionSorter[i][0]) == "1") { AvailableVersions.Add(VersionSorter[i]); }//This adds version names 
                else if (VersionSorter[i].Contains("http")) { Servers.URLs.Add(VersionSorter[i]); }          //This adds the URLs
            }
            Version.Items = AvailableVersions; //Sets the combobox to the desired results
        }


        private void GoBack(object sender, RoutedEventArgs e)
        {
            ServerInfo.Version = "Not set";
            ServerInfo.URL = "Not set";

            switch (ServerInfo.Game)
            {
                case "Minecraft Java": Global.Display.Content = new RAM(); break;
                case "Terraria": Global.Display.Content = new UI.Welcome(); break;
            }
        }

        private void Continue(object sender, RoutedEventArgs e)
        {
            if (this.Find<ComboBox>("Version").SelectedIndex != -1)
            {
                ServerInfo.Version = this.Find<ComboBox>("Version").SelectedItem.ToString();
                ServerInfo.URL = Servers.URLs[this.Find<ComboBox>("Version").SelectedIndex];
                Global.Display.Content = new UI.Downloader();
            }

        }
    }
}
