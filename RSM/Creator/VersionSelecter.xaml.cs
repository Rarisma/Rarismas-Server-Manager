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
//Yes I know I spelled it wrong.
namespace RSM.Creator
{
    /// <summary>
    /// Interaction logic for VersionSelecter.xaml
    /// </summary>
    public partial class VersionSelecter : Page
    {
        public VersionSelecter()
        {
            InitializeComponent();
            LoadOptions();
        }

        public void LoadOptions()
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    Variants.Items.Add("Paper (Recomended)");
                    Variants.Items.Add("Forge (Modded)");
                    Variants.Items.Add("Vanilla");
                    LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/Paper", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Paper");
                    LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/Stock", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Stock");
                    LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/Forge", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Forge");
                    EULA.Text = "By clicking continue you agree to the Minecraft/Mojang EULA.";
                    break;
            }
        }

        private void VariantsUpdated(object sender, SelectionChangedEventArgs e)
        {
            List<String> VersionSorter = new();
            Servers.URLs.Clear();
            VersionSorter.Clear();
            switch (Variants.SelectedValue)
            {
                case "Paper (Recomended)":
                    Description.Text = "Paper is an improved version of the vanilla hosting software, it's much more efficent and supports plugins!";
                    ServerInfo.Variant = "Paper";
                    VersionSorter.AddRange(File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Paper"));
                    break;

                case "Vanilla":
                    Description.Text = "This is the vanilla hosting software made by Mojang. Unless you need it for a spesific reason you should use paper.";
                    ServerInfo.Variant = "Paper";
                    VersionSorter.AddRange(File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Stock"));
                    break;

                case "Forge (Modded)":
                    Description.Text = "Modded minecraft allows you to install mods which will add new things to minecraft\nPlease note that this doesn't come with any mods.";
                    ServerInfo.Variant = "Forge";
                    VersionSorter.AddRange(File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Forge"));
                    break;
            }
            VersionSorter.RemoveAll(str => string.IsNullOrEmpty(str)); //Update to use LibRarisma.Utils.Clean();
            for (int i = 0; i <= (VersionSorter.Count - 1); i++)
            {
                if (Convert.ToString(VersionSorter[i][0]) == "1") { Version.Items.Add(VersionSorter[i]); } //This adds version names
                else if (VersionSorter[i].Contains("https")) { Servers.URLs.Add(VersionSorter[i]); }
            }
        }

        private void VersionUpdated(object sender, SelectionChangedEventArgs e)
        {
            ServerInfo.Version = Convert.ToString(Version.SelectedValue);
            ServerInfo.URL = Servers.URLs[Version.SelectedIndex];
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            ServerInfo.Version = "Not set";
            ServerInfo.URL = "Not set";

            switch (ServerInfo.Game)
            {
                case "Minecraft Java": ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new RAMAllocator();  break;
                case "Terraria": ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new RSMGeneric.UI.LaunchPage();  break;
            }
        }

        private void Continue(object sender, RoutedEventArgs e) 
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java": RSMGeneric.ServerBuilder.MinecraftJava(); break;
                case "Terraria": RSMGeneric.ServerBuilder.Terraria();    break;
            }

        }

    }
}

