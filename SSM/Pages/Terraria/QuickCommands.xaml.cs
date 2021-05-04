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

namespace SSM.Pages.Terraria
{
    /// <summary>
    /// Interaction logic for QuickCommands.xaml
    /// </summary>
    public partial class QuickCommands : UserControl
    {
        public QuickCommands()
        {
            InitializeComponent();
        }


        private void Save(object sender, RoutedEventArgs e) { SendCommand("save"); }


        public static void SendCommand(string command) { File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.ServerLabel + "\\SSM\\SSM.txt", command); }

        private void StopServer(object sender, RoutedEventArgs e)
        {
            SendCommand("stop");
            ServerInfo.IsServerRunning = false;
        }

        private void OpenServer(object sender, RoutedEventArgs e)
        {
            if (ServerInfo.IsServerRunning == true) { ModernWpf.MessageBox.Show("Server is already running."); }
            else 
            {
                ServerInfo.cmd.Close();
                ServerInfo.cmd.Start();
                SSM_GUI.ServerUtils.LaunchServer();}
        }

    }
}