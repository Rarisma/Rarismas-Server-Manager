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
        public static string AllocatedRAM = "";


        public static Process Server = new();
    }
}
