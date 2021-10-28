using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.IO;
//Int ErnalScreaming = 1;
//Rarisma's Server Manager 3.2
namespace RSMUltra
{

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
