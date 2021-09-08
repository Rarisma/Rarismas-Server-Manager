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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Microsoft.UI.Dispatching;
using Mono.Nat;

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
        }

        //Shows CMDBar when pointer has entered
        private void CmdBarFix(object? sender, object e) { CmdBar.IsOpen = true; }

        //Hides CMDBar when pointer has left
        private void CmdBarUnfix(object sender, PointerRoutedEventArgs e) { CmdBar.IsOpen = false; }

        private void StartServer(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = false;
            Stop.IsEnabled = true;

            ServerInfo.Server.StartInfo.RedirectStandardInput = true;
            ServerInfo.Server.StartInfo.RedirectStandardError = true;
            ServerInfo.Server.StartInfo.RedirectStandardOutput = true;
            ServerInfo.Server.StartInfo.CreateNoWindow = false; //Might want to set this to true when debugging to see if the server is actually running
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
                    ServerInfo.Server.StartInfo.Arguments = "-Xmx" + Convert.ToInt32((LibRarisma.Tools.GetRAM() / 2) - 1024) + "M -jar \"" + Global.ServerDir + "\\Server.jar" + "\" nogui";
                    break;


            }

            //Setups handling for output
            ServerInfo.Server.OutputDataReceived += new DataReceivedEventHandler((sender, e) => { if (!String.IsNullOrEmpty(e.Data)) { OutputRecieved(e.Data); }});
            ServerInfo.Server.ErrorDataReceived += new DataReceivedEventHandler((sender, e) => { if (!String.IsNullOrEmpty(e.Data)) { OutputRecieved(e.Data); }});

            ServerInfo.Server.Start(); //Starts the server running
            ServerInfo.Server.BeginErrorReadLine(); //Tells RSM to start reading any errors
            ServerInfo.Server.BeginOutputReadLine(); //Tells RSM to start reading the output
            
            //Starts the search for the router to Port Forward to via UPNP
            NatUtility.DeviceFound += PortForward;
            NatUtility.StartDiscovery();
        }

        private void PortForward(object sender, DeviceEventArgs args)
        {
            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    args.Device.CreatePortMap(new Mapping(Protocol.Tcp, 25565, 25565));
                    args.Device.CreatePortMap(new Mapping(Protocol.Udp, 25565, 25565));
                    break;
                case "Minecraft Bedrock": //Creating all the ports as a just in case thing
                    args.Device.CreatePortMap(new Mapping(Protocol.Udp, 19132, 19132));
                    args.Device.CreatePortMap(new Mapping(Protocol.Udp, 19133, 19133));
                    args.Device.CreatePortMap(new Mapping(Protocol.Tcp, 19132, 19132));
                    args.Device.CreatePortMap(new Mapping(Protocol.Tcp, 19133, 19133));
                    break;
                case "Terraria":
                    args.Device.CreatePortMap(new Mapping(Protocol.Tcp, 7777, 7777));
                    break;
                case "Mindustry":
                    args.Device.CreatePortMap(new Mapping(Protocol.Tcp, 6567, 6567));
                    args.Device.CreatePortMap(new Mapping(Protocol.Udp, 6567, 6567));
                    break;
                case "Factorio":
                    args.Device.CreatePortMap(new Mapping(Protocol.Udp, 34197, 34197));
                    break;
            }
        }

        public async Task OutputRecieved(string Data) //Actually displays output.
        {
            //TODO
            //Reimplement Cloudspotter

            DispatcherQueue.TryEnqueue(
                () =>
                {
                    ServerConsole.Text += "\n" + Data;
                    ServerConsole.SelectionStart = int.MaxValue;
                });

        }

    }
}
