using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CommunityToolkit.Mvvm.DependencyInjection;
using RSM.Data;
using RSM.Models;
using Variant = RSM.Models.Variant;

namespace RSM;
//Warnings?
//No lad, those are mere suggestions.
public static class XMLParser
{
    private static Global GlobalVM = Ioc.Default.GetService<Global>();

    private static void CopyDefaultFile()
    {
        if (File.Exists(Path.Combine(Data.Directories.Repositories, "RSM.XML"))) { File.Delete(System.IO.Path.Combine(Data.Directories.Repositories, "RSM.XML")); }
        Directory.CreateDirectory(Data.Directories.Repositories);
        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("RSM.RSMDefault.xml");
        var fileStream = File.Create(Path.Combine(Data.Directories.Repositories, "RSM.XML"));
        stream.Seek(0, SeekOrigin.Begin);
        stream.CopyTo(fileStream);
        fileStream.Close();
    }

    public static void UpdateRepositoryFiles()
    {
        CopyDefaultFile();
        GlobalVM.AvailableServers.Clear();
        GlobalVM.AvailableDependencies.Clear();
        foreach (string file in Directory.GetFiles(Data.Directories.Repositories))
        {
            XmlDocument doc = new();
            doc.Load(file);
            foreach (XmlNode DependencyNodes in doc.SelectNodes("Repository/Dependency")) { ParseDependencyNodes(DependencyNodes); }
            foreach (XmlNode ServerGroupNodes in doc.SelectNodes("Repository/ServerGroup")) { ParseServerGroup(ServerGroupNodes); }
        }
    }

    private static void ParseDependencyNodes(XmlNode Node)
    {
        Guid guid = Guid.Parse(Node.SelectSingleNode("GUID").InnerText);
        GlobalVM.AvailableDependencies.Add(guid,new Dependency(
            Node.SelectSingleNode("Name").InnerText, 
            Node.SelectSingleNode("URL").InnerText, 
            Node.SelectSingleNode("EntryPoint").InnerText, 
            guid));

    }

    private static void ParseServerGroup(XmlNode Node)
    {
        ServerGroup NewServerGroup = new();
        NewServerGroup = new()
        {
            Type = Node.SelectSingleNode("Type").InnerText,
            DefaultPort = int.Parse(Node.SelectSingleNode("IdealPort").InnerText),
            DownloadExtension = Node.SelectSingleNode("DownloadExtension").InnerText,
            LaunchCommand = Node.SelectSingleNode("LaunchCommand").InnerText,
            AvailableVariants = new()
        };

        if (Node.SelectSingleNode("LaunchCommand").SelectNodes("DefaultRAM").Count > 0) { NewServerGroup.DefaultRAM = int.Parse(Node.SelectSingleNode("DefaultRAM").InnerText); }

        //Checks if Zipped attribute exists
        if (Node.SelectNodes("Zipped").Count == 1) { NewServerGroup.IsDownloadZipped = bool.Parse(Node.SelectSingleNode("Zipped").InnerText); }

        //Checks if Eula attribute exists
        if (Node.SelectNodes("Eula").Count == 1) { NewServerGroup.EULA = Node.SelectSingleNode("Eula").InnerText; }
            
        if (Node.SelectNodes("GUID").Count == 1) { NewServerGroup.Guid = Guid.Parse(Node.SelectSingleNode("GUID").InnerText); }

        if (Node.SelectNodes("DesiredToolChain").Count == 1) { NewServerGroup.RequiredDependency = Guid.Parse(Node.SelectSingleNode("DesiredToolChain").InnerText); }


        foreach (XmlNode VariantNode in Node.SelectNodes("Variant"))
        {
            NewServerGroup.AvailableVariants.Add(ParseVariants(VariantNode));
        }
        GlobalVM.AvailableServers.Add(NewServerGroup);
    }

    private static Variant ParseVariants(XmlNode VariantNode)
    {
        Variant Variant = new() {Name = VariantNode.SelectSingleNode("VariantName").InnerText };

        foreach (XmlNode VersionNode in VariantNode.SelectNodes("Version"))
        {
            Variant.Versions.Add(VersionNode.SelectSingleNode("VersionString").InnerText, VersionNode.SelectSingleNode("URL").InnerText);
        }

        return Variant;
    }
}