using System;
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
//The mirrors image tells me its home time
//but I aint finished.
namespace RSM.Creator
{
    public partial class Difficulty : Page
    {
        public Difficulty()
        {
            InitializeComponent();
            switch (ServerInfo.Game)
            {
                case "Terraria":
                    DifficultyCombox.Items.Add("Journey");
                    DifficultyCombox.Items.Add("Classic");
                    DifficultyCombox.Items.Add("Expert");
                    DifficultyCombox.Items.Add("Master");
                    break;
            }

        }

        private void Continue(object sender, RoutedEventArgs e) { if (DifficultyCombox.Text != "") { ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new RSMGeneric.UI.Downloader(); } }
        private void GoBack(object sender, RoutedEventArgs e) { if (ServerInfo.Difficulty != "Not Set") { ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new VersionSelecter(); }  }
        private void DificultyUpdate(object sender, SelectionChangedEventArgs e) 
        {
            if (Convert.ToString(DifficultyCombox.SelectedItem).Contains("Journey")) { ServerInfo.Difficulty = "3"; }
            else if (Convert.ToString(DifficultyCombox.SelectedItem).Contains("Classic")) { ServerInfo.Difficulty = "3"; }
            else if (Convert.ToString(DifficultyCombox.SelectedItem).Contains("Expert")) { ServerInfo.Difficulty = "1"; }
            else if (Convert.ToString(DifficultyCombox.SelectedItem).Contains("Master")) { ServerInfo.Difficulty = "2"; }
        }
    }
}
