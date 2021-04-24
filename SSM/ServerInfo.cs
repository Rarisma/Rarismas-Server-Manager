using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This stores data for the creator
namespace SSM
{
    class ServerInfo
    {
        public static string ServerGame    = "None Set";    //Stores what game is being ran
        public static string ServerLabel   = "None Set";    //Stores the name of the server
        public static string ServerURL     = "None Set";    //URL used to download server files
        public static string ServerVersion = "None Set";    //Used to keep track of what version the server is running
        public static string ServerVariant = "None Set";    //Used if there are multiple variants (Eg Paper, Bukkit ans spiggot)
        public static bool IsServerRunning = false;         //If set to true SSM prevents closing
        public static Int64 RAM = 0;                        //Used for servers that need RAM to be allocated, servers that don't will just have it set as 0
        public static Process cmd = new();                  //Used for Command Prompts
        public static StreamWriter InputStream;

    }
}
