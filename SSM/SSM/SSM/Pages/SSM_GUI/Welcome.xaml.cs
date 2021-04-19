using System;
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
            ListView.Items.Add("Create a new server"); //Add the new server button

            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "//Servers//"))
            {
                string[] Servers = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "//Servers//");

                for (int i = 0; i <= Servers.Length - 1; i++ ) // Checks each directory found for an SSM.ini file
                { if (File.Exists(Servers[i] + "//SSM.ini")) { ListView.Items.Add(System.IO.Path.GetFileName(Servers[i])); } }
            }

            if (ListView.Items.Contains("Easter Egg")) { ((MainWindow)System.Windows.Application.Current.MainWindow).Title += " When using pokemon cards, please don't use a holographic!"; }
        }

        private void ServerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {   //If clicked sends them to server manager ( unless its the newserver option )
            if (Convert.ToString(ListView.SelectedValue) == "Create a new server") { ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new NewServer(); }
        }
    }
}
