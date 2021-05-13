﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RSM.QuickCommands
{
    /// <summary>
    /// Interaction logic for Terraria.xaml
    /// </summary>
    public partial class Terraria : Page
    {
        public Terraria() { InitializeComponent(); }

        private void Dawn(object sender, RoutedEventArgs e) { SendCommand("dawn"); }
        private void Dusk(object sender, RoutedEventArgs e) { SendCommand("dusk"); }
        private void Restart(object sender, RoutedEventArgs e) { SendCommand("restart"); }
        private void Bloodmoon(object sender, RoutedEventArgs e) { SendCommand("bloodmoon"); }
        private void Eclipse(object sender, RoutedEventArgs e) { SendCommand("ecl"); }

        private void Save(object sender, RoutedEventArgs e) { SendCommand("save"); }
        public static void SendCommand(string command) { ServerInfo.cmd.StandardInput.WriteLine(command); }

        private async void StopServer(object sender, RoutedEventArgs e)
        {
            SendCommand("stop");
            ServerInfo.Running = false;
            await Task.Delay(10000); //Gives about enough time for the world to save
            if (ModernWpf.MessageBox.Show("Do you want to close RSM?", "Server closed successfully", MessageBoxButton.YesNo) == MessageBoxResult.Yes) { await Task.Delay(10000); Application.Current.Shutdown(); }
        }

    }
}