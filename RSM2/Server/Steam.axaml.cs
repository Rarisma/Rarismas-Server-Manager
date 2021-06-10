using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
//Im taking my code and going home, hooome

namespace RSM2.Server
{
    public partial class Steam : UserControl
    {
        public Steam()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void Login(object sender, RoutedEventArgs e)
        {
            ServerInfo.User = this.Find<TextBox>("Name").Text;
            ServerInfo.Pass = this.Find<TextBox>("Pass").Text;
            ServerInfo.Version = "Latest";
            ServerInfo.MainWindow.Content = new UI.Downloader();
        }
    }
}
