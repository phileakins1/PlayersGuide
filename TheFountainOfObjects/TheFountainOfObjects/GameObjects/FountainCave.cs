// See https://aka.ms/new-console-template for more information

// Ignore Spelling: Amarok Amarok

// Experimental static usings.
using static System.Console;

namespace TheFountainOfObjects.GameObjects
{
    public class FountainCave : ICave
    {
        public string Description { get; private set; }

        public FountainCave()
        {
            Description = "You hear water dripping, The Fountain of Objects is here!";
        }

        public override string ToString()
        {
            ForegroundColor = ConsoleColor.Blue;
            WriteLine(Description);
            ResetColor();
            return string.Empty;
        }
    }
}