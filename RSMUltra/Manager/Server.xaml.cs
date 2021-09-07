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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RSMUltra.Manager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Server : Page
    {
        public Server()
        {
            this.InitializeComponent();
            CmdBar.IsOpen = true;
        }

        //Shows CMDBar when pointer has entered
        private void CmdBarFix(object? sender, object e) { CmdBar.IsOpen = true; }

        //Hides CMDBar when pointer has left
        private void CmdBarUnfix(object sender, PointerRoutedEventArgs e) { CmdBar.IsOpen = false; }

        private void StartServer(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = false;
            Stop.IsEnabled = true;

            ServerInfo.Server.StartInfo.RedirectStandardInput = true;
            ServerInfo.Server.StartInfo.RedirectStandardError = true;
            ServerInfo.Server.StartInfo.RedirectStandardOutput = true;
            ServerInfo.Server.StartInfo.CreateNoWindow = false; //Might want to set this to true when debugging to see if the server is actually running
            ServerInfo.Server.StartInfo.WorkingDirectory = Global.ServerDir;

            switch (ServerInfo.Game)
            {
                case "Minecraft Java Edition":
                    if (ServerInfo.Variant == "Forge")
                    {
                        ServerInfo.Server.StartInfo.FileName = Global.Java8; //Forge doesn't work on Java8
                    }
                    else
                    {
                        ServerInfo.Server.StartInfo.FileName = Global.Java16; //Everything else is cool with Java16
                    }
                    ServerInfo.Server.StartInfo.Arguments = "-Xmx" + Convert.ToInt32((LibRarisma.Tools.GetRAM() / 2) - 1024) + "M -jar \"" + Global.ServerDir + "\\Server.jar" + "\" nogui";
                    break;


            }

            ServerInfo.Server.Start();
        }

    }
}
