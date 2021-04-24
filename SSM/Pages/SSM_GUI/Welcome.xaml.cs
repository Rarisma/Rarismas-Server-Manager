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
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark; //Forces darkmode, lightmode should be added at somepoint
            ThemeManager.Current.AccentColor = Colors.White;
            ListView.Items.Add("Create a new server"); //Add the new server button

            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "//Servers//"))
            {
                string[] Servers = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "//Servers//");

                for (int i = 0; i <= Servers.Length - 1; i++) // Checks each directory found for an SSM.ini file
                { if (File.Exists(Servers[i] + "//SSM.ini")) { ListView.Items.Add(System.IO.Path.GetFileName(Servers[i])); } }
            }

            if (ListView.Items.Contains("Easter Egg")) { ((MainWindow)System.Windows.Application.Current.MainWindow).Title += " When using pokemon cards, please don't use a holographic!"; }
            else { ((MainWindow)System.Windows.Application.Current.MainWindow).Title = "SSM 1.1"; }
        }

        private void ServerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {   //If clicked sends them to server manager ( unless its the newserver option )
            if (Convert.ToString(ListView.SelectedValue) == "Create a new server") { ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new NewServer(); }
            else
            {
                List<string> SSM_INI = new();
                SSM_INI.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ListView.SelectedValue + "//SSM.ini"));
                ServerInfo.ServerGame    = SSM_INI[SSM_INI.IndexOf("### Game Name") + 1];
                ServerInfo.ServerLabel   = SSM_INI[SSM_INI.IndexOf("### Server label") + 1];
                ServerInfo.RAM           = Convert.ToInt64(SSM_INI[SSM_INI.IndexOf("### Ram allocated") + 1]);
                ServerInfo.ServerVariant = SSM_INI[SSM_INI.IndexOf("### Server variant") + 1];
                ServerInfo.ServerVersion = SSM_INI[SSM_INI.IndexOf("### Server version") + 1];

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
