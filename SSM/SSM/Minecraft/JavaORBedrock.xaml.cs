using System.Windows;
using System.Windows.Controls;

//Massive fucking shoutout to the forge devs for bulling me
//Was going to add forge as a server type but you can't get
//straight serverfiles like you can with paper and stock minecraft
//They use shite like adfly to host their files
//IF PEOPLE ARE SMART ENOUGH TO INSTALL AND RUN FORGE THEY ARE SMART ENOUGH TO USE AN ADBLOCKER 


namespace SSM.Minecraft
{
    /// <summary>
    /// Interaction logic for JavaORBedrock.xaml
    /// </summary>
    public partial class JavaORBedrock : UserControl
    {
        public JavaORBedrock()
        {
            InitializeComponent();
        }

        private void Bedrock(object sender, RoutedEventArgs e) { MinecraftCreatorData.Edition = "Bedrock"; } //Sets edition to bedrock when clicked
        private void Java(object sender, RoutedEventArgs e) { MinecraftCreatorData.Edition = "Java"; } //Sets edition to java
    }
}
