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

namespace SSM.Minecraft
{
    /// <summary>
    /// Interaction logic for bedrockFinalisation.xaml
    /// </summary>
    public partial class bedrockFinalisation : Page
    {
        public bedrockFinalisation()
        {
            Minecraft.MinecraftCreatorData.ServerType = "Bedrock";
            InitializeComponent();
        }

        //33020
        //This just updates the server name when the box is typed in
        private void NameChanged(object sender, RoutedEventArgs e) { MinecraftCreatorData.ServerName = ServerName.Text; }
    }
}
