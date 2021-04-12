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

namespace SSM.Minecraft
{
    /// <summary>
    /// Interaction logic for ServerManager.xaml
    /// </summary>
    public partial class ServerManager : UserControl
    {
        public ServerManager()
        {
            List<string> iniFile = new();
            iniFile.AddRange(System.IO.File.ReadAllLines(MinecraftCreatorData.ManagerFilepath + "\\SSM.ini"));

            MinecraftCreatorData.Edition = iniFile[iniFile.IndexOf("# Server Edition") + 1];

            MinecraftCreatorData.Version = iniFile[iniFile.IndexOf("# Game Version") + 1];
            MinecraftCreatorData.AllocatedRAM = Convert.ToInt32(iniFile[iniFile.IndexOf("# Ram Allocated") + 1]);
            MinecraftCreatorData.ServerName = iniFile[iniFile.IndexOf("# User Label") + 1];

            InitializeComponent();
        }

        private void Launcher(object sender, RoutedEventArgs e) { System.Diagnostics.Process.Start("CMD.exe", "/c java -xms" + Convert.ToString(MinecraftCreatorData.AllocatedRAM) + "m -jar " + MinecraftCreatorData.ManagerFilepath + "server.jar -nogui"); }
    }
}
