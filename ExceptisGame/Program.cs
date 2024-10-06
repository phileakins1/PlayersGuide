
using CustomException;

Random rand = new();

int cookieNumber = rand.Next(0, 10);
List<int> guesses = [];

string player1 = "Player 1";
string player2 = "Player 2";
string currentPlayer = player1;


Console.WriteLine(" \nAvoid the oatmeal and raisin cookie: type in a guess between 0 and 9, duplicate guesses will be rejected.");

try
{
    do
    {
        int currentGuess = GetGuess(guesses, currentPlayer);
        if (currentGuess == cookieNumber)
        {
            throw new OatmealRaisinCookieFoundException();
        }

        if (currentPlayer == player1)
            currentPlayer = player2;
        else
            currentPlayer = player1;

    } while (true);
}

catch (OatmealRaisinCookieFoundException oat)
{
    Console.WriteLine("\n" + oat.Message);
}
catch (Exception ex)
{
    Console.WriteLine("\n" + ex.Message);
}
finally
{
    if (currentPlayer == player1)
        Console.WriteLine("\nPlayer2 wins!");
    else
        Console.WriteLine("\nPlayer1 wins!");
}

static int GetGuess(List<int> guesses, string player)
{
    do
    {
        Console.Write($"\n{player} type in your guess: >  ");
        string? guess = Console.ReadLine();

        if (int.TryParse(guess, out int currentGuess))
        {
            if (currentGuess >= 0 && currentGuess <= 9)
            {
                if (guesses.Contains(currentGuess))
                {
                    Console.WriteLine($"Your guess {currentGuess} has already been attempted. Try again\n");
                }
                else
                {
                    guesses.Add(currentGuess);
                    return currentGuess;
                }
            }
            else
            {
                Console.WriteLine($"Your guess of {currentGuess} is not within the range 0 .. 9 inclusive. Try again\n");
            }
        }
        else
        {
            Console.WriteLine($"{guess} is not a number! Try again.\n");
        }
    } while (true);
}


namespace CustomException
{
    public class OatmealRaisinCookieFoundException : Exception
    {
        public OatmealRaisinCookieFoundException() : base("The oatmeal and raisin cookie has been consumed! Never mind, its good for you.") { }
    }
}