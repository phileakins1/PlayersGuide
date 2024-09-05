// See https://aka.ms/new-console-template for more information

using static BoardObjectStructs;
using static SpecialCavesMapPositionSetup;
using static System.Console;

// C# Players Guide Fountain of Objects Challenge.
// Basic challenge; plus:
// Expansion 1 - Three sizes of playing board
// Expansion 2 - Pit challenge (accommodating different size boards)
// Expansion 3 - Maelstrom challenge (accommodating different size boards


// TODO Recount pits on the large board to total 4 large & 2 on the medium board.
// TODO Recount maelstroms on the large board to total 2 large & 1 on the medium board.


(int _row, int _columns) = SelectCavernSize.GetCavernSize();
ICave[,] _caves = new ICave[_row, _columns];


BoardObjectStructs _playArea = new(_caves);
_playArea.SetupScreen();
_caves = _playArea.FillEmptyCaves();

PlayTheGame _play = new(_caves);

_play.GetPlayerMovement();


class SelectCavernSize
{
    protected SelectCavernSize() { }

    // TODO setup future X and Y choices for a custom size cavern - returns a tuple with row and column
    public static (int, int) GetCavernSize()
    {
        (int, int) _size = (0, 0);
        string? _choice;
        bool _validResponse = false;

        do
        {
            WriteMenu();
            Write("\n Enter size of board: > ");
            _choice = ReadLine()?.ToLower().Trim();

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
        return (4, 3);
    }

    public static (int, int) MaelstromCavePosition3(int rowMax, int columnMax)
    {
        return (5, 6);
    }

    public static (int, int) MaelstromCavePosition4(int rowMax, int columnMax)
    {
        return (6, 2);
    }
}

public class BoardObjectStructs(ICave[,] caves)
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

        public void IsAdjacentTo(PlayerCurrentPosition player)
        {
            if (!(this.Row - player.Row is < -1 or > 1 || this.Column - player.Column is < -1 or > 1) && (!IsTheSameAs(player)))
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("You feel a draught, There is a pit nearby!");
                ResetColor();
            }
        }

        public bool IsTheSameAs(PlayerCurrentPosition player)
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

        public void IsAdjacentTo(PlayerCurrentPosition player)
        {
            if (!(this.Row - player.Row is < -1 or > 1 || this.Column - player.Column is < -1 or > 1) && (!IsTheSameAs(player)))
            {
                ForegroundColor = ConsoleColor.Green;
                WriteLine("You hear the growling and groaning of a Maelstrom nearby!");
                ResetColor();
            }
        }

        public bool IsTheSameAs(PlayerCurrentPosition player)
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
    PitPositionStruct _pitPosition2;
    PitPositionStruct _pitPosition3;
    PitPositionStruct _pitPosition4;

    MaelstromPositionStruct _maestromPosition1;
    MaelstromPositionStruct _maestromPosition2;
    MaelstromPositionStruct _maestromPosition3;
    MaelstromPositionStruct _maestromPosition4;

    /// <summary>
    /// The playing board.
    /// </summary>
    public ICave[,] PlayArea { get; private set; }

    /// <summary>
    /// Set up the playing area caves depending on the size of the board
    /// </summary>
    /// <param name="playArea">The board with background setup to be populated with the interesting bits</param>

    public PlayTheGame(ICave[,] playArea)
    {
        PlayArea = playArea;

        PositionBaseObjects();
        FillTheBoardWithPerils();

        // Player always starts at the entrance - wherever that might end up
        _playerCurrentPosition.Row = _entrancePosition.Row;
        _playerCurrentPosition.Column = _entrancePosition.Column;
    }

    internal void PositionBaseObjects()
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

        PositionPitsOnBoard();
        PositionMaelstromsOnBoard();
    }

    private void PositionPitsOnBoard()
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

        // Extra pit on Medium board 
        if (_rowMax > 3 && _rowMax <= 5)
        {
            _pitPosition2 = new();
            (_row, _column) = PitCavePosition2(_rowMax, _colMax);
            _pitPosition2.Row = _row;
            _pitPosition2.Column = _column;
            PlayArea[_row, _column] = new ABottomlessPit();
        }

        // 3 extra pits on Large board
        if (_rowMax > 5)
        {
            _pitPosition2 = new();
            (_row, _column) = PitCavePosition2(_rowMax, _colMax);
            _pitPosition2.Row = _row;
            _pitPosition2.Column = _column;
            PlayArea[_row, _column] = new ABottomlessPit();

            _pitPosition3 = new();
            (_row, _column) = PitCavePosition3(_rowMax, _colMax);
            _pitPosition3.Row = _row;
            _pitPosition3.Column = _column;
            PlayArea[_row, _column] = new ABottomlessPit();

            _pitPosition4 = new();
            (_row, _column) = PitCavePosition4(_rowMax, _colMax);
            _pitPosition4.Row = _row;
            _pitPosition4.Column = _column;
            PlayArea[_row, _column] = new ABottomlessPit();
        }
    }

    private void PositionMaelstromsOnBoard()
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
        PlayArea[_row, _column] = new Maelstrom();

        // Medium board
        if (_rowMax > 3 && _rowMax <= 5)
        {
            _maestromPosition2 = new();
            (_row, _column) = MaelstromCavePosition2(_rowMax, _colMax);
            _maestromPosition2.Row = _row;
            _maestromPosition2.Column = _column;
            PlayArea[_row, _column] = new Maelstrom();
        }

        // Large board
        if (_rowMax > 5)
        {
            _maestromPosition2 = new();
            (_row, _column) = MaelstromCavePosition2(_rowMax, _colMax);
            _maestromPosition2.Row = _row;
            _maestromPosition2.Column = _column;
            PlayArea[_row, _column] = new Maelstrom();

            _maestromPosition3 = new();
            (_row, _column) = MaelstromCavePosition3(_rowMax, _colMax);
            _maestromPosition3.Row = _row;
            _maestromPosition3.Column = _column;
            PlayArea[_row, _column] = new Maelstrom();

            _maestromPosition4 = new();
            (_row, _column) = MaelstromCavePosition4(_rowMax, _colMax);
            _maestromPosition4.Row = _row;
            _maestromPosition4.Column = _column;
            PlayArea[_row, _column] = new Maelstrom();
        }
    }

    /// <summary>
    /// Main game loop.
    /// </summary>
    public void GetPlayerMovement()
    {
        bool _validResponse = false;

        do
        {
            WriteLine(new string('-', 90));
            ForegroundColor = ConsoleColor.Gray;

            // Action current perils after player movement.
            if (CheckForPits())
            {
                // Player is in an endless fall! No use hanging about.
                break;
            }

            CheckForMaelstroms();

            WriteLine($"You are in the cave at ({_playerCurrentPosition.Row}, {_playerCurrentPosition.Column})");

            PlayArea[_playerCurrentPosition.Row, _playerCurrentPosition.Column].ToString();

            string _playerMovement = GetMovementInstruction().ToLower().Trim();

            _validResponse = _playerMovement switch
            {
                "move east" or "east" or "e" => MoveEast(),
                "move west" or "west" or "w" => MoveWest(),
                "move north" or "north" or "n" => MoveNorth(),
                "move south" or "south" or "s" => MoveSouth(),
                "enable fountain" or "enable" or "activate" => EnableFountain(),
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
    /// Move - but stay on the board by wrapping round: predicated on each board's boundaries - not on 'magic' numbers.
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

    private bool EnableFountain()
    {
        // The fountain is present?
        if (_fountainPosition.IsTheSameAs(_playerCurrentPosition))
        {
            // Change messages
            ActivatedFountainCave _activeFountain = new();
            PlayArea[_playerCurrentPosition.Row, _playerCurrentPosition.Column] = _activeFountain;

            // Change cavern entrance message.
            ActivatedCavernEntrance _activeEntrance = new();
            PlayArea[_entrancePosition.Row, _entrancePosition.Column] = _activeEntrance;
        }
        else WriteLine("Can't be done - you  have to be present in the fountain cave to do that!");

        return true;
    }

    private bool CheckForPits()
    {
        // Assumes a rectangular board.
        int _rowMax = PlayArea.GetUpperBound(0);

        // One default pit on all boards

        if (_pitPosition1.IsTheSameAs(_playerCurrentPosition))
        {
            // You fell in!
            PlayArea[_playerCurrentPosition.Row, _playerCurrentPosition.Column].ToString();
            return true;
        }

        // A Pit is near but don't print message if the player is about to be blown away to a new cave.
        if (!_maestromPosition1.IsTheSameAs(_playerCurrentPosition))
        {
            _pitPosition1.IsAdjacentTo(_playerCurrentPosition);
        }

        // An extra pit on the Medium board
        if (_rowMax > 3 && _rowMax <= 5)
        {
            // Pit 2
            if (_pitPosition2.IsTheSameAs(_playerCurrentPosition))
            {
                PlayArea[_playerCurrentPosition.Row, _playerCurrentPosition.Column].ToString();
                return true;
            }
            if (!_maestromPosition2.IsTheSameAs(_playerCurrentPosition))
            {
                _pitPosition2.IsAdjacentTo(_playerCurrentPosition);
            }
        }

        // Three extra pits on Large board.
        if (_rowMax > 5)
        {
            // Pit 2
            if (_pitPosition2.IsTheSameAs(_playerCurrentPosition))
            {
                PlayArea[_playerCurrentPosition.Row, _playerCurrentPosition.Column].ToString();
                return true;
            }
            if (!_maestromPosition2.IsTheSameAs(_playerCurrentPosition))
            {
                _pitPosition2.IsAdjacentTo(_playerCurrentPosition);
            }

            // Pit 3
            if (_pitPosition3.IsTheSameAs(_playerCurrentPosition))
            {
                PlayArea[_playerCurrentPosition.Row, _playerCurrentPosition.Column].ToString();
                return true;
            }
            if (!_maestromPosition3.IsTheSameAs(_playerCurrentPosition))
            {
                _pitPosition3.IsAdjacentTo(_playerCurrentPosition);
            }

            // Pit 4
            if (_pitPosition4.IsTheSameAs(_playerCurrentPosition))
            {
                PlayArea[_playerCurrentPosition.Row, _playerCurrentPosition.Column].ToString();
                return true;
            }
            if (!_maestromPosition4.IsTheSameAs(_playerCurrentPosition))
            {
                _pitPosition4.IsAdjacentTo(_playerCurrentPosition);
            }
        }
        return false;
    }




    private void CheckForMaelstroms()
    {
        // Assumes rectangular board.
        int _rowMax = PlayArea.GetUpperBound(0);

        // Small board maelstrom1 is present on each board.

        _maestromPosition1.IsAdjacentTo(_playerCurrentPosition);


        if (_maestromPosition1.IsTheSameAs(_playerCurrentPosition))
        {
            PlayArea[_playerCurrentPosition.Row, _playerCurrentPosition.Column].ToString();
            BlowPlayerToANewCave();

            (int row, int column) = MoveMaelstrom(_maestromPosition1.Row, _maestromPosition1.Column);

            _maestromPosition1.Row = row;
            _maestromPosition1.Column = column;
        }

        // Medium 6X6 board with one extra Maelstrom
        if (_rowMax > 3 && _rowMax <= 5)
        {
            _maestromPosition2.IsAdjacentTo(_playerCurrentPosition);


            if (_maestromPosition2.IsTheSameAs(_playerCurrentPosition))
            {
                PlayArea[_playerCurrentPosition.Row, _playerCurrentPosition.Column].ToString();
                BlowPlayerToANewCave();

                (int row, int column) = MoveMaelstrom(_maestromPosition2.Row, _maestromPosition2.Column);

                _maestromPosition2.Row = row;
                _maestromPosition2.Column = column;
            }
        }

        // Large board with 3 extra Maelstroms
        if (_rowMax > 5)
        {
            _maestromPosition2.IsAdjacentTo(_playerCurrentPosition);

            if (_maestromPosition2.IsTheSameAs(_playerCurrentPosition))
            {
                PlayArea[_playerCurrentPosition.Row, _playerCurrentPosition.Column].ToString();
                BlowPlayerToANewCave();

                (int row, int column) = MoveMaelstrom(_maestromPosition2.Row, _maestromPosition2.Column);

                _maestromPosition2.Row = row;
                _maestromPosition2.Column = column;
            }

            _maestromPosition3.IsAdjacentTo(_playerCurrentPosition);

            if (_maestromPosition3.IsTheSameAs(_playerCurrentPosition))
            {
                PlayArea[_playerCurrentPosition.Row, _playerCurrentPosition.Column].ToString();
                BlowPlayerToANewCave();

                (int row, int column) = MoveMaelstrom(_maestromPosition3.Row, _maestromPosition3.Column);

                _maestromPosition3.Row = row;
                _maestromPosition3.Column = column;
            }

            _maestromPosition4.IsAdjacentTo(_playerCurrentPosition);

            if (_maestromPosition4.IsTheSameAs(_playerCurrentPosition))
            {
                PlayArea[_playerCurrentPosition.Row, _playerCurrentPosition.Column].ToString();
                BlowPlayerToANewCave();

                (int row, int column) = MoveMaelstrom(_maestromPosition4.Row, _maestromPosition4.Column);

                _maestromPosition4.Row = row;
                _maestromPosition4.Column = column;
            }
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

        PlayArea[row, column] = new Maelstrom();

        return (row, column);
    }

    private void BlowPlayerToANewCave()
    {
        MoveNorth();
        MoveEast();
        MoveEast();
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
    public CavernEntrance()
    {
        Description = "You see a light coming from outside the cavern. This is the entrance.";
    }
    public override string ToString()
    {
        ForegroundColor = ConsoleColor.Yellow;
        WriteLine(Description);
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
        Description = "\nYou have fallen into a bottomless pit. Good luck with that!  See you in the next life. Bye ....\n";
    }

    public override string ToString()
    {
        ForegroundColor = ConsoleColor.Red;
        WriteLine(Description);
        ResetColor();
        return string.Empty;
    }
}

public class Maelstrom : ICave
{
    public Maelstrom()
    {
        Description = "You have encountered a Maelstrom and been blown to another cave!";
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
        Description = "A eerily dark, dank, rough and deserted cave. Move along now - nothing to see here!";
    }

    public override string ToString()
    {
        WriteLine(Description);

        return string.Empty;
    }
}