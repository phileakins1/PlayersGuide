// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

Console.Title = "Vin Fletcher's Arrows";

float _shaftCostPerCm = 0.05f;
float _cost;

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\n Vin Fletcher's Arrow Emporium.");
Console.ResetColor();

Console.WriteLine("\n Available arrows ...\n");

string _showTypes = 
    """
     1   A Custom Arrow - built to your design.
     2   An Elite Arrow.
     3   The Beginners' Arrow.
     4   A Marksmans' Arrow.
    """;

Console.Write($"{_showTypes} \n\n Enter the number to choose an arrow > ");

if (int.TryParse(Console.ReadLine(), out int _choice))
{
    switch (_choice)
    {
        case 1:
            Arrow customArrow = new(ChooseArrowhead(), ChooseFletching(), ChooseShaftLength());
            _cost = customArrow.GetCost(_shaftCostPerCm);
            Console.WriteLine($"\nThe cost of the custom arrow, a {customArrow.ShaftLength}cm arrow with " +
                $"{customArrow.ArrowHeadType} head and {customArrow.FletchingType} fletching is {_cost:##.##} gold");
            break;

        case 2:
            Arrow eliteArrow = CreateEliteArrow();
            _cost = eliteArrow.GetCost(_shaftCostPerCm);
            Console.WriteLine($"\nThe cost of the Elite arrow, a {eliteArrow.ShaftLength}cm arrow with " +
                $"{eliteArrow.ArrowHeadType} head and {eliteArrow.FletchingType} fletching is {_cost:##.##} gold");
            break;

        case 3:
            Arrow beginnerArrow = CreateBeginnerArrow();
            _cost = beginnerArrow.GetCost(_shaftCostPerCm);
            Console.WriteLine($"\nThe cost of the Beginners' arrow, a {beginnerArrow.ShaftLength}cm arrow with " +
                $"{beginnerArrow.ArrowHeadType} head and {beginnerArrow.FletchingType} fletching is {_cost:##.##} gold");
            break;

        case 4:
            Arrow marksmanArrow = CreateMarksmanArrow();
            _cost = marksmanArrow.GetCost(_shaftCostPerCm);
            Console.WriteLine($"\nThe cost of the Marksmans' arrow, a {marksmanArrow.ShaftLength}cm arrow with " +
                $"{marksmanArrow.ArrowHeadType} head and {marksmanArrow.FletchingType} fletching is {_cost:##.##} gold");
            break;

        default:
            Console.WriteLine("\n Unknown arrow choice - program ending.");
            break;
    }
}
else
    Console.WriteLine("\n\n Unknown entry - program ending.");


Arrowhead ChooseArrowhead()
{
    Console.WriteLine("\n Arrowhead types available:\n");
    int i = 1;

    foreach (var item in Enum.GetNames(typeof(Arrowhead)))
    {
        Console.WriteLine($"{i,2} {item,-20}");
        i++;
    }

    Console.Write(" Select by number > ");
    string? input = Console.ReadLine();

    Arrowhead response = input switch
    {
        "1" => Arrowhead.Steel,
        "2" => Arrowhead.Obsidian,
        "3" => Arrowhead.Wood,
        _ => Arrowhead.Wood
    };

    return response;
}

Fletching ChooseFletching()
{
    Console.WriteLine("\n Fletching types available:\n");
    int i = 1;

    foreach (var item in Enum.GetNames(typeof(Fletching)))
    {
        string fletchingType = item switch
        {
            "Plastic" => "Plastic",
            "TurkeyFeathers" => "Turkey Feathers",
            "GooseFeathers" => "Goose Feathers",
            _ => "Unknown fletching"
        };

        Console.WriteLine($"{i,2} {fletchingType,-20}");
        i++;
    }

    Console.Write("Select by number > ");
    string? input = Console.ReadLine();

    Fletching response = input switch
    {
        "1" => Fletching.Plastic,
        "2" => Fletching.TurkeyFeathers,
        "3" => Fletching.GooseFeathers,
        _ => Fletching.GooseFeathers
    };

    return response;
}

int ChooseShaftLength()
{
    int length;

    do
    {
        Console.Write("\nEnter the length of the arrow shaft in cm (60 .. 100) > ");

        if (int.TryParse(Console.ReadLine(), out length))
        {
            if (length is >= 60 and <= 100)
            {
                break;
            }
        }
    } while (true);

    return length;
}

static Arrow CreateEliteArrow()
{
    return new(Arrowhead.Steel, Fletching.Plastic, 95);
}

static Arrow CreateBeginnerArrow()
{
    return new(Arrowhead.Wood, Fletching.GooseFeathers, 75);
}

static Arrow CreateMarksmanArrow()
{
    return new(Arrowhead.Steel, Fletching.GooseFeathers, 65);
}

internal class Arrow
{
    internal Arrowhead ArrowHeadType { get; private set; }
    internal Fletching FletchingType { get; private set; }
    internal int ShaftLength { get; private set; }

    internal Arrow(Arrowhead arrowHead, Fletching fletching, int shaftLength)
    {
        ArrowHeadType = arrowHead;
        FletchingType = fletching;
        ShaftLength = shaftLength;
    }

    internal float GetCost(float shaftCostPerCm)
    {
        float arrowCost = ShaftLength * shaftCostPerCm;

        if (ArrowHeadType == Arrowhead.Steel)
        {
            arrowCost += 10;
        }
        else if (ArrowHeadType == Arrowhead.Obsidian)
        {
            arrowCost += 5;
        }
        else
        {
            arrowCost += 3;
        }


        if (FletchingType == Fletching.Plastic)
        {
            arrowCost += 10;
        }
        else if (FletchingType == Fletching.TurkeyFeathers)
        {
            arrowCost += 5;
        }
        else
        {
            arrowCost += 3;
        }

        return arrowCost;
    }
}

public enum Arrowhead { Steel, Obsidian, Wood };
public enum Fletching { Plastic, TurkeyFeathers, GooseFeathers };