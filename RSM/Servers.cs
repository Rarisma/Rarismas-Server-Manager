using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ABI.Windows.ApplicationModel.Chat;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using RSM.Data;

namespace RSM;
public class OldServers
{
    public struct Server
    {
        public string Name;
        public string Type;
        public string Version;
        public string Variant;
        public string EntryPoint;
        public string EntryPointURL;
        public string QuickCommands;
        public string LaunchCommand;
        public string ParentDirectory;
        public string LaunchFile;
        public int MaxRAMLimit;
        public bool ShowRAM;
        public bool BackupEnabled;
        public string LastBackup;
        public string BackupFrequency;

        public Process ServerProcess;
        public string ServerLog;
        public int ActualPort;
        public int IdealPort;
        public bool IsOn;

        public string PowerStatus
        {
            get
            {
                if (IsOn)
                {
                    return "On";
                }

                return "Off";
            }
        }

        public void MakeBackup()
        {
            ZipFile.CreateFromDirectory(Path.Combine(Data.Directories.Instances, ParentDirectory), Path.Combine(Data.Directories.Backups, $"{Name} as of {DateTime.Now.ToString().Replace(":"," ").Replace(@"/"," ")}.zip"), CompressionLevel.SmallestSize,false);
            LastBackup = DateTime.Now.ToString();
            WriteINI();
        }

        /// <summary>
        /// This takes a server struct and writes to an INI.
        /// </summary>
        public void WriteINI()
        {
            try
            {
                File.WriteAllText(Path.Combine(Data.Directories.Instances, Name, "RSM.ini"),
                    $"Name={Name}" +
                    $"\nType={Type}" +
                    $"\nVersion={Version}" +
                    $"\nVariant={Variant}" +
                    $"\nEntryPoint={EntryPoint}" +
                    $"\nEntryPointURL={EntryPointURL}" +
                    $"\nQuickCommands={QuickCommands}" +
                    $"\nIdealPort={IdealPort}" +
                    $"\nLaunchFile={LaunchFile}" +
                    $"\nLaunchCommand={LaunchCommand}" +
                    $"\nBackupEnabled={BackupEnabled}" +
                    $"\nLastBackup={LastBackup}" +
                    $"\nMaxRAMLimit={MaxRAMLimit}" +
                    $"\nShowRAM={ShowRAM}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR:" + ex.Message);
            }
        }

        /// <summary>
        /// Creates a server object from a ini file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public void ReadINI(string path)
        {
            foreach (var line in File.ReadAllLines(path))
            {
                string[] parts = line.Split(new[] { '=' });
                switch (parts[0])
                {
                    case "Name": Name = parts[1]; break;
                    case "Type": Type = parts[1]; break;
                    case "Version": Version = parts[1]; break;
                    case "Variant": Variant = parts[1]; break;
                    case "EntryPoint": EntryPoint = parts[1]; break;
                    case "EntryPointURL": EntryPointURL = parts[1]; break;
                    case "QuickCommands": QuickCommands = parts[1]; break;
                    case "IdealPort": IdealPort = int.Parse(parts[1]); break;
                    case "LaunchCommand": LaunchCommand = parts[1]; break;
                    case "LaunchFile": LaunchFile = parts[1]; break;
                    case "BackupEnabled": BackupEnabled = bool.Parse(parts[1]); break;
                    case "LastBackup": LastBackup = parts[1]; break;
                    case "MaxRAMLimit": MaxRAMLimit = int.Parse(parts[1]); break;
                    case "ShowRAM": ShowRAM = bool.Parse(parts[1]); break;
                }
            }
            ParentDirectory = Path.GetDirectoryName(path);
        }

        public string GetDiskUsage()
        {
            try
            {
                DirectoryInfo dirInfo = new(ParentDirectory);
                return (dirInfo.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length) / 1024 / 1204 ).ToString();
            }
            catch
            {
                return "ERROR";
            }
        }

        public async Task<double> GetCPUUsage()
        {
            var startTime = DateTime.UtcNow;
            var startCpuUsage = ServerProcess.TotalProcessorTime;
            await Task.Delay(500);

            var endTime = DateTime.UtcNow;
            var endCpuUsage = ServerProcess.TotalProcessorTime;
            var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
            var totalMsPassed = (endTime - startTime).TotalMilliseconds;
            var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
            return cpuUsageTotal * 100;
        }
    }
    /*
    public static async void Provision(XamlRoot Root,string Name, string Type, string Variant, string Version, Repositories.Variant VariantInfo, Repositories.ServerFile ServerInfo, string URL)
    {
        Data.ShellComment = "Creating server info...";
        Server NewServer = new()
        {
            Name = Name,
            Type = Type,
            Variant = Variant,
            Version = Version,
            EntryPoint = ServerInfo.EntryPoint,
            EntryPointURL = ServerInfo.EntryPointURL,
            QuickCommands = ServerInfo.Variants[0].CommandAutoFill.Replace("\t", "").Replace("\n", "").Replace("\r", ""),
            IdealPort = ServerInfo.IdealPort,
            LaunchCommand = ServerInfo.LaunchCommand,
            LaunchFile = "ServerFile." + ServerInfo.DownloadExtension,
            ParentDirectory = Directory.CreateDirectory(Path.Combine(Data.Directories.Instances, Name)).FullName,
            ShowRAM = ServerInfo.IsRamRequired,
            MaxRAMLimit = 4096,
            LastBackup = "Never",
            BackupFrequency = "Daily"
        };

        Global.ShellComment = "Dealing with legal stuff...";

        //First complies with any EULA or legal agreement.
        if (!string.IsNullOrWhiteSpace(ServerInfo.Eula))
        {
            ContentDialog EulaDialog = new()
            {
                Content = "Do you agree to the following EULA:\n" + ServerInfo.Eula + "\n\nPressing disagree will cancel the installation.",
                PrimaryButtonText = "Agree",
                SecondaryButtonText = "Disagree",
                XamlRoot = Root,
            };
            var res = await EulaDialog.ShowAsync();
            if (res == ContentDialogResult.Primary)
            {
                File.WriteAllText(Path.Combine(NewServer.ParentDirectory, "eula.txt"), ServerInfo.Eula.Trim());
            }
            else { Directory.Delete(NewServer.ParentDirectory, true); }
        }

        //Downloads file
        Global.ShellComment = "Downloading Server...";
        LibRarisma.Connectivity.DownloadFile(URL , NewServer.ParentDirectory, "ServerFile." + ServerInfo.DownloadExtension, ServerInfo.Zipped);

        Global.ShellComment = "Downloading dependencies...";
        //Configure toolchain
        if (!File.Exists(Path.Combine(Data.Directories.Dependencies, NewServer.EntryPoint)) && NewServer.EntryPointURL != "NULL")
        {
            LibRarisma.Connectivity.DownloadFile(NewServer.EntryPointURL, Data.Directories.Dependencies, "Temp.zip", true);
        }

        if (VariantInfo.PostInstallCommand != "")
        {
            var a = new ProcessStartInfo()
            {
                FileName = "cmd.exe", CreateNoWindow = true,
                Arguments = " /c " + VariantInfo.PostInstallCommand.Replace("$ENTRYPOINT",Path.Combine(Data.Directories.Dependencies,NewServer.EntryPoint))
                    .Replace("$ServerVersion", NewServer.Version),
                WorkingDirectory = NewServer.ParentDirectory
            };
            Process.Start(a);
        }

        if (VariantInfo.LaunchFile != "")
        {
            NewServer.LaunchFile = VariantInfo.LaunchFile;
        }


        Data.ShellComment = "Finishing up...";
        NewServer.WriteINI();
        Data.InstalledServers.Add(NewServer);
        (Data.Content.Content as StackPanel).Children.Add(new WideNodes(ref NewServer));
        Data.ShellComment = "Finished making server";
        Thread.Sleep(1000);
        Data.ShellComment = "";
    }*/
}