using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using RSM.Data;


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
