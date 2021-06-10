using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace RSM2.Creator
{
    public partial class Difficulty : UserControl
    {
        public Difficulty()
        {
            AvaloniaXamlLoader.Load(this);
            ComboBox DifficultyCombobox = this.Find<ComboBox>("DifficultyCombox");
            switch (ServerInfo.Game)
            {
                case "Terraria":
                    string[] AcceptedDifficulties = { "Journey", "Classic", "Expert", "Master" };
                    DifficultyCombobox.Items = AcceptedDifficulties;
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
                ServerInfo.MainWindow.Content = new UI.Downloader();

            }
        }
        private void GoBack(object sender, RoutedEventArgs e) { if (ServerInfo.Difficulty != "Not Set") { ServerInfo.MainWindow.Content = new Version(); } }
    }
}
