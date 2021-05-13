using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSM.RSMGeneric
{
    class ServerBuilder
    {
        public static void MinecraftJava()
        {
            LibRarisma.IO.DownloadFile(ServerInfo.URL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//", "Server.jar");
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//" + "eula.txt", "#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula).\n# made by SSM\neula = true"); //Makes the EULA accepted
            if (ServerInfo.Variant == "Forge")
            {
                Process cmd = new();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.CreateNoWindow = false;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();
                cmd.StandardInput.AutoFlush = true;
                cmd.StandardInput.WriteLine("cd Servers");
                cmd.StandardInput.WriteLine("cd " + ServerInfo.Label);
                cmd.StandardInput.WriteLine("java -jar Server.jar --installServer");
                cmd.WaitForExit();
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//Server.jar");
                List<String> Jarfiles = new(); 
                Jarfiles.AddRange(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//", "*.jar", SearchOption.TopDirectoryOnly));
                if (Jarfiles[0].Contains("forge")) { File.Move(Jarfiles[0], AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//" + "Server.jar"); } else { File.Move(Jarfiles[1], AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//" + "Server.jar"); }
            }
            Finish();
        }

        public static void MinecraftBedrock()
        {
            ServerInfo.Variant = "Bedrock";
            ServerInfo.RAM = 0;

            //Gets latest links to server
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/bedrock", AppDomain.CurrentDomain.BaseDirectory + "//Cache//", "Bedrock");
            string[] ServerFile = System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Bedrock");
            ServerInfo.Version = ServerFile[0];
            ServerInfo.URL = ServerFile[1];
            LibRarisma.IO.DownloadFile(ServerFile[1], AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label, "//Server.zip", true);
            Finish();
        }

        public static void Terraria()
        {
            ServerInfo.Variant = "Normal";
            LibRarisma.IO.DownloadFile(ServerInfo.URL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//", "Terraria.zip", true);
            LibRarisma.IO.DownloadFile("https://github.com/Rarisma/Simple-Server-Manager/blob/main/ServerFiles/Terraria/SSMHelper.dll?raw=true", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//ServerPlugins//", "SSMHelper.dll");
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Label + "//Terraria.zip");
            Finish();
        }

        private static void Finish()
        {
            Utilities.Make_INI_File();
            ModernWpf.MessageBox.Show("Finished downloading server files");
            ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new UI.LaunchPage();
        }

    }
}
