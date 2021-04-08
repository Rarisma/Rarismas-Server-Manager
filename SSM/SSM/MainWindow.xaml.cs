using System.Threading;
using System.Threading.Tasks;
using System.Windows;
//Everywhere at the end of time is quite scary.
namespace SSM
{
    public class Public
    {
        public int PageCounter = 0;
        public int PageChecker = -1;
        public bool isJava;


    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Continue.Opacity = 0;
            //Manage.Opacity = 0;
            Manage.IsEnabled = false;
            PageWindow.Content = new WelcomePage();
        }

        public void MinecraftEditionSelect() { PageWindow.Content = new Minecraft.JavaORBedrock(); }

        private void NewButton(object sender, RoutedEventArgs e)
        {
            PageWindow.Content = new Minecraft.JavaORBedrock();
            Continue.IsEnabled = true;
            Continue.Opacity = 100;

            //Disables the New server and Manage button
            Manage.IsEnabled = false;
            New.IsEnabled = false;
            New.Opacity = 0;
            Manage.Opacity = 0;
        }

        private void ContinueButton(object sender, RoutedEventArgs e)
        {
            if (Minecraft.MinecraftCreatorData.ServerSetupChange == 0 && Minecraft.MinecraftCreatorData.Edition == "Java") { PageWindow.Content = new Minecraft.Java.ServerTypeSelect(); }
            else if (Minecraft.MinecraftCreatorData.ServerSetupChange == 1 && (Minecraft.MinecraftCreatorData.ServerType == "Paper" || Minecraft.MinecraftCreatorData.ServerType == "Stock")) { PageWindow.Content = new Minecraft.Java.ServerTypeSelect(); }
        }
    }
}
