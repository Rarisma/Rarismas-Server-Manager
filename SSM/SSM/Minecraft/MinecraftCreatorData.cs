using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSM.Minecraft
{
    public static class MinecraftCreatorData
    {
        public static string Edition; //Should be Java or Bedrock
        public static string ServerType = "NULL"; //Should be Paper, Bedrock, Java, Forge, Fabric
        public static int ServerSetupChange = 0; // 0 - Java or bedrock     1 - Server type
    }
}
