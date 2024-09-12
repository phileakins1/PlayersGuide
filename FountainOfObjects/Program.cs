// See https://aka.ms/new-console-template for more information



// Ignore Spelling: Amarok Amarock

using static BoardObjectStructs;
using static SpecialCavesMapPositionSetup;
using static System.Console;


// C# Players Guide Fountain of Objects Challenge. - 1000 XP at stake

// Basic challenge completed; plus:
// Expansion 1 - Three sizes of playing board - completed
// Expansion 2 - Pit challenge (accommodating different size boards) - completed
// Expansion 3 - Maelstrom challenge (accommodating different size boards) - completed
// Expansion 4 - Amaroks challenge (accommodating different size boards) - completed
// Expansion 5 - Bows and arrows

// TODO Refactor the program into separate class based files/name spaces


(int _row, int _columns) = SelectCavernSize.GetCavernSize();
ICave[,] _caves = new ICave[_row, _columns];

BoardObjectStructs _playArea = new(_caves);
_playArea.SetupScreen();
_caves = _playArea.FillEmptyCaves();

PlayTheGame _play = new(_caves);

_play.GetPlayerChoice();

class SelectCavernSize
{
    protected SelectCavernSize() { }

    // TODO setup future X and Y choices for a custom size cavern 

    /// <summary>
    /// Establishes the playing board size. 
    /// </summary>
    /// <returns>A tuple with the row and column numerations for its creation.</returns>

    internal static (int, int) GetCavernSize()
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
        return (1, 1);
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
        return (2, 6);
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

    public static (int, int) AmarokCavePosition1(int rowMax, int columnMax)
    {
        return (3, 0);
    }

    public static (int, int) AmarokCavePosition2(int rowMax, int columnMax)
    {
        return (5, 1);
    }

    public static (int, int) AmarokCavePosition3(int rowMax, int columnMax)
    {
        return (7, 6);
    }
}

public record BoardObjectStructs(ICave[,] Caves)
{
    public ICave[,] Caves { get; private set; } = Caves;

    public struct PlayerPosition
    {
        public int Arrows { get; set; }
        public bool Bow { get; set; }

        public int Row { get; set; }
        public int Column { get; set; }
        public PlayerPosition() { }
    }

    public struct FountainPositionStruct
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public FountainPositionStruct() { }

        // No need for a message if the player is adjacent.
        public bool IsTheSameAs(PlayerPosition player)
        {
            if (this.Row == player.Row && this.Column == player.Column) return true;
            else return false;
        }

        public bool IsTheSameAs(int row, int column)
        {
            if (this.Row == row && this.Column == column) return true;
            else return false;
        }
    }

    public struct EntrancePositionStruct
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public EntrancePositionStruct() { }

        public bool IsTheSameAs(int row, int column)
        {
            if (this.Row == row && this.Column == column) return true;
            else return false;
        }
    }
    public struct PitPositionStruct
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public PitPositionStruct() { }

        public void IsAdjacentTo(PlayerPosition player)
        {
            // TODO Detect round the board edges.
            if (!(this.Row - player.Row is < -1 or > 1 || this.Column - player.Column is < -1 or > 1) && (!IsTheSameAs(player)))
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("You feel a draught, There is a pit nearby!");
                ResetColor();
            }
        }

        public bool IsTheSameAs(PlayerPosition player)
        {
            if (this.Row == player.Row && this.Column == player.Column) return true;
            else return false;
        }
    }

    public struct MaelstromPositionStruct
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public MaelstromPositionStruct() { }

        public void IsAdjacentTo(PlayerPosition player)
        {
            // TODO Wrap round.
            if (!(this.Row - player.Row is < -1 or > 1 || this.Column - player.Column is < -1 or > 1) && (!IsTheSameAs(player)))
            {
                ForegroundColor = ConsoleColor.Green;
                WriteLine("You hear the growling and groaning of a Maelstrom nearby!");
                ResetColor();
            }
        }

        public bool IsTheSameAs(PlayerPosition player)
        {
            if (this.Row == player.Row && this.Column == player.Column) return true;
            else return false;
        }

        public bool IsTheSameAs(int row, int column)
        {
            if (this.Row == row && this.Column == column) return true;
            else return false;
        }
    }

    public struct AmarokPositionStruct
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public AmarokPositionStruct() { }

        public void IsAdjacentTo(PlayerPosition player)
        {
            // TODO Wrap round.
            if (!(this.Row - player.Row is < -1 or > 1 || this.Column - player.Column is < -1 or > 1) && (!IsTheSameAs(player)))
            {
                ForegroundColor = ConsoleColor.Yellow;
                WriteLine("You smell the rotten stench of an Amarok in a nearby cave!");
                ResetColor();
            }
        }

        public bool IsTheSameAs(PlayerPosition player)
        {
            if (this.Row == player.Row && this.Column == player.Column) return true;
            else return false;
        }

        public bool IsTheSameAs(int row, int column)
        {
            if (this.Row == row && this.Column == column) return true;
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
    PlayerPosition _gamePlayer = new();
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

    AmarokPositionStruct _amarockPosition1;
    AmarokPositionStruct _amarockPosition2;
    AmarokPositionStruct _amarockPosition3;

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
        _gamePlayer.Row = _entrancePosition.Row;
        _gamePlayer.Column = _entrancePosition.Column;
        _gamePlayer.Bow = true;
        _gamePlayer.Arrows = 5;
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
        PositionAmaroksOnBoard();
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
        if (_rowMax > 3)
        {
            _pitPosition2 = new();
            (_row, _column) = PitCavePosition2(_rowMax, _colMax);
            _pitPosition2.Row = _row;
            _pitPosition2.Column = _column;
            PlayArea[_row, _column] = new ABottomlessPit();
        }

        // 2 extra pits on Large board
        if (_rowMax > 5)
        {
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

        // Initial AMaelstrom S board @ 2, 2


        _maestromPosition1 = new();
        (_row, _column) = MaelstromCavePosition1(_rowMax, _colMax);
        _maestromPosition1.Row = _row;
        _maestromPosition1.Column = _column;
        PlayArea[_row, _column] = new AMaelstrom();


        // Medium board  One Maelstrom 
        if (_rowMax > 3)
        {
            _maestromPosition2 = new();
            (_row, _column) = MaelstromCavePosition2(_rowMax, _colMax);
            _maestromPosition2.Row = _row;
            _maestromPosition2.Column = _column;
            PlayArea[_row, _column] = new AMaelstrom();
        }

        // Large board plus two extra
        if (_rowMax > 5)
        {
            _maestromPosition3 = new();
            (_row, _column) = MaelstromCavePosition3(_rowMax, _colMax);
            _maestromPosition3.Row = _row;
            _maestromPosition3.Column = _column;
            PlayArea[_row, _column] = new AMaelstrom();

            _maestromPosition4 = new();
            (_row, _column) = MaelstromCavePosition4(_rowMax, _colMax);
            _maestromPosition4.Row = _row;
            _maestromPosition4.Column = _column;
            PlayArea[_row, _column] = new AMaelstrom();
        }
    }


    private void PositionAmaroksOnBoard()
    {
        int _rowMax = PlayArea.GetUpperBound(0);
        int _colMax = PlayArea.GetUpperBound(1);

        int _row;
        int _column;

        // initial Amarok placed on Small board

        _amarockPosition1 = new();
        (_row, _column) = AmarokCavePosition1(_rowMax, _colMax);
        _amarockPosition1.Row = _row;
        _amarockPosition1.Column = _column;
        PlayArea[_row, _column] = new AnAmarock();


        // Medium board  plus one  Amarok 
        if (_rowMax > 3)
        {
            _amarockPosition2 = new();
            (_row, _column) = AmarokCavePosition2(_rowMax, _colMax);
            _amarockPosition2.Row = _row;
            _amarockPosition2.Column = _column;
            PlayArea[_row, _column] = new AnAmarock();
        }

        // Large board plus one more Amarok
        if (_rowMax > 5)
        {
            _amarockPosition3 = new();
            (_row, _column) = AmarokCavePosition3(_rowMax, _colMax);
            _amarockPosition3.Row = _row;
            _amarockPosition3.Column = _column;
            PlayArea[_row, _column] = new AnAmarock();
        }

    }


    /// <summary>
    /// Main game loop.
    /// </summary>
    public void GetPlayerChoice()
    {
        bool _validResponse = false;

        do
        {
            WriteLine(new string('-', 90));
            ForegroundColor = ConsoleColor.Gray;

            // Discover/action perils in current cave.
            if (CheckForPits() || CheckForAmaroks())
            {
                // Player is in an endless fall or met an Amarok! No use hanging about.
                break;
            }
            CheckForMaelstroms();

            WriteLine($"You are in the cave at ({_gamePlayer.Row}, {_gamePlayer.Column})");
            if (_gamePlayer.Arrows > 0)
            {
                WriteLine($"You have a bow and {_gamePlayer.Arrows} arrows.");
            }

            // Print cave description before moving player.
            PlayArea[_gamePlayer.Row, _gamePlayer.Column].ToString();

            string _playerMovement = GetMovementInstruction().ToLower().Trim();

            _validResponse = _playerMovement switch
            {
                "move east" or "east" or "e" => MoveEast(),
                "move west" or "west" or "w" => MoveWest(),
                "move north" or "north" or "n" => MoveNorth(),
                "move south" or "south" or "s" => MoveSouth(),
                "enable fountain" or "enable" or "activate" => EnableFountain(),
                "attack" or "shoot" or "loose" or "a" => Attack(),
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
        if (_gamePlayer.Column + 1
            > PlayArea.GetUpperBound(0)) _gamePlayer.Column = PlayArea.GetLowerBound(0);
        else _gamePlayer.Column++;

        return true;
    }

    private bool MoveWest()
    {

        if (_gamePlayer.Column - 1
            < PlayArea.GetLowerBound(0)) _gamePlayer.Column = PlayArea.GetUpperBound(0);
        else _gamePlayer.Column--;

        return true;
    }

    private bool MoveNorth()
    {
        if (_gamePlayer.Row - 1
            < PlayArea.GetLowerBound(1)) _gamePlayer.Row = PlayArea.GetUpperBound(1);
        else _gamePlayer.Row--;

        return true;
    }

    private bool MoveSouth()
    {
        if (_gamePlayer.Row + 1
            > PlayArea.GetUpperBound(1)) _gamePlayer.Row = PlayArea.GetLowerBound(1);
        else _gamePlayer.Row++;

        return true;
    }

    private bool EnableFountain()
    {
        // Is the player is at fountain position?
        if (_fountainPosition.IsTheSameAs(_gamePlayer))
        {
            // Change messages
            ActivatedFountainCave _activeFountain = new();
            PlayArea[_gamePlayer.Row, _gamePlayer.Column] = _activeFountain;

            // Change cavern entrance message.
            ActivatedCavernEntrance _activeEntrance = new();
            PlayArea[_entrancePosition.Row, _entrancePosition.Column] = _activeEntrance;
        }
        // Nope!
        else WriteLine("Can't be done - you  have to be present in the fountain cave to do that!");

        return true;
    }

    private bool Attack()
    {
        if (_gamePlayer.Bow && _gamePlayer.Arrows > 0)
        {
            bool result = GetAttackDirection() switch
            {
                "1" or "n" or "north" => ShootNorth(),
                "2" or "e" or "east" => ShootEast(),
                "3" or "s" or "south" => ShootSouth(),
                "4" or "w" or "west" => ShootWest(),
                _ => true
            };

            return result;
        }
        else if (!_gamePlayer.Bow)
        {
            WriteLine("Attack with what?");
            return true;

        }
        else
        {
            WriteLine("You've no arrows in your quiver!");
            return true;

        }
    }

    private string GetAttackDirection()
    {
        string _menu = """

        You choose to attack - but in which direction? [enter a number or direction]

        1.  Shoot North
        2.  Shoot East
        3.  Shoot South
        4.  Shoot West

        """;

        WriteLine(_menu);
        Write(" >  ");
        return ReadLine()!.ToLower();
    }

    /// <summary>
    ///  Restrict shooting to within the board - no wrap round but no arrow reduction attempted shot off board!
    /// </summary>
    /// <returns> The success of the arrow</returns>
    private bool ShootNorth()
    {
        if (_gamePlayer.Row > 0)
        {
            DetermineArrowResult(_gamePlayer.Column, _gamePlayer.Row - 1);
            _gamePlayer.Arrows -= 1;
        }
        return true;
    }

    private bool ShootEast()
    {
        if (_gamePlayer.Column < PlayArea.GetUpperBound(0))
        {
            DetermineArrowResult(_gamePlayer.Row, _gamePlayer.Column + 1);
            _gamePlayer.Arrows -= 1;
        }

        return true;
    }

    private bool ShootSouth()
    {
        if (_gamePlayer.Row < PlayArea.GetUpperBound(0))
        {
            DetermineArrowResult(_gamePlayer.Row + 1, _gamePlayer.Column);
            _gamePlayer.Arrows -= 1;
        }

        return true;
    }
    private bool ShootWest()
    {
        if (_gamePlayer.Column > 0)
        {
            DetermineArrowResult(_gamePlayer.Row, _gamePlayer.Column - 1);
            _gamePlayer.Arrows -= 1;
        }

        return true;
    }

    private void DetermineArrowResult(int row, int column)
    {
        CheckForShotMaelstroms(row, column);
        CheckForShotAmaroks(row, column);
    }

    private void CheckForShotMaelstroms(int row, int column)
    {
        int _rowMax = PlayArea.GetUpperBound(0);

        // Small board
        if (_maestromPosition1.IsTheSameAs(row, column))
        {
            PlayArea[row, column] = new AnEmptyCave();
            _maestromPosition1.Row = -1;
            _maestromPosition1.Column = -1;
        }

        // Medium board
        if (_rowMax > 3 && _maestromPosition2.IsTheSameAs(row, column))
        {
            PlayArea[row, column] = new AnEmptyCave();
            _maestromPosition2.Row = -1;
            _maestromPosition2.Column = -1;
        }

        // Large board
        if (_rowMax > 5)
        {
            if (_maestromPosition3.IsTheSameAs(row, column))
            {
                PlayArea[row, column] = new AnEmptyCave();
                _maestromPosition3.Row = -1;
                _maestromPosition3.Column = -1;
            }

            if (_maestromPosition4.IsTheSameAs(row, column))
            {
                PlayArea[row, column] = new AnEmptyCave();
                _maestromPosition4.Row = -1;
                _maestromPosition4.Column = -1;
            }
        }

    }

    /// <summary>
    /// If the arrow hits an Amarok remove it from the board and remove the cave reference in the struct.
    /// </summary>
    /// <param name="column"></param>
    /// <param name="row"></param>
    private void CheckForShotAmaroks(int row, int column)
    {
        int _rowMax = PlayArea.GetUpperBound(0);

        //Small Board
        if (_amarockPosition1.IsTheSameAs(row, column))
        {
            PlayArea[row, column] = new AnEmptyCave();
            _amarockPosition1.Row = -1;
            _amarockPosition1.Column = -1;
        }

        // Medium Board
        if (_rowMax > 3 && _amarockPosition2.IsTheSameAs(row, column))
        {
            PlayArea[row, column] = new AnEmptyCave();
            _amarockPosition2.Row = -1;
            _amarockPosition2.Column = -1;
        }

        // Large Board
        if (_rowMax > 5 && _amarockPosition3.IsTheSameAs(row, column))
        {
            PlayArea[_gamePlayer.Row, _gamePlayer.Column] = new AnEmptyCave();
            _amarockPosition3.Row = -1;
            _amarockPosition3.Column = -1;
        }
    }

    private bool CheckForPits()
    {
        // Assumes a rectangular board.
        int _rowMax = PlayArea.GetUpperBound(0);

        // One default pit on all boards

        if (_pitPosition1.IsTheSameAs(_gamePlayer))
        {
            // You fell in!
            PlayArea[_gamePlayer.Row, _gamePlayer.Column].ToString();
            return true;
        }

        // A Pit is near but don't print its message if the player is about to be blown away to a new cave.
        if (!_maestromPosition1.IsTheSameAs(_gamePlayer))
        {
            _pitPosition1.IsAdjacentTo(_gamePlayer);
        }

        // An extra pit on the Medium board
        if (_rowMax > 3)
        {
            // Pit 2
            if (_pitPosition2.IsTheSameAs(_gamePlayer))
            {
                PlayArea[_gamePlayer.Row, _gamePlayer.Column].ToString();
                return true;
            }
            if (!_maestromPosition2.IsTheSameAs(_gamePlayer))
            {
                _pitPosition2.IsAdjacentTo(_gamePlayer);
            }
        }

        // Two  extra pits on Large board.
        if (_rowMax > 5)
        {
            // Pit 3
            if (_pitPosition3.IsTheSameAs(_gamePlayer))
            {
                PlayArea[_gamePlayer.Row, _gamePlayer.Column].ToString();
                return true;
            }
            if (!_maestromPosition3.IsTheSameAs(_gamePlayer))
            {
                _pitPosition3.IsAdjacentTo(_gamePlayer);
            }

            // Pit 4
            if (_pitPosition4.IsTheSameAs(_gamePlayer))
            {
                PlayArea[_gamePlayer.Row, _gamePlayer.Column].ToString();
                return true;
            }
            if (!_maestromPosition4.IsTheSameAs(_gamePlayer))
            {
                _pitPosition4.IsAdjacentTo(_gamePlayer);
            }
        }
        return false;
    }

    private bool CheckForAmaroks()
    {
        int _rowMax = PlayArea.GetUpperBound(0);

        if (_amarockPosition1.IsTheSameAs(_gamePlayer))
        {
            // You died!
            PlayArea[_gamePlayer.Row, _gamePlayer.Column].ToString();
            return true;
        }

        if (!_amarockPosition1.IsTheSameAs(_gamePlayer))
        {
            _amarockPosition1.IsAdjacentTo(_gamePlayer);
        }

        if (_rowMax > 3)
        {
            if (_amarockPosition2.IsTheSameAs(_gamePlayer))
            {
                PlayArea[_gamePlayer.Row, _gamePlayer.Column].ToString();
                return true;
            }

            if (!_amarockPosition2.IsTheSameAs(_gamePlayer))
            {
                _amarockPosition2.IsAdjacentTo(_gamePlayer);
            }
        }

        if (_rowMax > 5)
        {
            if (_amarockPosition3.IsTheSameAs(_gamePlayer))
            {
                PlayArea[_gamePlayer.Row, _gamePlayer.Column].ToString();
                return true;
            }

            if (!_amarockPosition3.IsTheSameAs(_gamePlayer))
            {
                _amarockPosition3.IsAdjacentTo(_gamePlayer);
            }
        }
        return false;
    }

    private void CheckForMaelstroms()
    {
        // Assumes rectangular board.
        int _rowMax = PlayArea.GetUpperBound(0);

        // Small board maelstrom1 is present.

        _maestromPosition1.IsAdjacentTo(_gamePlayer);

        if (_maestromPosition1.IsTheSameAs(_gamePlayer))
        {
            PlayArea[_gamePlayer.Row, _gamePlayer.Column].ToString();
            BlowPlayerToAnotherCave();

            (int row, int column) = MoveMaelstrom(_maestromPosition1.Row, _maestromPosition1.Column);

            _maestromPosition1.Row = row;
            _maestromPosition1.Column = column;
        }


        // Medium board one Maelstrom
        if (_rowMax > 3)
        {
            _maestromPosition2.IsAdjacentTo(_gamePlayer);


            if (_maestromPosition2.IsTheSameAs(_gamePlayer))
            {
                PlayArea[_gamePlayer.Row, _gamePlayer.Column].ToString();
                BlowPlayerToAnotherCave();

                (int row, int column) = MoveMaelstrom(_maestromPosition2.Row, _maestromPosition2.Column);

                _maestromPosition2.Row = row;
                _maestromPosition2.Column = column;
            }
        }

        // Large board with 2 extra Maelstroms
        if (_rowMax > 5)
        {
            _maestromPosition3.IsAdjacentTo(_gamePlayer);

            if (_maestromPosition3.IsTheSameAs(_gamePlayer))
            {
                PlayArea[_gamePlayer.Row, _gamePlayer.Column].ToString();
                BlowPlayerToAnotherCave();

                (int row, int column) = MoveMaelstrom(_maestromPosition3.Row, _maestromPosition3.Column);

                _maestromPosition3.Row = row;
                _maestromPosition3.Column = column;
            }

            _maestromPosition4.IsAdjacentTo(_gamePlayer);

            if (_maestromPosition4.IsTheSameAs(_gamePlayer))
            {
                PlayArea[_gamePlayer.Row, _gamePlayer.Column].ToString();
                BlowPlayerToAnotherCave();

                (int row, int column) = MoveMaelstrom(_maestromPosition4.Row, _maestromPosition4.Column);

                _maestromPosition4.Row = row;
                _maestromPosition4.Column = column;
            }
        }
    }

    /// <summary>
    /// //Move a Maelstrom to a new position after resetting 'old'  cave.
    /// </summary>
    /// <param names="row, column">Existing Maelstrom position</param>
    /// <returns></returns>
    private (int, int) MoveMaelstrom(int row, int column)
    {
        PlayArea[row, column] = new AnEmptyCave();

        do
        {
            // South
            if (row + 1 > PlayArea.GetUpperBound(1)) row = PlayArea.GetLowerBound(1);
            else row++;
            // West * 2
            if (column - 1 < PlayArea.GetLowerBound(0)) column = PlayArea.GetUpperBound(0);
            else column--;
            if (column - 1 < PlayArea.GetLowerBound(0)) column = PlayArea.GetUpperBound(0);
            else column--;
        }
        // Redo if the new cave position overwrites the fountain or entrance caves
        while (_fountainPosition.IsTheSameAs(row, column) || _entrancePosition.IsTheSameAs(row, column));

        PlayArea[row, column] = new AMaelstrom();

        return (row, column);
    }

    private void BlowPlayerToAnotherCave()
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
        Description = "You have fallen into a bottomless pit. Good luck with that!  See you in the next life. Bye .... ";
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
    public string Description { get; private set; }

    public AMaelstrom()
    {
        Description = "You have encountered A Maelstrom and been blown to another cave!";
    }

    public override string ToString()
    {
        ForegroundColor = ConsoleColor.Green;
        WriteLine(Description);
        ResetColor();
        return string.Empty;
    }
}

public class AnAmarock : ICave
{
    public string Description { get; set; }

    public AnAmarock()
    {
        Description = "You  stumbled into an Amarok's lair and died instantly. Too bad really ... Bye!";
    }

    public override string ToString()
    {
        ForegroundColor = ConsoleColor.Yellow;
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
        Description = "A creepy, dank, rough  and deserted cave. Move along now - nothing to see here!";
    }
    public override string ToString()
    {
        WriteLine(Description);

        return string.Empty;
    }
}