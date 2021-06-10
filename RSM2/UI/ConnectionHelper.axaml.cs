using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace RSM2.UI
{
    public partial class ConnectionHelper : Window
    {
        public ConnectionHelper() { AvaloniaXamlLoader.Load(this); }

        private void Hamachi(object sender, RoutedEventArgs e) { LibRarisma.Utils.OpenLink("https://rarisma.github.io/Rarismas-Server-Manager/help/connections/general/hamachi"); }
        private void PortForwarding(object sender, RoutedEventArgs e) { LibRarisma.Utils.OpenLink("https://rarisma.github.io/Rarismas-Server-Manager/help/connections/general/Help"); }
        private void LearnMore(object sender, RoutedEventArgs e) { LibRarisma.Utils.OpenLink("https://rarisma.github.io/Rarismas-Server-Manager/help/connections/general/hamachi"); }

    }
}
