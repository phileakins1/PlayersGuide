﻿// See https://aka.ms/new-console-template for more information

// Ignore Spelling: Amarok Amarok

// Experimental static usings.
using static BoardObjectPositions;
using static SpecialCavesMapPositionSetup;
using static System.Console;

// Using The Book with the C# 11 Expansion
// C# Players Guide Fountain of Objects Challenge. - 1000 XP at stake

// Basic challenge completed; plus:
// Expansion 1 - Three sizes of playing board - completed
// Expansion 2 - Pit challenge (accommodating different size boards) - completed
// Expansion 3 - Maelstrom challenge (accommodating different size boards) - completed
// Expansion 4 - Amarok challenge (accommodating different size boards) - completed
// Expansion 5 - Bows and arrows - completed for 4 cardinal points
// Expansion 6 - Instructions and help - Completed ...

// Refactored Version 30th September 2024.

// TODO Refactor the program into separate class based files / name spaces / directory structure

// Create the game board
(int _row, int _columns) = SelectCavernSize.GetCavernSize();
ICave[,] _caves = new ICave[_row, _columns];

// Initialize the board positions sending the _caves array in a class declaration parameter
BoardObjectPositions playArea = new(_caves);

// Window title and Instructions
playArea.StartupScreens();

// Fill the array with background cave objects
_caves = playArea.FillEmptyCaves();

// Create the game board sending a parameter of the array containing  'empty' caves
PlayTheGame play = new(_caves);

// Start the game loop
play.GetPlayerChoice();

public class SelectCavernSize
{
    private SelectCavernSize() { }

    // TODO setup future X and Y choices for a custom size cavern

    /// <summary>
    /// Establishes the playing board size.
    /// </summary>
    /// <returns>A tuple with the row and column numerations for its creation.</returns>

    public static (int row, int column) GetCavernSize()
    {
        (int, int) _size = (0, 0);
        string? _choice;
        bool _validResponse = false;

        do
        {
            WriteBoardSizeMenu();
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

    private static void WriteBoardSizeMenu()
    {
        WriteLine(
          """
                    Choose the size of the cavern
        
                    S for a small (4 X 4) cavern
                    M for a medium (6 X 6) cavern
                    L for a large (8 X 8) cavern
                    """);
    }
}

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

/// <summary>
/// Structs instead of classes - problem is that structs cannot be null when no longer needed to allow collection - set position off board.
/// </summary>
/// <param name="caves">The playing board</param>
public class BoardObjectPositions(ICave[,] caves)
{
    public ICave[,] Caves { get; private set; } = caves;

    /// <summary>
    /// Fill the play area with background noise.
    /// </summary>
    /// <returns>A board full of 'empty' caves</returns>

    public ICave[,] FillEmptyCaves()
    {
        for (int i = 0; i < Caves.GetUpperBound(0) + 1; i++)
        {
            for (int j = 0; j < Caves.GetUpperBound(1) + 1; j++)
            {
                Caves[i, j] = new EmptyCave();
            }
        }
        return Caves;
    }
    public struct PlayerPosition
    {
        public int Arrows { get; set; }
        public bool Bow { get; set; }

        public int Row { get; set; }
        public int Column { get; set; }

        public PlayerPosition() { }
    }

    /// <summary>
    ///  BaseObjectPosition has base members which PerilPosition could use but which cannot be inherited.
    /// </summary>

    public struct BaseObjectPosition
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public BaseObjectPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public readonly bool PlayerIsTheSameAs(PlayerPosition player)
        {
            if (this.Row == player.Row && this.Column == player.Column) return true;
            else return false;
        }

        public readonly bool IsTheSameAs(int row, int column)
        {
            if (this.Row == row && this.Column == column) return true;
            else return false;
        }
    }

    /// <summary>
    ///  PerilPositions (Qua Interface Segregation Principle - adds arrow position on top of Base conditions ).
    /// </summary>
    public struct PerilPosition
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public PerilPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public readonly bool PlayerIsTheSameAs(PlayerPosition player)
        {
            if (this.Row == player.Row && this.Column == player.Column) return true;
            else return false;
        }

        public readonly bool IsTheSameAs(int row, int column)
        {
            if (this.Row == row && this.Column == column) return true;
            else return false;
        }

        public readonly bool ArrowIsTheSameAs(int row, int column)
        {
            if (this.Row == row && this.Column == column)
                return true;
            else return false;
        }

        public readonly bool PlayerIsAdjacentTo(PlayerPosition player)
        {
            if (!(this.Row - player.Row is < -1 or > 1 || this.Column - player.Column is < -1 or > 1) && (!PlayerIsTheSameAs(player)))
                return true;
            else return false;
        }
    }


    /// <summary>
    /// Beginning setup
    /// </summary>
    public void StartupScreens()
    {
        Console.Title = "The quest for the Fountain of Objects";
        Console.Clear();

        WriteLine(StartUpMenu());

        Write("Press any key to continue:  ");
        Console.ReadLine();
    }

    public static string StartUpMenu()
    {
        string _menu = """

            Once upon a time, in a galaxy far, far away ...

            You, a fearless adventurer, enter the Cavern of Objects, a maze of caves filled with dangerous pits to seek and activate the Fountain of Objects.

            Light is visible only in the entrance, little other light is to be found in the cavern.  You must navigate the caves with your senses. Find the Fountain of Objects, activate it, and return to the entrance.

            Look out for Pits, you will feel a breeze if a pit is nearby. If you enter a cave with a pit, you will fall eternally.

            Maelstroms are violent forces of sentient wind. Enter a cave with one and you will be violently ejected into another cave. You will be able to hear the growling and groaning from nearby caves.

            Amaroks roam the caverns. Encountering one is certain death, but you can smell their rotten stench for caves around.

            If you carry with you a bow and a quiver of arrows, you can shoot at the monsters in adjacent caves but beware, you have a limited supply of arrows so don't waste them.

            Typing 'Help' or ('H' for short) will show a list of commands which will help you navigate the Cavern.

            """;
        return _menu;
    }


}

/// <summary>
///  Setting up the board
/// </summary>
public class PlayTheGame
{
    PlayerPosition gamePlayer;

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
    public PlayTheGame(ICave[,] playArea)
    {
        PlayArea = playArea;
        PositionTheBaseObjects();
        FillTheBoardWithPerils();

        // Player always starts at the _entrance - wherever that might end up
        gamePlayer = new() { Row = _baseObjectPositions["entrance"].Row, Column = _baseObjectPositions["entrance"].Column };

        // Armed and dangerous
        gamePlayer.Bow = true;
        gamePlayer.Arrows = 5;
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
        var pit = PitCavePosition1(rowMax, colMax);
        AddPits(pit);

        // Extra pit on Medium board
        if (rowMax > 3)
        {
            pit = PitCavePosition2(rowMax, colMax);
            AddPits(pit);

            // 2 extra pits on Large board
            if (rowMax > 5)
            {
                pit = PitCavePosition3(rowMax, colMax);
                AddPits(pit);

                pit = PitCavePosition4(rowMax, colMax);
                AddPits(pit);
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
        var maelstrom = MaelstromCavePosition1(rowMax, colMax);
        AddMaelstroms(maelstrom);

        // Medium board  plus one extra Maelstrom
        if (rowMax > 3)
        {
            maelstrom = MaelstromCavePosition2(rowMax, colMax);
            AddMaelstroms(maelstrom);

            // Large board plus two extra
            if (rowMax > 5)
            {
                maelstrom = MaelstromCavePosition3(rowMax, colMax);
                AddMaelstroms(maelstrom);

                maelstrom = MaelstromCavePosition4(rowMax, colMax);
                AddMaelstroms(maelstrom);
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
        var amarok = AmarokCavePosition1(rowMax, colMax);
        AddAmaroks(amarok);


        // Medium board  plus one  Amarok
        if (rowMax > 3)
        {
            amarok = AmarokCavePosition2(rowMax, colMax);
            AddAmaroks(amarok);

            // Large board plus one more Amarok
            if (rowMax > 5)
            {
                amarok = AmarokCavePosition3(rowMax, colMax);
                AddAmaroks(amarok);
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

            WriteLine($"You are in the cave at ({gamePlayer.Row}, {gamePlayer.Column})");

            if (gamePlayer.Arrows > 0)
            {
                WriteLine($"You have a bow and {gamePlayer.Arrows} arrows.");
            }

            // Print cave description before moving player.
            PlayArea[gamePlayer.Row, gamePlayer.Column].ToString();

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
        if (gamePlayer.Column + 1
            > PlayArea.GetUpperBound(0)) gamePlayer.Column = PlayArea.GetLowerBound(0);
        else gamePlayer.Column++;

        return true;
    }

    private bool MovePlayerWest()
    {
        if (gamePlayer.Column - 1
            < PlayArea.GetLowerBound(0)) gamePlayer.Column = PlayArea.GetUpperBound(0);
        else gamePlayer.Column--;

        return true;
    }

    private bool MovePlayerNorth()
    {
        if (gamePlayer.Row - 1
            < PlayArea.GetLowerBound(1)) gamePlayer.Row = PlayArea.GetUpperBound(1);
        else gamePlayer.Row--;

        return true;
    }

    private bool MovePlayerSouth()
    {
        if (gamePlayer.Row + 1
            > PlayArea.GetUpperBound(1)) gamePlayer.Row = PlayArea.GetLowerBound(1);
        else gamePlayer.Row++;

        return true;
    }

    private bool EnableFountain()
    {
        // Is the player is at fountain position?
        if (_baseObjectPositions["fountain"].PlayerIsTheSameAs(gamePlayer))
        {
            // Change fountain message
            PlayArea[gamePlayer.Row, gamePlayer.Column] = new ActivatedFountainCave();

            // Change cavern _entrance message.
            PlayArea[_baseObjectPositions["entrance"].Row, _baseObjectPositions["entrance"].Column] = new ActivatedCavernEntrance();
        }
        // Nope!
        else WriteLine("\nCan't be done - you  have to be present in the fountain cave to do that!");

        return true;
    }

    private bool Attack()
    {
        if (gamePlayer.Bow && gamePlayer.Arrows > 0)
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
        else if (!gamePlayer.Bow)
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
        if (gamePlayer.Row > 0)
        {
            DetermineArrowShotResult(gamePlayer.Column, gamePlayer.Row - 1);
            gamePlayer.Arrows--;
        }
        return true;
    }

    private bool ShootArrowEast()
    {
        if (gamePlayer.Column < PlayArea.GetUpperBound(0))
        {
            DetermineArrowShotResult(gamePlayer.Row, gamePlayer.Column + 1);
            gamePlayer.Arrows--;
        }
        return true;
    }

    private bool ShootArrowSouth()
    {
        if (gamePlayer.Row < PlayArea.GetUpperBound(0))
        {
            DetermineArrowShotResult(gamePlayer.Row + 1, gamePlayer.Column);
            gamePlayer.Arrows--;
        }
        return true;
    }

    private bool ShootArrowWest()
    {
        if (gamePlayer.Column > 0)
        {
            DetermineArrowShotResult(gamePlayer.Row, gamePlayer.Column - 1);
            gamePlayer.Arrows--;
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

        foreach (var item in _maelstromPositions)
        {
            if (item.ArrowIsTheSameAs(row, column))
            {
                AnAttackedMaelstrom();
                index = _maelstromPositions.IndexOf(item);

                break;
            }
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

        foreach (var item in _amarokPositions)
        {
            if (item.ArrowIsTheSameAs(row, column))
            {
                AnAttackedAmarok();
                index = _amarokPositions.IndexOf(item);

                break;
            }
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
        foreach (var item in _pitPositions)
        {
            if (item.ArrowIsTheSameAs(row, column)) Console.WriteLine("\nYou hear your arrow clatter uselessly into the pit. What a waste!");
        }
    }

    private bool CheckForPits()
    {
        // One default pit on all boards

        // Search through Maelstroms List to find whether the player is about to be blown away, no need for a peril adjacent message.
        foreach (var item in _maelstromPositions)
        {
            if (item.PlayerIsTheSameAs(gamePlayer))
            {
                return false;
            }
        }

        foreach (var item in _pitPositions)
        {
            if (item.PlayerIsAdjacentTo(gamePlayer))
            {
                AnAdjacentPit();
            }

            if (item.PlayerIsTheSameAs(gamePlayer))
            {
                // You fell in!
                PlayArea[gamePlayer.Row, gamePlayer.Column].ToString();
                return true;
            }
        }
        return false;
    }

    private bool CheckForAmaroks()
    {
        foreach (var item in _amarokPositions)
        {

            if (item.PlayerIsTheSameAs(gamePlayer))
            {
                // You died!
                PlayArea[gamePlayer.Row, gamePlayer.Column].ToString();
                return true;
            }

            if (item.PlayerIsAdjacentTo(gamePlayer))
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
            if (item.PlayerIsAdjacentTo(gamePlayer))
            {
                AnAdjacentMaelstrom();
            }

            if (item.PlayerIsTheSameAs(gamePlayer))
            {
                PlayArea[gamePlayer.Row, gamePlayer.Column].ToString();
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

        // Redo if the new cave position overwrites the fountain or _entrance caves
        while (_baseObjectPositions["entrance"].IsTheSameAs(row, column) || _baseObjectPositions["fountain"].IsTheSameAs(row, column));

        PlayArea[row, column] = new Maelstrom();

        return (row, column);
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
        Description = "You see a light coming from outside the cavern. This is the _entrance.";
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
        Description = "The entrance cavern. \nThe Fountain of Objects has been re-activated. You have escaped with your life!\n";
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

public class BottomlessPit : ICave
{
    public string Description { get; private set; }

    public BottomlessPit()
    {
        Description = "You have blundered into a bottomless pit. So sad!  See you in the next life. Bye ........ ";
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
    public string Description { get; private set; }

    public Maelstrom()
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

public class Amarok : ICave
{
    public string Description { get; set; }

    public Amarok()
    {
        Description = "You have stumbled into an Amarok's lair and have instantly died. Too bad really ... Bye!";
    }

    public override string ToString()
    {
        ForegroundColor = ConsoleColor.Yellow;
        WriteLine(Description);
        ResetColor();
        return string.Empty;
    }
}

public class EmptyCave : ICave
{
    public string Description { get; private set; }

    public EmptyCave()
    {
        Description = "A creepy, dark, pitted  and deserted cave. Move along - nothing to see here!";
    }

    public override string ToString()
    {
        WriteLine(Description);

        return string.Empty;
    }
}