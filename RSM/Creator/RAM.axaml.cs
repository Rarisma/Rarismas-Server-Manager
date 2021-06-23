using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
//I really hate hosting companies
//Like christ how has no one else done this
//Actually they probably have and I just havent found it
namespace RSM.Creator
{
    public partial class RAM : UserControl
    {
        public RAM()
        {
            AvaloniaXamlLoader.Load(this);
            var client = new MemoryMetricsClient();
            var metrics = client.GetMetrics();
            //Sets the max slider based on the ammount of ram the user has - 3GB
            this.Find<Slider>("RamSlider").Maximum = Convert.ToInt64(metrics.Total) - 3072; 
            //Sets the max label
            this.Find<Label>("MaxRam").Content = Convert.ToString(Convert.ToInt64(LibRarisma.IO.GetRAM() - 3072)) + "MB";
            this.Find<TextBlock>("AllocationAdvice").Text = "You will need around 400mb per person on your server + 1024, So for 5 people you will need 3GB of RAM. If you find that your server is laggy then you can allocate more in your servers configuration page when your server isn't running.";
        }

        private void GoBack(object sender, RoutedEventArgs e) { Global.Display.Content = new New(); }

        private void Continue(object sender, RoutedEventArgs e)
        {
            switch (ServerInfo.Game){ case "Minecraft Java": Global.Display.Content = new Creator.Version(); break; }
        }

    }
}
