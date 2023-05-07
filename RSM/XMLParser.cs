using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.DependencyInjection;
using RSM.Data;
using RSM.Models;

namespace RSM;
//Warnings?
//No lad, those are mere suggestions.
public static class XMLParser
{
    private static Global GlobalVM = Ioc.Default.GetService<Global>();

    private static void CopyDefaultFile()
    {
        //Delete file if it exists already
        if (File.Exists(Path.Combine(Directories.Repositories, "RSM.XML")))
        {
            File.Delete(Path.Combine(Directories.Repositories, "RSM.XML"));
        }

        //Create directory if it doesn't exist
        Directory.CreateDirectory(Directories.Repositories);

        //Summon file from void and copy it to the directory
        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("RSM.RSMDefault.xml");
        var fileStream = File.Create(Path.Combine(Directories.Repositories, "RSM.XML"));
        stream.Seek(0, SeekOrigin.Begin);
        stream.CopyTo(fileStream);

        //Close the filestream to unlock the file
        fileStream.Close();
    }

    public static void UpdateRepositoryFiles()
    {
        CopyDefaultFile();
        GlobalVM.AvailableServers.Clear();
        GlobalVM.AvailableDependencies.Clear();
        GlobalVM.AvailableDockerServers.Clear();
        foreach (string file in Directory.GetFiles(Directories.Repositories))
        {
            XmlDocument doc = new();
            doc.Load(file);
            foreach (XmlNode DependencyNodes in doc.SelectNodes("Repository/Dependency")) { ParseDependencyNodes(DependencyNodes); }
            foreach (XmlNode ServerGroupNodes in doc.SelectNodes("Repository/ServerGroup")) { ParseServerGroup(ServerGroupNodes); }
            foreach (XmlNode DockerNodes in doc.SelectNodes("Repository/Docker")) { ParseDocker(DockerNodes); }
        }
    }
    
    /// <summary>
    /// This parses docker XML
    /// </summary>
    private static void ParseDocker(XmlNode Node)
    {
        DockerModel Model;
        string name = Node.SelectSingleNode("DockerCatagory").InnerText;
        string link = Node.SelectSingleNode("DockerLink").InnerText;
        Model = new DockerModel(name, link);

        GlobalVM.AvailableDockerServers.Add(Model);
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