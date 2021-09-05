using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.IO;
//Int ErnalScreaming = 1;

namespace RSMUltra
{
    /* This is a complete rewrite of RSM, titled RSM3 (or RSMUltra Internally)
     * Ideally this should be the last rewrite and should be easily extendable
     * RSM2 hasn't been referenced specifically as a challenge so it will
     * not be backwards compatible as I plan to introduce a new ini format
     * Also there aren't gonna be a lot of easter eggs and the code base is gonna
     * be well documented so some other dude can work on this when its finished
     *
     * -  Jake Rarisma.
     * Professionally Based Programmer.
     */
    public sealed partial class MainWindow : Window
    {
        private List<string> Paths = new();
        private List<string> Names = new();
        public static Frame GlobalFrame = new();
        public MainWindow()
        {
            this.InitializeComponent();
            Title = "RSM 3.0 Alpha";
            GlobalFrame = MainFrame;
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
                    MainFrame.Content = new UltraUI.NewServer();
                    break;
                case "Import":
                    break;
                case "Settings":
                    MainFrame.Content = new UltraUI.Settings();
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
                    MainFrame.Content = new UltraUI.Manager();
                    break;
            }
        }
    }
}