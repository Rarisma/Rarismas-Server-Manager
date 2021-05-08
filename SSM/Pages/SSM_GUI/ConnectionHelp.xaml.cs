using System.Windows;


namespace SSM.Pages.SSM_GUI
{
    /// <summary>
    /// Interaction logic for ConnectionHelp.xaml
    /// </summary>
    public partial class ConnectionHelp : Window
    {
        public ConnectionHelp()
        {
            InitializeComponent();
        }

        private void Hamachi(object sender, RoutedEventArgs e) { SSMGeneric.OpenLink("https://rarisma.github.io/Simple-Server-Manager/help/connections/general/hamachi"); }
        private void PortForwarding(object sender, RoutedEventArgs e) { SSMGeneric.OpenLink("https://rarisma.github.io/Simple-Server-Manager/help/connections/general/Help"); }
        private void LearnMore(object sender, RoutedEventArgs e) { SSMGeneric.OpenLink("https://rarisma.github.io/Simple-Server-Manager/help/connections/general/hamachi"); }
    }
}
