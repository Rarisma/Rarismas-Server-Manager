using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using System;
using System.Diagnostics;
using Microsoft.UI.Xaml;

namespace RSM;

public sealed partial class Info : Page
{
    Servers.Server Server;
    public Info(ref Servers.Server server)
    {
        this.InitializeComponent();
        Server = server;
        ServerSKU.Text = $"{server.Type} {server.Variant} {server.Version}";
        BackupInfo.Text = "The last backup was " + server.LastBackup;
        if (server.ShowRAM)
        {
            RAMSlider.Value = Server.MaxRAMLimit;
            RAMSlider.Maximum = Data.MachineTotalRAM - 2048;
        }

        switch (server.BackupFrequency)
        {
            case "Hourly": BackupCombo.SelectedIndex = 0; break;
            case "Weekly": BackupCombo.SelectedIndex = 2; break;
            case "Monthly": BackupCombo.SelectedIndex = 3; break;
            case "Never": BackupCombo.SelectedIndex = 4; break;
            default: BackupCombo.SelectedIndex = 1; break;
        }
    }

    private void RAMSlider_OnValueChanged(object sender, RangeBaseValueChangedEventArgs e)
    {
        if (RAMDisplay == null) { return; ; }

        RAMDisplay.Text = "Allocated: " + RAMSlider.Value + "MB";
        Server.MaxRAMLimit = (int)RAMSlider.Value;
        Server.WriteINI();
    }

    private void MakeBackupClick(object sender, RoutedEventArgs e)
    {
        Server.MakeBackup();
        BackupInfo.Text = "The last backup was " + Server.LastBackup;
    }

    private void OpenBackupFolder(object sender, RoutedEventArgs e) { Process.Start("explorer.exe", "/select, " + Data.Backups + "\\"); }

    private void BackupToggle(object sender, DependencyPropertyChangedEventArgs e)
    {
        Server.BackupEnabled = BackupEnabled.IsOn;
        Server.WriteINI();
    }

    private void BackupChanged(object sender, SelectionChangedEventArgs e)
    {
        if (BackupCombo == null || Server.BackupFrequency == null) { return;;}
        Server.BackupFrequency = BackupCombo.SelectionBoxItem.ToString();
        Server.WriteINI();
    }
}