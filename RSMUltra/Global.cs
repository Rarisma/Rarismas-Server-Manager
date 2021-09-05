 using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments;
using Windows.Foundation.Metadata;

namespace RSMUltra
{
    class Global
    {
        public const string DefaultRepository = "https://github.com/Rarisma/Rarismas-Server-Manager/blob/RSM3/ServerFiles/ServerFiles.zip?raw=true"; //This is the default repo used by RSM.

        
        public static string Sources = AppDomain.CurrentDomain.BaseDirectory + "//Sources//";
        public static string Instances = AppDomain.CurrentDomain.BaseDirectory + "//Instances//";
        public static string ServerDir = "";

        //Server Info

    }
}
