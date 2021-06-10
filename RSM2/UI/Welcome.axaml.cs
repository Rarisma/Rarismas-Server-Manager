using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
//My shadow is gone, I deleted his gitter account
//Avalonia kept calling me back like I was hanging up on it

namespace RSM2.UI
{
    public partial class Welcome : UserControl
    {
        public Welcome()
        {
            AvaloniaXamlLoader.Load(this);

            var ServerList = this.Find<ComboBox>("ServerList");
            List<string> FilteredServers = new();
            FilteredServers.Add("+ Create a new server");
            FilteredServers.Add("Import a server");
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "//Servers//"))
            {
                string[] UnfilteredServers = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "//Servers//"); //Gets all directories but we only want ones that contain RSM.ini
                FilteredServers.Add("   ");
                for (int i = 0; i <= UnfilteredServers.Length - 1; i++) { if (File.Exists(UnfilteredServers[i] + "//RSM.ini")) { FilteredServers.Add(Path.GetFileName(UnfilteredServers[i])); } }
            }
            ServerList.Items = FilteredServers;
        }

        private void ServerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ServerList = this.Find<ComboBox>("ServerList");
            if (ServerList.SelectedItem.ToString() == "+ Create a new server" ) { ServerInfo.MainWindow.Content = new NewServer(); }
            else if (ServerList.SelectedItem.ToString() == "Import a server") { }
            else if (ServerList.SelectedIndex != 2)
            {
                Utilities.ReadINI(ServerList.SelectedItem.ToString());
                ServerInfo.MainWindow.Content = new Server.Manager();
            }
        }
    }
}
