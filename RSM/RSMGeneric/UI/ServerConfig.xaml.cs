using System.Windows.Controls;
//If you ain't in on the RSM train, you shoulda came
namespace RSM.RSMGeneric.UI
{

    public partial class ServerConfig : Page
    {
        public ServerConfig()
        {
            InitializeComponent();
            ServerName.Text = ServerInfo.Label + " configuration";

            //Sets the page for general
            switch (ServerInfo.Game)
            {
                case "Minecraft Java": PerGameSettings.Content = new PerGameSettings.Minecraft(); break ;
            }
        }


        private void FrequencyOnLaunch(object sender, System.Windows.RoutedEventArgs e) { ServerInfo.BackupFrequency = "On Launch"; Utilities.Make_INI_File(); }
        private void FrequencyWeekly(object sender, System.Windows.RoutedEventArgs e) { ServerInfo.BackupFrequency = "Weekly"; Utilities.Make_INI_File(); }
        private void FrequencyMonthly(object sender, System.Windows.RoutedEventArgs e) { ServerInfo.BackupFrequency = "Monthly"; Utilities.Make_INI_File(); }
        private void FrequencyDisabled(object sender, System.Windows.RoutedEventArgs e) { ServerInfo.BackupFrequency = "Disabled"; Utilities.Make_INI_File(); }
    }
}
