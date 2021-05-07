using System;
using System.Collections.Generic;
//One last ride to dreamland.
namespace SSM
{
	class Automode 
	{
    	public static void CreatePaperServer()
    	{
       		ServerInfo.ServerVariant = "Paper";
       		LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/Paper", AppDomain.CurrentDomain.BaseDirectory + "//Cache//","Paper");
       		List<String> ServerReader = new();
       		ServerReader.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Paper"));
       		ServerInfo.ServerVersion = ServerReader[0];
			ServerInfo.ServerURL = ServerReader[1];
       		ServerInfo.RAM = Convert.ToInt64(LibRarisma.IO.GetRAM()) - 4096; //Should leave enough for the user to play aswell
       		SSMGeneric.BuildServer();
      	}
    	
		public static void CreateForgeServer()
    	{
			ServerInfo.ServerVariant = "Forge";
       		LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/Forge", AppDomain.CurrentDomain.BaseDirectory + "//Cache//","Forge");
       		List<String> ServerReader = new();
       		ServerReader.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Forge"));
       		ServerInfo.ServerVersion = ServerReader[0];
       		ServerInfo.RAM = Convert.ToInt64(LibRarisma.IO.GetRAM()) - 4096; //Should leave enough for the user to play aswell
			ServerInfo.ServerURL = ServerReader[1];
       		SSMGeneric.BuildServer();
		}
	}
}
