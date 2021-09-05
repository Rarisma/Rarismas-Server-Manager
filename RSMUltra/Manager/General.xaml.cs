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

            StackPanel Content = new();
            switch (ServerInfo.Game)
            {
                case "Minecraft Java Edition":
                    Content.Children.Add(new TextBlock{Text = "Open Mods folder"});
                    Content.Children.Add(new TextBlock{Text = "Open Datapacks folder"});
                    Content.Children.Add(new TextBlock{Text = "Open Plugins folder"});
                    break;
            }
            //Links.Flyout.SetValue(StackPanel,Content);
        }

        private async void Delete(object sender, RoutedEventArgs e)
        {
            ContentDialog DeleteMessage = new ContentDialog();
            DeleteMessage.Title = "Are you sure you want to delete this server?";
            DeleteMessage.PrimaryButtonText = "Delete";
            DeleteMessage.CloseButtonText = "Go back";
            DeleteMessage.ShowAsync();
            /*
            var test = .GetResults();

            if (test == ContentDialogResult.Primary)
            {
                Directory.Delete(Global.ServerDir, true);
            }*/
        }
    }
}
