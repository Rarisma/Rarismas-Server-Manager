using Microsoft.UI.Xaml.Input;
using System;

namespace RSM;

public sealed partial class RSMConfig
{

    public string[] eggs = {
        "XAML by XAML",
        "Till we get it right",
        "Night of Nights",
        "Luna Nights",
        "Bullet Hell",
        "D3TOUR",
        "Pull Up",
        "StoryCAD",
        "Angry Server Developer",
        "Broadcasting live from the Midnight Channel",
        "Exit the Code Zone",
        "Too many hackers, some of yall gotta find another occupation",
        "Is that a trapezoid?",
        "The road to hell is paved with good intentions",
        "TIME, GENTLEMEN, PLEASE.",
        //Renderer.MainWindow.Width.ToString() + "x" + Renderer.MainWindow.Height.ToString(), 
        "How did you even find this",
        "I Shoulda Known!!",
        "Now (un)Banned in DirtCraft!", //LETS FUCKING GOOOO
        "I Make the World Spin",
        DateTime.Today.AddDays(1).ToString(), //Ultimate comedy moment
        "Forever one more day",
        $"Howdy {Environment.UserName}", //Ultimate comedy moment 2
        "24 hour party people",
        "Cloudspotter",
        "Now on GitHub!",
        "FES: The Answer",
        "GBA Enhanced!",
        "Incredibly Based Hotel and Casino",
        "Certified Hood Classic",
        "Virgin Paid Hosting vs Chad RSM User",
        "Scaled and Icy",
        "creamapi.inf",
        "Cloak and Dagger",
        "Matryoshka",
        "Heir to the code throne",
        "Your code won't last",
        "Taking my code and going home",
        "Input Unsanitised.",
        "No sharp edges",
        "It was a mistake to code here",
        "A splash of code to seal the deal",
        "Makin this much code aint easy",
        "Speedy code dealer",
        "Code all ye faithful",
        "Farther of lies code in disguise",
        "Sacred Code blade",
        "Fatal code theft",
        "Margret Thatcher the code snatcher",
        "all your code belong to us",
        "Manager of code",
        "Code slave back from the grave",
        "I tripped in the code keepers crypt",
        "The dark souls of code",
        "The code farther's secret stash",
        "Give me code or give me death",
        "Consume the code chalice", //Fuck everyone named alex.
        "The code collector",
        "Code framed and code blamed",
        "Wasting light",
        "Patience Echoes Silence and Grace",
        "Learn to Fly",
        "Still pouring one out for Meriu", //STILL POURING ONE OUT IN 2023!
        "RIGHT 2 REPAIR",
        "Minicraft server support never", //SIKE
        "Oasis by wonderwall",
        "Heartful cry",
        "Literally made on a steam link",
        "More based than pterodactyl",
        "You got the right to shut that shit up",
        "The server management incident",
        "Access is denied",
        "Old pound coin best coin",
        "Most code written at 2 am",
        "Plastic Texture pack",
        "Living with determination",
        "triple darkness",
        "Delete your search history",
        "Check out my OST at 192.168.0.1",
        "The test came back negative \n>mfw when its an IQ Test",
        "call 911\nwhats the number?",
        "Battle of Jake 2032",
        "Be there for Y2k38",
        "Developer accused of unreadable code refuses to comment.",
        "Yeah its CODING time!",
        "Thanks to all the geezers dedicated to hating microsoft",
        "To get to this level your gonna need a step stool",
        "Delicous pancakes",
        "Your family tree looks like recycling symbol",
        "Writing code at Midnight",
        "E for everyone but you.",
        "You tryna whip the polygon",
        "Internal Bread Service",
        "Federal Bread Investigation",
        "Old yellow bricks",
        "Virtual Insanity",
        "Lofty's Comet",
        "Everywhere at the end of time",
        "Letters from a day that tomorrow forgot.",
        "last tour",
        "DREAMLAND",
        "Deal with the devil",
        "End Game",
        "This code goes hard feel free to screenshot",
        "The code is a lie", //This was suggested by copilot
        "Big ships sink but my boat's afloat.",
        "Adblock, use it",
        "Fuck ben, dude is cringe." //fuck you ben.
    };

    private void EasterEgg(object sender, PointerRoutedEventArgs e)
    {
        Easter.Text = eggs[new Random().Next(0, eggs.Length)];
    }

    public RSMConfig() { InitializeComponent(); }
}