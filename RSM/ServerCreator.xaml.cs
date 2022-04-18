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
namespace RSM;


public sealed partial class ServerCreator : Page
{
    private Repositories Repositories = new();
    public string Name;
    public string Type;
    public string Variant;
    public string Version;

    public Repositories.ServerFile ServerFile;
    public string ServerURL;
    public Repositories.ToolChain Toolchain;
    public Repositories.Variant VariantInfo;
    public ServerCreator()
    {
        InitializeComponent();
        Repositories.GetRepoFiles();
        foreach (Repositories.ServerFile ServerFile in Repositories.PossibleServers)
        {
            ServernameCombo.Items.Add(ServerFile.Type);
        }
    }

    private void UpdateVariants(object sender, SelectionChangedEventArgs e)
    {
        Desc.Text = Repositories.PossibleServers[ServernameCombo.SelectedIndex].Description;
        Variants.Items.Clear();
        Variants.IsEnabled = true;
        foreach (var VARIABLE in Repositories.PossibleServers[ServernameCombo.SelectedIndex].Variants)
        {
            Variants.Items.Add(VARIABLE.VariantName);
        }
    }

    private void UpdateVersions(object sender, SelectionChangedEventArgs e)
    {
        Versions.Items.Clear();
        VariantInfo = Repositories.PossibleServers[ServernameCombo.SelectedIndex].Variants[Variants.SelectedIndex];
        List<string[]> versions = Repositories.PossibleServers[ServernameCombo.SelectedIndex].Variants[Variants.SelectedIndex].Versions;
        Versions.IsEnabled = true;
        foreach (var VARIABLE in versions)
        {
            Versions.Items.Add(VARIABLE[0]);
        }
    }
    private void SetURL(object sender, SelectionChangedEventArgs e)
    {
        ServerURL = Repositories.PossibleServers[ServernameCombo.SelectedIndex].Variants[Variants.SelectedIndex].Versions[Versions.SelectedIndex][1];
        ServerFile = Repositories.PossibleServers[ServernameCombo.SelectedIndex];
    }
}