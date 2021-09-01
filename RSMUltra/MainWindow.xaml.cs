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
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;

//Int ErnalScreaming = 1;

namespace RSMUltra
{
    /* This is a complete rewrite of RSM, titled RSM3 (or RSMUltra Internally)
     * Ideally this should be the last rewrite and should be easily extendable
     * RSM2 has not been referenced spesifcally as a challenge so it will
     * not be backwards compatible as I plan to introduce a new ini format
     * Also there aren't gonna be a lot of easter eggs and the code base is gonna
     * be well documented so some other dude can work on this when its finished
     *
     * -  Jake Rarisma.
     * Professionally Based Programmer.
     */
    public sealed partial class MainWindow : Window
    {

        public MainWindow()
        {
            this.InitializeComponent();
            Title = "RSM 3.0 Alpha";
        }


        private void SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            switch (args.SelectedItemContainer.Name)
            {
                case "Create new server":
                    MainFrame.Content = new UltraUI.NewServer();
                    break;
                case "Import a server":
                    break;
                case "Settings":
                    MainFrame.Content = new UltraUI.Settings();
                    break;
                default:
                    //SideBar.SelectedItem.ToString();
                    break;
            }
        }
    }
}