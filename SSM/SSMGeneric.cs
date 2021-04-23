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
                "SSM Config File V2\n\n" +
                "### Game Name\n" +
                 ServerInfo.ServerGame + "\n\n" +
                "### Server label\n" +
                ServerInfo.ServerLabel + "\n\n" +
                "### Ram allocated\n" +
                ServerInfo.RAM + "\n\n" +
                "### Server variant\n" +
                ServerInfo.ServerVariant + "\n\n" +
                "### Server version\n" +
                ServerInfo.ServerVersion + "\n\n");
        }
    }
}
