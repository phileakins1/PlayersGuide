using System.Text;

Console.Title = "Pack Inventory";

Pack _container = new(maxWeight: 11.0f, maxVolume: 9.0f, maxAllowedItems: 7);

string _menu = """
    Select a number to add an item to the pack (9 to exit)

    1 - Arrow
    2 - Bow
    3 - Rope
    4 - Water
    5 - Food rations
    6 _ Sword

    9 To exit
    """;

string _option = "";
bool _IsAdded;

do
{
    Console.WriteLine(_menu);

    Console.Write("\n>  ");
    _option = Console.ReadLine()!;

    _IsAdded = _option switch
    {
        "1" => _container.Add(new Arrow()),
        "2" => _container.Add(new Bow()),
        "3" => _container.Add(new Rope()),
        "4" => _container.Add(new Water()),
        "5" => _container.Add(new Food()),
        "6" => _container.Add(new Sword()),
        _ => true
    };

    if (_option is not "9" && _IsAdded)
    {
        Console.WriteLine($"{_container.ToString()}");
        Console.WriteLine();
    }
    else
    {
        if (_container.CurrentItemCount > 0) Console.WriteLine("\nThe pack is closed.");
    }

} while (_option is not "9");


public class Pack
{
    public float MaxWeight { get; private set; }
    public float MaxVolume { get; private set; }
    public int MaxAllowedItems { get; private set; }
    public float CurrentWeight { get; set; }
    public float CurrentVolume { get; set; }
    public int CurrentItemCount { get; set; }

    readonly InventoryItem[] _packContents;

    public Pack(float maxWeight, float maxVolume, int maxAllowedItems)
    {
        MaxWeight = maxWeight;
        MaxVolume = maxVolume;
        MaxAllowedItems = maxAllowedItems;

        _packContents = new InventoryItem[MaxAllowedItems];
    }

    public bool Add(InventoryItem item)
    {
        if (CurrentItemCount == MaxAllowedItems)
        {
            Console.WriteLine($"\nAdding the {item.GetType().Name} exceeds the pack's capacity of {MaxAllowedItems} items.");
            return false;
        }

        if (CurrentVolume + item.Volume > MaxVolume)
        {
            Console.WriteLine($"\nAdding the {item.GetType().Name}: volume of {item.Volume} would exceed the pack's maximum volume of {MaxVolume}.");
            return false;
        }

        if (CurrentWeight + item.Weight > MaxWeight)
        {
            Console.WriteLine($"\nAdding the {item.GetType().Name}: weight of {item.Weight} exceeds the pack's maximum weight of {MaxWeight} .");
            return false;
        }

        Console.WriteLine($"\nThe {item.GetType().Name} was successfully added to the pack.");
        CurrentVolume += item.Volume;
        CurrentWeight += item.Weight;
        _packContents[CurrentItemCount] = item;
        CurrentItemCount++;
        return true;

    }



    public override string ToString()
    {
        int _arrows = 0, _bow = 0, _rope = 0, _water = 0, _food = 0, _sword = 0;

        StringBuilder _builder = new StringBuilder();

        for (int i = 0; i < CurrentItemCount; i++)
        {
            if (_packContents[i].ToString() == "Arrow") _arrows++;
            if (_packContents[i].ToString() == "Bow") _bow++;
            if (_packContents[i].ToString() == "Rope") _rope++;
            if (_packContents[i].ToString() == "Water") _water++;
            if (_packContents[i].ToString() == "Food") _food++;
            if (_packContents[i].ToString() == "Sword") _sword++;
        }

        if (_arrows > 0)
        {
            if (_builder.Length > 0) _builder.Append(", ");

            if (_arrows == 1)
            {
                _builder.Append("An arrow");
            }
            else
            {
                _builder.Append($"{_arrows} arrows");
            }
        }

        if (_bow > 0)
        {
            if (_builder.Length > 0) _builder.Append(", ");

            if (_bow == 1)
            {
                _builder.Append("A bow");
            }
            else
            {
                _builder.Append($"{_bow} bows");
            }
        }

        if (_rope > 0)
        {
            if (_builder.Length > 0) _builder.Append(", ");

            if (_rope == 1)
            {
                _builder.Append("A rope");
            }
            else
            {
                _builder.Append($"{_rope} ropes");
            }
        }

        if (_water > 0)
        {
            if (_builder.Length > 0) _builder.Append(", ");

            if (_water == 1)
            {
                _builder.Append("A water flask");
            }
            else
            {
                _builder.Append($"{_water} water flasks");
            }
        }

        if (_food > 0)
        {
            if (_builder.Length > 0) _builder.Append(", ");

            if (_food == 1)
            {
                _builder.Append("A day's food ration");
            }
            else
            {
                _builder.Append($"Rations for {_food} days");
            }
        }

        if (_sword > 0)
        {
            if (_builder.Length > 0) _builder.Append(", ");

            if (_sword == 1)
            {
                _builder.Append("A sword");
            }
            else
            {
                _builder.Append($"{_sword} swords");
            }
        }

        return ($"\nThe pack now contains {_builder.ToString()}");
    }
}

public abstract class InventoryItem
{
    public float Weight { get; set; }
    public float Volume { get; set; }
    protected InventoryItem(float weight, float volume)
    {
        Weight = weight;
        Volume = volume;
    }
}
public class Arrow : InventoryItem
{
    public Arrow() : base(0.1f, 0.05f) { }

    public override string ToString()
    {
        return "Arrow";
    }
}
public class Bow : InventoryItem
{
    public Bow() : base(1.0f, 4.0f) { }

    public override string ToString()
    {
        return "Bow";
    }
}
public class Rope : InventoryItem
{
    public Rope() : base(1.0f, 1.5f) { }

    public override string ToString()
    {
        return "Rope";
    }
}
public class Water : InventoryItem
{
    public Water() : base(2.0f, 3.0f) { }

    public override string ToString()
    {
        return "Water";
    }
}
public class Food : InventoryItem
{
    public Food() : base(1.0f, 0.5f) { }

    public override string ToString()
    {
        return "Food";
    }
}
public class Sword : InventoryItem
{
    public Sword() : base(5.0f, 3.0f) { }

    public override string ToString()
    {
        return "Sword";
    }
}