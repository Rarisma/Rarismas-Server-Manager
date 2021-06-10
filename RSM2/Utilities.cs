using RconSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
//RSM: You push a button and we do the rest!
//Whaddya mean you've never seen blade runner
namespace RSM2
{
    class Utilities
    {
        public static void MakeINI()
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//RSM.ini",
                "RSM Config File V3\n\n" +
                "### Game\n" +
                 ServerInfo.Game + "\n\n" +
                "### Name\n" +
                ServerInfo.Name + "\n\n" +
                "### Ram Allocated\n" +
                ServerInfo.RAM + "\n\n" +
                "### Variant\n" +
                ServerInfo.Variant + "\n\n" +
                "### Version\n" +
                ServerInfo.Version + "\n\n" +
                "### World Size\n" +
                ServerInfo.WorldSize + "\n\n" +
                "### Backup Frequency\n" +
                ServerInfo.BackupFrequency + "\n\n" +
                "### Last Backup\n" +
                ServerInfo.Lastbackup + "\n\n" +
                "### Difficulty\n" +
                ServerInfo.Difficulty
                );
        }

        public static void ReadINI(Object ServerName)
        {
            List<string> RSM_INI = new();
            RSM_INI.AddRange(File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerName + "//RSM.ini"));

            ServerInfo.Game = RSM_INI[RSM_INI.IndexOf("### Game") + 1];
            ServerInfo.Name = RSM_INI[RSM_INI.IndexOf("### Name") + 1];
            ServerInfo.RAM  = Convert.ToInt64(RSM_INI[RSM_INI.IndexOf("### Ram Allocated") + 1]);
            ServerInfo.Variant = RSM_INI[RSM_INI.IndexOf("### Variant") + 1];
            ServerInfo.Version = RSM_INI[RSM_INI.IndexOf("### Version") + 1];
            ServerInfo.WorldSize = RSM_INI[RSM_INI.IndexOf("### World Size") + 1];
            ServerInfo.BackupFrequency = RSM_INI[RSM_INI.IndexOf("### Backup Frequency") + 1];
            ServerInfo.Lastbackup = RSM_INI[RSM_INI.IndexOf("### Last Backup") + 1];
            ServerInfo.Difficulty = RSM_INI[RSM_INI.IndexOf("### Difficulty") + 1];
        }

        public static void LaunchServer() //Handles actually launching the server and setting quick commands
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Bedrock":
                    ServerInfo.cmd.StandardInput.WriteLine("bedrock_server.exe");
                    ServerInfo.cmd.StandardInput.Flush();
                    break;

                case "Minecraft Java":
                    if (ServerInfo.Variant != "Modded")
                    {
                        ServerInfo.cmd.StandardInput.WriteLine(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Java\\bin\\java.exe -Xms" + ServerInfo.RAM + "M -jar Server.jar nogui");

                    }
                    else
                    {
                        List<String> Jarfiles = new();
                        Jarfiles.AddRange(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//", "*.jar", SearchOption.TopDirectoryOnly));
                        if (Jarfiles[0].Contains("forge")) { ServerInfo.cmd.StandardInput.WriteLine(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Java\\bin\\java.exe -Xms" + ServerInfo.RAM + "M -jar \"" + Jarfiles[0] + "\" --nogui --fmlserver"); }
                        else { ServerInfo.cmd.StandardInput.WriteLine(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Java\\bin\\java.exe -Xms" + ServerInfo.RAM + "M -jar " + Jarfiles[0] + " --nogui --fmlserver"); }

                    }
                    ServerInfo.cmd.StandardInput.Flush();
                    break;

                case "Factorio":
                    ServerInfo.cmd.StandardInput.WriteLine("cd bin");
                    ServerInfo.cmd.StandardInput.Flush();
                    ServerInfo.cmd.StandardInput.WriteLine("cd x64");
                    ServerInfo.cmd.StandardInput.Flush();
                    ServerInfo.cmd.StandardInput.WriteLine("factorio.exe --start-server \"" + AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.Name + "\\bin\\x64\\saves\\RSM.zip\"");
                    break;
            }
        }


        //Why the fuck isnt this in System.IO.Directory
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs) 
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, true);
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                string tempPath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
            }
        }

    }
}
