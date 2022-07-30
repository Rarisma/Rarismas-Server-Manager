using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RSM.Models
{
    public class Server
    {
        public string ServerName;
        public string Branding;
        public string LaunchCommand;
        public int AllocatedRAM;

        public string ParentDirectory;
        public Guid Guid;
        public Guid Dependency;
        public int DefaultPort;

        public Server(string serverName, string branding, string launchCommand, int allocatedRam, Guid guid, string parentDirectory, Guid dependency)
        {
            ServerName = branding;
            Branding = branding;
            LaunchCommand = branding;
            ParentDirectory = parentDirectory;
            AllocatedRAM = allocatedRam;
            Guid = guid;
            Dependency = dependency;
        }

        protected Server() { }

        public void SaveConfiguration()
        {
            string Message = "RSM Config File V4";
            Message += $"\nServerName={ServerName}";
            Message += $"\nLaunchCommand={LaunchCommand}";
            Message += $"\nAllocatedRAM={AllocatedRAM}";
            Message += $"\nGuid={Guid}";
            Message += $"\nDependency={Dependency}";
            System.IO.File.WriteAllText(System.IO.Path.Combine(ParentDirectory,"RSM.conf"), Message);
        }

        public void ReadConfiguration(string path)
        {
            foreach (var line in File.ReadAllLines(path))
            {
                string[] parts = line.Split(new[] { '=' });
                switch (parts[0])
                {
                    case "ServerName": ServerName = parts[1]; break;
                    case "LaunchCommand": LaunchCommand = parts[1]; break;
                    case "AllocatedRAM": AllocatedRAM = int.Parse(parts[1]); break;
                    case "Guid": Guid = Guid.Parse(parts[1]); break;
                    case "Dependency": Dependency = Guid.Parse(parts[1]); break;
                }
            }
            ParentDirectory = Path.GetDirectoryName(path);
        }


    }
}
