using System;
using System.Collections.Generic;

namespace RSM;
internal class Preferences
{
    public List<string> CustomURLs; //These are Custom repository URLs
    public bool DisableRSMDefault; //This decides if RSMDefault.XML is used
    public int UIMode; //This sets the ui

    public void WriteConfig()
    {
        string text = "CustomURLs=";
        foreach (var URL in CustomURLs) { text += URL + ","; }

        text += $"\nRSMDefaultDisable={DisableRSMDefault}";
        System.IO.File.WriteAllText(text, System.IO.Path.Combine(Data.Directories.RootFolder, "RSM.ini"));
    }

    public void ReadConfig()
    {
        string text = System.IO.File.ReadAllText(System.IO.Path.Combine(Data.Directories.RootFolder, "RSM.ini"));

        CustomURLs.Clear();
        foreach (var URL in text.Split("\n")[0].Split(",")) { CustomURLs.Add(URL); }

        DisableRSMDefault = Boolean.Parse(System.IO.Path.Combine(Data.Directories.RootFolder, "RSM.ini"));
    }
}