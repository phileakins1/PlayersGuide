// See https://aka.ms/new-console-template for more information

// Ignore Spelling: Amarok Amarok

// Experimental static usings.
using static System.Console;

namespace TheFountainOfObjects.GameObjects
{
    public class EmptyCave : ICave
    {
        public string Description { get; private set; }

        public EmptyCave()
        {
            Description = "A creepy, dark, pitted  and deserted cave. Move along - nothing to see here!";
        }

        public override string ToString()
        {
            ForegroundColor = ConsoleColor.Gray;
            WriteLine(Description);
            ResetColor();

            return string.Empty;
        }
    }
}