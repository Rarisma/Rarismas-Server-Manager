using ModernWpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;


//Thoe code on this page controls the items that show up in the LIstView
namespace SSM.Pages.SSM_GUI
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Page
    {
        public Welcome()
        {
            InitializeComponent();

            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "//Servers//"))
            {
                string[] Servers = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "//Servers//");

                for (int i = 0; i <= Servers.Length - 1; i++) // Checks each directory found for an SSM.ini file
                { if (File.Exists(Servers[i] + "//SSM.ini")) { ListView.Items.Add(System.IO.Path.GetFileName(Servers[i])); } }
            }

            if (ListView.Items.Contains("Easter Egg")) { ((MainWindow)System.Windows.Application.Current.MainWindow).Title += " When using pokemon cards, please don't use a holographic!"; }
            else { ((MainWindow)System.Windows.Application.Current.MainWindow).Title = "SSM Next"; }
        }

        private void ServerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {   //If clicked sends them to server manager ( unless its the newserver option )
            if (Convert.ToString(ListView.SelectedValue).Contains("Create a new server")) { ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new NewServer(); }
            else
            {
                SSMGeneric.Read_INI_File(Convert.ToString(ListView.SelectedValue));

                switch (ServerInfo.ServerGame)
                {
                    case "Minecraft Java":
                        ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new ServerManager();
                        break;

                    case "Minecraft Bedrock":
                        ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new ServerManager();
                        break;

                    case "Terraria":
                        ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new ServerManager();
                        break;
                }


            }
        }
    }
}
