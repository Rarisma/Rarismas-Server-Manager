using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RSM.Data;
using RSM.Models;

namespace RSM
{
    public static class Provisioner
    {
        public static async void Provision(ServerGroup NewInstance, string url, bool zipped)
        {
            if (DownloadServerFile(NewInstance, url, zipped) == false) { return; }
            if (DownloadDependencies(NewInstance, url) == false) { return; }
            if (PostInstall(NewInstance, url) == false) { return; }


        }

        private static bool DownloadServerFile(ServerGroup NewInstance, string url, bool Zipped)
        {
            try
            {
                new WebClient().DownloadFile(url, Path.Combine(NewInstance.ParentDirectory, "ServerFile"));
                if (Zipped)
                {
                    try
                    {
                        System.IO.Compression.ZipFile.ExtractToDirectory(Path.Combine(NewInstance.ParentDirectory, "ServerFile"), NewInstance.ParentDirectory);

                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            catch
            {
                throw new IOException();
                return false;
            }
            return true;
        }

        private static bool DownloadDependencies(ServerGroup NewInstance, string url)
        {
            try
            {
                Dependency dep; 
                Global.AvailableDependencies.TryGetValue(NewInstance.Dependency,out dep);
                if (true)
                {
                    throw new NotImplementedException();
                    new WebClient().DownloadFile(dep.DownloadURL, Path.Combine(Directories.Dependencies, dep.Guid.ToString(), "Download.zip"));
                    System.IO.Compression.ZipFile.ExtractToDirectory(Path.Combine(Directories.Dependencies, dep.Guid.ToString(), "Download.zip"), Path.Combine(Directories.Dependencies, dep.Guid.ToString()));
                }
            }
            catch (Exception ex)
            {
            }
            return true;
        }

        public static bool PostInstall(ServerGroup NewInstance, string InstallCommand)
        {
            if (!String.IsNullOrEmpty(InstallCommand))
            {
                var a = new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    CreateNoWindow = true,
                    //Arguments = " /c " + NewInstance.PostInstallCommand.Replace("$ENTRYPOINT", Path.Combine(Directories.Dependencies, NewInstance.EntryPoint)).Replace("$ServerVersion", NewServer.Version),
                    WorkingDirectory = NewInstance.ParentDirectory
                };
                Process.Start(a);
            }

            return true;
        }
    }
}
