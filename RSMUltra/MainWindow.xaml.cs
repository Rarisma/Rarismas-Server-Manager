using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.IO;
//Int ErnalScreaming = 1;
//Rarisma's Server Manager (Ultra/3.0) by Jake Rarisma (Rarisma#3767)
namespace RSMUltra
{
    /* This is a complete rewrite of RSM, titled RSM3 (or RSMUltra Internally)
     * Ideally this should be the last rewrite and should be easily extendable
     * RSM2 hasn't been referenced a ton as a challenge so it will
     * not be backwards compatible as I plan to introduce a new ini format
     * Also there aren't gonna be a lot of easter eggs and the code base is gonna
     * be well documented so some other dude can work on this when its finished
     *
     * -  Jake Rarisma
     * !/UltraUI/  - Contains the UI for stuff not releated to the server
     * !/Manager/  - Contains stuff releated to the server
     */
    public sealed partial class MainWindow : Window
    {
        public static Frame Frame;
        public MainWindow()
        {
            this.InitializeComponent();
            Title = "RSM 3.0";
            Global.GlobalTitle = Title;
            Frame = MainFrame;

            //Gets the boring stuff out the way to prevent crashes later

            MainFrame.Content = new UltraUI.Main();
        }
    }
}