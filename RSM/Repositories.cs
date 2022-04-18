using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RSM;
public class Repositories
{
    public List<ServerFile> PossibleServers = new();
    public List<ToolChain> AvailableToolChains = new();

    public struct ToolChain
    {
        public string Name;
        public string DownloadURL;
        public string EntryPoint;
    }
    public struct ServerFile
    {
        public string Type;
        public string Description;
        public string LaunchCommand;
        public ToolChain RequiredToolChain;
        public string ToolChainName;
        public List<Variant> Variants;
        public int IdealPort;
        public string Extension;
        public bool Zipped;
        public bool IsRamRequired;
        public string DownloadExtension;
        public string EntryPoint;
        public string EntryPointURL;
        public string Eula;
    }

    public struct Variant
    {
        public string CommandAutoFill;
        public string VariantName;
        public List<string[]> Versions;
        public string PostInstallCommand;
        public string LaunchFile;
    }

    public void GetRepoFiles()
    {
        CopyDefaultFile();
        foreach (string file in Directory.GetFiles(Data.Repositories))
        {
            XmlDocument doc = new();
            doc.Load(file);
            foreach (XmlNode ToolNodes in doc.SelectNodes("Repository/Loader")) { GetToolChains(ToolNodes); }
            foreach (XmlNode ServerNode in doc.SelectNodes("Repository/ServerGroup")) { ParseServerGroup(ServerNode); }
        }
    }

    private void GetToolChains(XmlNode Node)
    {
        AvailableToolChains.Add(new ToolChain()
        {
            Name = Node.SelectSingleNode("Name").InnerText,
            //Description = Node.SelectSingleNode("Description").InnerText,
            DownloadURL = Node.SelectSingleNode("URL").InnerText,
            EntryPoint = Node.SelectSingleNode("EntryPoint").InnerText,
            //FileType = Node.SelectSingleNode("FileType").InnerText
        });
    }
    private void CopyDefaultFile()
    {
        if (File.Exists(Path.Combine(Data.Repositories, "RSM.XML"))) { File.Delete(System.IO.Path.Combine(Data.Repositories, "RSM.XML")); }
        Directory.CreateDirectory(Data.Repositories);
        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("RSM.RSMDefault.xml");
        var fileStream = File.Create(Path.Combine(Data.Repositories, "RSM.XML"));
        stream.Seek(0, SeekOrigin.Begin);
        stream.CopyTo(fileStream);
        fileStream.Close();
    }
    private void ParseServerGroup(XmlNode Node)
    {
        ServerFile NewServerGroup = new();
        NewServerGroup.Variants = new();

        //Gets required nodes
        NewServerGroup.Type = Node.SelectSingleNode("Name").InnerText;
        NewServerGroup.Description = Node.SelectSingleNode("Description").InnerText;
        NewServerGroup.IdealPort = int.Parse(Node.SelectSingleNode("IdealPort").InnerText);
        NewServerGroup.DownloadExtension = Node.SelectSingleNode("DownloadExtension").InnerText;
        NewServerGroup.LaunchCommand = Node.SelectSingleNode("LaunchCommand").InnerText;
        NewServerGroup.IsRamRequired = bool.Parse(Node.SelectSingleNode("Zipped").InnerText);

        //Checks if Zipped attribute exists
        if (Node.SelectNodes("RamRequired").Count == 1) { NewServerGroup.IsRamRequired = bool.Parse(Node.SelectSingleNode("RamRequired").InnerText); }
        else { NewServerGroup.IsRamRequired = false; }

        //Checks if Zipped attribute exists
        if (Node.SelectNodes("Zipped").Count == 1) { NewServerGroup.Zipped = bool.Parse(Node.SelectSingleNode("Zipped").InnerText); }
        else { NewServerGroup.Zipped = false; }

        //Checks if Eula attribute exists
        if (Node.SelectNodes("Eula").Count == 1) { NewServerGroup.Eula = Node.SelectSingleNode("Eula").InnerText; }
        else { NewServerGroup.Eula = ""; }

        foreach (var ToolChain in AvailableToolChains)
        {
            if (ToolChain.Name == Node.SelectSingleNode("InstallType").InnerText)
            {
                NewServerGroup.EntryPointURL = ToolChain.DownloadURL;
                NewServerGroup.EntryPoint = ToolChain.EntryPoint;
                break;
            }
        }
        foreach (XmlNode VariantNode in Node.SelectNodes("Variant")) { NewServerGroup.Variants.Add(ParseVariants(VariantNode)); }
        PossibleServers.Add(NewServerGroup);
    }

    private Variant ParseVariants(XmlNode VariantNode)
    {
        Variant Variant = new()
        {
            Versions = new(),
            VariantName = VariantNode.SelectSingleNode("VariantName").InnerText
        };

        if (VariantNode.SelectNodes("QuickCommands").Count == 1) { Variant.CommandAutoFill = VariantNode.SelectSingleNode("QuickCommands").InnerText; }
        else { Variant.CommandAutoFill = ""; }
        
        //Override checks
        if (VariantNode.SelectNodes("PostInstallCommands").Count == 1) { Variant.PostInstallCommand = VariantNode.SelectSingleNode("PostInstallCommands").InnerText; }
        else { Variant.PostInstallCommand = ""; }

        if (VariantNode.SelectNodes("LaunchFile").Count == 1) { Variant.LaunchFile = VariantNode.SelectSingleNode("LaunchFile").InnerText; }
        else { Variant.LaunchFile = ""; }

        foreach (XmlNode VersionNode in VariantNode.SelectNodes("Version"))
        {
            Variant.CommandAutoFill = VariantNode.SelectSingleNode("QuickCommands").InnerText;
            Variant.Versions.Add(new[]
            {
                    VersionNode.SelectSingleNode("VersionString").InnerText,
                    VersionNode.SelectSingleNode("URL").InnerText
            });
        }

        return Variant;
    }
}