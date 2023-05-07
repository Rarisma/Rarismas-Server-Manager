using System;
using System.Collections.ObjectModel;

namespace RSM.Models;

public class ServerGroup : Server
{
    public string Type { get; set; }
    public ObservableCollection<Variant> AvailableVariants { get; set; }
    public string PostInstallCommand;
    public string LaunchCommand;
    public Guid RequiredDependency;
    public int DefaultRAM = -1;
    public string EULA;
    public string DownloadExtension;
    public bool IsDownloadZipped = false;
        
}