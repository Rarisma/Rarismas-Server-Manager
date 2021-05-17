using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RSM.RSMGeneric
{
    class Utilities
    {
        public static void Make_INI_File()
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//RSM.ini",
                "RSM Config File V3\n\n" +
                "### Game Name\n" +
                 ServerInfo.Game + "\n\n" +
                "### Server label\n" +
                ServerInfo.Label + "\n\n" +
                "### Ram allocated\n" +
                ServerInfo.RAM + "\n\n" +
                "### Server variant\n" +
                ServerInfo.Variant + "\n\n" +
                "### Server version\n" +
                ServerInfo.Version + "\n\n" +
                "### Server size\n" +
                ServerInfo.WorldSize + "\n\n" +
                "### Backup Frequency\n" +
                ServerInfo.BackupFrequency + "\n\n" +
                "### Last Backup\n" +
                ServerInfo.Lastbackup );
        }

        public static void Read_INI_File(Object ServerName)
        {
            List<string> SSM_INI = new();
            SSM_INI.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerName + "//RSM.ini"));

            ServerInfo.Game = SSM_INI[SSM_INI.IndexOf("### Game Name") + 1];
            ServerInfo.Label = SSM_INI[SSM_INI.IndexOf("### Server label") + 1];
            ServerInfo.RAM = Convert.ToInt64(SSM_INI[SSM_INI.IndexOf("### Ram allocated") + 1]);
            ServerInfo.Variant = SSM_INI[SSM_INI.IndexOf("### Server variant") + 1];
            ServerInfo.Version = SSM_INI[SSM_INI.IndexOf("### Server version") + 1];
            ServerInfo.WorldSize = SSM_INI[SSM_INI.IndexOf("### Server size") + 1];
            ServerInfo.BackupFrequency = SSM_INI[SSM_INI.IndexOf("### Backup Frequency") + 1];
            ServerInfo.Lastbackup = SSM_INI[SSM_INI.IndexOf("### Last Backup") + 1];
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
                    ServerInfo.cmd.StandardInput.WriteLine("java -Xms" + ServerInfo.RAM + "M -jar Server.jar nogui");
                    ServerInfo.cmd.StandardInput.Flush();
                    break;
            }
        }

        public static void OpenFolder(string path)
        {
            Clipboard.SetText(path);
            Process.Start(new ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = true,
                Verb = "open"
            });
        } //This is going in LibRarisma
        public static void OpenLink(string link) //Also going in LibRarisma
        {
            var LinkOpener = new ProcessStartInfo(link) { UseShellExecute = true, Verb = "open" };
            Process.Start(LinkOpener);
        }


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
