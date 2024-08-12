
internal class Program()
{
    static void Main()
    {
        ThePoint thePoint = new ThePoint();
        Console.WriteLine($"X = {thePoint.X}, Y = {thePoint.Y}");
        ThePoint thePoint2 = new ThePoint(2, 3);
        Console.WriteLine($"X = {thePoint2.X}, Y = {thePoint2.Y}");
        ThePoint thePoint3 = new ThePoint(-4, 0);
        Console.WriteLine($"X = {thePoint3.X}, Y = {thePoint3.Y}");
    }
}

internal class ThePoint
{
    public int X { get; set; }
    public int Y { get; set; }

    public ThePoint()
    {
        X = 0;
        Y = 0;
    }

    public ThePoint(int x, int y)
    {
        X = x;
        Y = y;
    }
}