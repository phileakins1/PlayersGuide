// See https://aka.ms/new-console-template for more information

// Ignore Spelling: Amarok Amarok

// Experimental static usings.
using static System.Console;

namespace TheFountainOfObjects.GameObjects
{
    public class BottomlessPit : ICave
    {
        public string Description { get; private set; }

        public BottomlessPit()
        {
            Description = "You have blundered into a bottomless pit. So sad!  See you in the next life. Bye ........ ";
        }

        public override string ToString()
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine(Description);
            ResetColor();
            return string.Empty;
        }
    }
}