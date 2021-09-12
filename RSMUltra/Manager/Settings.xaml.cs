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

            switch (ServerInfo.Game)
            {
                case "Minecraft Java Edition":
                    ServerProps.Text = File.ReadAllText(Global.ServerDir + "\\server.properties");
                    break;
            }
        }

        private void RAMAllocationChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            RAMSlider.Header = "Allocated RAM " + RAMSlider.Value + " MB";
        }
    }
}
