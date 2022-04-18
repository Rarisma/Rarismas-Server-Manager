using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using ABI.Windows.ApplicationModel.Appointments.DataProvider;
using Mono.Nat;

namespace RSM;

public partial class App : Application
{
    public App() { InitializeComponent(); }

    /// <summary>
    /// Invoked when the application is launched normally by the end user.  Other entry points
    /// will be used such as when the application is launched to open a specific file.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        Data.MainWindow = new();
        Data.PathCheck();
        NatUtility.DeviceFound += NatUtilityOnDeviceFound;
        NatUtility.StartDiscovery();
        Data.MainWindow.Height = 920;
        Data.MainWindow.Width = 1500;
        Data.MainWindow.MinHeight = 920;
        Data.MainWindow.MinWidth = 1500;
        Data.MainWindow.Title = "RSM 4.0 Closed Beta";
        Data.MainWindow.Content = new Main();
        Data.MainWindow.Activate();
        Data.MainWindow.BringToFront();
        GetTotalRAM();
    }

    private void NatUtilityOnDeviceFound(object? sender, DeviceEventArgs e)
    {
        Data.Router = e.Device;
    }

    private void GetTotalRAM()
    {
        //todo update to use librarisma.
        Process info = new();
        info.StartInfo.RedirectStandardOutput = true;
        info.StartInfo.CreateNoWindow = true;
        info.StartInfo.FileName = "wmic";
        info.StartInfo.Arguments = "ComputerSystem get TotalPhysicalMemory";
        info.Start();
        info.WaitForExit();
        Data.MachineTotalRAM = Int64.Parse(info.StandardOutput.ReadToEnd().Trim().Split("\n")[1]) / 1024 / 1024;
    }
}