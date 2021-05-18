using System;
using System.Diagnostics;

namespace RSMUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.File.Delete("RSM.exe");
            LibRarisma.IO.DownloadFile("https://github.com/Rarisma/Rarismas-Server-Manager/raw/main/ServerFiles/RSM/RSM.zip", AppDomain.CurrentDomain.BaseDirectory, "RSM.zip");
            System.IO.Compression.ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\RSM.zip", AppDomain.CurrentDomain.BaseDirectory, true);
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\RSM.exe");
            //Environment.Exit(0);
        }
    }
}
