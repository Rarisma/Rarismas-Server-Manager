using System;
using System.Collections.Generic;
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

/*
 Murder Snap Crackle and Pop Ima have for lunch
Choking Count Chocula while I'm stabbing Captain Crunch
Leaving Toucan Sam slumped when I blast the pump
Switching to the buckshot for that Silly Rabbit chump
Smack the Smacks frog literally
With Raid spray for that Honey Nut Cheerio Bee
Shoot the stripes off of Tony when I'm gripping the trigger
Do you get it now? I'm a fucking cereal killer
 */
namespace SSM.Pages.Minecraft_Java
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
            Variants.Items.Add("Paper (Recomended)");
            Variants.Items.Add("Vanilla");
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Paper", AppDomain.CurrentDomain.BaseDirectory + "//Cache//","Paper");
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Stock", AppDomain.CurrentDomain.BaseDirectory + "//Cache//","Stock");
        }

        private void VariantsUpdated(object sender, SelectionChangedEventArgs e)
        {
            List<String> VersionSorter = new();
            switch (Variants.SelectedValue)
            {
                case "Paper (Recomended)":
                    Servers.URLs.Clear();
                    VersionSorter.Clear();
                    Description.Text = "Paper is an improved version of the vanilla hosting software, it's much more efficent and supports plugins!";
                    ServerInfo.ServerVariant = "Paper";
                    VersionSorter.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Paper"));
                    VersionSorter.RemoveAll(str => string.IsNullOrEmpty(str)); //Update to use LibRarisma.Utils.Clean();

                    for (int i = 0; i <= (VersionSorter.Count - 1); i++)
                    {
                        if (Convert.ToString(VersionSorter[i][0]) == "1") { Version.Items.Add(VersionSorter[i]); } //This adds version names
                        else if (VersionSorter[i].Contains("https")) { Servers.URLs.Add(VersionSorter[i]); }
                    }
                    break;

                case "Vanilla":
                    Servers.URLs.Clear();
                    VersionSorter.Clear();
                    Description.Text = "This is the vanilla hosting software made by Mojang. Unless you need it for a spesific reason you should use paper.";
                    ServerInfo.ServerVariant = "Paper";
                    VersionSorter.Clear();
                    VersionSorter.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Stock"));
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
            LibRarisma.IO.DownloadFile(ServerInfo.ServerURL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//", "Server.jar");
            SSMGeneric.Make_INI_File();
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//" + "eula.txt", "#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula).\n# made by SSM\neula = true"); //Makes the EULA accepted

        }

    }
}
