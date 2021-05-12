using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace RSM.RSMGeneric.UI
{
    /// <summary>
    /// Interaction logic for LaunchPage.xaml
    /// </summary>
    public partial class LaunchPage : Page
    {
        public LaunchPage()
        {
            InitializeComponent();

            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "//Servers//"))
            {
                string[] Servers = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "//Servers//");

                for (int i = 0; i <= Servers.Length - 1; i++) // Checks each directory found for an SSM.ini file
                { if (File.Exists(Servers[i] + "//RSM.ini")) { ListView.Items.Add(Path.GetFileName(Servers[i])); } }
            }

            if (ListView.Items.Contains("Easter Egg")) { ((MainWindow)Application.Current.MainWindow).Title += "SSM? Whats that?"; } //Special Thanks to Ben for this one
            else { ((MainWindow)System.Windows.Application.Current.MainWindow).Title = "RSM 1.0"; }
        }

        private void ServerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {   //If clicked sends them to server manager ( unless its the new server option )
            string SelectedValue = ListView.SelectedValue.ToString();
            SelectedValue = SelectedValue.Replace("ModernWpf.Controls.ListViewItem: ", "");
            ServerInfo.Automatic = false;
            if (SelectedValue == "Create a new server") { ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new NewServer(); }
            else if (SelectedValue == "Create new server (Automatic)") { ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new NewServer(); ServerInfo.Automatic = true; }
            else { LoadPage(); } //Calls the function to start to load the game
        }

        private void LoadPage()
        {
            string SelectedValue = ListView.SelectedValue.ToString();
            SelectedValue = SelectedValue.Replace("ModernWpf.Controls.ListViewItem: ", "");
            Utilities.Read_INI_File(SelectedValue);
            ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new ServerManger(); ;
        }
    }
}
