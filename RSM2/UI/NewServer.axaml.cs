using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.IO;

namespace RSM2.UI
{
    public partial class NewServer : UserControl
    {
        public NewServer()
        {
            AvaloniaXamlLoader.Load(this);
            ComboBox GameCombobox = this.Find<ComboBox>("Games");
            GameCombobox.Items = new string[] { "Minecraft Java (Modded) - Currently broken", "Minecraft Java", "Minecraft Bedrock", "Terraria", "Factorio"};
        }

        private void Continue(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Servers"); //Tries to make a servers folder
            TextBox ServerName = this.Find<TextBox>("Servername");
            ComboBox GameCombobox = this.Find<ComboBox>("Games");
            CheckBox AutomodeCheck = this.Find<CheckBox>("Auto");
            ServerInfo.Name = ServerName.Text;
            try { ServerInfo.Game = GameCombobox.SelectedItem.ToString(); } catch { return; }
            try
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\" + ServerInfo.Name);
                Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\" + ServerInfo.Name);
            }
            catch { ServerName.Watermark = "You can't call your server that!"; ServerName.Text = "";  return;  }



            if (ServerInfo.Name != "None Set" && ServerInfo.Game != "None Set" && AutomodeCheck.IsChecked == false)
            {
                if (ServerInfo.Game == "Minecraft Java") { ServerInfo.Variant = "Vanilla"; ServerInfo.MainWindow.Content = new Creator.RAM(); }
                else if (ServerInfo.Game == "Minecraft Java (Modded)") { ServerInfo.Game = "Minecraft Java"; ServerInfo.Variant = "Modded"; ServerInfo.MainWindow.Content = new Creator.RAM(); }
                else if (ServerInfo.Game == "Minecraft Bedrock") { Automode.CreateBedrockServer(); }
                else if (ServerInfo.Game == "Terraria") { ServerInfo.MainWindow.Content = new Creator.WorldSize(); }
                else if (ServerInfo.Game == "Factorio") { ServerInfo.Variant = "Vanilla"; ServerInfo.AppID = "427520"; ServerInfo.MainWindow.Content = new Server.Steam(); }
            }
            else if (ServerInfo.Name != "None Set" && ServerInfo.Game != "None Set" && AutomodeCheck.IsChecked == true)
            {
                if (ServerInfo.Game == "Minecraft Java") { Automode.CreatePaperServer(); }
                else if (ServerInfo.Game == "Minecraft Java (Modded)") { ServerInfo.Game = "Minecraft Java";  Automode.CreateForgeServer(); }
                else if (ServerInfo.Game == "Minecraft Bedrock") { Automode.CreateBedrockServer(); }
                else if (ServerInfo.Game == "Terraria") { Automode.CreateTerrariaServer(); }
            }

        } 

        private void GoBack(object sender, RoutedEventArgs e){ ServerInfo.MainWindow.Content = new Welcome();}

    }
}
