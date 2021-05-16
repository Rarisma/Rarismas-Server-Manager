using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RSM.PerGameSettings
{
    /// <summary>
    /// Interaction logic for Minecraft.xaml
    /// </summary>
    public partial class Minecraft : Page
    {
        public Minecraft()
        {
            InitializeComponent();

            ServerPropities.ServerFile.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.Label + "\\server.properties"));
            if (ServerPropities.ServerFile.Contains("enable-command-block=true")) { CommandBlocks.IsOn = true; }
            else if (ServerPropities.ServerFile.Contains("enable-command-block=false")) { CommandBlocks.IsOn = false; }
            else { CommandBlocks.IsEnabled = false; }

            if (ServerPropities.ServerFile.Contains("pvp=true")) { PVP.IsOn = true; }
            else if (ServerPropities.ServerFile.Contains("pvp=false")) { PVP.IsOn = false; }
            else { PVP.IsEnabled = false; }

            if (ServerPropities.ServerFile.Contains("hardcore=true")) { Hardcore.IsOn = true; }
            else if (ServerPropities.ServerFile.Contains("hardcore=false")) { Hardcore.IsOn = false; }
            else { Hardcore.IsEnabled = false; }

            if (ServerPropities.ServerFile.Contains("spawn-monsters=true")) { MobSpawning.IsOn = true; }
            else if (ServerPropities.ServerFile.Contains("spawn-monsters=false")) { MobSpawning.IsOn = false; }
            else { MobSpawning.IsEnabled = false; }

            if (ServerPropities.ServerFile.Contains("enforce-whitelist=true")) { Whitelist.IsOn = true; }
            else if (ServerPropities.ServerFile.Contains("enforce-whitelist=false")) { Whitelist.IsOn = false; }
            else { Whitelist.IsEnabled = false; }

            if (ServerPropities.ServerFile.Contains("online-mode=true")) { OnlineMode.IsOn = true; }
            else if (ServerPropities.ServerFile.Contains("online-mode=false")) { OnlineMode.IsOn = false; }
            else { OnlineMode.IsEnabled = false; }

            if (ServerPropities.ServerFile.Contains("difficulty=peaceful")) { Difficulty.SelectedIndex = 0; }
            else if (ServerPropities.ServerFile.Contains("difficulty=easy")) { Difficulty.SelectedIndex = 1; }
            else if (ServerPropities.ServerFile.Contains("difficulty=normal")) { Difficulty.SelectedIndex = 2; }
            else if (ServerPropities.ServerFile.Contains("difficulty=hard")) { Difficulty.SelectedIndex = 3; }
            else { Difficulty.IsEnabled = false; }

            if (ServerPropities.ServerFile.Contains("gamemode=survival")) { Gamemode.SelectedIndex = 0; }
            else if (ServerPropities.ServerFile.Contains("gamemode=creative")) { Gamemode.SelectedIndex = 1; }
            else { Gamemode.IsEnabled = false; }

        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (ServerPropities.ServerFile.IndexOf("pvp=false") != -1) { ServerPropities.ServerFile[ServerPropities.ServerFile.IndexOf("pvp=false")] = "pvp=" + PVP.IsOn; }
            else if (ServerPropities.ServerFile.IndexOf("pvp=true") != -1) { ServerPropities.ServerFile[ServerPropities.ServerFile.IndexOf("pvp=true")] = "pvp=" + PVP.IsOn; }

            if (ServerPropities.ServerFile.IndexOf("enable-command-block=false") != -1) { ServerPropities.ServerFile[ServerPropities.ServerFile.IndexOf("enable-command-block=false")] = "enable-command-block=" + CommandBlocks.IsOn; }
            else if (ServerPropities.ServerFile.IndexOf("enable-command-block=true") != -1) { ServerPropities.ServerFile[ServerPropities.ServerFile.IndexOf("enable-command-block=true")] = "enable-command-block=" + CommandBlocks.IsOn; }

            if (ServerPropities.ServerFile.IndexOf("hardcore=false") != -1) { ServerPropities.ServerFile[ServerPropities.ServerFile.IndexOf("hardcore=false")] = "hardcore=" + Hardcore.IsOn; }
            else if (ServerPropities.ServerFile.IndexOf("hardcore=true") != -1) { ServerPropities.ServerFile[ServerPropities.ServerFile.IndexOf("hardcore=true")] = "hardcore=" + Hardcore.IsOn; }

            if (ServerPropities.ServerFile.IndexOf("spawn-monsters=false") != -1) { ServerPropities.ServerFile[ServerPropities.ServerFile.IndexOf("spawn-monsters=false")] = "spawn-monsters=" + MobSpawning.IsOn; }
            else if (ServerPropities.ServerFile.IndexOf("spawn-monsters=true") != -1) { ServerPropities.ServerFile[ServerPropities.ServerFile.IndexOf("spawn-monsters=true")] = "spawn-monsters=" + MobSpawning.IsOn; }

            if (ServerPropities.ServerFile.IndexOf("enforce-whitelist=false") != -1) { ServerPropities.ServerFile[ServerPropities.ServerFile.IndexOf("enforce-whitelist=false")] = "enforce-whitelist=" + Whitelist.IsOn; }
            else if (ServerPropities.ServerFile.IndexOf("enforce-whitelist=true") != -1) { ServerPropities.ServerFile[ServerPropities.ServerFile.IndexOf("enforce-whitelist=true")] = "enforce-whitelist=" + Whitelist.IsOn; }

            if (ServerPropities.ServerFile.IndexOf("online-mode=false") != -1) { ServerPropities.ServerFile[ServerPropities.ServerFile.IndexOf("online-mode=false")] = "online-mode=" + OnlineMode.IsOn; }
            else if (ServerPropities.ServerFile.IndexOf("online-mode=true") != -1) { ServerPropities.ServerFile[ServerPropities.ServerFile.IndexOf("online-mode=true")] = "online-mode=" + OnlineMode.IsOn; }

            if (ServerPropities.ServerFile.Contains("difficulty=peaceful")) { ServerPropities.ServerFile[ServerPropities.ServerFile.IndexOf("difficulty=peaceful")] = "difficulty=" + Difficulty.Text; }
            else if (ServerPropities.ServerFile.Contains("difficulty=easy")) { ServerPropities.ServerFile[ServerPropities.ServerFile.IndexOf("difficulty=easy")] = "difficulty=" + Difficulty.Text; }
            else if (ServerPropities.ServerFile.Contains("difficulty=normal")) { ServerPropities.ServerFile[ServerPropities.ServerFile.IndexOf("difficulty=normal")] = "difficulty=" + Difficulty.Text; }
            else if (ServerPropities.ServerFile.Contains("difficulty=hard")) { ServerPropities.ServerFile[ServerPropities.ServerFile.IndexOf("difficulty=hard")] = "difficulty=" + Difficulty.Text; }
            else { Difficulty.IsEnabled = false; }

            if (ServerPropities.ServerFile.Contains("gamemode=survival")) { ServerPropities.ServerFile[ServerPropities.ServerFile.IndexOf("gamemode=survival")] = "gamemode=" + Gamemode.Text; }
            else if (ServerPropities.ServerFile.Contains("gamemode=creative")) { ServerPropities.ServerFile[ServerPropities.ServerFile.IndexOf("gamemode=creative")] = "gamemode=" + Gamemode.Text; }

            for (int i = 0; i < ServerPropities.ServerFile.Count; i++)
            {
                if(ServerPropities.ServerFile[i][0..4] == "motd") { ServerPropities.ServerFile[i] = "motd=" + MOTD.Text; break; }
            }


            List<String> FileToWrite = new();
            FileToWrite.AddRange(ServerPropities.ServerFile.ConvertAll(converter: low => low.ToLowerInvariant())); //Converts entire list to lowercase
            System.IO.File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\Servers\\" + ServerInfo.Label + "\\server.properties", FileToWrite);
        }
    }

    class ServerPropities { public static List<String> ServerFile = new(); }
}
