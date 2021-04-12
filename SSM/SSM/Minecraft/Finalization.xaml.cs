using System;
using System.Collections.Generic;
using System.Management;
using System.Windows.Controls;

//Avalonia, WPF and ModernWPF whats next?
//Can you guys agree on somthing that isnt a clusterfuck to use?
//Initally I wanted this to be crossplatform, so I tried using Avalonia but struggled to get stuff done in it
//Could have probably done this in Xamarin if it let me compile as win32 and not UWP
//ModernWPF is nice but it isn't natively crossplatform
//Hopefully it will work with WINE

//I also want to add more servers
//If I do then terraria/TSHOCK is probably next. 

//Though LibRarisma is due for an update, as I want to add
//A custom function for finding free RAM

//Rarisma.

//Just as I finished writing this I went for dinner,
//By the time I came back Prince Philip died.

namespace SSM.Minecraft
{
    /// <summary>
    /// Interaction logic for Finalization.xaml
    /// </summary>
    public partial class Finalization : UserControl
    {
        public Finalization()
        {
            InitializeComponent();
            List<String> VersionSorter = new();

            ObjectQuery wql = new("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new(wql);
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject result in results)
            {
                RamSlider.Minimum = 1024;
                RamSlider.Maximum = (Convert.ToUInt64(result["TotalVisibleMemorySize"]) / 1024) - 3072;
                MaxRam.Content = (Convert.ToUInt64(result["TotalVisibleMemorySize"]) / 1024) - 3072;
            }

            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Cache\\");
            LibRarisma.IO.DownloadFile(LinkToServerFiles(), AppDomain.CurrentDomain.BaseDirectory + "\\Cache\\", MinecraftCreatorData.ServerType);
            VersionSorter.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\Cache\\" + MinecraftCreatorData.ServerType));
            VersionSorter.RemoveAll(str => string.IsNullOrEmpty(str)); //Removes empty strings such as ""
            
            for (int i = 0; i <= (VersionSorter.Count - 1); i++)
            {
                if (Convert.ToString(VersionSorter[i][0]) == "1") { VersionSelector.Items.Add(VersionSorter[i]); } //This adds version names
                else if (VersionSorter[i].Contains("https")) { MinecraftCreatorData.URLs.Add(VersionSorter[i]); }
            }

        }

        private static string LinkToServerFiles()
        {
            string[] ServerListingURLs = { "https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Paper", "https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Stock" };
            if (MinecraftCreatorData.ServerType == "Paper") { return ServerListingURLs[0]; }
            else if (MinecraftCreatorData.ServerType == "Stock") { return ServerListingURLs[1]; }
            else { throw new ArgumentException("ServerFilesNotFound"); }
        }


        private void SliderChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e) //This updates the display when the user interacts with the slider
        {
            AllocatedRAMDisplay.Content = "Allocated ammount of RAM: " + Convert.ToInt64(RamSlider.Value) + " MB";
            MinecraftCreatorData.AllocatedRAM = Convert.ToInt64(RamSlider.Value);
        }


        //This function runs when the user changes the version in menu box
        private void VersionBoxUpdate(object sender, SelectionChangedEventArgs e) 
        { 
            MinecraftCreatorData.Version = Convert.ToString(VersionSelector.SelectedValue);
            MinecraftCreatorData.ServerFilesURL = MinecraftCreatorData.URLs[VersionSelector.SelectedIndex]; //This updates the URL needed to fetch the Server.jar file
        }

        //This function is called when the user edits the text in the ServerName Textbox
        private void NameUpdated(object sender, TextChangedEventArgs e) { MinecraftCreatorData.ServerName = ServerName.Text; }
    }
}
