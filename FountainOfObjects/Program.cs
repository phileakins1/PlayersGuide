// See https://aka.ms/new-console-template for more information

using static SetUp;
using static System.Console;
using static UniqueBoardPositions;



(int X, int Y) = SelectCavernSize.GetCavernSize();
ICave[,] _caves = new ICave[X, Y];


SetUp _playArea = new SetUp(_caves);
_playArea.SetupScreen();
_caves = _playArea.FillCaves();

PlayTheGame _play = new(_caves);
_play.PlayGame();


class SelectCavernSize
{
    // TODO setup future X and Y choices for a custom size cavern - requires a tuple to be returned
    public static (int, int) GetCavernSize()
    {
        (int, int) _size = (0, 0);
        string _choice;
        bool _validResponse = false;

        do
        {
            WriteMenu();
            Write("\n Enter size > ");
            _choice = ReadLine()!.ToLower().Trim();

            if (_choice == "l" || _choice == "m" || _choice == "s") _validResponse = true;

            if (_choice == "m")
            {
                _size = (6, 6);
            }
            else if (_choice == "l")
            {
                _size = (8, 8);
            }
            else if (_choice == "s")
            {
                _size = (4, 4);
            }

            Console.Clear();
            if (!_validResponse) WriteLine("Only L, M or S are acceptable entries!\n");
        } while (!_validResponse);

        return _size;
    }
    private static void WriteMenu()
    {
        string _menu = """
            Choose the size of the cavern

             S for a small (4 X 4) cavern
             M for a medium (6 X 6) cavern
             L for a large (8 X 8) cavern
            """;
        WriteLine(_menu);
    }
}

/// <summary>
///  Centralize 'special' class array co-ordinates
/// </summary>
static class UniqueBoardPositions
{
    // TODO Prepare for random positions later on
    static Random rand = new();

    public static (int, int) FountainCavePosition(int rowMax, int columnMax)
    {
        var _pos = (X: 0, Y: columnMax - 2);
        return _pos;
    }

    public static (int, int) EntranceCavePosition(int rowMax, int columnMax)
    {
        var _pos = (V: 0, W: 0);
        return _pos;
    }
}

public class SetUp(ICave[,] caves)
{
    public ICave[,] Caves { get; private set; } = caves;

    public struct PlayerCurrentPosition
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public PlayerCurrentPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }

    public struct FountainPosition
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public FountainPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }
        public bool IsTheSame(PlayerCurrentPosition player)
        {
            if (this.Row == player.Row && this.Column == player.Column) return true;
            else return false;
        }
    }

    public struct EntrancePosition
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public EntrancePosition(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }


    public void SetupScreen()
    {
        Console.Title = "The quest for the Fountain of Objects";
    }
    public ICave[,] FillCaves()
    {
        // Fill the caves with background noise.

        for (int i = 0; i < Caves.GetUpperBound(0) + 1; i++)
        {
            for (int j = 0; j < Caves.GetUpperBound(1) + 1; j++)
            {
                Caves[i, j] = new AnEmptyCave();
            }
        }

        return Caves;
    }
}

public class PlayTheGame
{
    PlayerCurrentPosition _currentPosition = new();
    EntrancePosition _entrancePosition = new();
    FountainPosition _fountainPosition = new();

    public ICave[,] PlayArea { get; private set; }

    public PlayTheGame(ICave[,] playArea)
    {
        PlayArea = playArea;

        // Using the factory output, fill the object position structs and add the objects to map.

        (int X, int Y) = FountainCavePosition(playArea.GetUpperBound(0), PlayArea.GetUpperBound(1));
        _fountainPosition.Row = X;
        _fountainPosition.Column = Y;
        PlayArea[X, Y] = new FountainCave();

        (int V, int W) = EntranceCavePosition(playArea.GetUpperBound(0), PlayArea.GetUpperBound(1));
        _entrancePosition.Row = V;
        _entrancePosition.Column = W;
        PlayArea[V, W] = new CavernEntrance();

        // Player always starts at the entrance - wherever that might be
        _currentPosition.Row = _entrancePosition.Row;
        _currentPosition.Column = _entrancePosition.Column;
    }

    public void PlayGame()
    {
        bool _response = false;

        do
        {
            WriteLine(new string('-', 80));
            ForegroundColor = ConsoleColor.Gray;
            WriteLine($"You are in the cave at ({_currentPosition.Row}, {_currentPosition.Column})");

            PlayArea[_currentPosition.Row, _currentPosition.Column].ToString();

            string _playerMovement = GetMovementInstruction().ToLower().Trim();

            _response = _playerMovement switch
            {
                "move east" or "east" or "e" => MoveEast(),
                "move west" or "west" or "w" => MoveWest(),
                "move north" or "north" or "n" => MoveNorth(),
                "move south" or "south" or "s" => MoveSouth(),
                "enable fountain" or "enable" or "activate" => EnableFountain(),
                "quit" or "exit" or "q" => false,
                _ => true
            };

        } while (_response);

    }

    private string GetMovementInstruction()
    {
        Write("What do you want to do? >   ");
        ForegroundColor = ConsoleColor.Cyan;
        string? _response = ReadLine();
        ResetColor();

        return _response!;
    }

    /// <summary>
    /// Move - but stay on the board by wrapping round: predicated on board's actual boundaries - not on 'magic' numbers.
    /// </summary>
    /// <returns> Returns new position </returns>

    private bool MoveEast()
    {
        if (_currentPosition.Column + 1 > PlayArea.GetUpperBound(0)) _currentPosition.Column = PlayArea.GetLowerBound(0);
        else _currentPosition.Column++;

        return true;
    }

    private bool MoveWest()
    {
        if (_currentPosition.Column - 1 < PlayArea.GetLowerBound(0)) _currentPosition.Column = PlayArea.GetUpperBound(0);
        else _currentPosition.Column--;

        return true;
    }

    private bool MoveNorth()
    {
        if (_currentPosition.Row - 1 < PlayArea.GetLowerBound(1)) _currentPosition.Row = PlayArea.GetUpperBound(1);
        else _currentPosition.Row--;


        return true;
    }

    private bool MoveSouth()
    {
        if (_currentPosition.Row + 1 > PlayArea.GetUpperBound(1)) _currentPosition.Row = PlayArea.GetLowerBound(1);
        else _currentPosition.Row++;


        return true;
    }

    private bool EnableFountain()
    {
        // The fountain is present
        if (_fountainPosition.IsTheSame(_currentPosition))
        {
            // Change message

            FountainCave _tempFountain = new();
            _tempFountain.Activated = true;
            PlayArea[_currentPosition.Row, _currentPosition.Column] = _tempFountain;

            // Change cavern entrance message.
            CavernEntrance _tempEntrance = new();
            _tempEntrance.FountainIsActivated = true;
            PlayArea[_entrancePosition.Row, _entrancePosition.Column] = _tempEntrance;
        }
        else WriteLine("Can't be done, you actually have to be close to the fountain to do that!");

        return true;
    }
}


public interface ICave
{
    string Description { get; }

    string ToString();
}

public class CavernEntrance : ICave
{
    public string Description { get; private set; }
    public string Leaving { get; private set; }
    public bool FountainIsActivated { get; set; } = false;

    public CavernEntrance()
    {
        Description = "You see a light coming from outside the cavern. This is the entrance.";
        Leaving = "The Fountain of Objects has been re-activated. You have escaped with your life!";
    }
    public override string ToString()
    {
        ForegroundColor = ConsoleColor.Yellow;

        if (FountainIsActivated) WriteLine(Leaving);
        else WriteLine(Description);

        ResetColor();
        return string.Empty;
    }
}

public class FountainCave : ICave
{
    public string Description { get; private set; }
    public string ActivatedDescription { get; private set; }
    public bool Activated { get; set; } = false;
    public FountainCave()
    {
        Description = "You hear water dripping, The Fountain of Objects is here!";
        ActivatedDescription = "You hear rushing waters from the Fountain of Objects. It has been reactivated!";
    }

    public override string ToString()
    {
        ForegroundColor = ConsoleColor.Blue;
        if (Activated) WriteLine(ActivatedDescription);
        else WriteLine(Description);

        ResetColor();
        return string.Empty;
    }
}

public class AnEmptyCave : ICave
{
    public string Description { get; private set; }
    public AnEmptyCave()
    {
        Description = "A dark, empty, dusty and rough cave. Move along - nothing of interest to see here!";
    }

    public override string ToString()
    {
        WriteLine(Description);

        return string.Empty;
    }
}