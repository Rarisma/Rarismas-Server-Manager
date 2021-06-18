using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
//polygraph tests are dumb as fuck
namespace RSM.UI
{
    public partial class Downloader : UserControl
    {
        public Downloader()
        {
            AvaloniaXamlLoader.Load(this);
            Initalise();
        }

        async void Initalise() //This runs the tasks 
        {
            ServerInfo.Dir = AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//";
            Label ServerVersion = this.Find<Label>("ServerVersion");
            Label CurrentTask = this.Find<Label>("CurrentTask");

            ServerVersion.Content = "Setting up " + ServerInfo.Name;
            CurrentTask.Content = "Downloading files";
            await Task.Run(() => DownloadFiles());

            CurrentTask.Content = "Configuring Server";
            await Task.Run(() => ServerConfig());

            CurrentTask.Content = "Setting up your server";
            await Task.Run(() => ServerSetup());

            CurrentTask.Content = "Cleaning up";
            await Task.Run(() => CleanUp());

            CurrentTask.Content = "Grabbing some extras";
            await Task.Run(() => Extras());

            Global.User = "";
            Global.Pass = "";
            Utilities.MakeINI();
            Global.Display.Content = new Welcome();
        }

        public void DownloadFiles() //Downloads the required files (either the server or SteamDepot)
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java": LibRarisma.IO.DownloadFile(ServerInfo.URL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//", "Server.jar"); break;
                case "Factorio": try { LibRarisma.IO.DownloadFile("https://github.com/SteamRE/DepotDownloader/releases/download/DepotDownloader_2.4.1/depotdownloader-2.4.1.zip", AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Steam\\", "Steam.zip", true); } catch { }; break;
                default: LibRarisma.IO.DownloadFile(ServerInfo.URL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name, "//Server.zip", true); break;
            }
        }

        public void ServerConfig() //Creates/Configures server files
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Java16") == false)
                    {
                        LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerFiles/2.0/Java", AppDomain.CurrentDomain.BaseDirectory + "\\Cache\\", "Java");
                        string[] Java = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\Cache\\Java");
                        LibRarisma.IO.DownloadFile(Java[0], AppDomain.CurrentDomain.BaseDirectory + "\\Cache\\", "JDK.zip");
                        System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Cache\\JDK.zip", AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Temp\\");
                        string[] Dirs = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Temp\\");
                        Directory.Move(Dirs[0], AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Java16\\");
                    }
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Java8") == false)
                    {
                        LibRarisma.IO.DownloadFile("https://www.dropbox.com/s/qdrgmj607r92uch/Java8.zip?dl=1", AppDomain.CurrentDomain.BaseDirectory + "\\Cache\\", "JDK8.zip");
                        System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Cache\\JDK8.zip", AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Temp\\");
                        string[] Dirs = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Temp\\");
                        Directory.Move(Dirs[0], AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Java8\\");
                    }
                    LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerFiles/Minecraft/ServerConfigs/stock", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//", "server.properties");
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//" + "eula.txt", "#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula).\n# made by RSM\neula = true"); //Makes the EULA accepted
                    break;
            }
        }

        public void ServerSetup() //Sorts out files eg worldgen and EULA
        {
            switch (Convert.ToString(ServerInfo.Game + " " + ServerInfo.Variant))
            {
                case "Minecraft Java Modded":
                    Process cmd = new();
                    cmd.StartInfo.FileName = Global.Java8 + "Java.exe";
                    cmd.StartInfo.Arguments = "-jar \""  + ServerInfo.Dir + "Server.jar\" --installServer";
                    cmd.StartInfo.WorkingDirectory = ServerInfo.Dir;
                    cmd.StartInfo.CreateNoWindow = false;
                    cmd.Start();
                    cmd.WaitForExit();
                    break;
                case "Factorio Vanilla":    
                    Directory.CreateDirectory(ServerInfo.Dir);
                    Process SteamInstance = new(); 
                    SteamInstance.StartInfo.FileName = Global.Steam + "DepotDownloader.exe";
                    SteamInstance.StartInfo.Arguments = "-app " + Global.AppID + " -username " + Global.User + " -password " + Global.Pass + " -dir \"" + ServerInfo.Dir + "\"";
                    SteamInstance.Start();
                    SteamInstance.WaitForExit();
                    break;
            }
        }

        public void CleanUp()
        {
            switch (Convert.ToString(ServerInfo.Game + " " + ServerInfo.Variant))
            {
                case "Terraria TShock": File.Delete( ServerInfo.Dir + "Server.zip"); break;
                case "Bedrock": File.Delete(ServerInfo.Dir + "Server.zip"); break;
            }
        }

        public void Extras()
        {
            switch (Convert.ToString(ServerInfo.Game + " " + ServerInfo.Variant))
            {
                case "Minecraft Java Vanillia": Directory.CreateDirectory(ServerInfo.Dir + "plugins//"); break;
                case "Minecraft Java Modded": Directory.CreateDirectory(ServerInfo.Dir+ "mods//"); break;
                case "Terraria TShock": LibRarisma.IO.DownloadFile("https://github.com/Rarisma/Rarismas-Server-Manager/raw/main/ServerFiles/Terraria/RSMHelper.dll", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//ServerPlugins//", "RSM.dll"); break;
                case "Minecraft Bedrock":
                    try
                    {
                        Process Workaround = new();
                        Workaround.StartInfo.Verb = "runas";
                        Workaround.StartInfo.FileName = "powershell.exe";
                        Workaround.StartInfo.Arguments = "-NoExit -Command CheckNetIsolation LoopbackExempt -a -p=S-1-15-2-1958404141-86561845-1752920682-3514627264-368642714-62675701-733520436";
                        Workaround.StartInfo.UseShellExecute = true;
                        Workaround.Start();
                    }
                    catch { }
                    break;
                case "Factorio Vanilla":
                    string[] configfile = File.ReadAllLines(ServerInfo.Dir + "//config-path.cfg");
                    configfile[0] = "config-path=" + ServerInfo.Dir + "//Config//";
                    configfile[configfile.Length - 1] = "use-system-read-write-data-directories=false";
                    File.WriteAllLines(ServerInfo.Dir + "config-path.cfg", configfile);

                    Process worldgen = new();
                    worldgen.StartInfo.FileName = ServerInfo.Dir + "bin//x64//Factorio.exe";
                    worldgen.StartInfo.Arguments = "--create \"" + ServerInfo.Dir + "//bin//x64//saves//RSM.zip\"";
                    worldgen.Start();
                    worldgen.WaitForExit();
                    break;
            }
        }
    }
}
