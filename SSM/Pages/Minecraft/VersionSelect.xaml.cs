using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SSM.Pages.Minecraft_Java
{
    /// <summary>
    /// Interaction logic for VersionSelect.xaml
    /// </summary>
    /// 

    class Servers
    {
        public static List<String> URLs = new();
    }

    public partial class VersionSelect : UserControl
    {
        public VersionSelect()
        {
            InitializeComponent();
            Variants.Items.Add("Paper (Recomended)");
            Variants.Items.Add("Forge (Modded)");
            Variants.Items.Add("Vanilla");
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/Paper", AppDomain.CurrentDomain.BaseDirectory + "//Cache//","Paper");
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/Stock", AppDomain.CurrentDomain.BaseDirectory + "//Cache//","Stock");
            LibRarisma.IO.DownloadFile("https://raw.githubusercontent.com/Rarisma/Simple-Server-Manager/main/ServerFiles/Minecraft/Forge", AppDomain.CurrentDomain.BaseDirectory + "//Cache//","Forge");
        }

        private void VariantsUpdated(object sender, SelectionChangedEventArgs e)
        {
            List<String> VersionSorter = new();
            Servers.URLs.Clear();
            VersionSorter.Clear();
            switch (Variants.SelectedValue)
            {
                case "Paper (Recomended)":
                    Description.Text = "Paper is an improved version of the vanilla hosting software, it's much more efficent and supports plugins!";
                    ServerInfo.ServerVariant = "Paper";
                    VersionSorter.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Paper"));
                    break;

                case "Vanilla":
                    Description.Text = "This is the vanilla hosting software made by Mojang. Unless you need it for a spesific reason you should use paper.";
                    ServerInfo.ServerVariant = "Paper";
                    VersionSorter.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Stock"));
                    break;

                case "Forge (Modded)":
                    Description.Text = "Modded minecraft allows you to install mods which will add new things to minecraft\nPlease note that this doesn't come with any mods.";
                    ServerInfo.ServerVariant = "Forge";
                    VersionSorter.AddRange(System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "//Cache//Forge"));
                    break;
            }
            VersionSorter.RemoveAll(str => string.IsNullOrEmpty(str)); //Update to use LibRarisma.Utils.Clean();
            for (int i = 0; i <= (VersionSorter.Count - 1); i++)
            {
                if (Convert.ToString(VersionSorter[i][0]) == "1") { Version.Items.Add(VersionSorter[i]); } //This adds version names
                else if (VersionSorter[i].Contains("https")) { Servers.URLs.Add(VersionSorter[i]); }
            }
        }

        private void VersionUpdated(object sender, SelectionChangedEventArgs e) 
        {
            ServerInfo.ServerVersion = Convert.ToString(Version.SelectedValue);
            ServerInfo.ServerURL = Servers.URLs[Version.SelectedIndex];
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            ServerInfo.ServerVersion = "Not set";
            ServerInfo.ServerURL = "Not set";
            ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new RamAllocation();
        }
        
        private void Continue(object sender, RoutedEventArgs e)
        {
            LibRarisma.IO.DownloadFile(ServerInfo.ServerURL, AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//", "Server.jar");
            SSMGeneric.Make_INI_File();
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.ServerLabel + "//" + "eula.txt", "#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula).\n# made by SSM\neula = true"); //Makes the EULA accepted
             
            if (ServerInfo.ServerVariant == "Forge")
            {
                ServerInfo.cmd.StartInfo.FileName = "cmd.exe";
                ServerInfo.cmd.StartInfo.RedirectStandardInput = true;
                ServerInfo.cmd.StartInfo.CreateNoWindow = false;
                ServerInfo.cmd.StartInfo.UseShellExecute = false;
                ServerInfo.cmd.Start();
                ServerInfo.cmd.StandardInput.WriteLine("cd Servers");
                ServerInfo.cmd.StandardInput.Flush();
                ServerInfo.cmd.StandardInput.WriteLine("cd " + ServerInfo.ServerLabel);
                ServerInfo.cmd.StandardInput.Flush();
                ServerInfo.cmd.StandardInput.WriteLine("java -jar Server.jar --installServer exit");
                ServerInfo.cmd.StandardInput.Flush();
            }

            ModernWpf.MessageBox.Show("Finished downloading server files");
            ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new SSM_GUI.Welcome();
        }

    }
}
