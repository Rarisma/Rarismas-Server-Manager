using System;
using System.Collections.Generic;
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
        
        public static void OpenFolder(string path)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() {
            FileName = path,
            UseShellExecute = true,
            Verb = "open"
            });
        }
            
        }
        

    }
}
