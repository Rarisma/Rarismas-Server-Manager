using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments;
using Windows.Foundation.Metadata;
 using ABI.Microsoft.UI.Xaml.Controls;

 namespace RSMUltra
{
    class Global
    {
        public const string DefaultRepository = "https://github.com/Rarisma/Rarismas-Server-Manager/blob/RSM3/ServerFiles/ServerFiles.zip?raw=true"; //This is the default repo used by RSM.

        public static string Cache = AppDomain.CurrentDomain.BaseDirectory + "//Cache//";
        public static string Sources = AppDomain.CurrentDomain.BaseDirectory + "//Sources//";
        public static string Instances = AppDomain.CurrentDomain.BaseDirectory + "//Instances//";
        public static string ServerDir = "";

        public static Microsoft.UI.Xaml.Controls.Frame GlobalFrame;

        public static string Java8 = "";  //placeholder
        public static string Java16 = ""; //placeholder

    }
}
