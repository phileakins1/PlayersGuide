// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


// Ignore Spelling: Coloured
// Ignore Spelling: Colour

Sword _sword = new();
Bow _bow = new();
Axe _axe = new();

ColouredItem<Sword> _genericSword = new(_sword, ConsoleColor.Blue);
_genericSword.Display();
Console.WriteLine(_genericSword.ToString());

ColouredItem<Bow> _genericBow = new(_bow, ConsoleColor.Red);
_genericBow.Display();
Console.WriteLine(_genericBow.ToString());


ColouredItem<Axe> _genericAxe = new(_axe, ConsoleColor.Green);
_genericAxe.Display();
Console.WriteLine(_genericAxe.ToString());

public class Sword { }
public class Bow { }
public class  Axe { }

public class ColouredItem<T> where T : class
{
    public T Item { get; set; }
    public ConsoleColor Colour { get; set; } 

    public ColouredItem(T item, ConsoleColor colour)
    {
        Item = item;
        Colour = colour;
    }

    public void Display()
    {
        Console.ForegroundColor = Colour;
    }
}