using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSM
{
    class ServerInfo
    {
        public static string Game = "None Set";       //Stores what game is being ran
        public static string Label = "None Set";      //Stores the name of the server
        public static string URL = "None Set";        //URL used to download server files
        public static string Version = "None Set";    //Used to keep track of what version the server is running
        public static string Variant = "None Set";    //Used if there are multiple variants (Eg Paper, Bukkit ans spiggot)
        public static string WorldSize = "None Set";  //Used if World size is configurable
        public static Int64 RAM = 0;                  //Used for servers that need RAM to be allocated, servers that don't will just have it set as 0

        public static bool Automatic = false;         //Used to know if automode should be used
        public static bool Running = false;           //If set to true SSM prevents closing
        public static Process cmd = new();            //Used for Command Prompts
    }
}
