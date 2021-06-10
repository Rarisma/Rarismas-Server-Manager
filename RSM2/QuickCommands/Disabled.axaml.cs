using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RSM2.QuickCommands
{
    public partial class Disabled : UserControl
    {
        public Disabled()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
