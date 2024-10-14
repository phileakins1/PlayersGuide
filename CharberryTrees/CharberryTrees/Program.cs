// See https://aka.ms/new-console-template for more information

CharberryTree tree = new();
Notifier notifier = new(tree);
Harvester harvester = new(tree);


while (true)
{
    tree.MayBeGrow();
}


public class Notifier
{
    private static void OnRipened() => Console.WriteLine("The Charberry fruit has ripened.");

    public Notifier(CharberryTree tree)
    {
        tree.Ripened += OnRipened;
    }
}

public class Harvester
{
    private static CharberryTree? Tree { get; set; }
    private static void OnRipened()
    {
        Tree!.Ripe = false;
        Console.WriteLine("The fruit has been harvested.");
    }

    public Harvester(CharberryTree tree)
    {
        Tree = tree;
        tree.Ripened += OnRipened;
    }
}

public class CharberryTree
{
    private readonly Random _random = new();

    public event Action? Ripened;
    public bool Ripe { get; set; }

    public void MayBeGrow()
    {
        if (_random.NextDouble() < 0.00000001)
        {
            Ripe = true;
            Ripened();
        }
    }
}