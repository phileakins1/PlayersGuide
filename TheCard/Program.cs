// Ignore Spelling: Colour

public class Program
{
    private static void Main()
    {
        Console.Title = "The Card.";

        // Enumerate through each of the enums.
        foreach (int i in Enum.GetValues(typeof(Colour)))
        {
            foreach (int j in Enum.GetValues(typeof(Rank)))
            {
                Card card = new((Colour)i, (Rank)j);
                Console.WriteLine($"The {card.CardColour} {card.CardRank}");
            }
        }
    }
}

public class Card
{
    public Colour CardColour { get; init; }
    public Rank CardRank { get; init; }

    public Card(Colour cardColour, Rank cardRank)
    {
        CardColour = cardColour;
        CardRank = cardRank;
    }

    public bool IsNumericCard()
    {
        if ((int) CardRank is >= 1 and <= 10)
        {
            return true;
        }
        return false;
    }
}

public enum Colour { Red, Green, Blue, Yellow };
public enum Rank { Ace = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Dollar, Percent, Caret, Ampersand }
