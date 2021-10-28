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
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RSMUltra.UltraUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RepoUpdater : Page
    {
        public RepoUpdater()
        {
            this.InitializeComponent();
            init();
        }

        async void init() //Await Task.Run requires async
        {
            await Task.Run(() => UpdateSources());
            Global.GlobalFrame.Content = new NewServer();

        }


        //Downloads sources to make sure they are up to date.
        static void UpdateSources()
        {
            if (Directory.Exists(Global.Sources)) { Directory.Delete(Global.Sources, true); }
            LibRarisma.Connectivity.DownloadFile(Global.DefaultRepository, Global.Sources, "Temp.zip", true);
        }

        public static void ReadConfig(string path)
        {
            List<string> ConfigFile = new();
            ConfigFile.AddRange(File.ReadAllLines(path + "//Config"));
        
            foreach (string line in ConfigFile)
            {
                if (line.Contains("#eula file path"))
                {
                    ServerInfo.ConfigEulaPath = ConfigFile[ConfigFile.IndexOf(line)];
                    File.WriteAllText(ConfigFile[ConfigFile.IndexOf(line)], ConfigFile[ConfigFile.IndexOf(line)] + 2);
                }

            }
        
        }

    }
}