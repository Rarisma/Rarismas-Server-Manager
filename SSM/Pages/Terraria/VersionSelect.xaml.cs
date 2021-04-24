using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

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
            //Variants.Items.Add("TShock");
            Variants.Items.Add("Vanilla");
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Terraria/Stock", AppDomain.CurrentDomain.BaseDirectory + "//Cache//","TerraiaVanilla");
        }

        private void VariantsUpdated(object sender, SelectionChangedEventArgs e)
        {
            List<String> VersionSorter = new();
            switch (Variants.SelectedValue)
            {
                case "Vanilla":
                    Servers.URLs.Clear();
                    VersionSorter.Clear();
                    Description.Text = "This is the vanilla hosting software made by Re-logic.";
                    ServerInfo.ServerVariant = "Paper";
                    VersionSorter.Clear();
                    VersionSorter.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//TerraiaVanilla"));
                    VersionSorter.RemoveAll(str => string.IsNullOrEmpty(str)); //Update to use LibRarisma.Utils.Clean();

                    for (int i = 0; i <= (VersionSorter.Count - 1); i++)
                    {
                        if (Convert.ToString(VersionSorter[i][0]) == "1") { Version.Items.Add(VersionSorter[i]); } //This adds version names
                        else if (VersionSorter[i].Contains("https")) { Servers.URLs.Add(VersionSorter[i]); }
                    }
                    break;
            }
        }

        private void VersionUpdated(object sender, SelectionChangedEventArgs e) 
        {
            ServerInfo.ServerVersion = Convert.ToString(Version.SelectedValue);
            ServerInfo.ServerURL = Servers.URLs[Version.SelectedIndex];
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            ServerInfo.ServerVersion = "Not set";
            ServerInfo.ServerURL = "Not set";
        }
        
        private void Continue(object sender, RoutedEventArgs e)
        {
            LibRarisma.IO.DownloadFile(ServerInfo.ServerURL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//", "Terraria.zip",true);
            ServerInfo.ServerVariant = "Vanilla";
            SSMGeneric.Make_INI_File();
            ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new SSM_GUI.Welcome();
            string[] Servers = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//");
            File.Copy(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//" + Path.GetFileName(Servers[0]) + "//Windows//TerrariaServer.exe", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//TerrariaServer.exe");
            File.Copy(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//" + Path.GetFileName(Servers[0]) + "//Windows//ReLogic.Native.dll", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//ReLogic.Native.dll");
            Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//" + System.IO.Path.GetFileName(Servers[0]), true);
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//Terraria.zip");
            ModernWpf.MessageBox.Show("Finished downloading server files");
        }

    }
}
