// Ignore Spelling: Colour

internal class Program
{
    private static void Main(string[] args)
    {
        Colour _colour = new(155, 0, 0);
        Console.WriteLine($"Red = {_colour.RedChannel}, {_colour.GreenChannel}, {_colour.BlueChannel}");

        Colour _blue = Colour.Blue();
        Console.WriteLine($"Blue = {_blue.RedChannel}, {_blue.GreenChannel}, {_blue.BlueChannel}");
    }
}

internal class Colour
{
    internal int RedChannel { get; set; }
    internal int GreenChannel { get; set; }
    internal int BlueChannel { get; set; }

    internal Colour(int red, int green, int blue)
    {
        RedChannel = red;
        GreenChannel = green;
        BlueChannel = blue;
    }

    internal static Colour White()
    {
        return new Colour(255, 255, 255);
    }
    internal static Colour Black()
    {
        return new Colour(0, 0, 0);
    }
    internal static Colour Red()
    {
        return new Colour(255, 0, 0);
    }
    internal static Colour Orange()
    {
        return new Colour(255, 165, 0);
    }
    internal static Colour Yellow()
    {
        return new Colour(255, 2550, 0);
    }
    internal static Colour Green()
    {
        return new Colour(0, 128, 0);
    }
    internal static Colour Blue()
    {
        return new Colour(0, 0, 255);
    }
    internal static Colour Purple()
    {
        return new Colour(128, 0, 128);
    }
}

