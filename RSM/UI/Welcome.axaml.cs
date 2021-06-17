using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using Mono.Nat;

namespace RSM.UI
{
    public partial class Welcome : UserControl
    {
        public Welcome()
        {
            AvaloniaXamlLoader.Load(this);
            List<string> Servers = new();
            Servers.AddRange(new string[] { "+ Create a new server", "Import a server" });

            //This iterates through any folder inside //Servers// if it exists
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "//Servers//"))
            {
                Servers.Add("   "); //Adds a blank for clarity

                foreach (string dir in Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "//Servers//"))
                {
                    //Gets the name of the folder from the path if it has RSM.ini
                    if (File.Exists(dir + "//RSM.ini")) { Servers.Add(Path.GetFileName(dir)); }
                }
            }

            this.Find<ComboBox>("ServerList").Items = Servers; // Sets the comobobox to the servers
        }

        private void ServerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ServerList = this.Find<ComboBox>("ServerList"); //Finds the server combobox
            if (ServerList.SelectedItem.ToString() == "+ Create a new server") { Global.Display.Content = new Creator.New(); }
            else if (ServerList.SelectedItem.ToString() == "Import a server") 
            {

            } //Left empty until added
            else if (ServerList.SelectedIndex != 2)
            {
                Utilities.ReadINI(ServerList.SelectedItem.ToString()); //Fills server info
                Global.Display.Content = new Server.Manager();
            }
        }


    }
}