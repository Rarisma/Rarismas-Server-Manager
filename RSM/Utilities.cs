using Mono.Nat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Counter Strike Globally Offended
namespace RSM
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
            ServerInfo.RAM = Convert.ToInt64(RSM_INI[RSM_INI.IndexOf("### Ram Allocated") + 1]);
            ServerInfo.Variant = RSM_INI[RSM_INI.IndexOf("### Variant") + 1];
            ServerInfo.Version = RSM_INI[RSM_INI.IndexOf("### Version") + 1];
            ServerInfo.WorldSize = RSM_INI[RSM_INI.IndexOf("### World Size") + 1];
            ServerInfo.BackupFrequency = RSM_INI[RSM_INI.IndexOf("### Backup Frequency") + 1];
            ServerInfo.Lastbackup = RSM_INI[RSM_INI.IndexOf("### Last Backup") + 1];
            ServerInfo.Difficulty = RSM_INI[RSM_INI.IndexOf("### Difficulty") + 1];
            ServerInfo.Dir = AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//";
        }

        public static void LaunchServer() //Handles actually launching the server and setting quick commands
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Bedrock":
                    Global.Server.StandardInput.WriteLine("bedrock_server.exe");
                    Global.Server.StandardInput.Flush();
                    break;

                case "Minecraft Java":
                    if (ServerInfo.Variant != "Modded")
                    {
                        Global.Server.StandardInput.WriteLine(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Java\\bin\\java.exe -Xms" + ServerInfo.RAM + "M -jar Server.jar nogui");
                    }
                    else
                    {
                        List<String> Jarfiles = new();
                        Jarfiles.AddRange(Directory.GetFiles(ServerInfo.Dir + "//", "*.jar", SearchOption.TopDirectoryOnly));
                        if (Jarfiles[0].Contains("forge")) { Global.Server.StandardInput.WriteLine(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Java\\bin\\java.exe -Xms" + ServerInfo.RAM + "M -jar \"" + Jarfiles[0] + "\" --nogui --fmlserver"); }
                        else { Global.Server.StandardInput.WriteLine(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Java\\bin\\java.exe -Xms" + ServerInfo.RAM + "M -jar " + Jarfiles[0] + " --nogui --fmlserver"); }

                    }
                    Global.Server.StandardInput.Flush();
                    break;

                case "Factorio":
                    Global.Server.StandardInput.WriteLine("cd bin");
                    Global.Server.StandardInput.Flush();
                    Global.Server.StandardInput.WriteLine("cd x64");
                    Global.Server.StandardInput.Flush();
                    Global.Server.StandardInput.WriteLine("factorio.exe --start-server \"" + ServerInfo.Dir + "\\bin\\x64\\saves\\RSM.zip\"");
                    break;
            }
        }

        public static void StopServer()
        {
            //SaveServer();
            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    Global.Server.StandardInput.WriteLine("/quit");
                    break;
                case "Minecraft Bedrock":
                    Global.Server.StandardInput.WriteLine("stop");
                    break;
                case "Terraria":
                    File.WriteAllText(ServerInfo.Dir + "RSM//RSM.txt", "/stop");
                    break;
            }
            Global.Server.StandardInput.Flush();
            NatUtility.DeviceFound += DeviceFound;
            NatUtility.StartDiscovery();
        }

        private static void DeviceFound(object? sender, DeviceEventArgs e)
        {
            INatDevice device = e.Device;
            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    device.DeletePortMap(new Mapping(Protocol.Tcp, 25565, 25565));
                    device.DeletePortMap(new Mapping(Protocol.Udp, 25565, 25565));
                    break;
                case "Minecraft Bedrock": //Creating all the ports as a just in case thing
                    device.DeletePortMap(new Mapping(Protocol.Udp, 19132, 19132));
                    device.DeletePortMap(new Mapping(Protocol.Udp, 19133, 19133));
                    device.DeletePortMap(new Mapping(Protocol.Tcp, 19132, 19132));
                    device.DeletePortMap(new Mapping(Protocol.Tcp, 19133, 19133));
                    break;
                case "Terraria":
                    device.DeletePortMap(new Mapping(Protocol.Tcp, 7777, 7777));
                    break;
                case "Factorio":
                    device.DeletePortMap(new Mapping(Protocol.Udp, 34197, 34197));
                    break;
            }
        }
    }
}
