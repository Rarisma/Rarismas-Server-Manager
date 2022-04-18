using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Mono.Nat;
using WinUIEx;

namespace RSM;
public static class Data
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
    public static string ToolChains = Path.Combine(RootFolder, "ToolChains");
    /// <summary>
    /// This is where backups are stored
    /// </summary>
    public static string Backups = Path.Combine(RootFolder, "Backups");

    /// <summary>
    /// List of port RSMLib is using 
    /// </summary>
    public static List<int> OpenPorts = new();

    public static long MachineTotalRAM;

    public static List<Servers.Server> InstalledServers = new();
    public static string PublicIP { get => Router.GetExternalIP().ToString(); }
    public static INatDevice Router;
    public static WindowEx MainWindow = new();
    public static Frame Content = new() { VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Stretch, VerticalContentAlignment = VerticalAlignment.Stretch };
    public static string ShellComment = "Welcome to RSM";
    /// <summary>
    /// This creates all the paths if they don't exist
    /// </summary>
    public static void PathCheck()
    {
        try
        {
            Directory.CreateDirectory(RootFolder);
            Directory.CreateDirectory(Instances);
            Directory.CreateDirectory(Repositories);
            Directory.CreateDirectory(ToolChains);
            Directory.CreateDirectory(Backups);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
}