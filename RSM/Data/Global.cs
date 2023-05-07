using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Mono.Nat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RSM.Models;
using WinUIEx;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RSM.Data;

public class Global : ObservableRecipient
{
    public List<int> OpenPorts = new();

    public long MachineTotalRAM;

    public Dictionary<Guid, Server> InstalledServers = new();
    public Dictionary<Guid, Dependency> AvailableDependencies = new();
    public List<ServerGroup> AvailableServers = new();
    public ObservableCollection<DockerModel> AvailableDockerServers = new();
    public string PublicIP { get => Router.GetExternalIP().ToString(); }
    public INatDevice Router;
    public WindowEx MainWindow = new();
    public Frame Content = new() { VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Stretch, VerticalContentAlignment = VerticalAlignment.Stretch };
    public string ShellComment = "Welcome to RSM";

    public ServerGroup SelectedServer { get; set; }
    public Variant SelectedVariant { get; set; }
    public string SelectedVersion { get; set; }
    public string ServerLabel { get; set; }
}