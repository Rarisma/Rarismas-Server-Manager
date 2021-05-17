using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace RSM.RSMGeneric.UI
{
    public partial class Downloader : Page
    {
        public Downloader() 
        { 
            InitializeComponent();
            Task.Delay(1000);
            switch (ServerInfo.Game)
            {
                case "Minecraft Java": MinecraftJava(); break;
                case "Minecraft Bedrock": MinecraftBedrock(); break;
                case "Terraria": Terraria(); break;
            }
        }

        public void MinecraftJava()
        {
            CurrentTask.Text = "Downloading files...";
            Task.Delay(100); 
            LibRarisma.IO.DownloadFile(ServerInfo.URL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//", "Server.jar");
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerFiles/Minecraft/ServerConfigs/stock", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//", "server.properties");

            CurrentTask.Text = "Accepting EULA...";
            Task.Delay(100);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//" + "eula.txt", "#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula).\n# made by SSM\neula = true"); //Makes the EULA accepted
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//" + "plugins//");
            if (ServerInfo.Variant == "Forge")
            {
                CurrentTask.Text = "Setting up forge...";
                Task.Delay(100);
                Process cmd = new();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = false;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();
                cmd.StandardInput.AutoFlush = true;
                cmd.StandardInput.WriteLine("cd Servers");
                cmd.StandardInput.WriteLine("cd " + ServerInfo.Label);
                cmd.StandardInput.WriteLine("java -jar Server.jar --installServer");

                Task.Delay(60000); //Gives about enough time for forge to install
                cmd.Close();
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//Server.jar");
                List<String> Jarfiles = new();
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//" + "mods//");
                Jarfiles.AddRange(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//", "*.jar", SearchOption.TopDirectoryOnly));
                if (Jarfiles[0].Contains("forge")) { File.Move(Jarfiles[0], AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//" + "Server.jar"); } else { File.Move(Jarfiles[1], AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//" + "Server.jar"); }
            }
            CurrentTask.Text = "Finished!";
            Task.Delay(100);
            Finish();
        }

        public static void MinecraftBedrock()
        {
            ServerInfo.Variant = "Bedrock";
            ServerInfo.RAM = 0;

            //Gets latest links to server
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/bedrock", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Bedrock");
            string[] ServerFile = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Bedrock");
            ServerInfo.Version = ServerFile[0];
            ServerInfo.URL = ServerFile[1];
            LibRarisma.IO.DownloadFile(ServerFile[1], AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label, "//Server.zip", true);

            //You have no fukcing idea how much stress this caused me
            if (ModernWpf.MessageBox.Show("If you want to host and play on this system you will need to apply a patch.\n\nWould you like to apply it now?", "Apply workaround?", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    Process Workaround = new();
                    Workaround.StartInfo.Verb = "runas";
                    Workaround.StartInfo.FileName = "powershell.exe";
                    Workaround.StartInfo.Arguments = "-NoExit -Command CheckNetIsolation LoopbackExempt -a -p=S-1-15-2-1958404141-86561845-1752920682-3514627264-368642714-62675701-733520436";
                    Workaround.StartInfo.UseShellExecute = true;
                    Workaround.Start();
                }
                catch { ModernWpf.MessageBox.Show("An error occurred when running the command."); }
            }

            Finish();
        }

        public static void Terraria()
        {
            ServerInfo.Variant = "Normal";
            LibRarisma.IO.DownloadFile(ServerInfo.URL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//", "Terraria.zip", true);
            LibRarisma.IO.DownloadFile("https://github.com/Rarisma/Simple-Server-Manager/blob/main/ServerFiles/Terraria/SSMHelper.dll?raw=true", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//ServerPlugins//", "SSMHelper.dll");
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//Terraria.zip");
            Finish();
        }

        private static void Finish()
        {
            Utilities.Make_INI_File();
            ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new LaunchPage();
        }

    }
}
