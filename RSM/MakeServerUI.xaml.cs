using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using RSM.Data;
using RSM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace RSM;

public sealed partial class MakeServerUI : Page
{
    private Global GlobalVM = Ioc.Default.GetService<Global>();
    public MakeServerUI() 
    {
        InitializeComponent();
        this.DataContext = GlobalVM;
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (GlobalVM.SelectedServer != null) 
        {
            varbox.ItemsSource = GlobalVM.SelectedServer.AvailableVariants;
        }
        else { GlobalVM.SelectedVariant = null; }

        if (GlobalVM.SelectedVariant != null)
        {
            verbox.ItemsSource = GlobalVM.SelectedVariant.Versions.Keys;
        }
    }
}
