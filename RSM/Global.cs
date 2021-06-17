using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Give me my respect Im Don Coreleone 
namespace RSM
{
    //This stores variables that are called across pages (Similar to how serverinfo used to be)
    class Global
    {
        public static bool Automatic = false;         //Used to know if automode should be used
        public static ContentControl? Display;        //Allows use of mainwindow from anywhere
        public static TextBox ConfigFileTextBox;      //Used for finding the config file
        public static string AppID = "";              //Used for SteamCMD when downloading
        public static string User = "";               //Used for steam games
        public static string Pass = "";               //Used for steam games
        public static Process Server = new();         //Contains the server process
        public static bool IsServerRunning = false;   //Used to tell if the server is running

        public static string Steam = AppDomain.CurrentDomain.BaseDirectory + "//Tools//Steam//";
        public static string Java16 = AppDomain.CurrentDomain.BaseDirectory + "//Tools//Java16//bin//";
        public static string Java8 = AppDomain.CurrentDomain.BaseDirectory + "//Tools//Java8//bin//";
    }
}
