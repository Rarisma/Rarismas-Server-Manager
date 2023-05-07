namespace RSM.Models;

public class DockerModel
{
    public string Name;
    public string Link;
    public  string Version = "Latest";

    public DockerModel(string name, string link)
    {
        this.Name = name;
        this.Link = link;
    }
}