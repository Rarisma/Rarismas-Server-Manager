using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Threading.Tasks;

namespace RSM2.QuickCommands
{
    public partial class Minecraft : UserControl
    {
        public Minecraft()
        {
            AvaloniaXamlLoader.Load(this);
            ComboBox Difficulty = this.Find<ComboBox>("Difficulties");
            Difficulty.SelectedIndex = 0;
            if (ServerInfo.Game == "Minecraft Bedrock")
            {
                Button SeedButton = this.Find<Button>("SeedButton");
                Button SaveButton = this.Find<Button>("SaveButton");
                SeedButton.IsEnabled = false; //Disables seed button as /seed isn't supported on bedrock
                SeedButton.Opacity = 0;
                SaveButton.IsEnabled = false;
                SaveButton.Opacity = 0;
            }
        }

        private void WeatherClear(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("weather clear");
            ServerInfo.cmd.StandardInput.Flush();
        }

        private void TimeSetDay(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("time set day");
            ServerInfo.cmd.StandardInput.Flush();
        }

        private void TimeSetNight(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("time set night");
            ServerInfo.cmd.StandardInput.Flush();
        }

        private void Peaceful(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("difficulty peaceful");
            ServerInfo.cmd.StandardInput.Flush();
        }



        private void DifficultySet(object sender, RoutedEventArgs e)
        {
            ComboBox Difficulty = this.Find<ComboBox>("Difficulties");
            if (Difficulty.SelectedIndex == 3) { ServerInfo.cmd.StandardInput.WriteLine("difficulty hard"); }
            else if (Difficulty.SelectedIndex == 2) { ServerInfo.cmd.StandardInput.WriteLine("difficulty medium"); }
            else if (Difficulty.SelectedIndex == 1) { ServerInfo.cmd.StandardInput.WriteLine("difficulty easy"); }
            else if (Difficulty.SelectedIndex == 0) { ServerInfo.cmd.StandardInput.WriteLine("difficulty peaceful"); }
            ServerInfo.cmd.StandardInput.Flush();
        }

        private void Seed(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("seed");
            ServerInfo.cmd.StandardInput.Flush();
        }

        private void OpenServer(object sender, RoutedEventArgs e)
        {
            if (ServerInfo.Running == false) { Utilities.LaunchServer(); }
            else { /*ModernWpf.MessageBox.Show("ServerInfo is already running.");*/ }
        }


        private void Save(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("save-all");
            ServerInfo.cmd.StandardInput.Flush();
        }

        private async void StopServer(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("stop");
            ServerInfo.cmd.StandardInput.Flush();
            ServerInfo.Running = false;
            await Task.Delay(10000); //Gives about enough time for the world to save
            /*if (ModernWpf.MessageBox.Show("Do you want to go back?", "Server closed successfully", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\RSM.exe");
                Application.Current.Shutdown();
            }*/
        }
    }
}

