// See https://aka.ms/new-console-template for more information

// Ignore Spelling: Amarok Amarok

// Experimental static usings.
using static System.Console;

namespace TheFountainOfObjects.Utilities
{
    public class SelectCavernSize
    {
        private SelectCavernSize() { }

        // TODO setup future X and Y choices for a custom size cavern

        /// <summary>
        /// Establishes the playing board size.
        /// </summary>
        /// <returns>A tuple with the row and column numerations for its creation.</returns>

        public static (int row, int column) GetCavernSize()
        {
            (int, int) _size = (0, 0);
            string? _choice;
            bool _validResponse = false;

            do
            {
                WriteBoardSizeMenu();
                Write("\n Enter size of board: > ");
                _choice = ReadLine()?.ToLower().Trim();

                if (_choice == "m")
                {
                    _size = (6, 6);
                    _validResponse = true;
                }
                else if (_choice == "l")
                {
                    _size = (8, 8);
                    _validResponse |= true;
                }
                else if (_choice == "s")
                {
                    _size = (4, 4);
                    _validResponse = true;
                }
                Clear();
                if (!_validResponse) WriteLine("Only L, M or S are acceptable entries!\n");
            } while (!_validResponse);

            return _size;
        }

        private static void WriteBoardSizeMenu()
        {
            WriteLine(
              """
                    Choose the size of the cavern
        
                    S for a small (4 X 4) cavern
                    M for a medium (6 X 6) cavern
                    L for a large (8 X 8) cavern
                    """);
        }
    }


}

