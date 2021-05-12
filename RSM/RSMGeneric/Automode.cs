using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This class fills out the stuff that RSM needs, and then calls ServerBuilder
namespace RSM.RSMGeneric
{
    class Automode
    {
        public static void CreatePaperServer()
        {
            ServerInfo.Variant = "Paper";
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/Paper", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Paper");
            List<String> ServerReader = new();
            ServerReader.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Paper"));
            ServerInfo.Version = ServerReader[0];
            ServerInfo.URL = ServerReader[1];
            ServerInfo.RAM = Convert.ToInt64(LibRarisma.IO.GetRAM()) - 4096; //Should leave enough for the user to play aswell
            ServerBuilder.Terraria();
        }

        public static void CreateForgeServer()
        {
            ServerInfo.Variant = "Forge";
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/Forge", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Forge");
            List<String> ServerReader = new();
            ServerReader.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Forge"));
            ServerInfo.Version = ServerReader[0];
            ServerInfo.RAM = Convert.ToInt64(LibRarisma.IO.GetRAM()) - 4096; //Should leave enough for the user to play aswell
            ServerInfo.URL = ServerReader[1];
            ServerBuilder.Terraria();
        }

        public static void CreateTerrariaServer()
        {
            ServerInfo.WorldSize = "2";
            ServerBuilder.Terraria();
        }

        public static void CreateBedrockServer() { ServerBuilder.MinecraftBedrock(); }
    }
}
