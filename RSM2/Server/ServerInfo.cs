using System;
using System.Collections.Generic;
using System.Diagnostics; //Used for CMD
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RconSharp;
//This isn't even ServerInfo anymore, its just Global variables
namespace RSM2 //Its stored in UI but acts as as if its in the top level (This is intentional.)
{
    class ServerInfo
    {
        //These store data about the server itself
        public static string Game = "None Set";             //Stores what game is being ran (Mostly so that RSM knows what its doing)
        public static string Name = "None Set";             //Stores the name of the server
        public static string Version = "None Set";          //Used to keep track of what version the server is running for updates ect
        public static string Variant = "None Set";          //Used if there are multiple variants (Eg Minecraft Paper)
        public static string WorldSize  = "None Set";       //Used if world size needs to be set (Eg Terraria)
        public static string Difficulty = "None Set";       //Used if difficulty needs to be set (Eg Terraria)
        public static Int64 RAM = 0;                        //Used if the server needs to given a set ammount of RAM (Eg Minecraft)


        //These are used by RSM to track infomation about the server
        public static string BackupFrequency = "Weekly";                              //Used by RSM to know when the server should be backed up
        public static string Lastbackup = DateTime.Now.ToString("dd/MM/yyyy");        //Used to keep track of when the server was last backed up
        public static string URL = "None Set";                                        //URL used to download server files (Not stored currently)


        //These aren't saved
        public static bool Automatic = false;         //Used to know if automode should be used
        public static bool Running = false;           //If set to true SSM prevents closing
        public static Process cmd = new();            //Used for Command Prompts
        public static ContentControl? MainWindow;     //Allows use of mainwindow from anywhere
        public static TextBox ConfigFileTextBox;      //Used for finding the config file
        public static string AppID = "";              //Used for SteamCMD when downloading
        public static string User = "";               //Used for SteamCMD when downloading
        public static string Pass = "";               //Used for SteamCMD when downloading
        public static RconClient RCONClient;          //Used to connect to servers that use RCON
    }
}
