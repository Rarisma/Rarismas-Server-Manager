using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Useful and reusuable functions are stored here,
//Most functions will be pushed to LibRarisma unless it's SSM Spesific
namespace SSM
{
    class SSMGeneric
    {
        public static void Make_INI_File()
        {
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//SSM.ini",
                "SSM Config File V3\n\n" +
                "### Game Name\n" +
                 ServerInfo.ServerGame + "\n\n" +
                "### Server label\n" +
                ServerInfo.ServerLabel + "\n\n" +
                "### Ram allocated\n" +
                ServerInfo.RAM + "\n\n" +
                "### Server variant\n" +
                ServerInfo.ServerVariant + "\n\n" +
                "### Server version\n" +
                ServerInfo.ServerVersion + "\n\n" +
                "### Server size\n" +
                ServerInfo.ServerWorldSize);
        }

        public static void Read_INI_File(Object ServerName)
        {
            List<string> SSM_INI = new();
            SSM_INI.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerName + "//SSM.ini"));
            
            ServerInfo.ServerGame = SSM_INI[SSM_INI.IndexOf("### Game Name") + 1];
            ServerInfo.ServerLabel = SSM_INI[SSM_INI.IndexOf("### Server label") + 1];
            ServerInfo.RAM = Convert.ToInt64(SSM_INI[SSM_INI.IndexOf("### Ram allocated") + 1]);
            ServerInfo.ServerVariant = SSM_INI[SSM_INI.IndexOf("### Server variant") + 1];
            ServerInfo.ServerVersion = SSM_INI[SSM_INI.IndexOf("### Server version") + 1];
            ServerInfo.ServerWorldSize = SSM_INI[SSM_INI.IndexOf("### Server size") + 1];
        }
        
        public static void BuildServer()
        {
            LibRarisma.IO.DownloadFile(ServerInfo.ServerURL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//", "Server.jar");
            SSMGeneric.Make_INI_File();
            
            // This switch handles extras such as making aditional files if needed.
            switch (ServerInfo.ServerGame)
            {
                case "Minecraft Java":
                    System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//" + "eula.txt", "#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula).\n# made by SSM\neula = true"); //Makes the EULA accepted
                    if (ServerInfo.ServerVariant == "Forge")
                    {
                        Process cmd = new();
                        cmd.StartInfo.FileName = "cmd.exe";
                        cmd.StartInfo.RedirectStandardInput = true;
                        cmd.StartInfo.CreateNoWindow = false;
                        cmd.StartInfo.UseShellExecute = false;
                        //cmd.StartInfo.StandardInput.AutoFlush = true; //Readd this
                        cmd.Start();
                        cmd.StandardInput.WriteLine("cd Servers");
                        cmd.StandardInput.Flush();
                        cmd.StandardInput.WriteLine("cd " + ServerInfo.ServerLabel);
                        cmd.StandardInput.Flush();
                        cmd.StandardInput.WriteLine("java -jar Server.jar --installServer exit");
                        cmd.StandardInput.Flush();
                    }                    
                    break;
                case "Terraria":
                    ServerInfo.ServerVariant = "Normal";
                    LibRarisma.IO.DownloadFile(ServerInfo.ServerURL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//", "Terraria.zip",true);
                    LibRarisma.IO.DownloadFile("https://github.com/Rarisma/Simple-Server-Manager/blob/main/ServerFiles/Terraria/SSMHelper.dll?raw=true", AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//ServerPlugins//", "SSMHelper.dll");
                    SSMGeneric.Make_INI_File();
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//Terraria.zip");
                    break;
            }
            
            ModernWpf.MessageBox.Show("Finished downloading server files");
            //((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new Pages.SSM_GUI.Welcome();
        }
        
        
        public static void OpenFolder(string path) { Process.Start("explorer.exe", "/select" + path);  } //This is going in LibRarisma
        public static void OpenLink(string link) //Also going in LibRarisma
        {
            var LinkOpener = new ProcessStartInfo(link)
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(LinkOpener); 
        }

    }
}
