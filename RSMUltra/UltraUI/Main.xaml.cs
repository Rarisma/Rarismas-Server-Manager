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
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RSMUltra.UltraUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Main : Page
    {
        private List<string> Paths = new();
        private List<string> Names = new();
        public Main()
        {
            this.InitializeComponent();
            Global.GlobalFrame = MainFrame;
            //Loads instances
            if (Directory.Exists(Global.Instances))
            {
                foreach (var VARIABLE in Directory.GetDirectories(Global.Instances))
                {
                    Paths.Add(VARIABLE);
                    Names.Add(Path.GetFileName(VARIABLE));
                    SideBar.MenuItems.Add(Path.GetFileName(VARIABLE));
                }
            }
        }


        private void SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            switch (args.SelectedItemContainer.Name)
            {
                case "New":
                    MainFrame.Content = new RepoUpdater();
                    break;
                case "Import":
                    break;
                case "Settings":
                    MainFrame.Content = new Settings();
                    break;
                default:
                    Global.ServerDir = Paths[Names.IndexOf(args.SelectedItemContainer.Content.ToString())];
                    string[] ini = File.ReadAllLines(Global.ServerDir + "\\RSM.ini");
                    ServerInfo.Name = args.SelectedItemContainer.Content.ToString();
                    ServerInfo.Game = ini[1];
                    ServerInfo.Version = ini[2];
                    ServerInfo.Variant = ini[3];
                    ServerInfo.LastBackup = ini[5];
                    ServerInfo.BackupFrequency = ini[4];
                    MainFrame.Content = new Manager();
                    break;
            }
        }

    }
}