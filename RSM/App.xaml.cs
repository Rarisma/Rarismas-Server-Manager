using Microsoft.UI.Xaml;
using System;
using System.Diagnostics;
using Mono.Nat;
using RSM.Data;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using RSM.Models;

namespace RSM;
//MY MATRYOSHKA
//Oh my fucking lord I was clearly on top shelf boof when writing this shit so I
//supose it's time to wrap this skeleton in the closet once and for all.
//Introducing the grand fucking clean up because seeing this code makes me sad.
public partial class App
{
    public App() { InitializeComponent(); }

    private Global GlobalVM;
    
    /// <summary>
    /// Invoked when the application is launched normally by the end user.  Other entry points
    /// will be used such as when the application is launched to open a specific file.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        Ioc.Default.ConfigureServices(new ServiceCollection().AddSingleton<Global>().BuildServiceProvider());
        GlobalVM = Ioc.Default.GetService<Global>();

        GlobalVM.MainWindow = new();
        Directories.PathCheck();
        NatUtility.DeviceFound += NatUtilityOnDeviceFound;
        NatUtility.StartDiscovery();
        GlobalVM.MainWindow.Height = 920;
        GlobalVM.MainWindow.Width = 1500;
        GlobalVM.MainWindow.MinHeight = 920;
        GlobalVM.MainWindow.MinWidth = 1500;
        GlobalVM.MainWindow.Title = "Rarisma Server Manager 4.0 Experimental";
        GlobalVM.MainWindow.Content = new UI.Main();
        GlobalVM.MainWindow.Activate();
        GlobalVM.MainWindow.BringToFront();
        GetTotalRAM();

        if (Directory.GetDirectories(Directories.Instances).Length == 0) { return; }
        foreach (var directory in Directory.GetDirectories(Directories.Instances))
        {
            if (!File.Exists(Path.Combine(directory, "RSM.CONF"))) { continue; }
            _ = new Server(Path.Combine(directory, "RSM.CONF"));
        }
    }

    private void NatUtilityOnDeviceFound(object? sender, DeviceEventArgs e) { GlobalVM.Router = e.Device; }

    private void GetTotalRAM()
    {
        //TODO: update to use librarisma.
        Process info = new();
        info.StartInfo.RedirectStandardOutput = true;
        info.StartInfo.CreateNoWindow = true;
        info.StartInfo.FileName = "wmic";
        info.StartInfo.Arguments = "ComputerSystem get TotalPhysicalMemory";
        info.Start();
        info.WaitForExit();
        GlobalVM.MachineTotalRAM = Int64.Parse(info.StandardOutput.ReadToEnd().Trim().Split("\n")[1]) / 1024 / 1024;
    }
}