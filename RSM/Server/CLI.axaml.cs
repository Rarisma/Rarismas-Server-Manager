using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Mono.Nat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TextCopy;
//Incredibly Based hotel and casino
namespace RSM.Server
{
    public partial class CLI : UserControl
    {
        public CLI()
        {
            AvaloniaXamlLoader.Load(this);
            this.Find<TextBlock>("ServerName").Text = ServerInfo.Name;
            GenericSetup();
            SetExecutable();
            OpenPorts();
            SetQuickCommands();
            SetupOutput();
            Extras();
        }

        void Extras()
        {
            switch (ServerInfo.Game)
            {
                case "Mindustry":
                    Global.Server.StandardInput.WriteLine("config autosave on");
                    Global.Server.StandardInput.Flush();
                    Global.Server.StandardInput.WriteLine("config desc Hosted by RSM");
                    Global.Server.StandardInput.Flush();
                    Global.Server.StandardInput.WriteLine("config name " + ServerInfo.Name);
                    Global.Server.StandardInput.Flush();
                    Global.Server.StandardInput.WriteLine("config strict on");
                    Global.Server.StandardInput.Flush();
                    Global.Server.StandardInput.WriteLine("config antiSpam on");
                    Global.Server.StandardInput.Flush();
                    Global.Server.StandardInput.WriteLine("config motd Welcome to " + ServerInfo.Name);
                    Global.Server.StandardInput.Flush();
                    break;
            }
        }
        void GenericSetup() //Just redirects output, input and tells the os not to make a window
        {
            Global.Server.StartInfo.RedirectStandardInput = true;
            Global.Server.StartInfo.RedirectStandardOutput = true;
            Global.Server.StartInfo.RedirectStandardError = true;
            Global.Server.StartInfo.CreateNoWindow = true;
            Global.Server.StartInfo.WorkingDirectory = ServerInfo.Dir;
        }

        void SetExecutable() //Sets the server executable and any arguments
        {
            switch (ServerInfo.Game)
            {
                case "Terraria":
                    Global.Server.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//TerrariaServer.exe";
                    Global.Server.StartInfo.Arguments = "-autocreate " + ServerInfo.WorldSize + " -world \"" + ServerInfo.Dir + "World.wld\" -difficulty " + ServerInfo.Difficulty;
                    break;

                case "Minecraft Java":
                    if (ServerInfo.Variant != "Modded")
                    {
                        if (Global.IsWindows) { Global.Server.StartInfo.FileName = Global.Java16 + "Java.exe"; }
                        else { Global.Server.StartInfo.FileName = Global.Java16 + "Java"; }
                        Global.Server.StartInfo.Arguments = "-Xms" + ServerInfo.RAM + "M -jar \"" + ServerInfo.Dir + "Server.jar\" nogui"; 
                    }
                    else
                    {
                        if (Global.IsWindows) { Global.Server.StartInfo.FileName = Global.Java8 + "Java.exe"; }
                        else { Global.Server.StartInfo.FileName = Global.Java8 + "Java"; }
                        List<String> Jarfiles = new();
                        Jarfiles.AddRange(Directory.GetFiles(ServerInfo.Dir, "*.jar", SearchOption.TopDirectoryOnly));
                        string ServerFile = "";
                        if (Jarfiles[0].Contains("forge")) { ServerFile = Jarfiles[0]; }
                        else { ServerFile = Jarfiles[1]; }
                        Global.Server.StartInfo.Arguments = "-Xms" + ServerInfo.RAM + "M -jar \"" + ServerFile + "\" nogui";
                    }
                    break;

                case "Minecraft Bedrock":
                    Global.Server.StartInfo.FileName = ServerInfo.Dir + "//bedrock_server.exe";
                    break;

                case "Factorio":
                    Global.Server.StartInfo.FileName = ServerInfo.Dir + "//bin//x64//factorio.exe";
                    Global.Server.StartInfo.Arguments = "--start-server \"" + ServerInfo.Dir + "\\bin\\x64\\saves\\RSM.zip\"";
                    break;

                case "Mindustry":
                    if (Global.IsWindows) { Global.Server.StartInfo.FileName = Global.Java16 + "Java.exe"; }
                    else { Global.Server.StartInfo.FileName = Global.Java16 + "Java"; }
                    Global.Server.StartInfo.Arguments = "-jar \"" + ServerInfo.Dir + "Server.jar\"";
                    break;
            }
        }

        void SetupOutput() //Sets event handlers for error and output events
        {
            TextBox ServerConsole = this.Find<TextBox>("ServerConsole");
            Global.Server.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    if (e.Data.Contains("/setup") && Global.Admininfo == "") { Global.Admininfo = "Type " + e.Data.ToString().Substring(44) + " to give yourself admin"; Dispatcher.UIThread.InvokeAsync(new Action(() => { this.Find<ContentControl>("QuickCommandsFrame").Content = new Game.Terraria(); })); }

                    Dispatcher.UIThread.InvokeAsync(new Action(() => { ServerConsole.Text += "\n" + Cloudspotter.Filter(Cloudspotter.Filter(e.Data.ToString())); }));
                    Dispatcher.UIThread.InvokeAsync(new Action(() => { ServerConsole.CaretIndex = int.MaxValue - 200; }));
                }
            });

            Global.Server.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data)) { Dispatcher.UIThread.InvokeAsync(new Action(() => { ServerConsole.Text += "\nERROR: " + e.Data; })); }
                Dispatcher.UIThread.InvokeAsync(new Action(() => { ServerConsole.CaretIndex = int.MaxValue; }));
            });

            Global.Server.Start();
            Global.Server.BeginErrorReadLine();
            Global.Server.BeginOutputReadLine();
        }


        void OpenPorts()//Doesnt actually forward the ports but gets it started
        {
            NatUtility.DeviceFound += DeviceFound;
            NatUtility.StartDiscovery();
        }
        private void DeviceFound(object sender, DeviceEventArgs args)
        {
            INatDevice device = args.Device;
            Global.IP = device.GetExternalIP().ToString();
            Dispatcher.UIThread.InvokeAsync(new Action(() => { this.Find<Button>("ExternalIP").Content = "Tell people to join your server at: " + Global.IP; }));
            switch (ServerInfo.Game)
            {
                case "Minecraft Java":
                    device.CreatePortMap(new Mapping(Protocol.Tcp, 25565, 25565));
                    device.CreatePortMap(new Mapping(Protocol.Udp, 25565, 25565));
                    break;
                case "Minecraft Bedrock": //Creating all the ports as a just in case thing
                    device.CreatePortMap(new Mapping(Protocol.Udp, 19132, 19132));
                    device.CreatePortMap(new Mapping(Protocol.Udp, 19133, 19133));
                    device.CreatePortMap(new Mapping(Protocol.Tcp, 19132, 19132));
                    device.CreatePortMap(new Mapping(Protocol.Tcp, 19133, 19133));
                    break;
                case "Terraria":
                    device.CreatePortMap(new Mapping(Protocol.Tcp, 7777, 7777));
                    break;
                case "Mindustry":
                    device.CreatePortMap(new Mapping(Protocol.Tcp, 6567, 6567));
                    device.CreatePortMap(new Mapping(Protocol.Udp, 6567, 6567));
                    break;
                case "Factorio":
                    device.CreatePortMap(new Mapping(Protocol.Udp, 34197, 34197));
                    break;
            }
        }

        private void SetQuickCommands() //Sets Quick command frame
        {
            ContentControl QuickCommandsFrame = this.Find<ContentControl>("QuickCommandsFrame");
            switch (ServerInfo.Game)
            {
                case "Minecraft Bedrock":
                    QuickCommandsFrame.Content = new Game.Minecraft();
                    this.Find<AutoCompleteBox>("Input").Items = new string[] { "help", "ban", "clear", "deop", "difficulty", "kick", "op", "save", "say", "seed", "stop", "tp", "weather", "xp" };
                    break;
                case "Minecraft Java":
                    this.Find<AutoCompleteBox>("Input").Items = new string[] { "help", "ban", "clear", "deop", "difficulty", "kick", "op", "save", "say", "seed", "stop", "tp", "weather", "xp" };
                    QuickCommandsFrame.Content = new Game.Minecraft();
                    break;
                case "Factorio":
                    this.Find<AutoCompleteBox>("Input").Items = new string[] { "/clear", "/evolution", "/seed", "/time", "/ban", "/unban", "/demote", "/promote", "/kick", "/players", "/whitelist", "/help" };
                    break;
                case "Mindustry":
                    this.Find<AutoCompleteBox>("Input").Items = new string[] { "help", "exit", "host", "maps", "status", "reloadmaps", "mods", "say", "pause on", "pause off", "rules", "playerlimit", "fillitems ", "shuffle all", "shuffle none", "shuffle builtin", "nextmap", "kick", "ban", "unban", "pardon", "admins", "admins", "Players", "runwave", "gameover",  "info"};
                    break;
            }
        }

        private void SendInput(object sender, RoutedEventArgs e)
        {
            switch (ServerInfo.Game)
            {
                default:
                    AutoCompleteBox Input = this.Find<AutoCompleteBox>("Input");
                    Global.Server.StandardInput.WriteLine(Input.Text);
                    Global.Server.StandardInput.Flush();
                    Input.Text = "";
                    break;
            }

        }
        
        private void CopyIP(object sender, RoutedEventArgs e) { ClipboardService.SetText(Global.IP); }
        
        private void Stop(object sender, RoutedEventArgs e)
        {
            Utilities.StopServer();
            Global.Server.CancelOutputRead();
            Global.Server.CancelErrorRead();
            Global.Server.WaitForExit();
            Global.Server.Close();
            Global.Display.Content = new Manager();
        }
    }
}
