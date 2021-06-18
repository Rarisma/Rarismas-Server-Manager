using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RSM.Game
{
    public partial class Terraria : UserControl
    {
        public Terraria()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.Find<Label>("AdminInfo").Content = Global.Admininfo;
        }
    }
}
