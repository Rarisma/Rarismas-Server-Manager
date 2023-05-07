using CommunityToolkit.Mvvm.DependencyInjection;
using RSM.Data;

namespace RSM.UI;

/// <summary>
/// New Server UI for Docker Servers.
/// </summary>
public sealed partial class ServerCreatorUI
{
    Global GlobalVM = Ioc.Default.GetService<Global>();
    public ServerCreatorUI()
    {
        this.InitializeComponent();
        XMLParser.UpdateRepositoryFiles();
    }
}