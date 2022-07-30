using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RSM.Models
{
    public class ServerGroup : Server
    {
        public string Type;
        public List<Variant> AvailableTypes = new();
        public string PostInstallCommand;
        public string LaunchCommand;
        public Guid RequiredDependency;
        public int DefaultRAM = -1;
        public string EULA;
        public string DownloadExtension;
        public bool IsDownloadZipped = false;
        
    }
}
