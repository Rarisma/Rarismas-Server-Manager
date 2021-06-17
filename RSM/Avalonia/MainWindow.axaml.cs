using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.ComponentModel;
//With a new rewrite of RSM comes a new slew of easter eggs (Bad Apple!!)
//This is actually a hard rewrite as in i'm going to organise shit
//and make this stuff documented
//This version has some more "radical" changes such as server info being split
namespace RSM
{
    public partial class MainWindow : Window
    {
        //Some avalonia initalisation code, all pages contain this
        public MainWindow() 
        {
            AvaloniaXamlLoader.Load(this);//I don't know what this really does 
            Global.Display = this.Find<ContentControl>("MainWindowControl"); //Exposes the content control to global
            Global.Display.Content = new UI.Welcome(); //Calls the welcome window so its not just blank 
        } 


        //This is ran when the program is told to close 
        public static void Closing(object sender, CancelEventArgs e) 
        {
            //ServerInfo.cmd.Close(); //Calls a function that requests a server to shutdown
        }
    }
}