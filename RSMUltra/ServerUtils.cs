using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Nat;
using RSMUltra.Manager;
using ThreadStaticAttribute = System.ThreadStaticAttribute;

namespace RSMUltra
{
    class ServerUtils
    {
        public static void StopServer()
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java Edition":
                    ServerInfo.Server.StandardInput.WriteLine("stop");
                    break;
            }

            System.Threading.Thread.Sleep(20000);

            int a = 0;
            while (!ServerInfo.Server.HasExited)
            {
                a++;
                System.Threading.Thread.Sleep(250);

                if (a == 160) //Force kills server after 40 seconds
                {
                    ServerInfo.Server.Kill();
                    break;
                }
            }

            NatUtility.DeviceFound += UnForward;
            NatUtility.StartDiscovery();
        }

        //Creates port mappings
        public static void Forward(object sender, DeviceEventArgs args)
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    args.Device.CreatePortMap(new Mapping(Protocol.Tcp, 25565, 25565));
                    args.Device.CreatePortMap(new Mapping(Protocol.Udp, 25565, 25565));
                    break;
                case "Minecraft Bedrock": //Creating all the ports as a just in case thing
                    args.Device.CreatePortMap(new Mapping(Protocol.Udp, 19132, 19132));
                    args.Device.CreatePortMap(new Mapping(Protocol.Udp, 19133, 19133));
                    args.Device.CreatePortMap(new Mapping(Protocol.Tcp, 19132, 19132));
                    args.Device.CreatePortMap(new Mapping(Protocol.Tcp, 19133, 19133));
                    break;
                case "Terraria":
                    args.Device.CreatePortMap(new Mapping(Protocol.Tcp, 7777, 7777));
                    break;
                case "Mindustry":
                    args.Device.CreatePortMap(new Mapping(Protocol.Tcp, 6567, 6567));
                    args.Device.CreatePortMap(new Mapping(Protocol.Udp, 6567, 6567));
                    break;
                case "Factorio":
                    args.Device.CreatePortMap(new Mapping(Protocol.Udp, 34197, 34197));
                    break;
            }
        }

        //Deletes port mappings
        public static void UnForward(object sender, DeviceEventArgs args)
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    args.Device.DeletePortMap(new Mapping(Protocol.Tcp, 25565, 25565));
                    args.Device.DeletePortMap(new Mapping(Protocol.Udp, 25565, 25565));
                    break;
                case "Minecraft Bedrock": //Creating all the ports as a just in case thing
                    args.Device.DeletePortMap(new Mapping(Protocol.Udp, 19132, 19132));
                    args.Device.DeletePortMap(new Mapping(Protocol.Udp, 19133, 19133));
                    args.Device.DeletePortMap(new Mapping(Protocol.Tcp, 19132, 19132));
                    args.Device.DeletePortMap(new Mapping(Protocol.Tcp, 19133, 19133));
                    break;
                case "Terraria":
                    args.Device.DeletePortMap(new Mapping(Protocol.Tcp, 7777, 7777));
                    break;
                case "Factorio":
                    args.Device.DeletePortMap(new Mapping(Protocol.Udp, 34197, 34197));
                    break;
                case "Mindustry":
                    args.Device.DeletePortMap(new Mapping(Protocol.Udp, 6567, 6567));
                    args.Device.DeletePortMap(new Mapping(Protocol.Tcp, 6567, 6567));
                    break;
            }

        }

        public static string[] Blacklist = //Items on the blacklist are just removed, most of this is forge stuff since the logs are stupidly large for some reason
        {                                  //I am well aware that this will interfere with debugging efforts but A) Thats not my problem, B) Most servers save logs
            "[minecraft/MinecraftServer]",
            "[forge]",
            "[minecraft/DedicatedServer]",
            "[minecraft/NetworkSystem]",
            "[ne.mi.fm.lo.FixSSL/CORE]",
            "[ne.mi.fm.VersionChecker/]",
            "[modloading-worker-3/INFO]",
            "[minecraft/Commands]",
            "[minecraft/SimpleReloadableResourceManager]",
            "[minecraft/MinecraftServer]",
            "[minecraft/LoggingChunkStatusListener]",
            "[minecraft/ChunkManager]" ,
            "[Server thread/INFO]",
            "[minecraft/LoggingChunkStatusListener]",
            "[Worker-Main-",
            "/INFO]",
            "[main/WARN]",
            "[Forge Version Check/INFO]",
            "[main/INFO]",
            "ThreadedAnvilChunkStorage",
            "[minecraft/AdvancementList]",
            "[main",
            "[modloading-worker-",
            "Forge Version Check",
            "[mixin/]",
            "[cp.mo.mo.LaunchServiceHandler/MODLAUNCHER]",
            "[ne.mi.co.MinecraftForge/FORGE]",
            "[minecraft/RecipeManager]",
            "[ne.mi.co.ForgeMod/FORGEMOD]",
            "[mojang/YggdrasilAuthenticationService]",
        };

        public static List<string[]> Replace = new List<string[]> //Reduces needless fluff from console
        {
            new string[] {"]:","]"},
            new string[] { "Starting version check at https://files.minecraftforge.net/maven/net/minecraftforge/forge/promotions_slim.json", " Starting version check..."},
            new string[] { "starting: java version 1.8.0_202 by Oracle Corporation", "Starting java"},
            new string[] { "Launching target 'fmlserver' with arguments [--gameDir, ., nogui]", "Starting Forge server"},
            new string[] { "Preparing start region for dimension minecraft:overworld", "Loading overworld"},
            new string[] { "Loadedrecipes", "Loaded recipes"},
            new string[] { "Reloading ResourceManager:", "Reloading resource manager"},
            new string[] { "DIM1", "The End"},
            new string[] { "DIM-1", "The Nether"},
        };

        public static string Filter(string data) //Cleans up the server console to make it easier
        {
            foreach (var VARIABLE in Blacklist) //runs the blacklist 
            {
                data = data.Replace(VARIABLE, "");
            }

            foreach (var VARIABLE in Replace) //runs the replace list
            {
                data = data.Replace(VARIABLE[0], VARIABLE[1]);
            }

            return data;
        }

        public static string[] SetupCommands()
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Bedrock":
                    return new[] { "help", "ban", "clear", "deop", "difficulty", "kick", "op", "save", "say", "seed", "stop", "tp", "weather", "xp" };
                case "Minecraft Java Edition":
                    return new[] { "help", "ban", "clear", "deop", "difficulty", "kick", "op", "save", "say", "seed", "stop", "tp", "weather", "xp" };
                case "Factorio":
                    return new[] { "/clear", "/evolution", "/seed", "/time", "/ban", "/unban", "/demote", "/promote", "/kick", "/players", "/whitelist", "/help" };
                case "Mindustry":
                    return new[] { "help", "exit", "host", "maps", "status", "reloadmaps", "mods", "say", "pause on", "pause off", "rules", "playerlimit", "fillitems ", "shuffle all", "shuffle none", "shuffle builtin", "nextmap", "kick", "ban", "unban", "pardon", "admins", "admins", "Players", "runwave", "gameover", "info" };
                default:
                    return new[] {""};
            }
        }
    }
}
