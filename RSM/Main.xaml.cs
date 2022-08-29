using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using RSM.Data;
using System;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace RSM;
//Traveling around to see a world brand new?
public sealed partial class Main
{
    public Global GlobalVM = Ioc.Default.GetService<Global>();
    public Main() { InitializeComponent(); }

    private async void LoadNew(object sender, RoutedEventArgs e)
    {
        XMLParser.UpdateRepositoryFiles();
        ContentDialog ServerCreator = new()
        {
            Title = "Create new server",
            PrimaryButtonText = "Create",
            SecondaryButtonText = "Cancel",
            Content = new MakeServerUI(),
            XamlRoot = XamlRoot
        };
        if (await ServerCreator.ShowAsync() == ContentDialogResult.Primary)
        {
            Provisioner.Provision();
        }
    }

    private async void LoadSettings(object sender, RoutedEventArgs e)
    {
        await new ContentDialog
        {
            Content = new RSMConfig(),
            XamlRoot = XamlRoot,
            Title = "RSM Settings",
            PrimaryButtonText = "Save",
            SecondaryButtonText = "Cancel",
        }.ShowAsync();
    }
}