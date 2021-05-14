using ModernWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RSM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark; //Forces darkmode, lightmode should be added at somepoint
            ThemeManager.Current.AccentColor = Colors.White;
            UserDisplay.Content = new RSMGeneric.UI.LaunchPage();
        }

        public static void Manager() //For some reason if the server.propities file isn't found
        { //it will still load the page even though the code tries to send it back, so this is called instead
            ((MainWindow)Application.Current.MainWindow).UserDisplay.Content = new RSMGeneric.UI.ServerManger();
            ModernWpf.MessageBox.Show("Failed to find server.propities file, try loading the server, closing it and then come back.");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ServerInfo.Running == true)
            {
                e.Cancel = true;
                ModernWpf.MessageBox.Show("Please close the server before closing RSM");
            }
        }
    }
}

/*
Hi
You might have noticed a sudden rebrand from Simple Server Manager to Rarismas Server Manager
I fucking hate this, I dont want my name to be intriniscally attached to this, as I want it to
be a community project. However Simple Server Manager doesnt even show up on Page 5 of fucking google.
So if you have an alternate name, that isn't fucking stupid and has little to no searches on google
then please contact me at Rarisma#3767 on discord.

Also terraria is gonna be wierd as Im helping TShock test -diffculty flag and -redirect flag which would 
solve all of our problems as I could just redirect the output

Jake.
Jake Rarisma. */