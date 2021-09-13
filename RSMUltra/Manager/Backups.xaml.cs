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
using Windows.Storage.Search;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RSMUltra.Manager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Backups : Page
    {
        private List<string> Dirs = new();
        public Backups()
        {
            InitializeComponent();

            if (Directory.Exists(Global.Backups + "//" + ServerInfo.Name + "//"))
            {
                foreach (var VARIABLE in Directory.GetDirectories(Global.Backups + "//" + ServerInfo.Name + "//"))
                {
                    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    string a = Path.GetFileName(VARIABLE);
                    BackupList.Items.Add(dateTime.AddSeconds(Convert.ToDouble(a)).ToLocalTime());
                    Dirs.Add(a);
                }

                BackupList.SelectedIndex = 0;
            }
            else
            {
                RestorePanel.Opacity = 0; 
            }

            switch (ServerInfo.BackupFrequency)
            {
                case "Daily":
                    Backupbox.SelectedIndex = 0;
                    break;
                case "Fortnightly":
                    Backupbox.SelectedIndex = 2;
                    break;
                case "Monthly":
                    Backupbox.SelectedIndex = 3;
                    break;
                case "Never":
                    Backupbox.SelectedIndex = 4;
                    break;
                default:
                    Backupbox.SelectedIndex = 1;
                    break;
            }
        }

        private void FrequencyUpdated(object sender, SelectionChangedEventArgs e)
        {
            ServerInfo.BackupFrequency = Backupbox.SelectedValue.ToString();
            File.WriteAllText(Global.ServerDir + "//RSM.ini", $"RSMUltra info file\n{ServerInfo.Game}\n{ServerInfo.Version}\n{ServerInfo.Version}\n{ServerInfo.BackupFrequency}\n{ServerInfo.LastBackup}\n{ServerInfo.AllocatedRAM}\nWORLD PLACEHOLDER");
        }

        private void BackupNow(object sender, RoutedEventArgs e)
        {
            ServerUtils.Backup();
            UltraUI.Manager.ServerFrame.Content = new Manager.Backups();
        }

        private void Restore(object sender, RoutedEventArgs e)
        {
            Directory.Delete(Global.ServerDir,true);
            Directory.Move(Global.Backups + "//" + Dirs[BackupList.SelectedIndex] + "//", Global.ServerDir);
            UltraUI.Manager.ServerFrame.Content = new Manager.Backups();

        }

    }
}
