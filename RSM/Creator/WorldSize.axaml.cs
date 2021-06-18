using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.IO;
//My batteries full charged, yall aint got shit on me!
namespace RSM.Creator
{
    public partial class WorldSize : UserControl
    {
        public WorldSize()
        {
            AvaloniaXamlLoader.Load(this);
            //Servers.URLs.Clear();
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Terraria/Tshock", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "TerraiaTShock");

            string[] URLs = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//TerraiaTShock");
            ServerInfo.Version = URLs[0];
            ServerInfo.URL = URLs[1];
            ServerInfo.Variant = "TShock";

            //This sets the items inside the textbox
            switch (ServerInfo.Game)
            {
                case "Terraria":
                    this.Find<ComboBox>("SizeBox").Items = new string[] { "Large", "Medium", "Small" };
                    break;
            }
        }

        private void WorldSizeUpdated(object sender, SelectionChangedEventArgs e)
        {   //Parses data from the combobox
            ComboBox SizeBox = this.Find<ComboBox>("SizeBox");
            if (Convert.ToString(SizeBox.SelectedItem).Contains("Large")) { ServerInfo.WorldSize = "3"; }
            else if (Convert.ToString(SizeBox.SelectedItem).Contains("Medium")) { ServerInfo.WorldSize = "2"; }
            else if (Convert.ToString(SizeBox.SelectedItem).Contains("Small")) { ServerInfo.WorldSize = "1"; }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            ServerInfo.Version = "Not set";
            ServerInfo.URL = "Not set";
            Global.Display.Content = new UI.Welcome();
        }

        private void Continue(object sender, RoutedEventArgs e) { Global.Display.Content = new Difficulty(); }
    }
}
