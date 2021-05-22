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
//Night of the living code
namespace RSM.PerGameSettings
{
    public partial class MinecraftText : Page
    {
        public MinecraftText()
        {
            InitializeComponent();
            ServerFile.Text = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Servers//" + ServerInfo.Label + "//Server.properties");
        }

        private void Save(object sender, TextChangedEventArgs e)
        {
            string[] sep = new string[] { "\r\n" };
            string[] lines = ServerFile.Text.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            System.IO.File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//Server.properties", lines);
        }
    }
}
