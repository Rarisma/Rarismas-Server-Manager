using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace RSM.Creator
{
    public partial class Difficulty : UserControl
    {
        public Difficulty()
        {
            AvaloniaXamlLoader.Load(this);

            switch (ServerInfo.Game)
            {
                case "Terraria":
                    string[] AcceptedDifficulties = { "Journey", "Classic", "Expert", "Master" };
                    this.Find<ComboBox>("DifficultyCombox").Items = AcceptedDifficulties;
                    break;
            }

        }

        private void Continue(object sender, RoutedEventArgs e)
        {
            ComboBox DifficultyCombobox = this.Find<ComboBox>("DifficultyCombox");
            if (DifficultyCombobox.SelectedIndex != -1)
            {
                if (Convert.ToString(DifficultyCombobox.SelectedItem).Contains("Journey")) { ServerInfo.Difficulty = "3"; }
                else if (Convert.ToString(DifficultyCombobox.SelectedItem).Contains("Classic")) { ServerInfo.Difficulty = "0"; }
                else if (Convert.ToString(DifficultyCombobox.SelectedItem).Contains("Expert")) { ServerInfo.Difficulty = "1"; }
                else if (Convert.ToString(DifficultyCombobox.SelectedItem).Contains("Master")) { ServerInfo.Difficulty = "2"; }
                ServerInfo.Variant = "TShock";
                Global.Display.Content = new UI.Downloader();

            }
        }
        private void GoBack(object sender, RoutedEventArgs e) { if (ServerInfo.Difficulty != "Not Set") { Global.Display.Content = new Creator.Version(); } }

    }
}
