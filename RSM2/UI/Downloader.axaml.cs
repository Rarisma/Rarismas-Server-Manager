using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
/*
 Hey, Rarisma here.
Chances are you've looked at some of the UI
this is my first large UI app
I've learned a lot of things
So when RSM 2.0 is released
2.1 will be a rewrite of the UI

Also add Hitochi no Oobroi (I definitely missspelled that) to my recomendations
 */
namespace RSM2.UI
{
    public partial class Downloader : UserControl
    {
        public Downloader()
        {
            AvaloniaXamlLoader.Load(this);
            Initalise();
        }

        async void Initalise()
        {
            TextBlock ServerVersion = this.Find<TextBlock>("ServerVersion");
            TextBlock CurrentTask = this.Find<TextBlock>("CurrentTask");

            ServerVersion.Text = "Setting up " + ServerInfo.Name;
            CurrentTask.Text = "Downloading files";
            await Task.Run(() => DownloadFiles());

            CurrentTask.Text = "Configuring Server";
            await Task.Run(() => ServerConfig());

            CurrentTask.Text = "Setting up your server";
            await Task.Run(() => ServerSetup());

            CurrentTask.Text = "Cleaning up";
            await Task.Run(() => CleanUp());

            CurrentTask.Text = "Grabbing some extras";
            await Task.Run(() => Extras());

            ServerInfo.User = "";
            ServerInfo.Pass = "";
            Utilities.MakeINI();
            ServerInfo.MainWindow.Content = new Welcome();
        }

        public void DownloadFiles()
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java": LibRarisma.IO.DownloadFile(ServerInfo.URL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//", "Server.jar"); break;
                case "Factorio": try { LibRarisma.IO.DownloadFile("https://github.com/SteamRE/DepotDownloader/releases/download/DepotDownloader_2.4.1/depotdownloader-2.4.1.zip", AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Steam\\", "Steam.zip", true); } catch { }; break;
                default: LibRarisma.IO.DownloadFile(ServerInfo.URL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name, "//Server.zip", true); break;
            }
        }

        public void ServerConfig()
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Java") == false)
                    {
                        LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerFiles/2.0/Java", AppDomain.CurrentDomain.BaseDirectory + "\\Cache\\", "Java");
                        string[] Java = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\Cache\\Java");
                        LibRarisma.IO.DownloadFile(Java[0], AppDomain.CurrentDomain.BaseDirectory + "\\Cache\\", "JDK.zip");

                        System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Cache\\JDK.zip", AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Temp\\");
                        string[] Dirs = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Temp\\");
                        Directory.Move(Dirs[0], AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Java");
                    }
                    LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerFiles/Minecraft/ServerConfigs/stock", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//", "server.properties");
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//" + "eula.txt", "#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula).\n# made by SSM\neula = true"); //Makes the EULA accepted
                    break;
            }
        }

        public void ServerSetup()
        {
            switch (Convert.ToString(ServerInfo.Game + " " + ServerInfo.Variant))
            {
                case "Minecraft Java Modded":
                    File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//Forge.bat", new string[] {"\"" + AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Java\\bin\\java.exe\" -jar \"" + AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.Name + "\\Server.jar\" --installServer", "exit" });
                    Process cmd = new();
                    cmd.StartInfo.FileName = "cmd.exe";
                    cmd.StartInfo.RedirectStandardInput = true;
                    cmd.StartInfo.CreateNoWindow = true;
                    cmd.Start();
                    cmd.StandardInput.WriteLine("cd Servers");
                    Task.Delay(2000);
                    cmd.StandardInput.Flush();
                    cmd.StandardInput.WriteLine("cd " + ServerInfo.Name);
                    Task.Delay(2000);
                    cmd.StandardInput.Flush();
                    cmd.StandardInput.WriteLine("Forge.bat");
                    Task.Delay(2000);
                    cmd.StandardInput.Flush();
                    cmd.WaitForExit();
                    break;
                case "Terraria TShock": LibRarisma.IO.DownloadFile("https://github.com/Rarisma/Rarismas-Server-Manager/raw/main/ServerFiles/Terraria/RSMHelper.dll", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//ServerPlugins//", "RSM.dll"); break;
                case "Factorio Vanilla":
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//");
                    Process SteamInstance = new();
                    SteamInstance.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Steam\\DepotDownloader.exe";
                    SteamInstance.StartInfo.Arguments = "-app " + ServerInfo.AppID + " -username " + ServerInfo.User + " -password " + ServerInfo.Pass + " -dir \"" + AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//\"";
                    SteamInstance.Start();
                    SteamInstance.WaitForExit();
                    break;
            }
        }

        public void CleanUp()
        {
            switch (Convert.ToString(ServerInfo.Game + " " + ServerInfo.Variant))
            {
                case "Terraria TShock": File.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//Server.zip"); break;
                case "Bedrock": File.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//Server.zip"); break;
            }
        }

        public void Extras()
        {
            switch (Convert.ToString(ServerInfo.Game + " " + ServerInfo.Variant))
            {
                case "Minecraft Java Vanillia": Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//" + "plugins//"); break;
                case "Minecraft Java Modded": Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//" + "mods//"); break;
                case "TShock": LibRarisma.IO.DownloadFile("https://github.com/Rarisma/Simple-Server-Manager/blob/main/ServerFiles/Terraria/SSMHelper.dll?raw=true", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//ServerPlugins//", "SSMHelper.dll"); break;
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
                    string[] configfile = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.Name + "\\config-path.cfg");
                    configfile[0] = "config-path=" + AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.Name + "\\Config\\";
                    configfile[configfile.Length - 1] = "use-system-read-write-data-directories=false";
                    File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.Name + "\\config-path.cfg", configfile);

                    Process worldgen = new();
                    worldgen.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.Name + "\\bin\\x64\\Factorio.exe";
                    worldgen.StartInfo.Arguments = "--create \"" + AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.Name + "\\bin\\x64\\saves\\RSM.zip\"";
                    worldgen.StartInfo.RedirectStandardInput = true;
                    worldgen.Start();
                    worldgen.WaitForExit();
                    break;
            }
        }

    }
}