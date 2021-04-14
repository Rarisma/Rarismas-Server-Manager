using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSM.Minecraft
{
    class MinecraftCreatorData
    {
        public static string Edition;               //Should be Java or Bedrock
        public static string ServerType = "NULL";   //Should be Paper, Bedrock, Java, Forge, Fabric
        public static int ServerSetupChange = 0;    //0 - Java or bedrock     1 - Server type
        public static Int64 AllocatedRAM;           //How many MB of RAM should be allocate 
        public static string Version;               //What version is being downloaded?
        public static string ServerName = "";       //What is the name of the server?
        public static string ServerFilesURL;        //Link to server files

        public static List<string> URLs = new();    //Used to store the links from the Server File Listings in Finalization.xaml.cs 
        public static string ManagerFilepath;       //Used by the ServerManger to know where the server path is
    }
}
