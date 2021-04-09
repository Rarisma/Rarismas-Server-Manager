using System;
using System.Collections.Generic;
using System.Management;
using System.Windows.Controls;
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

            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject result in results)
            {
                RamSlider.Maximum = (Convert.ToUInt64(result["TotalVisibleMemorySize"]) / 1024) - 3072;
                MaxRam.Content = (Convert.ToUInt64(result["TotalVisibleMemorySize"]) / 1024) - 3072;
            }
            
            
           
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Paper", "C:\\Users\\Rarisma\\Desktop\\", "paper");
            List<String> VersionSorter = new List<string>();
            List<String> URLs = new List<string>();
            VersionSorter.AddRange(System.IO.File.ReadAllLines("C:\\Users\\Rarisma\\Desktop\\paper"));
            VersionSorter.RemoveAll(str => string.IsNullOrEmpty(str)); ; // Removes empty

            for (int i = 0; i <= (VersionSorter.Count - 1); i++)
            {
                if (Convert.ToString(VersionSorter[i][0])  == "1") 
                {
                    VersionSelector.Items.Add(VersionSorter[i]);
                } //This adds version names
                else if (VersionSorter[i].Contains("https")) 
                {
                    URLs.Add(VersionSorter[i]);
                }
            }

        }

        private void SliderChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            AllocatedRAMDisplay.Content = "Allocated ammount of RAM: " + Convert.ToInt64(RamSlider.Value) + " MB";
        }
    }
}
