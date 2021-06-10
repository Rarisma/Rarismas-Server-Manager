using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RSM2.QuickCommands
{
    public partial class Terraria : UserControl
    {
        public Terraria()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void Save(object sender, RoutedEventArgs e) { SendCommand("/save"); }
        public static void SendCommand(string command) { File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.Name + "\\RSM\\RSM.txt", command); System.Threading.Thread.Sleep(1000); /*ServerInfo.cmd.StandardInput.WriteLine(command);*/ }

        private async void StopServer(object sender, RoutedEventArgs e)
        {
            SendCommand("/stop");
            ServerInfo.Running = false;
            await Task.Delay(10000); //Gives about enough time for the world to save
            /*if (ModernWpf.MessageBox.Show("Do you want to go back?", "Server closed successfully", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\RSM.exe");
                Application.Current.Shutdown();
            } */
        }

    }
}
