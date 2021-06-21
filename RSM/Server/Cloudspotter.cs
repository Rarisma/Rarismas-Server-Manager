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


        public static string[] EaserEgg = new string[]
        {
            "Cloudspotter", "As seen on GitHub!", "GBA Enhanced", "PC Exclusive","Now with less RAM usage", "Included with your gamepass subscription",
            "RTX Enhanced", "I honnestly tried Xbox Support", "Built with AvaloniaUI", "Hanamaru Kunikida", "Ruby Kurosawa", "kotori minami",
            "Lucas Newdawn, Remember?", "YAG Rougealike", "Where are my stock options", "Incredibly Based Hotel and Casino", "Hey all, Jake here!",
            "Metroid Dread looks like an upscaled 3ds game", "FES - The Answer", "FES - The Journey", "Eternal", "C#", "RAM Required", "Remastered", "Upscaled",
            "Certified hood classic", "Join the most based discord server ever at https://discord.gg/GzGUefWCSP", "Tonight on unsolved mysteries who made the ruby chest?",
            "Logan Bones", "1080i", "My shirt was signed by Matt Greene", "Rarisma", "Rarismoe"," motherfuckers act like they forgot about SSM", "Shout out to the pirate bay",
            "Now with 100% less polys",  "Read /v/", "Oh my fucking god its BigBlaker", "Easier to use than {Insert hosting  company here}", "Software is like sex, its better when its free!",
            "wtf is SSM", "Check out my discord bot, when I get relasing it", "Fuck dirtcraft and your shitty DW20 server", "Removed herobrine", "Nerfed artillery spam",
            "Shout out to JB1999", "1911", "Virgin Nodecraft user vs Chad RSM user", "Scaled and Icy", "Stop simping over LL women who have no plot", "I love the new mario party 1 remaster",
            "Use Z X , .   Based af keybinds", "I can beat week 4 but not 2", "Mega yikes to animdude", "Bad Apple Terminal Support" , "You have like a 1/60 chance of seeing this", "ASGORE is better than Megalovania"
            , "UGH", "Stress", "BASED", "Guns", "Managuns", "Testing in production", "Girlfriend is the final boss ez", "GOTY edition", "Stonks", "Displeasement", " 'PRETTY DOPE ASS GAME' PLAYSTATION MAGAZINE MAY 2003 ISSUE ",
            "James2 Based", "The Pretender", "The end code", "The codezone", "Heir to the code throne", "Your code won't last", "Sponsor block","endcard/non music section", "End Layer", "I'm feeling lucky"
        };


    }
}
