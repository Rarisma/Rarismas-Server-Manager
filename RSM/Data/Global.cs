using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Mono.Nat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSM.Models;
using WinUIEx;

namespace RSM.Data
{
    public static class Global
    {
        public static List<int> OpenPorts = new();

        public static long MachineTotalRAM;

        public static Dictionary<Guid, Server> InstalledServers = new();
        public static Dictionary<Guid, Dependency> AvailableDependencies = new();
        public static List<ServerGroup> AvailableServers = new();
        public static string PublicIP { get => Router.GetExternalIP().ToString(); }
        public static INatDevice Router;
        public static WindowEx MainWindow = new();
        public static Frame Content = new() { VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Stretch, VerticalContentAlignment = VerticalAlignment.Stretch };
        public static string ShellComment = "Welcome to RSM";
    }
}
