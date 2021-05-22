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
using System.Windows.Shapes;

namespace RSM.RSMGeneric.UI
{
    /// <summary>
    /// Interaction logic for ConnectionHelper.xaml
    /// </summary>
    public partial class ConnectionHelper : Window
    {
        public ConnectionHelper()
        {
            InitializeComponent();
        }

        private void Hamachi(object sender, RoutedEventArgs e) { Utilities.OpenLink("https://rarisma.github.io/Rarismas-Server-Manager/help/connections/general/hamachi"); }
        private void PortForwarding(object sender, RoutedEventArgs e) { Utilities.OpenLink("https://rarisma.github.io/Rarismas-Server-Manager/help/connections/general/Help"); }
        private void LearnMore(object sender, RoutedEventArgs e) { Utilities.OpenLink("https://rarisma.github.io/Rarismas-Server-Manager/help/connections/general/hamachi"); }
    }
}
