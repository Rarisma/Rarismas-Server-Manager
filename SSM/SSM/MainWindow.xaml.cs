using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
//Everywhere at the end of time is quite scary.
namespace SSM
{
    public class Public
    {
        public int PageCounter = 0;
        public int PageChecker = -1;
        public bool isJava;


    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Continue.Opacity = 0;
            PageWindow.Content = new WelcomePage();
        }

        public void MinecraftEditionSelect() { PageWindow.Content = new Minecraft.JavaORBedrock(); }

        private void NewButton(object sender, RoutedEventArgs e)
        {
            PageWindow.Content = new Minecraft.JavaORBedrock();
            Continue.IsEnabled = true;
            Continue.Opacity = 100;

            //Disables the New server and Manage button
            Manage.IsEnabled = false;
            New.IsEnabled = false;
            New.Opacity = 0;
            Manage.Opacity = 0;
        }

        private void ContinueButton(object sender, RoutedEventArgs e)
        {
            if (Minecraft.MinecraftCreatorData.ServerSetupChange == 0 && Minecraft.MinecraftCreatorData.Edition == "Java") { PageWindow.Content = new Minecraft.Java.ServerTypeSelect(); Minecraft.MinecraftCreatorData.ServerSetupChange = 1; }
            else if (Minecraft.MinecraftCreatorData.ServerSetupChange == 1 && Minecraft.MinecraftCreatorData.ServerType != "NULL") { PageWindow.Content = new Minecraft.Finalization(); Minecraft.MinecraftCreatorData.ServerSetupChange = 2; }
            else if (Minecraft.MinecraftCreatorData.Version != "" && Minecraft.MinecraftCreatorData.AllocatedRAM != 0 && Minecraft.MinecraftCreatorData.ServerName != "" && Minecraft.MinecraftCreatorData.ServerSetupChange == 2) 
            {
                if (Minecraft.MinecraftCreatorData.ServerName == "EasterEgg") { this.Title = "SMM -  antimatter for the master plan"; }

                string ServerDir = AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + Minecraft.MinecraftCreatorData.ServerName + "\\";
                try { System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + Minecraft.MinecraftCreatorData.ServerName); }
                catch //This will run if the server name contains illegal characters (eg / . + ! null con ect)
                {
                    ServerDir = AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + Minecraft.MinecraftCreatorData.ServerName + "\\";
                    Minecraft.MinecraftCreatorData.ServerName = Convert.ToString((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds);
                    System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + Minecraft.MinecraftCreatorData.ServerName);
                }

                LibRarisma.IO.DownloadFile(Minecraft.MinecraftCreatorData.ServerFilesURL, ServerDir, "Server.jar"); //Downloads server file
                //System.IO.File.WriteAllText(ServerDir + "eula.txt", "This server was created by SSM\nThe user aknowlegdes by using SSM and creating a server that they agree to the Mojang EULA\neula=true"); //Makes the EULA accepted
                System.IO.File.WriteAllText(ServerDir + "SSM.ini", "# SSM Configuration File Version 1\n\n# Game Name\nMinecraft\n\n# Server type\n" + Minecraft.MinecraftCreatorData.ServerType + "\n\n# Server Edition\n" + Minecraft.MinecraftCreatorData.Edition + "\n\n# Game Version\n" + Minecraft.MinecraftCreatorData.Version
                + "\n\n# Ram Allocated\n" + Minecraft.MinecraftCreatorData.AllocatedRAM + "\n\n# User Label\n" + Minecraft.MinecraftCreatorData.ServerName);

                WelcomePage();

            }
            else if (Minecraft.MinecraftCreatorData.ServerSetupChange == -1) 
            {
                PageWindow.Content = new Minecraft.ServerManager();
                Continue.IsEnabled = false;
                Continue.Opacity = 0;
                Minecraft.MinecraftCreatorData.ServerSetupChange = -2;

            }
        }

        public void Console()
        {
            PageWindow.Content = new Minecraft.ServerConsole(); 
        }

        public void WelcomePage() 
        {
            PageWindow.Content = new WelcomePage();
            Manage.IsEnabled = true;
            New.IsEnabled = true;
            New.Opacity = 100;
            Manage.Opacity = 100;
        }

        //This sends the user to the server selector window when the Manage a server button is clicked
        private void ManageServer(object sender, RoutedEventArgs e)
        {
            Continue.IsEnabled = true;
            Continue.Opacity = 100;

            //Disables the New server and Manage button
            Manage.IsEnabled = false;
            New.IsEnabled = false;
            New.Opacity = 0;
            Manage.Opacity = 0;

            PageWindow.Content = new ServerSelector();
            Minecraft.MinecraftCreatorData.ServerSetupChange = -1;
        }
    }
}
