using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace RSM;

public sealed partial class Main : Page
{
    public Main()
    {
        InitializeComponent();
        Frame.Content = Data.Content;
        Servers.GetServers();
        Servers.Server a = new();
        new WideNodes(ref a).Render();
        DispatcherTimer Timer = new();
        Timer.Interval = new(0, 0, 0, 0, 500);
        Timer.Tick += UpdateTicker;
        Timer.Start();
    }

    private void UpdateTicker(object? sender, object e)
    {
        Info.Text = Data.ShellComment;
    }

    private void ShowServers(object sender, RoutedEventArgs e)
    {
        Servers.Server a = new();
        new WideNodes(ref a).Render();
    }

    private async void LoadNew(object sender, RoutedEventArgs e)
    {
        ServerCreator NSC = new();
        ContentDialog ServerCreator = new();
        ServerCreator.Title = "Create new server";
        ServerCreator.PrimaryButtonText = "Create";
        ServerCreator.SecondaryButtonText = "Cancel";
        ServerCreator.Content = NSC;
        ServerCreator.XamlRoot = XamlRoot;
        if (await ServerCreator.ShowAsync() == ContentDialogResult.Primary)
        {
            Servers.Provision(XamlRoot, NSC.Name, NSC.Type, NSC.Variant, NSC.Version, NSC.VariantInfo, NSC.ServerFile, NSC.ServerURL);
        }
    }

    private async void LoadSettings(object sender, RoutedEventArgs e)
    {
        await new ContentDialog()
        {
            Content = new RSMConfig(),
            XamlRoot = this.XamlRoot,
            Title = "RSM Settings",
            PrimaryButtonText = "Save",
            SecondaryButtonText = "Cancel",
        }.ShowAsync();
    }

};