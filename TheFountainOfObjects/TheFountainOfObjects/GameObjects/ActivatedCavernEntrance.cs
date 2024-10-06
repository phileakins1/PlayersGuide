// See https://aka.ms/new-console-template for more information

// Ignore Spelling: Amarok Amarok

// Experimental static usings.
using static System.Console;

namespace TheFountainOfObjects.GameObjects
{
    public class ActivatedCavernEntrance : ICave
    {
        public string Description { get; private set; }

        public ActivatedCavernEntrance()
        {
            Description = "The entrance cavern. \nThe Fountain of Objects has been re-activated. You have escaped with your life!\n";
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