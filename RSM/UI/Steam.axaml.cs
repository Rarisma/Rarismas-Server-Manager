using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
//Thinking about renaming RSM to somthing that light eg Aluminium, Eg ASM
namespace RSM.UI
{
    public partial class Steam : UserControl
    {
        public Steam()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void Login(object sender, RoutedEventArgs e)
        {
            Global.User = this.Find<TextBox>("Name").Text;
            Global.Pass = this.Find<TextBox>("Pass").Text;
            Global.Display.Content = new UI.Downloader();
        }
    }
}
