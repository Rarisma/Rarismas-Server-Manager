using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RSM.Creator
{
    class Servers { public static List<String> URLs = new(); }

    public partial class WorldSize : Page
    {
        public WorldSize()
        {
            InitializeComponent();
            Servers.URLs.Clear();
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Terraria/Tshock", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "TerraiaTShock");

            string[] URLs = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//TerraiaTShock");
            ServerInfo.Version = URLs[0];
            ServerInfo.URL = URLs[1];


            //This sets the items inside the textbox
            switch (ServerInfo.Game)
            {
                case "Terraria":
                    SizeBox.Items.Add("Large");
                    SizeBox.Items.Add("Medium");
                    SizeBox.Items.Add("Small");
                    break;
            }
        }

        private void WorldSizeUpdated(object sender, SelectionChangedEventArgs e)
        {
            if (Convert.ToString(SizeBox.SelectedValue).Contains("Large")) { ServerInfo.WorldSize = "3"; }
            else if (Convert.ToString(SizeBox.SelectedValue).Contains("Medium")) { ServerInfo.WorldSize = "2"; }
            else if (Convert.ToString(SizeBox.SelectedValue).Contains("Small")) { ServerInfo.WorldSize = "1"; }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            ServerInfo.Version = "Not set";
            ServerInfo.URL = "Not set";
            ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new RSMGeneric.UI.LaunchPage();
        }

        private void Continue(object sender, RoutedEventArgs e) { ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new RSMGeneric.UI.Downloader(); }
    }
}