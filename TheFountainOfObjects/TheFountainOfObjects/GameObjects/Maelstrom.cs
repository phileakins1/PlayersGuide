// See https://aka.ms/new-console-template for more information

// Ignore Spelling: Amarok Amarok

// Experimental static usings.
using static System.Console;

namespace TheFountainOfObjects.GameObjects
{
    public class Maelstrom : ICave
    {
        public string Description { get; private set; }

        public Maelstrom()
        {
            Description = "You have encountered A Maelstrom and been blown to another cave!";
        }

        public override string ToString()
        {
            ForegroundColor = ConsoleColor.Green;
            WriteLine(Description);
            ResetColor();
            return string.Empty;
        }
    }
}