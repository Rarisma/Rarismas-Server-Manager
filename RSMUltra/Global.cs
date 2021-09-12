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
        public static string Tools = AppDomain.CurrentDomain.BaseDirectory + "//Tools//";
        public static string ServerDir = "";

        public static Microsoft.UI.Xaml.Controls.Frame GlobalFrame;
        public static Microsoft.UI.Xaml.Controls.NavigationView TopBar;

        public static string Java8 = Tools + "Java8//jre1.8.0_202//bin//java.exe";  //Direct path to Java 8
        public static string Java16 = Tools + "Java16//jdk-16.0.1+9-jre//bin//java.exe";    //Direct path to Java 16
        
    }
}
