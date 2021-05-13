﻿using System;
using System.Collections.Generic;
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
    /// Interaction logic for Minecraft.xaml
    /// </summary>
    public partial class Minecraft : UserControl
    {
        public Minecraft()
        {
            InitializeComponent();
            if (ServerInfo.Game == "Minecraft Bedrock")
            {
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

        private void Easy(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("difficulty easy");
            ServerInfo.cmd.StandardInput.Flush();
        }

        private void Normal(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("difficulty normal");
            ServerInfo.cmd.StandardInput.Flush();
        }

        private void Hard(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("difficulty hard");
            ServerInfo.cmd.StandardInput.Flush();
        }

        private void Seed(object sender, RoutedEventArgs e)
        {
            ServerInfo.cmd.StandardInput.WriteLine("seed");
            ServerInfo.cmd.StandardInput.Flush();
        }

        private void OpenServer(object sender, RoutedEventArgs e)
        {
            if (ServerInfo.Running == false) {RSMGeneric.Utilities.LaunchServer(); }
            else { ModernWpf.MessageBox.Show("ServerInfo is already running."); }
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
            if (ModernWpf.MessageBox.Show("Do you want to close SSM?", "Server closed successfully", MessageBoxButton.YesNo) == MessageBoxResult.Yes) { await Task.Delay(10000); Application.Current.Shutdown(); }
        }
    }
}