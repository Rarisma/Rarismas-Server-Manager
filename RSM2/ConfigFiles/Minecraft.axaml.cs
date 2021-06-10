using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace RSM2.ConfigFiles
{
    public partial class Minecraft : UserControl
    {
        public Minecraft()
        {
            AvaloniaXamlLoader.Load(this);
            ServerInfo.ConfigFileTextBox = this.Find<TextBox>("ServerFile");
            TextBox ServerFile = this.Find<TextBox>("ServerFile");
            ServerInfo.ConfigFileTextBox.Text = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Servers//" + ServerInfo.Name + "//Server.properties");
        }
    }
}

