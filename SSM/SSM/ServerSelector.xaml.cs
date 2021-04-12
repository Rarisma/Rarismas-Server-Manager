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

namespace SSM
{
    /// <summary>
    /// Interaction logic for ServerSelector.xaml
    /// </summary>
    public partial class ServerSelector : UserControl
    {
        public ServerSelector()
        {
            InitializeComponent();
            for (int i = 0; i <= data.Servers.Length - 1; i++) 
            { 
                if (File.Exists(data.Servers[i] + "//SSM.ini")) { ListView.Items.Add(System.IO.Path.GetFileName(data.Servers[i])); }
            }
        }

        private void ServerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Minecraft.MinecraftCreatorData.ManagerFilepath = Convert.ToString(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ListView.SelectedValue + "//");
        }
    }

    public static class data { public static string[] Servers = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "//Servers//"); }
}
