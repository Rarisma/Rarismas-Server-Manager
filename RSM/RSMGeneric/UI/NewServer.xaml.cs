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

namespace RSM.RSMGeneric.UI
{
    /// <summary>
    /// Interaction logic for NewServer.xaml
    /// </summary>
    public partial class NewServer : Page
    {
        public NewServer()
        {
            InitializeComponent();
            if (ServerInfo.Automatic == true) 
            {
                AutoModeNotice.Opacity = 1; // Shows warning that automode is on when applicable.
                AvailableServers.Items.Add("Minecraft Java (Modded)");
            } 


            AvailableServers.Items.Add("Minecraft Java");
            AvailableServers.Items.Add("Minecraft Bedrock");
            AvailableServers.Items.Add("Terraria");
            //AvailableServers.Items.Add("Factorio"); //no, not yet.
            //AvailableServers.Items.Add("Minecraft Unified"); //Keep dreaming
        }

        private void ServerChoiceUpdated(object sender, SelectionChangedEventArgs e) //This function updates the server description
        {
            switch (Convert.ToString(AvailableServers.SelectedValue))
            {
                case "Minecraft Bedrock":
                    ServerInfo.Game = "Minecraft Bedrock";
                    ServerDescription.Text = "Minecraft Bedrock is crossplatform, but doesn't support mods and may require workarounds to get people to connect to your server however if you want to just play a vanilla survival world Bedrock is probably the better option";
                    break;

                case "Minecraft Java":
                    ServerInfo.Game = "Minecraft Java";
                    ServerDescription.Text = "Minecraft Java is the original PC version, however you can only connect to other minecraft java players, it is worth noting that these servers support plugins and custom resource packs, Minecraft Java servers are easier to setup.";
                    break;

                case "Minecraft Java (Modded)":
                    ServerInfo.Game = "Minecraft Java (Modded)";
                    ServerDescription.Text = "Minecraft Java is the original PC version, however this version has mod support.";
                    break;

                case "Minecraft Unified":
                    ServerInfo.Game = "Minecraft Reuinion";
                    ServerDescription.Text = "Want to play with both versions?\nThen this experemental version might allow you to, this is much harder than setting up a regular minecraft server and the server will use the Java edition to base the gameplay on, meaning that bedrock players will have changes such as combat update. It is worth noting that this build supports plugins.\nThis edition is currently unavailable at this time.";
                    break;

                case "Terraria":
                    ServerInfo.Game = "Terraria";
                    ServerDescription.Text = "This will allow players to connect to your world even when you aren't playing terraria yourself";
                    break;

                default: ServerDescription.Text = "Failed to find a description for " + AvailableServers.SelectedValue + "\nThis should not happen if you are using a release version of SSM."; break;
            }
        }

        //This updates the serverlabel when the textbox is updated
        private void ServerNameUpdated(object sender, TextChangedEventArgs e) { ServerInfo.Label = ServerName.Text; }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            ServerInfo.Game = "None Set";
            ServerInfo.Label = "None Set";
            ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new LaunchPage();
        }

        private void Continue(object sender, RoutedEventArgs e)
        {
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Servers"); //Tries to make a servers folder
            LibRarisma.IO.RecreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Cache");

            if (ServerInfo.Label != "None Set" && ServerInfo.Game != "None Set" && ServerInfo.Automatic == false)
            {
                switch (ServerInfo.Game)
                {

                    case "Minecraft Bedrock": Automode.CreateBedrockServer(); break;
                    case "Minecraft Java": ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new Creator.RAMAllocator(); break;
                    case "Minecraft Reunion": ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new Creator.RAMAllocator(); break;
                    case "Terraria": ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new Creator.WorldSize(); break;

                }
            }
            else if (ServerInfo.Label != "None Set" && ServerInfo.Game != "None Set" && ServerInfo.Automatic == true)
            {
                switch (ServerInfo.Game)
                {
                    case "Minecraft Java": Automode.CreatePaperServer(); break;
                    case "Minecraft Java (Modded)": ServerInfo.Game = "Minecraft Java"; Automode.CreateForgeServer(); break;
                    case "Minecraft Bedrock": Automode.CreateBedrockServer(); break;
                    case "Terraria": Automode.CreateTerrariaServer(); break;
                }
            }

        }
    }
}
