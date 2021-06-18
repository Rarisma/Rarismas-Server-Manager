using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Got banned from cringetopia about five minutes ago
namespace RSM
{
    class ServerInfo
    {
        //These store data about the server itself
        public static string Game = "None Set";             //Stores what game is being ran (Mostly so that RSM knows what its doing)
        public static string Name = "None Set";             //Stores the name of the server
        public static string Version = "None Set";          //Used to keep track of what version the server is running for updates ect
        public static string Variant = "None Set";          //Used if there are multiple variants (Eg Minecraft Paper)
        public static string WorldSize = "None Set";       //Used if world size needs to be set (Eg Terraria)
        public static string Difficulty = "None Set";       //Used if difficulty needs to be set (Eg Terraria)
        public static Int64 RAM = 0;                        //Used if the server needs to given a set ammount of RAM (Eg Minecraft)


        //These are used by RSM to track infomation about the server
        public static string BackupFrequency = "Weekly";                              //Used by RSM to know when the server should be backed up
        public static string Lastbackup = DateTime.Now.ToString("dd/MM/yyyy");        //Used to keep track of when the server was last backed up
        public static string URL = "None Set";                                        //URL used to download server files (Not stored currently)
        public static string Dir = "None Set";                                        //Stores the full path to the server for easy access

    }
}
