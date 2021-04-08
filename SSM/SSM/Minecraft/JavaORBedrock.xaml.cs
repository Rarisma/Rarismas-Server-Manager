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
    /// Interaction logic for JavaORBedrock.xaml
    /// </summary>
    public partial class JavaORBedrock : UserControl
    {
        public JavaORBedrock()
        {
            InitializeComponent();
        }

        private void Bedrock(object sender, RoutedEventArgs e) { MinecraftCreatorData.Edition = "Bedrock"; } //Sets edition to bedrock when clicked
        private void Java(object sender, RoutedEventArgs e) { MinecraftCreatorData.Edition = "Java"; } //Sets edition to java
    }
}
