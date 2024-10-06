// See https://aka.ms/new-console-template for more information

// Ignore Spelling: Amarok Amarok

// Experimental static usings.
using static System.Console;

namespace TheFountainOfObjects.GameObjects
{
    public class Amarok : ICave
    {
        public string Description { get; set; }

        public Amarok()
        {
            Description = "You have stumbled into an Amarok's lair and have instantly died. Too bad really ... Bye!";
        }

        public override string ToString()
        {
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine(Description);
            ResetColor();
            return string.Empty;
        }
    }
}