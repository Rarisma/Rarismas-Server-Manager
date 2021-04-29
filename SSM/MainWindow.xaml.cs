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

namespace SSM
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
            UserDisplay.Content = new Pages.SSM_GUI.Welcome();
        }

        public static void Manager() //For some reason if the server.propities file isn't found
        { //it will still load the page even though the code tries to send it back, so this is called instead
            ((MainWindow)System.Windows.Application.Current.MainWindow).UserDisplay.Content = new Pages.SSM_GUI.ServerManager();
            ModernWpf.MessageBox.Show("Failed to find server.propities file, try loading the server, closing it and then come back.");

        }
    }
}
