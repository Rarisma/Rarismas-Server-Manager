using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
//Stay ABAP
//As ballin as possible
namespace SSM.Pages.Terraria
{
    /// <summary>
    /// Interaction logic for VersionSelect.xaml
    /// </summary>
    /// 

    class Servers
    {
        public static List<String> URLs = new();
    }

    public partial class VersionSelect : UserControl
    {
        public VersionSelect()
        {
            InitializeComponent();
            Servers.URLs.Clear();
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Terraria/Tshock", AppDomain.CurrentDomain.BaseDirectory + "//Cache//","TerraiaTShock");
            
            string[] URLs = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//TerraiaTShock");
            ServerInfo.ServerVersion = URLs[0];
            ServerInfo.ServerVariant = "Normal";
            ServerInfo.ServerURL = URLs[1];
        }

        private void WorldSizeUpdated(object sender, SelectionChangedEventArgs e) 
        {
            if (Convert.ToString(WorldSize.SelectedValue).Contains("Large")) { ServerInfo.ServerWorldSize = "3"; }
            else if (Convert.ToString(WorldSize.SelectedValue).Contains("Medium")) { ServerInfo.ServerWorldSize = "2"; }
            else if (Convert.ToString(WorldSize.SelectedValue) == "Small") { ServerInfo.ServerWorldSize = "1"; }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            ServerInfo.ServerVersion = "Not set";
            ServerInfo.ServerURL = "Not set";
            ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new SSM_GUI.Welcome();
        }

        private void Continue(object sender, RoutedEventArgs e)
        {
            LibRarisma.IO.DownloadFile(ServerInfo.ServerURL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//", "Terraria.zip",true);
            LibRarisma.IO.DownloadFile("https://github.com/Rarisma/Simple-Server-Manager/blob/main/ServerFiles/Terraria/SSMHelper.dll?raw=true", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//ServerPlugins//", "SSMHelper.dll");
            SSMGeneric.Make_INI_File();
            ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new SSM_GUI.Welcome();
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//Terraria.zip");
            ModernWpf.MessageBox.Show("Finished downloading server files");
        }

    }
}
