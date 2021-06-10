using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
/*
 
    So I was planning to postpone RSM, hell I even announced it.
    I have another project in the works. I don't really wanna announce it.
    but you're here and so I'll give you a sneak preview.

    Its called Project Cobalt. Its gonna be a front end, focused on making everything
    stupidily fucking easy. basically RSM but for vidya games. As of writing this 6/6/2021
    all I really have atm is somthing that identifies wii and gc roms
 
 */

namespace RSM2.UI
{
    public partial class SteamLogin : Window
    {
        public SteamLogin()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void Login(object sender, RoutedEventArgs e)
        {
            ServerInfo.User = this.Find<TextBox>("Name").Text;
            ServerInfo.Pass = this.Find<TextBox>("Pass").Text;
            ServerInfo.MainWindow = new UI.Downloader();
        }
    }
}
