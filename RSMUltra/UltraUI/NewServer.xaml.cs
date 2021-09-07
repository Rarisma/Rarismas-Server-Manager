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
            UpdateSources();

            //This lists every game RSM is capable of installing
            foreach (string directory in Directory.GetDirectories(Global.Sources))
            {
                //Path.GetDirectoryName doesn't help and FileName probably just strips anything before the last \
                GameLists.Items.Add(Path.GetFileName(directory));   
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

        //Downloads sources to make sure they are up to date.
        static void UpdateSources()
        {
            if (Directory.Exists(Global.Sources)) { Directory.Delete(Global.Sources, true); }
            LibRarisma.Connectivity.DownloadFile(Global.DefaultRepository, Global.Sources, "Temp.zip", true);
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


            File.WriteAllText(Global.ServerDir + "//RSM.ini",$"RSMUltra info file\n{GameLists.SelectedItem}\n{Versions.SelectedItem}\n{Variant.SelectedItem}\nWeekly\n{DateTime.Now:dd/MM/yyyy}\nRAM PLACEHOLDER\nWORLD PLACEHOLDER");

            //Reads ini
            string[] ini = File.ReadAllLines(Global.ServerDir + "//RSM.ini");
            ServerInfo.Name = NameBox.Text;
            ServerInfo.Game = ini[1];
            ServerInfo.Version = ini[2];
            ServerInfo.Variant = ini[3];
            ServerInfo.LastBackup = ini[5];
            ServerInfo.BackupFrequency = ini[4];

            //Java downloader
            switch (ServerInfo.Game)
            {
                case "Minecraft Java Edition":
                    if (Directory.Exists(Global.Java16) == false && ServerInfo.Variant == "Vanilla") { InstallJava16(); }
                    if (Directory.Exists(Global.Java8) == false && ServerInfo.Variant == "Modded") { InstallJava8(); }
                    break;
                case "Mindustry":
                    if (Directory.Exists(Global.Java16) == false) { InstallJava16(); }
                    break;
            }


            MainWindow.Frame.Content = new Main();
            Global.GlobalFrame.Content = new RSMUltra.Manager.General();
        }

        public static void InstallJava16()
        {
            string url = "https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerURLs/Windows/Tools/Java16";
            LibRarisma.Connectivity.DownloadFile(url, Global.Cache, "Java");

            string[] Java = File.ReadAllLines(Global.Cache + "Java");
            LibRarisma.Connectivity.DownloadFile(Java[0], Global.Cache, "JDK.zip");
            System.IO.Compression.ZipFile.ExtractToDirectory(Global.Cache + "JDK.zip", AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Temp\\");
            string[] Dirs = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Temp\\");
            Directory.Move(Dirs[0], AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Java16\\");
        }

        public static void InstallJava8()
        {
            string url = "https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerURLs/Windows/Tools/Java8";

            LibRarisma.IO.DownloadFile("https://www.dropbox.com/s/qdrgmj607r92uch/Java8.zip?dl=1", Global.Cache, "JDK8.zip");
            System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Cache\\JDK8.zip", AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Temp\\");
            string[] Dirs = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Temp\\");
            Directory.Move(Dirs[0], AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Java8\\");
        }

    }
}
