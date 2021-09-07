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
using Windows.Security.Cryptography.Core;
using ABI.Windows.UI.Popups;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RSMUltra.Manager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class General : Page
    {
        public General()
        {
            this.InitializeComponent();
            Name.Text = $"{ServerInfo.Name}";
            Game.Text = $"{ServerInfo.Game} {ServerInfo.Version} ({ ServerInfo.Variant})";

            //This section controls the buttons that appears in LinkButtons
            switch (ServerInfo.Game)
            {
                case "Minecraft Java Edition":
                    Button modButton = new() { Content = "Open Mods folder", HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(20) };
                    modButton.Click += Mods();
                    LinkPanels.Children.Add(modButton);
                    Button DatapacksButton = new(){Content = "Open Datapacks folder", HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(20) };
                    DatapacksButton.Click += Plugins();
                    LinkPanels.Children.Add(DatapacksButton);
                    Button PluginsButton = new(){Content = "Open Plugins folder", HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(20) };
                    PluginsButton.Click += Datapacks();
                    LinkPanels.Children.Add(PluginsButton);
                    break;
            }

        }

        private RoutedEventHandler Mods()
        {
            return null;
        }
        private RoutedEventHandler Plugins()
        {
            return null;
        }
        private RoutedEventHandler Datapacks()
        {
            return null;
        }

        private void DeleteServer(object sender, RoutedEventArgs e)
        {
            Directory.Delete(Global.ServerDir,true);
            MainWindow.Frame.Content = new UltraUI.Main();
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            Global.GlobalFrame.Content = new Server();
        }
    }
}
