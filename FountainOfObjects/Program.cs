// See https://aka.ms/new-console-template for more information

using static MapPositionStructs;
using static SpecialCavesMapPositionSetup;
using static System.Console;

// C# Players Guide Fountain of Objects Challenge.
// Basic challenge; plus:
// Expansion 1 - Three sizes of playing board
// Expansion 2 - Pit challenge (accommodating different size boards)
// Expansion 3 - Maelstrom challenge (accommodating different size boards


// TODO Recount pits on the large board to total 4 & 2 on the medium board.
// TODO Recount maelstroms on the large board to total 2 & 1 on the medium board.


(int X, int Y) = SelectCavernSize.GetCavernSize();
ICave[,] _caves = new ICave[X, Y];


MapPositionStructs _playArea = new(_caves);
_playArea.SetupScreen();
_caves = _playArea.FillEmptyCaves();

PlayTheGame _play = new(_caves);

_play.PlayerMovements();


class SelectCavernSize
{
    protected SelectCavernSize() { }

    // TODO setup future X and Y choices for a custom size cavern - returns a tuple with row and column
    public static (int, int) GetCavernSize()
    {
        (int, int) _size = (0, 0);
        string _choice;
        bool _validResponse = false;

        do
        {
            WriteMenu();
            Write("\n Enter size of board: > ");
            _choice = ReadLine()!.ToLower().Trim();

            if (_choice == "m")
            {
                _size = (6, 6);
                _validResponse = true;
            }
            else if (_choice == "l")
            {
                _size = (8, 8);
                _validResponse |= true;
            }
            else if (_choice == "s")
            {
                _size = (4, 4);
                _validResponse = true;
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
static class SpecialCavesMapPositionSetup
{
    // TODO Parameters will prepare for random position generation later on
    static Random rand = new();

    // TODO make sure cave positions are inside playing area depending on size of map
    public static (int, int) FountainCavePosition(int rowMax, int columnMax)
    {
        return (0, 2);
    }

    public static (int, int) EntranceCavePosition(int rowMax, int columnMax)
    {
        return (0, 0);
    }

    //  Pits S 1 - 2 M - 4 L
    public static (int, int) PitCavePosition1(int rowMax, int columnMax)
    {
        return (1, 2);
    }

    public static (int, int) PitCavePosition2(int rowMax, int columnMax)
    {
        return (4, 5);
    }

    public static (int, int) PitCavePosition3(int rowMax, int columnMax)
    {
        return (6, 4);
    }

    public static (int, int) PitCavePosition4(int rowMax, int columnMax)
    {
        return (2, 7);
    }


    // Maelstroms 1 S - 1 M - 2 L
    public static (int, int) MaelstromCavePosition1(int rowMax, int columnMax)
    {
        return (2, 2);
    }

    public static (int, int) MaelstromCavePosition2(int rowMax, int columnMax)
    {
        return (5, 3);
    }

    public static (int, int) MaelstromCavePosition3(int rowMax, int columnMax)
    {
        return (4, 6);
    }

    public static (int, int) MaelstromCavePosition4(int rowMax, int columnMax)
    {
        return (7, 2);
    }
}

public class MapPositionStructs(ICave[,] caves)
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

    public struct FountainPositionStruct
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public FountainPositionStruct(int row, int column)
        {
            Row = row;
            Column = column;
        }
        public bool IsTheSameAs(PlayerCurrentPosition player)
        {
            if (this.Row == player.Row && this.Column == player.Column) return true;
            else return false;
        }
    }

    public struct EntrancePositionStruct
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public EntrancePositionStruct(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
    public struct PitPositionStruct
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public PitPositionStruct(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public bool IsAdjacentToPit(PlayerCurrentPosition player)
        {
            if (!(this.Row - player.Row is < -1 or > 1 || this.Column - player.Column is < -1 or > 1) && (!IsTheSameAsPit(player)))
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("You feel a draught, There is a pit nearby!");
                ResetColor();
            }
            return false;
        }

        public bool IsTheSameAsPit(PlayerCurrentPosition player)
        {
            if (this.Row == player.Row && this.Column == player.Column) return true;
            else return false;
        }
    }

    public struct MaelstromPositionStruct
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public MaelstromPositionStruct(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public void IsAdjacentToMaelstrom(PlayerCurrentPosition player)
        {
            if (!(this.Row - player.Row is < -1 or > 1 || this.Column - player.Column is < -1 or > 1) && (!IsTheSameAsMaelstrom(player)))
            {
                ForegroundColor = ConsoleColor.Green;
                WriteLine("You hear the growling and groaning of a Maelstrom nearby!");
                ResetColor();
            }
        }

        public bool IsTheSameAsMaelstrom(PlayerCurrentPosition player)
        {
            if (this.Row == player.Row && this.Column == player.Column) return true;
            else return false;
        }
    }


    public void SetupScreen()
    {
        Console.Title = "The quest for the Fountain of Objects";
    }

    /// <summary>
    /// Fill the play area with background noise.
    /// </summary>
    /// 
    /// <returns>A board full of empty caves</returns>

    public ICave[,] FillEmptyCaves()
    {

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
    PlayerCurrentPosition _playerCurrentPosition = new();
    EntrancePositionStruct _entrancePosition = new();
    FountainPositionStruct _fountainPosition = new();

    PitPositionStruct _pitPosition1;
    /*PitPositionStruct _pitPosition2;
    PitPositionStruct _pitPosition3;
    PitPositionStruct _pitPosition4;*/

    MaelstromPositionStruct _maestromPosition1;
    /*MaelstromPositionStruct _maestromPosition2;
    MaelstromPositionStruct _maestromPosition3;
    MaelstromPositionStruct _maestromPosition4;*/

    public ICave[,] PlayArea { get; private set; }

    /// <summary>
    /// Set up the playing area caves depending on the size of the board
    /// </summary>
    /// <param name="playArea">The board with background setup to be populated with the interesting bits</param>


    public PlayTheGame(ICave[,] playArea)
    {
        PlayArea = playArea;

        PositionAssets();
        FillTheBoardWithPerils();

        // Player always starts at the entrance - wherever that might end up
        _playerCurrentPosition.Row = _entrancePosition.Row;
        _playerCurrentPosition.Column = _entrancePosition.Column;
    }

    internal void PositionAssets()
    {
        int _rowMax = PlayArea.GetUpperBound(0);
        int _colMax = PlayArea.GetUpperBound(1);

        int _row;
        int _column;

        (_row, _column) = FountainCavePosition(_rowMax, _colMax);
        _fountainPosition.Row = _row;
        _fountainPosition.Column = _column;
        PlayArea[_row, _column] = new FountainCave();

        (_row, _column) = EntranceCavePosition(_rowMax, _colMax);
        _entrancePosition.Row = _row;
        _entrancePosition.Column = _column;
        PlayArea[_row, _column] = new CavernEntrance();
    }

    internal void FillTheBoardWithPerils()
    {
        // Using the factory output, fill the object position Structs and complete the board according to the Expansions.

        PositionPits();
        PositionMaelstroms();
    }

    private void PositionPits()
    {
        int _rowMax = PlayArea.GetUpperBound(0);
        int _colMax = PlayArea.GetUpperBound(1);

        int _row;
        int _column;

        // Add Pits according to board size
        // One default on small board @ 1, 2

        _pitPosition1 = new();
        (_row, _column) = PitCavePosition1(_rowMax, _colMax);
        _pitPosition1.Row = _row;
        _pitPosition1.Column = _column;
        PlayArea[_row, _column] = new ABottomlessPit();
    }

    private void PositionMaelstroms()
    {
        int _rowMax = PlayArea.GetUpperBound(0);
        int _colMax = PlayArea.GetUpperBound(1);

        int _row;
        int _column;

        // Initial Maelstrom S board @ 2, 2
        _maestromPosition1 = new();
        (_row, _column) = MaelstromCavePosition1(_rowMax, _colMax);
        _maestromPosition1.Row = _row;
        _maestromPosition1.Column = _column;
        PlayArea[_row, _column] = new AMaelstrom();
    }
    public void PlayerMovements()
    {
        bool _validResponse = false;

        do
        {
            WriteLine(new string('-', 90));
            ForegroundColor = ConsoleColor.Gray;


            if (CheckForPits())
            {
                // Player is in an endless fall! No use waiting about.
                break;
            }

            DiscoverMaelstroms();

            WriteLine($"You are in the cave at ({_playerCurrentPosition.Row}, {_playerCurrentPosition.Column})");

            PlayArea[_playerCurrentPosition.Row, _playerCurrentPosition.Column].ToString();

            string _playerMovement = GetMovementInstruction().ToLower().Trim();

            _validResponse = _playerMovement switch
            {
                "move east" or "east" or "e" => MoveEast(),
                "move west" or "west" or "w" => MoveWest(),
                "move north" or "north" or "n" => MoveNorth(),
                "move south" or "south" or "s" => MoveSouth(),
                "enable fountain" or "enable" or "activate" => EnabledFountain(),
                "quit" or "exit" or "q" => false,
                _ => true
            };

        } while (_validResponse);

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

        if (_playerCurrentPosition.Column + 1 > PlayArea.GetUpperBound(0)) _playerCurrentPosition.Column = PlayArea.GetLowerBound(0);
        else _playerCurrentPosition.Column++;

        return true;
    }

    private bool MoveWest()
    {

        if (_playerCurrentPosition.Column - 1 < PlayArea.GetLowerBound(0)) _playerCurrentPosition.Column = PlayArea.GetUpperBound(0);
        else _playerCurrentPosition.Column--;

        return true;
    }

    private bool MoveNorth()
    {
        if (_playerCurrentPosition.Row - 1 < PlayArea.GetLowerBound(1)) _playerCurrentPosition.Row = PlayArea.GetUpperBound(1);
        else _playerCurrentPosition.Row--;

        return true;
    }

    private bool MoveSouth()
    {
        if (_playerCurrentPosition.Row + 1 > PlayArea.GetUpperBound(1)) _playerCurrentPosition.Row = PlayArea.GetLowerBound(1);
        else _playerCurrentPosition.Row++;

        return true;
    }

    private bool EnabledFountain()
    {
        // The fountain is present?
        if (_fountainPosition.IsTheSameAs(_playerCurrentPosition))
        {
            // Change message
            ActivatedFountainCave _activeFountain = new();
            PlayArea[_playerCurrentPosition.Row, _playerCurrentPosition.Column] = _activeFountain;

            // Change cavern entrance message.
            ActivatedCavernEntrance _activeEntrance = new();
            PlayArea[_entrancePosition.Row, _entrancePosition.Column] = _activeEntrance;
        }
        else WriteLine("Can't be done, you  have actually to be close to the fountain to do that!");

        return true;
    }

    private bool CheckForPits()
    {
        // One default pit on all boards

        if (_pitPosition1.IsTheSameAsPit(_playerCurrentPosition))
        {
            PlayArea[_playerCurrentPosition.Row, _playerCurrentPosition.Column].ToString();
            return true;
        }

        return _pitPosition1.IsAdjacentToPit(_playerCurrentPosition);

    }


    private void DiscoverMaelstroms()
    {
        // Small board maelstrom1 is present on each board.

        _maestromPosition1.IsAdjacentToMaelstrom(_playerCurrentPosition);


        if (_maestromPosition1.IsTheSameAsMaelstrom(_playerCurrentPosition))
        {
            MovePlayer();

            (int row, int column) = MoveMaelstrom(_maestromPosition1.Row, _maestromPosition1.Column);

            _maestromPosition1.Row = row;
            _maestromPosition1.Column = column;
        }

    }

    private (int, int) MoveMaelstrom(int row, int column)
    {
        //Move Maelstrom to new position after resetting 'old'  cave.
        PlayArea[row, column] = new AnEmptyCave();

        // South
        if (row + 1 > PlayArea.GetUpperBound(1)) row = PlayArea.GetLowerBound(1);
        else row++;
        // West * 2
        if (column - 1 < PlayArea.GetLowerBound(0)) column = PlayArea.GetUpperBound(0);
        else column--;
        if (column - 1 < PlayArea.GetLowerBound(0)) column = PlayArea.GetUpperBound(0);
        else column--;

        PlayArea[row, column] = new AMaelstrom();

        return (row, column);
    }

    private void MovePlayer()
    {
        // Blow player to new position
        MoveNorth();
        MoveEast();
        MoveEast();

        ForegroundColor = ConsoleColor.Green;
        WriteLine($"You have encountered the Maelstrom and been blown to the cave at ({_playerCurrentPosition.Row}, {_playerCurrentPosition.Column})");
        ResetColor();
        WriteLine(new string('-', 90));
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

public class ActivatedCavernEntrance : ICave
{
    public string Description { get; private set; }

    public ActivatedCavernEntrance()
    {
        Description = "The Fountain of Objects has been re-activated. You have escaped with your life!";
    }
    public override string ToString()
    {
        ForegroundColor = ConsoleColor.Yellow;

        WriteLine(Description);

        ResetColor();
        return string.Empty;
    }
}

public class FountainCave : ICave
{
    public string Description { get; private set; }
    public FountainCave()
    {
        Description = "You hear water dripping, The Fountain of Objects is here!";
    }

    public override string ToString()
    {
        ForegroundColor = ConsoleColor.Blue;
        WriteLine(Description);

        ResetColor();
        return string.Empty;
    }
}

public class ActivatedFountainCave : ICave
{
    public string Description { get; private set; }
    public ActivatedFountainCave()
    {
        Description = "You hear rushing waters from the Fountain of Objects. It has been reactivated!";
    }

    public override string ToString()
    {
        ForegroundColor = ConsoleColor.Blue;

        WriteLine(Description);

        ResetColor();
        return string.Empty;
    }
}

public class ABottomlessPit : ICave
{
    public string Description { get; private set; }
    public ABottomlessPit()
    {
        //WriteLine(new string('-', 90));

        Description = "You have fallen into a bottomless pit. Good luck with that! See you in the next life. Bye.";
    }

    public override string ToString()
    {
        ForegroundColor = ConsoleColor.Red;

        WriteLine(Description);

        ResetColor();
        return string.Empty;
    }
}

public class AMaelstrom : ICave
{
    public AMaelstrom()
    {
        Description = "You have been blown into another cave!";
    }

    public string Description { get; private set; }

    public override string ToString()
    {
        ForegroundColor = ConsoleColor.Green;
        WriteLine(Description);
        ResetColor();

        return string.Empty;
    }
}

public class AnEmptyCave : ICave
{
    public string Description { get; private set; }
    public AnEmptyCave()
    {
        Description = "A eerily dark, musty, rough and deserted cave. Move along - nothing of interest here!";
    }

    public override string ToString()
    {
        WriteLine(Description);

        return string.Empty;
    }
}