using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
//It would be funny af if this caused a mass exodus from hosting companies
//I just found out I can automate portforwarding
//I hold the opinion that hosting companies are making money from peoples stupidity
//It shouldn't be £10 a month to play a fucking minecraft SMP with your friends
//So RSM is for those people, if you are running a server with like 20+ people then yeah you should probably
//use a company as your pc probs cant handle it

//Its the github repo after next, so mark your calendar
namespace RSM.Creator
{
    public partial class RAM : UserControl
    {
        public RAM()
        {
            AvaloniaXamlLoader.Load(this);
            //Sets the max slider based on the ammount of ram the user has - 3GB
            this.Find<Slider>("RamSlider").Maximum = Convert.ToInt64(LibRarisma.IO.GetRAM()) - 3072; 
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
