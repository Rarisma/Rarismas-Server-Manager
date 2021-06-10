using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSM2
{
    class Automode
    {
        public static void CreatePaperServer()
        {
            ServerInfo.Variant = "Vanilla";
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/Paper", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Paper");
            List<String> ServerReader = new();
            ServerReader.AddRange(File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Paper"));
            ServerInfo.Version = ServerReader[0];
            ServerInfo.URL = ServerReader[1];
            ServerInfo.RAM = Convert.ToInt64(LibRarisma.IO.GetRAM()) - 4096; //Should leave enough for the user to play aswell
            ServerInfo.MainWindow.Content = new UI.Downloader();
        }

        public static void CreateForgeServer()
        {
            ServerInfo.Variant = "Modded";
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/Forge", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Forge");
            List<String> ServerReader = new();
            ServerReader.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Forge"));
            ServerInfo.Version = ServerReader[0];
            ServerInfo.RAM = Convert.ToInt64(LibRarisma.IO.GetRAM()) - 4096; //Should leave enough for the user to play aswell
            ServerInfo.URL = ServerReader[1];
            ServerInfo.MainWindow.Content = new UI.Downloader();
        }

        public static void CreateTerrariaServer()
        {
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Terraria/Tshock", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "TerraiaTShock");
            ServerInfo.Variant = "TShock";
            string[] URLs = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//TerraiaTShock");
            ServerInfo.Version = URLs[0];
            ServerInfo.URL = URLs[1];
            ServerInfo.Difficulty = "0"; //Creates a classic mode world
            ServerInfo.WorldSize = "2"; //Creates a medium world
            ServerInfo.MainWindow.Content = new UI.Downloader();
        }

        public static void CreateBedrockServer()
        {
            ServerInfo.Variant = "Bedrock";
            ServerInfo.RAM = 0;

            //Gets latest links to server
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/bedrock", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Bedrock");
            string[] ServerFile = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Bedrock");
            ServerInfo.Version = ServerFile[0];
            ServerInfo.URL = ServerFile[1];
            ServerInfo.MainWindow.Content = new UI.Downloader();
        }

    }
}