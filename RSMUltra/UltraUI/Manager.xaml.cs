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

namespace RSMUltra.UltraUI
{
    public sealed partial class Manager : Page
    {
        public static NavigationViewItem General = new ();
        public static NavigationViewItem Backup = new ();
        public static NavigationViewItem Setting = new ();
        public static Frame ServerFrame = new ();

        public Manager()
        {
            this.InitializeComponent();
            General = GeneralNavi;
            Backup = BackupsNavi;
            Setting = SettingsNavi;
            ManagerFrame.Content = new RSMUltra.Manager.Server();
            ServerFrame = ManagerFrame;
        }

        //Changes the manager frame
        private void FrameChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            switch (args.SelectedItemContainer.Content)
            {
                case "General":
                    ManagerFrame.Content = new RSMUltra.Manager.Server();
                    break;

                case "Settings":
                    ManagerFrame.Content = new RSMUltra.Manager.Settings();
                    break;
                case "Backups":
                    ManagerFrame.Content = new RSMUltra.Manager.Backups();
                    break;
            }
        }
    }
}
