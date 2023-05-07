using System.Collections.Generic;

namespace RSM.Models;

public class Variant
{
    public string Name { get; set; }
    public Dictionary<string, string> Versions = new();
}