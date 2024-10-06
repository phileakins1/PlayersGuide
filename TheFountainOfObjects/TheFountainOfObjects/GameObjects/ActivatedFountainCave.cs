// See https://aka.ms/new-console-template for more information

// Ignore Spelling: Amarok Amarok

// Experimental static usings.
using static System.Console;

namespace TheFountainOfObjects.GameObjects
{
    public class ActivatedFountainCave : ICave
    {
        public string Description { get; private set; }

        public ActivatedFountainCave()
        {
            Description = "You hear rushing waters from the Fountain of Objects. It has been reactivated!";
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