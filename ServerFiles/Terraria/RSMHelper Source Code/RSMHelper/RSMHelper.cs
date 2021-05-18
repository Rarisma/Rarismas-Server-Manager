using System;
using System.IO;
using System.Threading.Tasks;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
//Hey if your reading this, thank you
//Especially if you like/use SSM or other FOSS!
namespace PluginTemplate
{
    [ApiVersion(2, 1)]
    public class SSMHelper : TerrariaPlugin
    {
        public override string Name => "RSMHelper";
        public override Version Version => new Version(2, 0, 0);
        public override string Author => "Rarisma";
        public override string Description => "A simple plugin that allows RSM to send commands to this server by writing to RSM.txt";
        public SSMHelper(Main game) : base(game) { }

        public override async void Initialize()
        {
            Directory.CreateDirectory("RSM");
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\RSM\\RSM.txt", " ");
            await Task.Delay(10000);
            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;
            fileSystemWatcher.Path = AppDomain.CurrentDomain.BaseDirectory + "\\RSM\\";
            fileSystemWatcher.EnableRaisingEvents = true;
            fileSystemWatcher.Changed += Changed;
        }

        private async void Changed(object sender, FileSystemEventArgs e)
        {
            await Task.Delay(1000); //Delay to prevent most crashes
            try
            {
                string text = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\RSM\\RSM.txt");
                Commands.HandleCommand((TSPlayer)(object)TSPlayer.Server, text);
                Console.WriteLine("RSMHelper Issued command " + text);
            }
            catch { }

            await Task.Delay(1000); //Delay to prevent this from being called lots of times by RSM
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing){ }
            base.Dispose(disposing);
        }
    }
}
