using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Match made in heaven, show me the light
namespace RSM.Server
{
    class Cloudspotter //Debloats server logs
    {
        public static string[] Blacklist = //Items on the blacklist are just removed
        {
            " : ",
            " [ ",
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
            " 1 ",
            " 2 ",
            " 3 ",
            " 4 ",
            " 5 ",
            " 6 ",
            " 7 ",
            " 8 ",
            " 9 ",  
            " 10 ",
            " 11 ",
            " 12 ",
            " 13 ",
            " 14 ",
            " 15 ",
            " 16 ",
            " 17 ",
            " 18 ",
            " 19 ",
            " 20 ",
            " 10 ",
            "[minecraft/RecipeManager]",
            "[ne.mi.co.ForgeMod/FORGEMOD]",
            "[mojang/YggdrasilAuthenticationService]",
        };
        public static List<string[]> Replace = new List<string[]>
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

        public static string Filter(string datatofilter)
        {
            //Removes any string that appears in the blacklist (Runs twice as a just in case)
            foreach (string Filter in Cloudspotter.Blacklist) { datatofilter = datatofilter.Replace(Filter, ""); }
            foreach (string Filter in Cloudspotter.Blacklist) { datatofilter = datatofilter.Replace(Filter, ""); }

            //Replaces stuff with better stuff
            foreach (string[] Filter in Cloudspotter.Replace) { datatofilter = datatofilter.Replace(Filter[0], Filter[1]); }
            foreach (string[] Filter in Cloudspotter.Replace) { datatofilter = datatofilter.Replace(Filter[0], Filter[1]); }

            return datatofilter;
        }
    }
}
