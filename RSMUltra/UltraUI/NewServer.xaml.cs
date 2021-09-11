using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.WiFi;
using Windows.Foundation;
using Windows.Foundation.Collections;
using RSMUltra.Manager;

//It looks like things are gonna get worse before they get better

//This is gonna be a lot more complicated than the original NewServer page
//because of the new repo feature
namespace RSMUltra.UltraUI
{
    public sealed partial class NewServer : Page
    {
        public NewServer()
        {
            InitializeComponent();

            //This lists every game RSM is capable of installing
            foreach (string directory in Directory.GetDirectories(Global.Sources))
            {
                //Path.GetDirectoryName doesn't help and FileName probably just strips anything before the last \
                if (Path.GetFileName(directory) != "RSM") //RSM folder in sources stores tools and stuff like Java
                {
                    GameLists.Items.Add(Path.GetFileName(directory));

                }
            }

        }

        private void GameChanged(object sender, SelectionChangedEventArgs e)
        {
            VersionPanel.Opacity = 1;
            Versions.Items.Clear(); //Clears panel
            //This lists every version RSM can install
            List<string> vers = new();
            foreach (string directory in Directory.GetDirectories(Global.Sources + "//" + GameLists.SelectedItem))
            {
                //Path.GetDirectoryName doesn't help and FileName probably just strips anything before the last \
                vers.Add(Path.GetFileName(directory));
            }

            //WinUI3 doesn't have a sort feature
            string[] vs = vers.OrderBy(r => r).ToArray();
            Versions.Items.Clear();

            foreach (var VARIABLE in vs)
            {
                Versions.Items.Add(VARIABLE);
            }
        }

        private void VersionChanged(object sender, SelectionChangedEventArgs e)
        {
            VariantPanel.Opacity = 1;

            Variant.Items.Clear(); //Clears variants
            //This lists every version RSM can install
            foreach (string file in Directory.GetFiles(Global.Sources + "//" + GameLists.SelectedItem + "//" + Versions.SelectedItem + "//"))
            {
                //Path.GetDirectoryName doesn't help and FileName probably just strips anything before the last \
                Variant.Items.Add(Path.GetFileName(file));
            }

        }

        //All this does is enable the continue button
        private void EnableContinue(object sender, SelectionChangedEventArgs e)
        {
            Continue.Opacity = 1;
            NameBox.Opacity = 1;
        }



        //Downloads the server
        private void ContinueClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Directory.CreateDirectory(Global.Instances + "//" + NameBox.Text);
            }
            catch //Will cause exception if server is called something that isn't allowed.
            {
                NameBox.Text = ""; //Delete erroneous name
                NameBox.PlaceholderText = "You can't name your server that!"; //Informs user
            }

            Continue.IsEnabled = false;
            string[] InfoFile = File.ReadAllLines(Global.Sources + $"//{GameLists.SelectedItem}//{Versions.SelectedItem}//{Variant.SelectedItem}");
            Global.ServerDir = Global.Instances + "//" + NameBox.Text + "//"; //Simplifies path to the server

            //This part downloads and extracts the file if needed
            if (InfoFile[0].Contains(".zip")) //Only extracts if .zip is in the url
            {
                LibRarisma.Connectivity.DownloadFile(InfoFile[0], Global.ServerDir, "Server.zip",true);
            }
            else if (InfoFile[0].Contains(".jar"))
            {
                LibRarisma.Connectivity.DownloadFile(InfoFile[0], Global.ServerDir, "Server.jar");

            }


            File.WriteAllText(Global.ServerDir + "//RSM.ini",$"RSMUltra info file\n{GameLists.SelectedItem}\n{Versions.SelectedItem}\n{Variant.SelectedItem}\nWeekly\n{DateTime.Now:dd/MM/yyyy}\n{ServerInfo.AllocatedRAM}\nWORLD PLACEHOLDER");

            //Reads ini
            string[] ini = File.ReadAllLines(Global.ServerDir + "//RSM.ini");
            ServerInfo.Name = NameBox.Text;
            ServerInfo.Game = ini[1];
            ServerInfo.Version = ini[2];
            ServerInfo.Variant = ini[3];
            ServerInfo.LastBackup = ini[5];
            ServerInfo.BackupFrequency = ini[4];
            ServerInfo.AllocatedRAM = Convert.ToString(Convert.ToInt32(LibRarisma.Tools.GetRAM() / 2) - 1024) ;

            //Java downloader
            switch (ServerInfo.Game)
            {
                case "Minecraft Java Edition":
                    if (Directory.Exists(Global.Java16) == false && ServerInfo.Variant != "Forge") { GetJava(); }
                    else { GetJava(true); }
                    break;
                case "Mindustry":
                    if (Directory.Exists(Global.Java16) == false) { GetJava(); }
                    break;
            }

            if (ServerInfo.Game == "Minecraft Java Edition") //Accepts the Minecraft Eula
            {
                File.WriteAllText(Global.ServerDir + "eula.txt", "#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula).\n# made by RSM\neula = true");
            }

            MainWindow.Frame.Content = new Main();
            Global.GlobalFrame.Content = new RSMUltra.Manager.Server();
        }

        public static void GetJava(bool Legacy = false)
        {
            if (Legacy)
            {
                if (!File.Exists(Global.Java8))
                {
                    LibRarisma.Connectivity.DownloadFile(File.ReadAllLines(Global.Sources + "//RSM//Java8")[0],Global.Tools + "//Java8//", "Java8.zip", true);

                }
            }
            else
            {
                if (!File.Exists(Global.Java16))
                {
                    LibRarisma.Connectivity.DownloadFile(File.ReadAllLines(Global.Sources + "//RSM//Java16")[0], Global.Tools + "//Java16//", "Java16.zip", true);
                }
            }

        }
    }
}
