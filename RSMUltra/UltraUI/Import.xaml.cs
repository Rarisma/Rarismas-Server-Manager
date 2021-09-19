using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RSMUltra.UltraUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Import : Page
    {
        public static string Directory;
        public Import()
        {
            this.InitializeComponent();
        }

        private void OpenFolder(object sender, RoutedEventArgs e)
        {
            FolderPicker open = new();
            open.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            SelectedFolderName.Text = "Selected folder: " + open.PickSingleFolderAsync();

        }
    }
}
