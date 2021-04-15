using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public ServerManager() //Fuck this, spent about 4 hours figuring out what was up with the code thats in
                               //Initalise() to find out That somehow the code executed here is different from a function
        {
            InitializeComponent();
            Initalise();
        }

        private void Initalise()
        {
            List<string> iniFile = new();
            iniFile.AddRange(System.IO.File.ReadAllLines(MinecraftCreatorData.ManagerFilepath + "\\SSM.ini"));

            MinecraftCreatorData.Edition = iniFile[iniFile.IndexOf("# Server Edition") + 1];
            MinecraftCreatorData.Version = iniFile[iniFile.IndexOf("# Game Version")   + 1];
            MinecraftCreatorData.AllocatedRAM = Convert.ToInt32(iniFile[iniFile.IndexOf("# Ram Allocated") + 1]);
            MinecraftCreatorData.ServerName = iniFile[iniFile.IndexOf("# User Label")  + 1];
            MinecraftCreatorData.ServerType = iniFile[iniFile.IndexOf("# Server type") + 1];

            ServerVersion.Content = "Minecraft " + MinecraftCreatorData.Version + "\n" + MinecraftCreatorData.Edition + " edition" + " (" + MinecraftCreatorData.ServerType + ")"; 
            ServerName.Content = MinecraftCreatorData.ServerName;
        }

        public void Launcher(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Process.Start("CMD.exe", "/c java -Xms" + Convert.ToString(MinecraftCreatorData.AllocatedRAM) + "M -jar " + (char)34 + MinecraftCreatorData.ManagerFilepath + "server.jar" + (char)34 + " -nogui");
            //System.Diagnostics.Process.Start("CMD.exe", "/k " + (char)34 + MinecraftCreatorData.ManagerFilepath + (char)34);
            // System.IO.File.WriteAllText("A:\\FuckYou.txt", "java -Xms" + Convert.ToString(MinecraftCreatorData.AllocatedRAM) + "M -jar " + (char)34 + MinecraftCreatorData.ManagerFilepath + "server.jar" + (char)34 + " -nogui");

            ((MainWindow)System.Windows.Application.Current.MainWindow).Console();

        }

        private void DeleteServer(object sender, RoutedEventArgs e) 
        {
            if (ModernWpf.MessageBox.Show("Are you sure you want to delete this server?","Confirm deletion",MessageBoxButton.YesNo) == MessageBoxResult.Yes) 
            {
                System.IO.Directory.Delete(MinecraftCreatorData.ManagerFilepath, true);
                ModernWpf.MessageBox.Show("Server deleted");
                ((MainWindow)System.Windows.Application.Current.MainWindow).WelcomePage();
            }
        }
    }
}
