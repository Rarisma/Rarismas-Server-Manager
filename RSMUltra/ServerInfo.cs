using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMUltra
{
    class ServerInfo
    {
        public static string Name = "";
        public static string Game = "";
        public static string Variant = "";
        public static string Version = "";
        public static string LastBackup = "";
        public static string BackupFrequency = "";
        public static string WorldSize = "2"; //Used by terraria
        public static string Difficulty = "2"; //terraria only
        public static string AllocatedRAM = Convert.ToString(Convert.ToInt32(LibRarisma.Tools.GetRAM() / 2) - 1024);


        public static Process Server = new();
    }
}
