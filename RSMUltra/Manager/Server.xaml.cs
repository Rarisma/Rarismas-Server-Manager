using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Microsoft.UI.Dispatching;
using Mono.Nat;
using RSMUltra;
using RSMUltra.Manager;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RSMUltra.Manager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Server : Page
    {
        public Server()
        {
            this.InitializeComponent();
            CmdBar.IsOpen = true;
            Name.Text = $"{ServerInfo.Name}";
            Game.Text = $"{ServerInfo.Game} {ServerInfo.Version} ({ ServerInfo.Variant})";


            //Sets up suggestions for CommandBar
            foreach (var VARIABLE in  ServerUtils.SetupCommands())
            {
                CommandBar.Items.Add(VARIABLE);
            }

            //This section controls the buttons that appears in LinkButtons
            switch (ServerInfo.Game)
            {
                case "Minecraft Java Edition":
                    if (ServerInfo.Variant == "Forge")
                    {
                        LinkPanels.Children.Remove(PluginButton);
                    }
                    else if (ServerInfo.Variant == "Paper")
                    {
                        LinkPanels.Children.Remove(ModButton);
                    }
                    break;
            }
        }

        //Shows CMDBar when pointer has entered
        private void CmdBarFix(object? sender, object e) { CmdBar.IsOpen = true; }

        //Hides CMDBar when pointer has left
        private void CmdBarUnfix(object sender, PointerRoutedEventArgs e) { CmdBar.IsOpen = false; }

        private void StartServer(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = false;
            Stop.IsEnabled = true;
            CommandBar.IsEnabled = true;

            UltraUI.Manager.Setting.IsEnabled = false;
            UltraUI.Manager.General.IsEnabled = false;
            UltraUI.Manager.Backup.IsEnabled = false;
            /*foreach (var VARIABLE in UltraUI.Main.ButtonRegistry)
            {
                VARIABLE.IsEnabled = false;
            }*/

            ServerConsole.Text += "[WARNING] This server is using RSM and has Project CloudSpotter enabled.\nIf you are reading this for debugging/error infomation see the following https://github.com/Rarisma/Rarismas-Server-Manager/wiki/Project-Cloudspotter";

            ServerInfo.Server.StartInfo.CreateNoWindow = true; //Might want to set this to true when debugging to see if the server is actually running
            ServerInfo.Server.StartInfo.WorkingDirectory = Global.ServerDir;

            switch (ServerInfo.Game)
            {
                case "Minecraft Java Edition":
                    if (ServerInfo.Variant == "Forge")
                    {
                        ServerInfo.Server.StartInfo.FileName = Global.Java8; //Forge doesn't work on Java16
                    }
                    else
                    {
                        ServerInfo.Server.StartInfo.FileName = Global.Java16; //Everything else is cool with Java16
                    }
                    ServerInfo.Server.StartInfo.Arguments = "-Xmx" + ServerInfo.AllocatedRAM + "M -jar \"" + Global.ServerDir + "\\Server.jar" + "\" nogui";
                    break;
                case "Mindustry":
                    ServerInfo.Server.StartInfo.FileName = Global.Java16;
                    ServerInfo.Server.StartInfo.Arguments = "-Xmx" + ServerInfo.AllocatedRAM + "M -jar \"" + Global.ServerDir + "\\Server.jar" + "\"";
                    break;
                case "Minecraft Bedrock":
                    ServerInfo.Server.StartInfo.FileName = Global.ServerDir + "//bedrock_server.exe";
                    break;
                case "Terraria":
                    ServerInfo.Server.StartInfo.FileName = Global.ServerDir + "//TerrariaServer.exe";
                    ServerInfo.Server.StartInfo.Arguments = "-autocreate " + ServerInfo.WorldSize + " -world \"" + Global.ServerDir + "World.wld\" -difficulty " + ServerInfo.Difficulty; break;  
                    break;
            }

            //Setups handling for output
            ServerInfo.Server.StartInfo.RedirectStandardInput = true; 
            ServerInfo.Server.StartInfo.RedirectStandardError = true;
            ServerInfo.Server.StartInfo.RedirectStandardOutput = true;
            ServerInfo.Server.OutputDataReceived += new DataReceivedEventHandler((sender, e) => { if (!String.IsNullOrEmpty(e.Data)) { Task.Run(() => OutputRecieved(e.Data)); }});
            ServerInfo.Server.ErrorDataReceived += new DataReceivedEventHandler((sender, e) => { if (!String.IsNullOrEmpty(e.Data)) { Task.Run(() => OutputRecieved(e.Data)); } });

            ServerInfo.Server.Start(); //Starts the server running


            ServerInfo.Server.BeginErrorReadLine(); //Tells RSM to start reading any errors
            ServerInfo.Server.BeginOutputReadLine(); //Tells RSM to start reading the output

            //Starts the search for the router to Port Forward to via UPNP
            NatUtility.DeviceFound += ServerUtils.Forward;
            NatUtility.StartDiscovery();
        }

        public async Task OutputRecieved(string Data) //Actually displays output.
        {
            DispatcherQueue.TryEnqueue(
                () =>
                {
                    ServerConsole.Text += "\n" + ServerUtils.Filter(Data);

                });
        }

        private void Reboot(object sender, RoutedEventArgs e)
        {
            ServerUtils.StopServer();
            StartServer(null,null);
        }

        private async void Cease(object sender, RoutedEventArgs e)
        {
            Stop.IsEnabled = false;

            await Task.Run(() => ServerUtils.StopServer());

            //Ends read operations to prevent crashes if Global.Server is needed again
            ServerInfo.Server.CancelErrorRead(); //Ends error read
            ServerInfo.Server.CancelOutputRead(); //Ends output read
            Start.IsEnabled = true;
            CommandBar.IsEnabled = false;
            UltraUI.Manager.Setting.IsEnabled = true;
            UltraUI.Manager.General.IsEnabled = true;
            UltraUI.Manager.Backup.IsEnabled = true;
            foreach (var VARIABLE in UltraUI.Main.ButtonRegistry)
            {
                VARIABLE.IsEnabled = true;
            }
        }

        private void ClearConsole(object sender, RoutedEventArgs e) { ServerConsole.Text = ""; } //Deletes server log (Does not delete server logs if server makes them itself.)

        private void Mods(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(Global.ServerDir + "//Mods//"))
            {
                Directory.CreateDirectory(Global.ServerDir + "//Mods//");
            }
            LibRarisma.Tools.OpenLink(Global.ServerDir + "//Mods//");
        }
        
        private void Plugins(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(Global.ServerDir + "//Plugins//"))
            {
                Directory.CreateDirectory(Global.ServerDir + "//Plugins//");
            }
            LibRarisma.Tools.OpenLink(Global.ServerDir + "//Plugins//");
        }

        private void DeleteServer(object sender, RoutedEventArgs e)
        {
            Directory.Delete(Global.ServerDir, true);
            MainWindow.Frame.Content = new UltraUI.Main();
        }

        private void Datapacks(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(Global.ServerDir + "//World//Datapacks//"))
            {
                Directory.CreateDirectory(Global.ServerDir + "//World//Datapacks//");
            }
            LibRarisma.Tools.OpenLink(Global.ServerDir + "//World//Datapacks//");
        }
 
        private void SendCommand(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            ServerInfo.Server.StandardInput.WriteLine(CommandBar.Text);
            CommandBar.Text = "";
        }
    }
}
