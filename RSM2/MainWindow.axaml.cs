using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.ComponentModel;
/*RSM 2 ay?
basically servers run linux
and RSM was programmed in WPF
which isn't linux friendly
So Im working on this.

No more fucking games either
we aren't fucking around
This version will be the server manager to end all server managers.
one man vs more or less every server hosting company

RSM1 was the journey
RSM2 is the answer. */
namespace RSM2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
#if DEBUG
            this.AttachDevTools();
#endif
            ServerInfo.MainWindow = this.Find<ContentControl>("MainWindowControl");
            ServerInfo.MainWindow.Content = new UI.Welcome();
        }

        public static void Closing(object sender, CancelEventArgs e)  { ServerInfo.cmd.Close(); }   
    }
}
