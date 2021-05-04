using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSM.Pages.SSM_GUI
{
    static class ServerUtils
    {
        public static void LaunchServer() //Handles actually launching the server and setting quick commands
        {
            switch (ServerInfo.ServerGame)
            {
                case "Minecraft Bedrock":
                    ServerInfo.cmd.StandardInput.WriteLine("bedrock_server.exe");
                    ServerInfo.cmd.StandardInput.Flush();
                    break;

                case "Minecraft Java":
                    ServerInfo.cmd.StandardInput.WriteLine("java -Xms" + ServerInfo.RAM + "M -jar Server.jar nogui");
                    ServerInfo.cmd.StandardInput.Flush();
                    break;

                case "Terraria":
                    //ServerInfo.cmd.StandardInput.WriteLine("TerrariaServer.exe -autocreate 3 -world C:\\Users\\Rarisma\\Documents\\My Games\\Terraria\\Worlds\\Test.wrld");
                    //ServerInfo.cmd.StandardInput.Flush();
                    break;
            }
        }

    }
}
