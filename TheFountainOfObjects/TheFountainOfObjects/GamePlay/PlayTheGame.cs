// See https://aka.ms/new-console-template for more information

// Ignore Spelling: Amarok Amarok

// Experimental static usings.
using TheFountainOfObjects.GameObjects;
using static System.Console;
using static TheFountainOfObjects.Utilities.BoardObjectPositions;
using static TheFountainOfObjects.Utilities.SpecialCavesMapPositionSetup;


namespace TheFountainOfObjects.GamePlay;

/// <summary>
///  Setting up the board
/// </summary>
public class PlayTheGame
{
    PlayerPosition _gamePlayer;

    // Dictionary - as identified positions are needed
    private readonly Dictionary<string, BaseObjectPosition> _baseObjectPositions = [];

    // Lists used as need a way to be iterate through several times.
    // TODO Amalgamate Maelstrom and Amerok Lists? Will not need to separate out different perils -
    // BUT Pits are different and cannot be shot, although arrows can be wasted on them.

    private readonly List<PerilPosition> _pitPositions = [];
    private readonly List<PerilPosition> _amarokPositions = [];
    private readonly List<PerilPosition> _maelstromPositions = [];


    /// <summary>
    /// The playing board.
    /// </summary>
    public ICave[,] PlayArea { get; private set; }

    /// <summary>
    /// Set up the playing area caves calculated on the size of the board
    /// </summary>
    /// <param name="playArea">The board with background setup to be populated with the interesting bits</param>
    internal PlayTheGame(ICave[,] playArea)
    {
        PlayArea = playArea;
        PositionTheBaseObjects();
        FillTheBoardWithPerils();

        // Player always starts at the _entrance - wherever that might end up
        _gamePlayer = new() { Row = _baseObjectPositions["entrance"].Row, Column = _baseObjectPositions["entrance"].Column };

        // Armed and dangerous
        _gamePlayer.Bow = true;
        _gamePlayer.Arrows = 5;
    }

    /// <summary>
    ///  Entrance and fountain positions written to the  _baseObjectPositions Dictionary
    /// </summary>

    // TODO Add a randomly positioned arrow cache (5?) to L and larger boards
    public void PositionTheBaseObjects()
    {
        int rowMax = PlayArea.GetUpperBound(0);
        int colMax = PlayArea.GetUpperBound(1);

        var (row, column) = FountainCavePosition(rowMax, colMax);
        BaseObjectPosition fountainPosition = new() { Row = row, Column = column };
        PlayArea[row, column] = new FountainCave();
        _baseObjectPositions["fountain"] = fountainPosition;

        (row, column) = EntranceCavePosition(rowMax, colMax);
        BaseObjectPosition entrancePosition = new() { Row = row, Column = column };
        PlayArea[row, column] = new CavernEntrance();
        _baseObjectPositions["entrance"] = entrancePosition;
    }


    /// <summary>
    ///  Fill the object position Structs and complete the board according to each of the Expansions.
    ///  Design ensures that only one method is needed to fill all sizes of board. 
    ///  
    /// Avoiding the use of 'hard coded' peril positions will enable computed positions to be used in  later iterations of the basic game
    /// avoiding the need for a major refactoring.
    /// </summary>
    public void FillTheBoardWithPerils()
    {
        // Using board sizes directly from the board (arrays) to avoid index out of bounds errors

        PositionPitsOnBoard();
        PositionMaelstromsOnBoard();
        PositionAmaroksOnBoard();
    }

    private void PositionPitsOnBoard()
    {
        int rowMax = PlayArea.GetUpperBound(0);
        int colMax = PlayArea.GetUpperBound(1);

        // First Pit on Small board

        AddPits(PitCavePosition1(rowMax, colMax));

        // Extra pit on Medium board
        if (rowMax > 3)
        {
            AddPits(PitCavePosition2(rowMax, colMax));

            // 2 extra pits on Large board
            if (rowMax > 5)
            {
                AddPits(PitCavePosition3(rowMax, colMax));

                AddPits(PitCavePosition4(rowMax, colMax));
            }
        }
    }

    private void AddPits((int row, int column) position)
    {
        PlayArea[position.row, position.column] = new BottomlessPit();
        _pitPositions.Add(new PerilPosition() { Row = position.row, Column = position.column });
    }

    private void PositionMaelstromsOnBoard()
    {
        int rowMax = PlayArea.GetUpperBound(0);
        int colMax = PlayArea.GetUpperBound(1);

        // Initial Maelstrom on Small board

        AddMaelstroms(MaelstromCavePosition1(rowMax, colMax));

        // Medium board  plus one extra Maelstrom
        if (rowMax > 3)
        {
            AddMaelstroms(MaelstromCavePosition2(rowMax, colMax));

            // Large board plus two extra
            if (rowMax > 5)
            {
                AddMaelstroms(MaelstromCavePosition3(rowMax, colMax));

                AddMaelstroms(MaelstromCavePosition4(rowMax, colMax));
            }
        }
    }

    private void AddMaelstroms((int row, int column) position)
    {
        PlayArea[position.row, position.column] = new Maelstrom();
        _maelstromPositions.Add(new PerilPosition() { Row = position.row, Column = position.column });
    }

    private void PositionAmaroksOnBoard()
    {
        int rowMax = PlayArea.GetUpperBound(0);
        int colMax = PlayArea.GetUpperBound(1);

        // initial Amarok on Small board

        AddAmaroks(AmarokCavePosition1(rowMax, colMax));


        // Medium board  plus one  Amarok
        if (rowMax > 3)
        {
            AddAmaroks(AmarokCavePosition2(rowMax, colMax));

            // Large board plus one more Amarok
            if (rowMax > 5)
            {
                AddAmaroks(AmarokCavePosition3(rowMax, colMax));
            }
        }
    }

    private void AddAmaroks((int row, int column) position)
    {
        PlayArea[position.row, position.column] = new Amarok();
        _amarokPositions.Add(new PerilPosition() { Row = position.row, Column = position.column });
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

            // Discover and take action for perils in current cave.
            if (CheckForPits() || CheckForAmaroks())
            {
                // Player is in an endless fall or has met an Amarok! That's that then.
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
                "move east" or "east" or "e" => MovePlayerEast(),
                "move west" or "west" or "w" => MovePlayerWest(),
                "move north" or "north" or "n" => MovePlayerNorth(),
                "move south" or "south" or "s" => MovePlayerSouth(),
                "enable fountain" or "enable" or "activate" => EnableFountain(),
                "attack" or "shoot" or "loose" or "a" => Attack(),
                "help" or "h" => PrintHelp(),
                "quit" or "exit" or "q" => false,
                _ => true
            };
        } while (_validResponse);

        ForegroundColor = ConsoleColor.Red;
        Write("\nGame over.");
        ResetColor();
        WriteLine("   Press any key to end.");
        ReadKey();
    }

    private static string GetMovementInstruction()
    {
        Write("What do you want to do now? >   ");
        ForegroundColor = ConsoleColor.Cyan;
        string? _response = ReadLine();
        ResetColor();

        return _response!;
    }

    /// <summary>
    /// Move - but stay on the board by wrapping round: predicated on each board's actual boundaries - not 'magic' numbers.
    /// </summary>
    /// <returns> Returns new position </returns>

    private bool MovePlayerEast()
    {
        if (_gamePlayer.Column + 1
            > PlayArea.GetUpperBound(0)) _gamePlayer.Column = PlayArea.GetLowerBound(0);
        else _gamePlayer.Column++;

        return true;
    }

    private bool MovePlayerWest()
    {
        if (_gamePlayer.Column - 1
            < PlayArea.GetLowerBound(0)) _gamePlayer.Column = PlayArea.GetUpperBound(0);
        else _gamePlayer.Column--;

        return true;
    }

    private bool MovePlayerNorth()
    {
        if (_gamePlayer.Row - 1
            < PlayArea.GetLowerBound(1)) _gamePlayer.Row = PlayArea.GetUpperBound(1);
        else _gamePlayer.Row--;

        return true;
    }

    private bool MovePlayerSouth()
    {
        if (_gamePlayer.Row + 1
            > PlayArea.GetUpperBound(1)) _gamePlayer.Row = PlayArea.GetLowerBound(1);
        else _gamePlayer.Row++;

        return true;
    }

    private bool EnableFountain()
    {
        // Is the player is at fountain position?
        if (_baseObjectPositions["fountain"].PlayerIsTheSameAs(_gamePlayer))
        {
            // Change fountain message
            PlayArea[_gamePlayer.Row, _gamePlayer.Column] = new ActivatedFountainCave();

            // Change cavern _entrance message.
            PlayArea[_baseObjectPositions["entrance"].Row, _baseObjectPositions["entrance"].Column] = new ActivatedCavernEntrance();
        }
        // Nope!
        else WriteLine("\nCan't be done - you  have to be present in the fountain cave to do that!");

        return true;
    }

    private bool Attack()
    {
        if (_gamePlayer.Bow && _gamePlayer.Arrows > 0)
        {
            bool result = GetAttackDirection() switch
            {
                "1" or "n" or "north" => ShootArrowNorth(),
                "2" or "e" or "east" => ShootArrowEast(),
                "3" or "s" or "south" => ShootArrowSouth(),
                "4" or "w" or "west" => ShootArrowWest(),
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

    private static string GetAttackDirection()
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
    ///  Restrict shooting to within the board - no wrap round but no arrow reduction for an off board shot!
    ///  TODO Revise when peril detection around board completed (ie shoot from (1,7) at  peril at (1.0).
    /// </summary>
    /// <returns> The success of the arrow being shot</returns>
    private bool ShootArrowNorth()
    {
        if (_gamePlayer.Row > 0)
        {
            DetermineArrowShotResult(_gamePlayer.Column, _gamePlayer.Row - 1);
            _gamePlayer.Arrows--;
        }
        return true;
    }

    private bool ShootArrowEast()
    {
        if (_gamePlayer.Column < PlayArea.GetUpperBound(0))
        {
            DetermineArrowShotResult(_gamePlayer.Row, _gamePlayer.Column + 1);
            _gamePlayer.Arrows--;
        }
        return true;
    }

    private bool ShootArrowSouth()
    {
        if (_gamePlayer.Row < PlayArea.GetUpperBound(0))
        {
            DetermineArrowShotResult(_gamePlayer.Row + 1, _gamePlayer.Column);
            _gamePlayer.Arrows--;
        }
        return true;
    }

    private bool ShootArrowWest()
    {
        if (_gamePlayer.Column > 0)
        {
            DetermineArrowShotResult(_gamePlayer.Row, _gamePlayer.Column - 1);
            _gamePlayer.Arrows--;
        }
        return true;
    }

    private void DetermineArrowShotResult(int row, int column)
    {
        // If successful remove all traces of the peril from the board and its List
        CheckForShotMaelstroms(row, column);
        CheckForShotAmaroks(row, column);
        CheckForShotsAtPits(row, column);
    }

    private void CheckForShotMaelstroms(int row, int column)
    {
        int index = -1;

        foreach (var item in _maelstromPositions.Where(x => x.ArrowIsTheSameAs(row, column)))
        {

            AnAttackedMaelstrom();
            index = _maelstromPositions.IndexOf(item);


        }

        if (index > -1)
        {
            // Success!
            PlayArea[row, column] = new EmptyCave();
            _maelstromPositions.RemoveAt(index);
        }
    }

    /// <summary>
    /// If the arrow hits an Amarok remove it from the board and remove the cave reference.
    /// </summary>
    /// <param name="column"></param>
    /// <param name="row"></param>
    private void CheckForShotAmaroks(int row, int column)
    {
        int index = -1;

        foreach (var item in _amarokPositions.Where(x => x.ArrowIsTheSameAs(row, column)))
        {
            AnAttackedAmarok();
            index = _amarokPositions.IndexOf(item);
        }

        if (index > -1)
        {
            // Good shot sir!
            PlayArea[row, column] = new EmptyCave();
            _amarokPositions.RemoveAt(index);
        }
    }

    /// <summary>
    ///  Leave a message about the wasted arrow
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    private void CheckForShotsAtPits(int row, int column)
    {
        foreach (var item in _pitPositions.Where(x => x.ArrowIsTheSameAs(row, column)))
        {
            WriteLine("\nYou hear your arrow clatter uselessly into the pit. What a waste!");
        }
    }

    private bool CheckForPits()
    {
        // One default pit on all boards

        // Search through Maelstroms List to find whether the player is about to be blown away, no need for a peril adjacent message.
        foreach (var item in _maelstromPositions.Where(x => x.PlayerIsTheSameAs(_gamePlayer)))
        {

            return false;

        }

        foreach (var item in _pitPositions)
        {
            if (item.PlayerIsAdjacentTo(_gamePlayer))
            {
                AnAdjacentPit();
            }

            if (item.PlayerIsTheSameAs(_gamePlayer))
            {
                // You fell in!
                PlayArea[_gamePlayer.Row, _gamePlayer.Column].ToString();
                return true;
            }
        }
        return false;
    }

    private bool CheckForAmaroks()
    {
        foreach (var item in _amarokPositions)
        {

            if (item.PlayerIsTheSameAs(_gamePlayer))
            {
                // You died!
                PlayArea[_gamePlayer.Row, _gamePlayer.Column].ToString();
                return true;
            }

            if (item.PlayerIsAdjacentTo(_gamePlayer))
            {
                AnAdjacentAmarok();
            }
        }
        return false;
    }

    private void CheckForMaelstroms()
    {
        // Assumes rectangular board.

        (int row, int column) _newPosition = (0, 0);
        int index = -1;

        foreach (var item in _maelstromPositions)
        {
            if (item.PlayerIsAdjacentTo(_gamePlayer))
            {
                AnAdjacentMaelstrom();
            }

            if (item.PlayerIsTheSameAs(_gamePlayer))
            {
                PlayArea[_gamePlayer.Row, _gamePlayer.Column].ToString();
                BlowPlayerToAnotherCave();

                _newPosition = MoveMaelstrom(item.Row, item.Column);
                index = _maelstromPositions.IndexOf(item);
                break;
            }
        }

        if (index > -1)
            _maelstromPositions[index] = new PerilPosition(_newPosition.row, _newPosition.column);
    }

    /// <summary>
    /// Move a Maelstrom to a new position after resetting 'old'  cave.
    /// </summary>
    /// <param names="row, column">Existing Maelstrom position</param>
    /// <returns></returns>
    private (int row, int column) MoveMaelstrom(int row, int column)
    {
        PlayArea[row, column] = new EmptyCave();

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

        // Redo if the new cave position overwrites game objects
        while (OverWritesGameObject(row, column));

        PlayArea[row, column] = new Maelstrom();

        return (row, column);
    }

    private bool OverWritesGameObject(int row, int column)
    {
        bool returnValue = false;

        if (_baseObjectPositions["entrance"].IsTheSameAs(row, column) || _baseObjectPositions["fountain"].IsTheSameAs(row, column))
        {
            return true;
        }

        foreach (var item in _amarokPositions.Where(x => x.Row == row && x.Column == column))
        {
            returnValue = true;
        }

        foreach (var item in _maelstromPositions.Where(x => x.Row == row && x.Column == column))
        {
            returnValue = true;
        }
        return returnValue;
    }

    private void BlowPlayerToAnotherCave()
    {
        MovePlayerNorth();
        MovePlayerEast();
        MovePlayerEast();

        // Check for perils around the new position - or have ended on top of a peril!
        CheckForAmaroks();
        CheckForMaelstroms();
        CheckForPits();
    }

    private static bool PrintHelp()
    {
        string _helpMenu = """

        'Move North' or 'North' or 'N'  => Move to the North
        'Move East' or 'East' or 'E'  => Move to the East
        'Move South' or 'South' or 'S'  => Move to the South
        'Move West' or 'West' or 'W'  => Move to the West

        'Attack' or 'Shoot' or 'Loose' will enable you to fire arrows at monsters, if you have them.
        "Enable Fountain" or "Enable" or "Activate" will restart the Fountain but only if you are in its presence.

        """;
        WriteLine(_helpMenu);

        return true;
    }

    private static void AnAdjacentPit()
    {
        ForegroundColor = ConsoleColor.Red;
        WriteLine("You feel a draught, There is a pit nearby!");
        ResetColor();
    }

    private static void AnAdjacentAmarok()
    {
        ForegroundColor = ConsoleColor.Yellow;
        WriteLine("You smell the rotten stench of an Amarok in a nearby cave!");
        ResetColor();
    }

    private static void AnAttackedAmarok()
    {
        ForegroundColor = ConsoleColor.Yellow;
        WriteLine("\nYou hear the whines and yelps of the fatally stricken Amarok. It will trouble you no more!");
        ResetColor();
    }

    private static void AnAdjacentMaelstrom()
    {
        ForegroundColor = ConsoleColor.Green;
        WriteLine("You hear the growling and groaning of a Maelstrom nearby!");
        ResetColor();
    }

    private static void AnAttackedMaelstrom()
    {
        ForegroundColor = ConsoleColor.Green;
        WriteLine("\nThe roaring of the Maelstrom fades away, transformed into a fragrant zephyr.");
        ResetColor();
    }
}

