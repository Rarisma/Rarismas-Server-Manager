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
        public override string Name => "SSMHelper";
        public override Version Version => new Version(1, 9, 0);
        public override string Author => "Rarisma";
        public override string Description => "A simple plugin that allows SSM to send commands to this server by writing to SSM.txt";
        public SSMHelper(Main game) : base(game) { }

        public override async void Initialize()
        {
            Directory.CreateDirectory("SSM");
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\SSM\\SSM.txt", " ");
            await Task.Delay(10000);
            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;
            fileSystemWatcher.Path = AppDomain.CurrentDomain.BaseDirectory + "\\SSM\\";
            fileSystemWatcher.EnableRaisingEvents = true;
            fileSystemWatcher.Changed += Changed;
        }

        private async void Changed(object sender, FileSystemEventArgs e)
        {
            string text = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\SSM\\SSM.txt");
            Commands.HandleCommand((TSPlayer)(object)TSPlayer.Server, text);
            Console.WriteLine("SSMHelper Issued command " + text);
            await Task.Delay(1000); //Delay to prevent this from being called lots of times by SSM
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing){ }
            base.Dispose(disposing);
        }
    }
}
