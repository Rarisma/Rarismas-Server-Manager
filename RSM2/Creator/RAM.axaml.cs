using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace RSM2.Creator
{
    public partial class RAM : UserControl
    {
        public RAM()
        {
            AvaloniaXamlLoader.Load(this);
            Slider RamSlider = this.Find<Slider>("RamSlider");
            Label MaxRam = this.Find<Label>("MaxRam");
            RamSlider.Maximum = Convert.ToInt64(LibRarisma.IO.GetRAM()) - 3072;
            MaxRam.Content = Convert.ToString(Convert.ToInt64(LibRarisma.IO.GetRAM() - 3072)) + "MB";
            TextBlock AllocationAdvice = this.Find<TextBlock>("AllocationAdvice");
            
            switch (ServerInfo.Game)
            {
                case "Minecraft Java": AllocationAdvice.Text = "You will need around 400mb per person on your server + 1024, So for 5 people you will need 3GB of RAM. If you find that your server is laggy then you can allocate more in your servers configuration page when your server isn't running."; break;
            }
        }

        private void GoBack(object sender, RoutedEventArgs e) { ServerInfo.MainWindow.Content = new UI.NewServer(); }

        private void Continue(object sender, RoutedEventArgs e)
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java": ServerInfo.MainWindow.Content = new Version(); break;
            }
        }
    }
}
