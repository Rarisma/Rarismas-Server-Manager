using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//You push a button and we do the rest!
//GBA Enhanced!
namespace RSM.Creator
{
    class Automode
    {
        static List<String> ServerReader = new();
        public static void Build()
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java": CreatePaperServer(); break;
                case "Minecraft Java (Modded)": CreateForgeServer(); break;
                case "Minecraft Bedrock": CreateBedrockServer(); break;
                case "Terraria": CreateTerrariaServer(); break;
                case "Factorio": ServerInfo.Variant = "Vanilla"; Global.AppID = "427520"; Global.Display.Content = new UI.Steam(); break;
            }
            ServerInfo.Version = ServerReader[0];
            ServerInfo.URL = ServerReader[1];
            Global.Display.Content = new UI.Downloader();
        }
        private static void CreatePaperServer()
        {
            ServerInfo.Variant = "Vanilla";
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerFiles/Minecraft/Paper", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Paper");
            ServerReader.AddRange(File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Paper"));
            ServerInfo.RAM = Convert.ToInt64(LibRarisma.IO.GetRAM()) - 4096; //Should leave enough for the user to play aswell
        }

        private static void CreateForgeServer()
        {
            ServerInfo.Game = "Minecraft Java";
            ServerInfo.Variant = "Modded";
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerFiles/Minecraft/Forge", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Forge");
            ServerReader.AddRange(File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Forge"));
            ServerInfo.RAM = Convert.ToInt64(LibRarisma.IO.GetRAM()) - 4096; //Should leave enough for the user to play aswell
        }

        private static void CreateTerrariaServer()
        {
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerFiles/Terraria/Tshock", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "TerrariaTShock");
            ServerInfo.Variant = "TShock";
            ServerReader.AddRange(File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//TerrariaTShock"));
            ServerInfo.Difficulty = "0"; //Creates a classic mode world
            ServerInfo.WorldSize = "2"; //Creates a medium world
        }

        private static void CreateBedrockServer()
        {
            ServerInfo.Variant = "Bedrock";
            ServerInfo.RAM = 0;

            //Gets latest links to server
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Rarismas-Server-Manager/main/ServerFiles/Minecraft/bedrock", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Bedrock");
            ServerReader.AddRange(File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Bedrock"));
        }
    }
}
