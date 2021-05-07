using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
//Stay ABAP
//As ballin as possible
namespace SSM.Pages.Terraria
{
    class Servers { public static List<String> URLs = new(); }

    public partial class VersionSelect : UserControl
    {
        public VersionSelect()
        {
            InitializeComponent();
            Servers.URLs.Clear();
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Terraria/Tshock", AppDomain.CurrentDomain.BaseDirectory + "//Cache//","TerraiaTShock");
            
            string[] URLs = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//TerraiaTShock");
            ServerInfo.ServerVersion = URLs[0];

            ServerInfo.ServerURL = URLs[1];
        }

        private void WorldSizeUpdated(object sender, SelectionChangedEventArgs e) 
        {
            if (Convert.ToString(WorldSize.SelectedValue).Contains("Large")) { ServerInfo.ServerWorldSize = "3"; }
            else if (Convert.ToString(WorldSize.SelectedValue).Contains("Medium")) { ServerInfo.ServerWorldSize = "2"; }
            else if (Convert.ToString(WorldSize.SelectedValue).Contains("Small")) { ServerInfo.ServerWorldSize = "1"; }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            ServerInfo.ServerVersion = "Not set";
            ServerInfo.ServerURL = "Not set";
            ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new SSM_GUI.Welcome();
        }

        private void Continue(object sender, RoutedEventArgs e) { SSMGeneric.BuildServer(); }

    }
}
