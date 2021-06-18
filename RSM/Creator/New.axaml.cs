using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.IO;
//My shadow is gone, I deleted their gitter account
namespace RSM.Creator
{
    public partial class New : UserControl
    {
        public New()
        {
            AvaloniaXamlLoader.Load(this);
            //Loads the currently supported games into the game combobox
            this.Find<ComboBox>("Games").Items = new string[] { "Minecraft Java (Modded)", "Minecraft Java", "Minecraft Bedrock", "Terraria", "Factorio" };
        }

        private void NameCheck() //checks if a folder can be made
        {
            try
            {
                ServerInfo.Name = this.Find<TextBox>("Servername").Text;
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//");
                Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "//Servers//" + ServerInfo.Name + "//", true); //Deletes the folder if it was made
            }
            catch
            { 
                this.Find<TextBox>("Servername").Watermark = "You can't call your server that!"; 
                this.Find<TextBox>("Servername").Text = "";
                ServerInfo.Name = "None set";
                return; 
            }

        }

        private void Continue(object sender, RoutedEventArgs e)
        {
            NameCheck(); //Checks for name and write perms
            if (this.Find<ComboBox>("Games").SelectedIndex != -1) { ServerInfo.Game = this.Find<ComboBox>("Games").SelectedItem.ToString(); }
            else { return; }

            if (ServerInfo.Name != null && ServerInfo.Game != "None Set") //Checks that the inputs have been checked
            {
                if (this.Find<CheckBox>("Auto").IsChecked == false) //This sends the user to next page if automode isn't enabled
                {
                    if (ServerInfo.Game == "Minecraft Java") { ServerInfo.Variant = "Vanilla"; Global.Display.Content = new RAM(); }
                    else if (ServerInfo.Game == "Minecraft Java (Modded)") { ServerInfo.Game = "Minecraft Java"; ServerInfo.Variant = "Modded"; Global.Display.Content = new RAM(); }
                    else if (ServerInfo.Game == "Minecraft Bedrock") { Automode.Build(); }
                    else if (ServerInfo.Game == "Terraria") { Global.Display.Content = new WorldSize(); }
                    else if (ServerInfo.Game == "Factorio") { ServerInfo.Variant = "Vanilla"; Global.AppID = "427520"; Global.Display.Content = new UI.Steam(); }
                }
                else if (this.Find<CheckBox>("Auto").IsChecked == true) { Automode.Build(); } //Automode will handle everything from there
            }


        }

        private void GoBack(object sender, RoutedEventArgs e) { Global.Display.Content = new UI.Welcome(); }


    }
}
