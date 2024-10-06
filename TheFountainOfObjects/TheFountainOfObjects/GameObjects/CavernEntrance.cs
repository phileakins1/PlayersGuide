// See https://aka.ms/new-console-template for more information

// Ignore Spelling: Amarok Amarok

// Experimental static usings.
using static System.Console;

namespace TheFountainOfObjects.GameObjects
{
    public class CavernEntrance : ICave
    {
        public string Description { get; private set; }

        public CavernEntrance()
        {
            Description = "You see a light coming from outside the cavern. This is the _entrance.";
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