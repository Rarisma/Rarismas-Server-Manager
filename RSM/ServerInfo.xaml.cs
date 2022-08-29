using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using System;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using RSM.Data;
using RSM.Models;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace RSM;

public sealed partial class Info : Page
{
    Server Server;
    private Global GlobalVM = Ioc.Default.GetService<Global>();

    public Info(ref Server server)
    {
        this.InitializeComponent();
        Server = server;
        ServerSKU.Text = Server.Branding;
        BackupInfo.Text = "The last backup was UNIMPLIMNETED";
        if (server.AllocatedRAM == -1)
        {
            RAMSlider.Value = Server.AllocatedRAM;
            RAMSlider.Maximum = GlobalVM.MachineTotalRAM - 2048;
        }

        /*switch (server.BackupFrequency)
        {
            case "Hourly": BackupCombo.SelectedIndex = 0; break;
            case "Weekly": BackupCombo.SelectedIndex = 2; break;
            case "Monthly": BackupCombo.SelectedIndex = 3; break;
            case "Never": BackupCombo.SelectedIndex = 4; break;
            default: BackupCombo.SelectedIndex = 1; break;
        }*/
    }

    private void RAMSlider_OnValueChanged(object sender, RangeBaseValueChangedEventArgs e)
    {
        if (RAMDisplay == null) { return; ; }

        RAMDisplay.Text = "Allocated: " + RAMSlider.Value + "MB";
        Server.AllocatedRAM = (int)RAMSlider.Value;
        Server.SaveConfiguration();
    }

    private void MakeBackupClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
        //Server.MakeBackup();
        //BackupInfo.Text = "The last backup was " + Server.LastBackup;
    }

    private void OpenBackupFolder(object sender, RoutedEventArgs e) { Process.Start("explorer.exe", "/select, " + Data.Directories.Backups + "\\"); }

    private void BackupToggle(object sender, DependencyPropertyChangedEventArgs e)
    {
        throw new NotImplementedException();
        //Server.BackupEnabled = BackupEnabled.IsOn;
        //Server.WriteINI();
    }

    private void BackupChanged(object sender, SelectionChangedEventArgs e)
    {
        throw new NotImplementedException();
        //if (BackupCombo == null || Server.BackupFrequency == null) { return;;}
        //Server.BackupFrequency = BackupCombo.SelectionBoxItem.ToString();
        //Server.WriteINI();
    }
}