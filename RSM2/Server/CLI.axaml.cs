using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System;
using System.Diagnostics;

namespace RSM2.Server
{
    public partial class CLI : UserControl
    {
        public CLI()
        {
            AvaloniaXamlLoader.Load(this);
            Initalise(); //Setups command prompt
            ServerInfo.Running = true;
            Utilities.LaunchServer();
            SetQuickCommands();
        }

        private void Initalise() //This runs all the server configuration eg mounting folders and redirection
        {
            TextBlock ServerName = this.Find<TextBlock>("ServerName");
            ServerName.Text = ServerInfo.Name;

            //This opens the command line and sets up the Output/Error redirection
            ServerInfo.cmd.StartInfo.RedirectStandardInput = true;
            ServerInfo.cmd.StartInfo.RedirectStandardOutput = true;
            ServerInfo.cmd.StartInfo.RedirectStandardError = true;
            ServerInfo.cmd.StartInfo.CreateNoWindow = true;
            SetServerType();

            TextBox ServerConsole = this.Find<TextBox>("ServerConsole");
            ServerInfo.cmd.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data)) { Dispatcher.UIThread.InvokeAsync(new Action(() => { ServerConsole.Text += "\n" + e.Data; })); }
                Dispatcher.UIThread.InvokeAsync(new Action(() => { ServerConsole.CaretIndex = int.MaxValue; }));
            });

            ServerInfo.cmd.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data)) { Dispatcher.UIThread.InvokeAsync(new Action(() => { ServerConsole.Text += "\nERROR: " + e.Data; })); }
                Dispatcher.UIThread.InvokeAsync(new Action(() => { ServerConsole.CaretIndex = int.MaxValue; }));
            });
        }

        private void SetServerType()
        {
            switch (ServerInfo.Game)
            {
                case "Terraria":
                    ServerInfo.cmd.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//TerrariaServer.exe";
                    ServerInfo.cmd.StartInfo.Arguments = "-autocreate " + ServerInfo.WorldSize + " -world \"" + AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.Name + "\\World.wld\" -difficulty " + ServerInfo.Difficulty;
                    ServerInfo.cmd.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//";
                    ServerInfo.cmd.Start();
                    ServerInfo.cmd.BeginOutputReadLine();
                    break;

                default:
                    ServerInfo.cmd.StartInfo.FileName = "cmd.exe";
                    ServerInfo.cmd.StartInfo.Arguments = Convert.ToString("/k " + AppDomain.CurrentDomain.BaseDirectory);
                    ServerInfo.cmd.Start();
                    try { ServerInfo.cmd.BeginOutputReadLine(); } catch { }

                    //This mounts the server
                    ServerInfo.cmd.StandardInput.WriteLine("cd Servers");
                    ServerInfo.cmd.StandardInput.Flush();
                    ServerInfo.cmd.StandardInput.WriteLine("cd " + ServerInfo.Name);
                    ServerInfo.cmd.StandardInput.Flush();
                    break;
            }
        }

        private void SetQuickCommands() //Sets Quick command frame
        {
            ContentControl QuickCommandsFrame = this.Find<ContentControl>("QuickCommandsFrame");
            switch (ServerInfo.Game)
            {
                case "Minecraft Bedrock":
                    QuickCommandsFrame.Content = new QuickCommands.Minecraft();
                    break;
                case "Minecraft Java":
                    this.Find<AutoCompleteBox>("Input").Items = new string[] { "help", "ban", "clear", "deop", "difficulty", "kick", "op", "save", "say", "seed", "stop", "tp", "weather", "xp" };
                    QuickCommandsFrame.Content = new QuickCommands.Minecraft(); 
                    break;
                case "Terraria": QuickCommandsFrame.Content = new QuickCommands.Terraria(); break;
            }
        }

        private void SendInput(object sender, RoutedEventArgs e)
        {
            switch (ServerInfo.Game)
            {
                default:
                    AutoCompleteBox Input = this.Find<AutoCompleteBox>("Input");
                    ServerInfo.cmd.StandardInput.WriteLine(Input.Text);
                    ServerInfo.cmd.StandardInput.Flush();
                    Input.Text = "";
                    break;
            }

        }
    }
}
