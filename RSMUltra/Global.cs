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

        public static string Cache = Windows.Storage.ApplicationData.Current.LocalCacheFolder.Path + "//Cache//";
        public static string Sources = Windows.Storage.ApplicationData.Current.LocalCacheFolder.Path + "//Sources//";
        public static string Instances = Windows.Storage.ApplicationData.Current.LocalCacheFolder.Path + "//Instances//";
        public static string Tools = Windows.Storage.ApplicationData.Current.LocalCacheFolder.Path + "//Tools//";
        public static string Backups = Windows.Storage.ApplicationData.Current.LocalCacheFolder.Path + "//Backups//";
        public static string ServerDir = "";

        public static Microsoft.UI.Xaml.Controls.Frame GlobalFrame;
        public static List<Microsoft.UI.Xaml.Controls.NavigationViewItem> SidebarBarRegistry = new(); //used to disable the sidebar
        public static string GlobalTitle = "";

        public static string Java8 = Tools + "Java8//jre1.8.0_202//bin//java.exe";  //Direct path to Java 8
        public static string Java16 = Tools + "Java16//jdk-16.0.1+9-jre//bin//java.exe";    //Direct path to Java 16
        
    }
}
