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
        public Downloader()  { InitializeComponent(); Initalise(); }

        async void Initalise()
        {
            ServerVersion.Text = "Setting up " + ServerInfo.Label;
            CurrentTask.Text = "Downloading files...";
            await Task.Run(() => DownloadFiles());

            CurrentTask.Text = "Configuring Server";
            await Task.Run(() => ServerConfig());

            CurrentTask.Text = "Setting up your server";
            await Task.Run(() => ServerSetup());

            CurrentTask.Text = "Cleaning up";
            CleanUp();

            Extras();
            CurrentTask.Text = "Grabbing some extras";

            Utilities.Make_INI_File();
            ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new LaunchPage();

        }

        public void DownloadFiles()
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java": LibRarisma.IO.DownloadFile(ServerInfo.URL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//", "Server.jar"); break;
                default: LibRarisma.IO.DownloadFile(ServerInfo.URL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label, "//Server.zip", true); break;
            }
        }

        public void ServerConfig()
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerFiles/Minecraft/ServerConfigs/stock", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//", "server.properties");
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//" + "eula.txt", "#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula).\n# made by SSM\neula = true"); //Makes the EULA accepted
                    break;
            }
        }

        public void ServerSetup()
        {
            switch (ServerInfo.Variant)
            {
                case "Forge":
                    LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerFiles/Minecraft/ServerConfigs/SetupForge.bat", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//", "Forge.bat");
                    Process cmd = new();
                    cmd.StartInfo.FileName = "cmd.exe";
                    cmd.StartInfo.RedirectStandardInput = true;
                    cmd.StartInfo.CreateNoWindow = false;
                    cmd.Start();
                    cmd.StandardInput.AutoFlush = true;
                    cmd.StandardInput.WriteLine("cd Servers");
                    cmd.StandardInput.WriteLine("cd " + ServerInfo.Label);
                    cmd.StandardInput.WriteLine("Forge.bat");
                    cmd.WaitForExit();
                    break;
                case "TShock": LibRarisma.IO.DownloadFile("https://github.com/Rarisma/Rarismas-Server-Manager/raw/main/ServerFiles/Terraria/RSMHelper.dll", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//ServerPlugins//", "RSM.dll"); break;

            }

            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Java") == false)
             {
                LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerFiles/2.0/Java", AppDomain.CurrentDomain.BaseDirectory + "\\Cache\\", "Java");
                string[] Java = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\Cache\\Java");
                LibRarisma.IO.DownloadFile(Java[0], AppDomain.CurrentDomain.BaseDirectory + "\\Cache\\", "JDK.zip");

                System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Cache\\JDK.zip", AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Temp\\");
                string[] Dirs = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Temp\\");
                Directory.Move(Dirs[0], AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Java");
            }
        }

        public void CleanUp()
        {
            switch (ServerInfo.Variant)
            {
                case "Forge":
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//Server.jar");

                    List<String> Jarfiles = new();
                    Jarfiles.AddRange(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//", "*.jar", SearchOption.TopDirectoryOnly));
                    if (Jarfiles[0].Contains("forge")) { File.Move(Jarfiles[0], AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//" + "Server.jar"); } else { File.Move(Jarfiles[1], AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//" + "Server.jar"); }
                    break;
                case "TShock": File.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//Server.zip"); break;
                case "Bedrock": File.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//Server.zip"); break;
            }
        }

        public void Extras()
        {
            CurrentTask.Text = "Grabbing some extras";
            switch (ServerInfo.Variant)
            {
                case "Paper": Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//" + "plugins//"); break;
                case "Forge": Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//" + "mods//"); break;
                case "TShock": LibRarisma.IO.DownloadFile("https://github.com/Rarisma/Simple-Server-Manager/blob/main/ServerFiles/Terraria/SSMHelper.dll?raw=true", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//ServerPlugins//", "SSMHelper.dll"); break;
                case "Bedrock":
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
                    break;
            }
        }

    }
}
