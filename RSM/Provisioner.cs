using System;
using System.IO;
using System.Net;
using CommunityToolkit.Mvvm.DependencyInjection;
using RSM.Data;
using RSM.Models;

//Down to the river to wash away all these things.

namespace RSM;

public static class Provisioner
{
    private static Global GlobalVM = Ioc.Default.GetService<Global>();
    private static string ParentDirectory { get => Path.Combine(Directories.Instances, GlobalVM.ServerLabel); }

    public static async void Provision()
    {
        Directory.CreateDirectory(ParentDirectory);
        if (DownloadServerFile() == false) { return; }
        if (DownloadDependencies() == false) { return; }
        if (PostInstall() == false) { return; }
        ConvertToServer();

    }

    private static bool DownloadServerFile()
    {
        try
        {
            string url;
            GlobalVM.SelectedVariant.Versions.TryGetValue(GlobalVM.SelectedVersion, out url);
            new WebClient().DownloadFile(url, Path.Combine(ParentDirectory, "ServerFile"));
            if (GlobalVM.SelectedServer.IsDownloadZipped)
            {
                try
                {
                    System.IO.Compression.ZipFile.ExtractToDirectory(Path.Combine(ParentDirectory, "ServerFile"), ParentDirectory);
                }
                catch
                {
                    return false;
                }
            }
        }
        catch { throw new IOException(); }
        return true;
    }

    private static bool DownloadDependencies()
    {
        try
        {
            Dependency dep; 
            GlobalVM.AvailableDependencies.TryGetValue(GlobalVM.SelectedServer.RequiredDependency, out dep);
            if (true)
            {
                Directory.CreateDirectory(Path.Combine(Directories.Dependencies, dep.Guid.ToString()));
                new WebClient().DownloadFile(dep.DownloadURL, Path.Combine(Directories.Dependencies, dep.Guid.ToString(), "Download.zip"));
                System.IO.Compression.ZipFile.ExtractToDirectory(Path.Combine(Directories.Dependencies, dep.Guid.ToString(), "Download.zip"), Path.Combine(Directories.Dependencies, dep.Guid.ToString()));
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Posts the install.
    /// </summary>
    /// <returns></returns>
    private static bool PostInstall()
    {
        /*if (!String.IsNullOrEmpty(GlobalVM.SelectedServer.PostInstallCommand))
        {
            throw new NotImplementedException();
            var a = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                CreateNoWindow = true,
                //Arguments = " /c " + NewInstance.PostInstallCommand.Replace("$ENTRYPOINT", Path.Combine(Directories.Dependencies, GlobalVM.SelectedServer.LaunchCommand)).Replace("$ServerVersion", GlobalVM.SelectedServer.Version),
                WorkingDirectory = ParentDirectory
            };
            Process.Start(a);

        }*/

        return true;
    }

    /// <summary>
    /// Converts bindings in GlobalVM to the server class.
    /// </summary>  
    /// <returns></returns>
    private static void ConvertToServer()
    {
        Server NewServer = new(
            GlobalVM.ServerLabel,
            GlobalVM.SelectedServer.Branding,
            GlobalVM.SelectedServer.LaunchCommand,
            GlobalVM.SelectedServer.AllocatedRAM,
            Guid.NewGuid(),
            ParentDirectory,
            GlobalVM.SelectedServer.RequiredDependency);
        NewServer.SaveConfiguration();
        GlobalVM.InstalledServers.Add(NewServer.Guid, NewServer);
    }
}