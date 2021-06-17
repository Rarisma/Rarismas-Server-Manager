using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace RSM.Game
{
    public partial class Minecraft : UserControl
    {
        public Minecraft()
        {
            AvaloniaXamlLoader.Load(this);
            ComboBox Difficulty = this.Find<ComboBox>("Difficulties");
            Difficulty.SelectedIndex = 0;
        }

        private void WeatherClear(object sender, RoutedEventArgs e)
        {
            Global.Server.StandardInput.WriteLine("weather clear");
            Global.Server.StandardInput.Flush();
        }

        private void TimeSetDay(object sender, RoutedEventArgs e)
        {
            Global.Server.StandardInput.WriteLine("time set day");
            Global.Server.StandardInput.Flush();
        }

        private void TimeSetNight(object sender, RoutedEventArgs e)
        {
            Global.Server.StandardInput.WriteLine("time set night");
            Global.Server.StandardInput.Flush();
        }

        private void DifficultySet(object sender, RoutedEventArgs e)
        {
            ComboBox Difficulty = this.Find<ComboBox>("Difficulties");
            if (Difficulty.SelectedIndex == 3) { Global.Server.StandardInput.WriteLine("difficulty hard"); }
            else if (Difficulty.SelectedIndex == 2) { Global.Server.StandardInput.WriteLine("difficulty medium"); }
            else if (Difficulty.SelectedIndex == 1) { Global.Server.StandardInput.WriteLine("difficulty easy"); }
            else if (Difficulty.SelectedIndex == 0) { Global.Server.StandardInput.WriteLine("difficulty peaceful"); }
            Global.Server.StandardInput.Flush();
        }

    }
}
