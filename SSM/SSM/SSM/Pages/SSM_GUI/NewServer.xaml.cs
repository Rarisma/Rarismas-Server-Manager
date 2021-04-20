using System;
using System.Windows;
using System.Windows.Controls;

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
            AvailableServers.Items.Add("Terraria");
            //AvailableServers.Items.Add("Terraria - TSHOCK"); //no, not yet.
            //AvailableServers.Items.Add("Terraria - Modded"); //You can wait.
            //AvailableServers.Items.Add("Minecraft Unified"); //Coming eventually
        }

        private void ServerChoiceUpdated(object sender, SelectionChangedEventArgs e) //This function updates the server description
        { 
            switch Convert.ToString(AvailableServers.SelectedValue)
            {
                case "Minecraft Bedrock":
                    ServerInfo.ServerGame  = "Minecraft Bedrock";
                    ServerDescription.Text = "Minecraft Bedrock is crossplatform, but doesn't support mods and may require workarounds to get people to connect to your server however if you want to just play a vanilla survival world Bedrock is probably the better option";
                    break;
                
                case "Minecraft Java":
                    ServerInfo.ServerGame  = "Minecraft Java";
                    ServerDescription.Text = "Minecraft Java is the original PC version, however you can only connect to other minecraft java players, it is worth noting that these servers support plugins and custom resource packs, Minecraft Java servers are easier to setup.";
                    break;
                    
                case "Minecraft Unified":
                    ServerInfo.ServerGame = "Minecraft Reuinion";
                    ServerDescription.Text = "Want to play with both versions?\nThen this experemental version might allow you to, this is much harder than setting up a regular minecraft server and the server will use the Java edition to base the gameplay on, meaning that bedrock players will have changes such as combat update. It is worth noting that this build supports plugins.\nThis edition is currently unavailable at this time.";
                    break;

                case "Terraria":
                    ServerInfo.ServerGame = "Terraria";
                    ServerDescription.Text = "This will allow player to connect to your world even when you aren't playing terraria yourself";
                    
                case "Terraria - TShock":
                    ServerInfo.ServerGame = "Terraria";
                    ServerInfo.Variant = "TShock;"
                    ServerDescription.Text = "A Terraria TShock server gives the server owner more control over their server and includes tools such as an anti cheat, custom commands and has support for plugins.";
                    
                default:
                    ServerDescription.Text = "Cannot find a description for " + AvailableServers.SelectedValue; 
                    break;
            }   
        }

        //This updates the serverlabel when the textbox is updated
        private void ServerNameUpdated(object sender, TextChangedEventArgs e) 
        {
            string[] DisallowedCharacters = { '/', '\\', '?',':', ';', '*', '"', '<', '>', '|' }
            ServerInfo.ServerLabel = ServerName.Text; 
            for (int i = 0; i <= ServerInfo.ServerLabel.Length; i++)
            {
                if (ServerInfo.ServerLabel[i] )
            }
            
        }

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
            System.IO.Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Cache", true);    //Deletes Cache
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Cache");
            LibRarisma.IO.Rema

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
                        
                    case "Terraria":
                        //((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new Minecraft_Java.RamAllocation();
                        break;
                        
                }
            }

        }
    }
}
 
