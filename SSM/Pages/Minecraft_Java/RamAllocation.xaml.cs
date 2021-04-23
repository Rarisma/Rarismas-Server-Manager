using System;
using System.Windows;
using System.Windows.Controls;

namespace SSM.Pages.Minecraft_Java
{
    /// <summary>
    /// Interaction logic for RamAllocation.xaml
    /// </summary>
    public partial class RamAllocation : UserControl
    {
        public RamAllocation()
        {
            InitializeComponent();
            RamSlider.Maximum = Convert.ToInt64(LibRarisma.IO.GetRAM()) - 3072;
            RamSlider.Minimum = 1024;
            MaxRam.Content = Convert.ToInt64(LibRarisma.IO.GetRAM() - 3072) + "MB";
        }

        private void SliderChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e) //This updates the display when the user interacts with the slider
        {
            AllocatedRAMDisplay.Content = "Allocated ammount of RAM: " + Convert.ToInt64(RamSlider.Value) + " MB";
            ServerInfo.RAM = Convert.ToInt64(RamSlider.Value);
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            ServerInfo.RAM = 0;
            ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new SSM_GUI.NewServer();
        }


        private void Continue(object sender, RoutedEventArgs e) { ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new Minecraft_Java.VersionSelect(); }

    }
}