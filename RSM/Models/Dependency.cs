using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSM.Models
{
    public class Dependency
    {
        public string Name;
        public string DownloadURL;
        public string EntryPoint;
        public Guid Guid;

        public Dependency(string name, string downloadUrl, string entryPoint, Guid guid)
        {
            Name = name;
            DownloadURL = downloadUrl;
            EntryPoint = entryPoint;
            Guid = guid;
        }

        //This writes all properties of Dependency to a file
        public void WriteConfig(string filePath)
        {
            using System.IO.StreamWriter file = new System.IO.StreamWriter(filePath);
            file.WriteLine(Name);
            file.WriteLine(DownloadURL);
            file.WriteLine(EntryPoint);
            file.WriteLine(Guid.ToString());
        }

        //this reads and loads all properties of Dependency to a file
        public Dependency ReadConfig(string filePath)
        {
            using System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            string name = file.ReadLine();
            string downloadURL = file.ReadLine();
            string entryPoint = file.ReadLine();
            string guid = file.ReadLine();
            return new Dependency(name, downloadURL, entryPoint, Guid.Parse(guid));
        }
    }
}
