using System.Windows.Controls;

namespace RSM.RSMGeneric.UI
{
    /// <summary>
    /// Interaction logic for ServerConfig.xaml
    /// </summary>
    public partial class ServerConfig : Page
    {
        public ServerConfig()
        {
            InitializeComponent();
            ServerName.Text = ServerInfo.Label + " configuration";

            //Sets the page for general
            switch (ServerInfo.Game)
            {
                case "Minecraft Java": PerGameSettings.Content = new PerGameSettings.Minecraft(); break;
            }
        }

        private void Save(object sender, System.Windows.RoutedEventArgs e)
        {
            switch()
            ServerInfo.BackupFrequency = 
        }
    }
}
