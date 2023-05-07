using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using RSM.Data;

namespace RSM.Models;

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

    private DispatcherTimer Timer = new() { Interval = new TimeSpan(0, 0, 0, 0, 500) };
    private Process ServerProcess;
    private string PendingOutput = String.Empty;
    private string ServerOutput = String.Empty;
    public bool IsOff { get; set; }

    public string PowerStatus { get; set; }

    public string CPUUsage
    {
        get
        {
            if (true)
            {
                return "x% usage";
            }
            else { return "0% usage"; }
        }
    }

    public string RAMUsage
    {
        get
        {
            if (AllocatedRAM == -1)
            {
                return "xmb usage";
            }
            else { return $"x of {AllocatedRAM} usage"; }
        }
    }

    public string DiskUsage { get; set; }


    public Server(string serverName, string branding, string launchCommand, int allocatedRam, Guid guid, string parentDirectory, Guid dependency)
    {
        ServerName = serverName;
        Branding = branding;
        LaunchCommand = launchCommand;
        AllocatedRAM = allocatedRam;
        ParentDirectory = parentDirectory;
        Guid = guid;
        Dependency = dependency;
        Timer.Tick += UpdateStats;
        Timer.Start();
    }

    private void UpdateStats(object sender, object e)
    {
        DirectoryInfo dirInfo = new(ParentDirectory);
        DiskUsage = (dirInfo.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length) / 1024 / 1204).ToString();
        if (ServerProcess == null || ServerProcess.Id == 0 || ServerProcess.HasExited)
        {
            IsOff = true;
            PowerStatus = "Powered off";
        }
        else
        {
            IsOff = false;
            PowerStatus = "Powered on";
            ServerOutput += "\n" + PendingOutput;
        }
    }

    public Server(string File)
    {
        ReadConfiguration(File);
        Ioc.Default.GetRequiredService<Global>().InstalledServers.Add(this.Guid, this);
        Timer.Tick += UpdateStats;
        Timer.Start();
    }

    protected Server() { }

    public void SaveConfiguration()
    {
            
        /*string Message = "RSM Config File V4";
        Message += $"\nServerName={ServerName}";
        Message += $"\nBranding={Branding}";
        Message += $"\nLaunchCommand={LaunchCommand}";
        Message += $"\nAllocatedRAM={AllocatedRAM}";
        Message += $"\nGuid={Guid}";
        Message += $"\nDependency={Dependency}";*/
        File.WriteAllText(Path.Combine(ParentDirectory, "RSM.conf"), JsonSerializer.Serialize(this));
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
                case "Branding": Branding = parts[1]; break;
                case "AllocatedRAM": AllocatedRAM = int.Parse(parts[1]); break;
                case "Guid": Guid = Guid.Parse(parts[1]); break;
                case "Dependency": Dependency = Guid.Parse(parts[1]); break;
            }
        }
        ParentDirectory = Path.GetDirectoryName(path);
    }


    public void TogglePower(object sender, RoutedEventArgs e)
    {
        if (IsOff)
        {
            if (Dependency == Guid.Empty)
            {
                Dependency dep;
                Ioc.Default.GetService<Global>().AvailableDependencies.TryGetValue(Dependency, out dep);
                ServerProcess = new Process()
                {
                    StartInfo = new()
                    {
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        FileName = "cmd.exe",
                        Arguments = "/k " + Path.Combine(ParentDirectory, dep.EntryPoint) + " " + LaunchCommand,
                        RedirectStandardError = true,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                    },
                };
                ServerProcess.OutputDataReceived += (_, e) => { if (!string.IsNullOrEmpty(e.Data)) { PendingOutput += "\n" + e.Data; } };
                ServerProcess.ErrorDataReceived += (_, e) => { if (!string.IsNullOrEmpty(e.Data)) { PendingOutput += "\n" + e.Data; } };
                ServerProcess.Start();
                ServerProcess.BeginErrorReadLine();
                ServerProcess.BeginOutputReadLine();
            }
        }
    } 

    private void DataRecieved(object sender, DataReceivedEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void Reboot(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void ClearConsole(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void DeleteServer(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void ShowInfoMenu(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void SendCommand(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        throw new NotImplementedException();
    }

    public void OpenParentDir(object sender, TappedRoutedEventArgs e)
    {
        Process.Start("explorer.exe", ParentDirectory);
    }
}