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

namespace SSM.Pages.SSM_GUI
{
    /// <summary>
    /// Interaction logic for NewServer.xaml
    /// </summary>
    public partial class NewServer : UserControl
    {
        public NewServer()
        {
            InitializeComponent();
            AvailableServers.Items.Add("Minecraft Java");
            AvailableServers.Items.Add("Minecraft Bedrock");
            //AvailableServers.Items.Add("Minecraft Reunion");
        }

        private void ServerChoiceUpdated(object sender, SelectionChangedEventArgs e)
        { //This function updates the server description
            if (Convert.ToString(AvailableServers.SelectedValue) == "Minecraft Bedrock")   
            {
                ServerInfo.ServerGame  = "Minecraft Bedrock";
                ServerDescription.Text = "Minecraft Bedrock is crossplatform, but doesn't support mods and may require workarounds to get people to connect to your server however if you want to just play a vanilla survival world Bedrock is probably the better option"; 
            }
            else if (Convert.ToString(AvailableServers.SelectedValue) == "Minecraft Java") 
            {
                ServerInfo.ServerGame  = "Minecraft Java";
                ServerDescription.Text = "Minecraft Java is the original PC version, however you can only connect to other minecraft java players, it is worth noting that these servers support plugins and custom resource packs, Minecraft Java servers are easier to setup.";
            }
            else if (Convert.ToString(AvailableServers.SelectedValue) == "Minecraft Reuinion")
            {
                ServerInfo.ServerGame = "Minecraft Reuinion";
                //ServerDescription.FontSize = 15;
                ServerDescription.Text = "Want to play with both versions?\nThen this experemental version might allow you to, this is much harder than setting up a regular minecraft server and the server will use the Java edition to base the gameplay on, meaning that bedrock players will have changes such as combat update. It is worth noting that this build supports plugins.\nThis edition is currently unavailable at this time.";
            }
            else { ServerDescription.Text = "Failed to load description."; }
        }

        //This updates the serverlabel when the textbox is updated
        private void ServerNameUpdated(object sender, TextChangedEventArgs e) { ServerInfo.ServerLabel = ServerName.Text; }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            ServerInfo.ServerGame  = "None Set";
            ServerInfo.ServerLabel = "None Set";
            ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new Welcome();
        }

        private void Continue(object sender, RoutedEventArgs e)
        {
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Servers"); //Tries to make a servers folder
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Cache"); //Tries to make a servers folder
            System.IO.Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Cache", true);
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Cache"); //Tries to make a servers folder

            if (ServerInfo.ServerLabel != "None Set" && ServerInfo.ServerGame != "None Set")
            {
                switch (ServerInfo.ServerGame)
                {

                    case "Minecraft Bedrock":
                        ServerInfo.ServerVariant = "Bedrock";
                        ServerInfo.RAM = 0;

                        //Gets latest links to server
                        LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/bedrock", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Bedrock");
                        string[] ServerFile = System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Bedrock");
                        ServerInfo.ServerVersion = ServerFile[0];
                        LibRarisma.IO.DownloadFile(ServerFile[1], AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel, "//Server.zip", true);
                        
                        SSMGeneric.Make_INI_File();
                        ModernWpf.MessageBox.Show("Finished downloading!");
                        ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new Welcome();
                        break;

                    case "Minecraft Java":
                        ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new Minecraft_Java.RamAllocation();
                        break;

                    case "Minecraft Reunion":
                        ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new Minecraft_Java.RamAllocation();
                        break;
                }
            }

        }
    }
}
 