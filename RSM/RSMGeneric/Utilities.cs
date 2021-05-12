using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSM.RSMGeneric
{
    class Utilities
    {
        public static void Make_INI_File()
        {
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//RSM.ini",
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
                ServerInfo.WorldSize);
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

        public static void OpenFolder(string path) { Process.Start("explorer.exe", "/select" + path); } //This is going in LibRarisma
        public static void OpenLink(string link) //Also going in LibRarisma
        {
            var LinkOpener = new ProcessStartInfo(link) { UseShellExecute = true, Verb = "open" };
            Process.Start(LinkOpener);
        }

    }
}
