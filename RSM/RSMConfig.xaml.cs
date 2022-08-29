using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace RSM;

public sealed partial class RSMConfig : Page
{

    public string[] eggs = {
        "XAML by XAML",
        "Till we get it right",
        "Dees Gees",
        "We don't talk about RSM3.0",
        "Flowering Night",
        "Night of Nights",
        "Luna Nights",
        "Bullet Hell",
        "D3TOUR",
        "Pull Up",
        "Smackaveli",
        "Jet Set Radio Future",
        "StoryBuilder",
        "Angry Server Developer",
        "Greatest Mario Hoops 3 on 3 Player",
        "Broadcasting live from the Midnight Channel",
        "Where is Mindstreaming Vol 2?",
        "Exit the Code Zone",
        "Fuck Lex Manos", //Fuck lex manos.
        "Compulsive Gambler",
        "Too many hackers, some of yall gotta find another occupation",
        "Making a call out post on my twitter dot com",
        "Is that a trapezoid?",
        "The road to hell is paved with good intentions",
        "TIME, GENTLEMEN, PLEASE.",
        "256 Wednesdays Later",
        //Renderer.MainWindow.Width.ToString() + "x" + Renderer.MainWindow.Height.ToString(), 
        "How did you even find this",
        "Algorithmic with the execution",
        "All blood everything, blood red adidas",
        "I Shoulda Known!!",
        "Now (un)Banned in DirtCraft!",
        "I Make the World Spin",
        DateTime.Today.AddDays(1).ToString(), //Ultimate commedy moment
        "Forever one more day",
        "Hello " + Environment.UserName, //Ultimate commedy moment 2
        "Skill issue",
        "24 hour party people",
        "Cloudspotter",
        "Now on GitHub!",
        "LibRarisma!",
        "FES: The Answer",
        "GBA Enhanced!",
        "Included with your Nintendo Switch Online Subscription!",
        "Incredibly Based Hotel and Casino",
        "Certified Hood Classic",
        "Virgin Paid Hosting vs Chad RSM User",
        "Scaled and Icy",
        "creamapi.inf",
        "Cloak and Dagger",
        "Matryoshka",
        "'PRETTY DOPE ASS GAME' PLAYSTATION MAGAZINE MAY 2003 ISSUE ",
        "Heir to the code throne",
        "Your code won't last",
        "Taking my code and going home",
        "Input Unsanitised.",
        "No sharp edges",
        "DOES NOT SUPPORT ANDROID (yet).",
        "Now featuring Rarisma from the DirtCraft may cry series",
        "Pokemon snap back to reality",
        "It was a mistake to code here",
        "All consuming lord of code",
        "A splash of code to seal the deal",
        "Makin this much code aint easy",
        "Speedy code dealer",
        "Code all ye faithful",
        "Farther of lies code in disguise",
        "Sacred Code blade",
        "Fatal code theft",
        "Margret Thatcher the code snatcher",
        "all your code belong to us",
        "How many holes does your code have?",
        "Manager of code",
        "Code slave back from the grave",
        "I tripped in the code keepers crypt",
        "The dark souls of code",
        "Code drowning awareness day",
        "The code farther's secret stash",
        "Give me code or give me death",
        "Consume the code chalice", //Fuck everyone named alex.
        "Code scented candle",
        "The code collector",
        "Code framed and code blamed",
        "Wasting light",
        "Patience Echoes Silence and Grace",
        "Learn to Fly",
        "Def Jam Vendetta",
        "Splitgate",
        "Still pouring one out for Meriu", //Unfortunet
        "RIGHT 2 REPAIR",
        "Minicraft server support never", //SIKE
        "Only man to have ever eaten taco bell and not had shits",
        "Now on akinator",
        "Oasis by wonderwall",
        "There are no hot MILFs in your area",
        "Heartful cry",
        "Literally made on a steam link",
        "More based than pterodactyl",
        "Leaving XGP Nov 5th",
        "Tim Cuck",
        "Pure blood as in not breeding outside the family bloodline",
        "BEWARE OF THE PIPELINE",
        "Programming Socks",
        "You got the right to shut that shit up",
        "~HANGER!~",
        "Fellow guys, have you ever done this? Taking your pen apart when you have nothing better to do?",
        "The server management incident",
        "Fuck ass 711",
        "Access is denied",
        "Breaking news Local guy trolls ZR discord group over fake switch port",
        "Old pound coin best coin",
        "Most code written at 2 am",
        "Plastic Texture pack",
        "Will Komi ever communicate?", //no.
        "I am currently inside the midnight channel",
        "Living with determination",
        "DETERMINATION",
        "triple darkness",
        "Wii are resorting to violence",
        "Nintendo Switch calculator speedrun Any% Glitchless",
        "Delete your search history",
        "Check out my OST at 192.168.0.1",
        "Where a 255.255.255.255",
        "The test came back negative mfw when its an IQ Test",
        "Bellsprout",
        "Mario where smooth move",
        "call 911\nwhats the number?",
        "Battle of Jake 2032",
        "Be there for Y2k38",
        "When you hear a crunch in soft food",
        "Developer accused of unreadable code refuses to comment.",
        "Boarderline forever",
        "Tiktok sounds like an broken smg",
        "Yeah its CODING time!",
        "Thanks to all the geezers dedicated to hating microsoft",
        "To get to this level your gonna need a step stool",
        "Delicous pancakes",
        "Your family tree looks like recycling symbol",
        "Writing code at Midnight",
        "E for everyone but you.",
        "In support of Ukraine.",
        "Not available in Russia.",
        "In support of the LGBT community.",
        "Gotta file my taxes",
        "You tryna whip the polygon",
        "Internal Bread Service",
        "Federal Bread Investigation",
        "One pocketwatch of blood please.",
        "Big fat mommy milkers - Dakota",
        "My hovercraft!",
        "Hola, Me llamo Chris",
        "Curtosy of StackOverflow",
        "Old yellow bricks",
        "Virtual Insanity",
        "Lofty's Comet",
        "Everywhere at the end of time",
        "Letters from a day that tomorrow forgot.",
        "last tour",
        "You already know I'm rockin fox notes",
        "MAXIMA",
        "And now a word from our sponsor, LibRarisma, the most based package in C#",
        "Deal with the devil",
        "End Game",
        "WIGGLECORE",
        "This code goes hard feel free to screenshot"
        
    };

    private void EasterEgg(object sender, PointerRoutedEventArgs e)
    {
        Easter.Text = eggs[new Random().Next(0, eggs.Length)];
    }

    public RSMConfig() { InitializeComponent(); }
}