using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSM.Data
{
    public static class Directories
    {
        /// <summary>
        ///  Where RSM should store all the files.
        /// </summary>
        public static string RootFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RSM");

        /// <summary>
        /// Where servers are stored.
        /// </summary>
        public static string Instances = Path.Combine(RootFolder, "Instances");

        /// <summary>
        /// This is where the repositories are stored.
        /// </summary>
        public static string Repositories = Path.Combine(RootFolder, "Repos");

        /// <summary>
        /// This is where stuff such as JDKs and SteamCMD are stored
        /// </summary>
        public static string Dependencies = Path.Combine(RootFolder, "ToolChains");

        /// <summary>
        /// This is where backups are stored
        /// </summary>
        public static string Backups = Path.Combine(RootFolder, "Backups");


        /// <summary>
        /// Creates all paths.
        /// </summary>
        public static void PathCheck()
        {
            try
            {
                Directory.CreateDirectory(RootFolder);
                Directory.CreateDirectory(Instances);
                Directory.CreateDirectory(Repositories);
                Directory.CreateDirectory(Dependencies);
                Directory.CreateDirectory(Backups);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
