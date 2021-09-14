using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
//This page controls the server settings.
//Do you random person reading this ever get strange kind of thoughts at night?
namespace RSMUltra.Manager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
            RAMSlider.Maximum = LibRarisma.Tools.GetRAM();
            RAMSlider.TickFrequency = LibRarisma.Tools.GetRAM() / (LibRarisma.Tools.GetRAM() / 1024);
            RAMSlider.Value = Convert.ToInt32(ServerInfo.AllocatedRAM);

            ServerName.Text = ServerInfo.Name;
            ServerName.PlaceholderText = ServerInfo.Name;

            try
            {
                if (!ServerInfo.Server.HasExited) //Prevents issues.
                {
                    SaveButton.Content = "You have to stop the server before saving your settings.";
                    SaveButton.IsEnabled = false;
                }
            }
            catch
            {

            }


            switch (ServerInfo.Game)
            {
                case "Minecraft Java Edition":
                    try
                    {
                        ServerProps.Text = File.ReadAllText(Global.ServerDir + "\\server.properties");
                    }
                    catch
                    {
                        ServerProps.Opacity = 0;
                        ServerProps.IsEnabled = false;
                    }
                    break;
            }
        }

        private void RAMAllocationChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            RAMSlider.Header = "Allocated RAM " + RAMSlider.Value + " MB";
        }

        private void Save(object sender, RoutedEventArgs e) //TODO Add WorldSize 
        {
            ServerInfo.AllocatedRAM = RAMSlider.Value.ToString();

            if (ServerProps.IsEnabled) //Only writes Server.props if it exists
            {
                File.WriteAllText(Global.ServerDir + "\\server.properties", ServerProps.Text);
            }
            File.WriteAllText(Global.ServerDir + "//RSM.ini", $"RSMUltra info file\n{ServerInfo.Game}\n{ServerInfo.Version}\n{ServerInfo.Variant}\n{ServerInfo.BackupFrequency}\n{ServerInfo.LastBackup}\n{ServerInfo.AllocatedRAM}\nWORLD PLACEHOLDER");

            //Reloads main page to force reload of the sidebar if name is changed
            if (ServerName.Text != ServerInfo.Name)
            {
                MainWindow.Frame.Content = new UltraUI.Main();
            }
        }
    }
}
