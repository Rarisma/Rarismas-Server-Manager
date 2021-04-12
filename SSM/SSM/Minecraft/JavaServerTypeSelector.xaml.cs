using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SSM.Minecraft.Java
{
    /// <summary>
    /// Interaction logic for ServerTypeSelect.xaml
    /// </summary>
    public partial class ServerTypeSelect : Page
    {
        public ServerTypeSelect()
        {
            InitializeComponent();
        }

        private void Paper(object sender, RoutedEventArgs e) { MinecraftCreatorData.ServerType = "Paper"; }
        private void Stock(object sender, RoutedEventArgs e) { MinecraftCreatorData.ServerType = "Stock"; }
        private void Modded(object sender, RoutedEventArgs e) { MinecraftCreatorData.ServerType = "Modded"; }
    }
}
