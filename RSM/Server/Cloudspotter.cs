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
            "RTX Enhanced", "I honnestly tried Xbox Support", "Built with AvaloniaUI", "Hanamaru Kunikida", "Ruby Kurosawa", "Kotori minami",
            "Lucas Newdawn, Remember?", "YAG Rougealike", "Where are my stock options", "Incredibly Based Hotel and Casino", "Hey all, Jake here!",
            "Metroid Dread looks like an upscaled 3ds game", "FES - The Answer", "FES - The Journey", "Eternal", "C#", "RAM Required", "Remastered", "Upscaled",
            "Certified hood classic", "Join the most based discord server ever at https://discord.gg/GzGUefWCSP", "Tonight on unsolved mysteries who made the ruby chest?",
            "Logan Bones", "1080i", "My shirt was signed by Matt Greene", "Rarisma", "Rarismoe"," motherfuckers act like they forgot about SSM", "Shout out to the pirate bay",
            "Now with 100% less polys",  "Read /v/", "Oh my fucking god its BigBlaker", "Easier to use than {Insert hosting  company here}", "Software is like sex, its better when its free!",
            "wtf is SSM", "Check out my discord bot, when I get relasing it", "Fuck dirtcraft and your shitty DW20 server", "Removed herobrine", "Nerfed artillery spam",
            "Shout out to JB1999", "1911", "Virgin Nodecraft user vs Chad RSM user", "Scaled and Icy", "Stop simping over LL women who have no plot", "I love the new mario party 1 remaster",
            "Use Z X , .   Based af keybinds", "I can beat week 4 but not 2", "Mega yikes to animdude", "Bad Apple Terminal Support" , "You have like a 1/60 chance of seeing this", "ASGORE is better than Megalovania"
            , "UGH", "Stress", "BASED", "Guns", "Managuns", "Testing in production", "Girlfriend is the final boss ez", "GOTY edition", "Stonks", "Displeasement", " 'PRETTY DOPE ASS GAME' PLAYSTATION MAGAZINE MAY 2003 ISSUE ",
            "James2 Based", "The Pretender", "The end code", "The codezone", "Heir to the code throne", "Your code won't last", "Sponsor block","endcard/non music section", "End Layer", "I'm feeling lucky",
            "RARISMARARISMARARISMARARISMARARISMARARISMARARISMA", "2.4 will exist soon.", "Taking my code and going home", "AAAAAAAAAAA on Unkown",
            "2.5 adds support for pokemon essentials", "Now supports WOW", "ba dum BOOM - Windows11 startup", "Will not work past  2038", "Will not work before 2000", "Hanamaru approved",
            "{INSERT IDOL NAME HERE} approves", "SQL not included", "Input sanitised", "Malware not included", "May contain sharp edges", "Turn it upto 11", "Shout out to my homies at EvolutionX",
            "DOES NOT SUPPORT ANDROID", "NTFS supported", "Silver macarons two star", "Kotori with a capital K", "Google drive off a cliff", "Dropbox dropshipping", "Theres a snake in my ass", "Now featuring dante from the devil my cry series",
             "Now featuring Rarisma from the Discord may cry series", "Pokemon snap back to reality", "\n\n\n", "Voltage +", "I don't play sifas but all my friends do help", "Removed herobrine",
            "Wii supported", "What does Fallout 1 and Persona 1 have in common?", "Fuck all with later entries", "The codefarthers secret stash", "Seth needs fes maru *dab*", "fes kotori where, not in seths account",
            "Mo1stkritikal", "Dhar man", "It was a mistake to code here", "All consuming lord of code", "code messiah", "Code cruzer", "Man shut the fuck up nerd", "ur gfs pussy tastes like my code",
            "Congratulations future code lords, I shape fates and -rm -rf -\\ dudes", "Hands off the code stash bubs", "Just chillin, code villian",
            "Fucked up? Now your code", "Stop the code ocean", "Your code sucks dick", "A splash of code to seal the deal", "Dick, heir of the code throne", "Give me the code scar",
            "Man shut fuck up nerd", "I actually beat a nerd to death", "Get paid money to worship satan", "Makin this much code aint easy", "Speedy code dealer", "Code all ye faithful",
            "Make bank, smoke dank", "Farther of lies code in disguise", "Sacred Code blade", "Fatal code theft", "Stealing code from the code czars trust","Margret Thatcher the code snatcher", "all your code belong to us",
            "How many holes does your code have?", "Mommys code tax", "Code spy", "Code treasurer", "Manager of code", "Code slave back from the grave", "I tripped in the code keepers crypt",
            "The dark souls of code", "Code drowning awareness day", "The tragic code sponge", "your code is fading", "The price of breaking a code oath", "The code farthers secret stash",
            "Give me code or give me death", "Consume the code chalice, fuck everyone named alice", "Farther drowned going down on the code clown", "Code scented candle", "James Hector - Code colecter",
            "The code collector", "Code framed and code blamed", "Your mom","As seen on RDS", "Rarisma#3767", "Fading light", "Wasting light", "Patience Echoes Silence and Grace", "177013",
            "Hot sex with Kanan Matsuura","The code collection", "Learn to Fly", "28 github commits later", "Nagataro", "Seth is based", "I browse /v/", "r/Hentaimemes", "RSM hits you like a car hits Kasumi"
        };


    }
}
