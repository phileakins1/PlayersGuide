// See https://aka.ms/new-console-template for more information

// Ignore Spelling: Amarok Amarok

// Experimental static usings.
namespace TheFountainOfObjects.Utilities;

/// <summary>
///  Centralize  game object class array co-ordinates
/// </summary>
public static class SpecialCavesMapPositionSetup
{
    // TODO Parameters will prepare for random position generation later on
    // TODO make sure cave positions are inside playing area depending on size of map
    // TODO add a random position arrow cache to the L board

    public static (int row, int column) EntranceCavePosition(int rowMax, int columnMax)
    {
        return (0, 0);
    }
    public static (int row, int column) FountainCavePosition(int rowMax, int columnMax)
    {
        return (0, 2);
    }


    //  Pits S 1 - 2 M - 4 L
    public static (int row, int column) PitCavePosition1(int rowMax, int columnMax)
    {
        return (1, 1);
    }

    public static (int row, int column) PitCavePosition2(int rowMax, int columnMax)
    {
        return (4, 5);
    }

    public static (int row, int column) PitCavePosition3(int rowMax, int columnMax)
    {
        return (6, 4);
    }

    public static (int row, int column) PitCavePosition4(int rowMax, int columnMax)
    {
        return (2, 6);
    }

    // Maelstroms 1 S - 1 M - 2 L
    public static (int row, int column) MaelstromCavePosition1(int rowMax, int columnMax)
    {
        return (2, 2);
    }

    public static (int row, int column) MaelstromCavePosition2(int rowMax, int columnMax)
    {
        return (4, 3);
    }

    public static (int row, int column) MaelstromCavePosition3(int rowMax, int columnMax)
    {
        return (5, 6);
    }

    public static (int row, int column) MaelstromCavePosition4(int rowMax, int columnMax)
    {
        return (6, 2);
    }

    // Amaroks  1 S - 2 M - 3 L
    public static (int row, int column) AmarokCavePosition1(int rowMax, int columnMax)
    {
        return (3, 0);
    }

    public static (int row, int column) AmarokCavePosition2(int rowMax, int columnMax)
    {
        return (5, 1);
    }

    public static (int row, int column) AmarokCavePosition3(int rowMax, int columnMax)
    {
        return (7, 6);
    }
}

