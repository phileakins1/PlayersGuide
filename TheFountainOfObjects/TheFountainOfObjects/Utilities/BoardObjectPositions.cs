// See https://aka.ms/new-console-template for more information

// Ignore Spelling: Amarok Amarok

// Experimental static usings.
using TheFountainOfObjects.GameObjects;

namespace TheFountainOfObjects.Utilities;

/// <summary>
/// Structs instead of classes - problem is that structs cannot be null when no longer needed to allow collection - set position off board.
/// </summary>
/// <param name="caves">The playing board</param>
internal class BoardObjectPositions(ICave[,] caves)
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
            if (Row == player.Row && Column == player.Column) return true;
            else return false;
        }

        public readonly bool IsTheSameAs(int row, int column)
        {
            if (Row == row && Column == column) return true;
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
            if (Row == player.Row && Column == player.Column) return true;
            else return false;
        }

        public readonly bool IsTheSameAs(int row, int column)
        {
            if (Row == row && Column == column) return true;
            else return false;
        }

        public readonly bool ArrowIsTheSameAs(int row, int column)
        {
            if (Row == row && Column == column)
                return true;
            else return false;
        }

        public readonly bool PlayerIsAdjacentTo(PlayerPosition player)
        {
            if (!(Row - player.Row is < -1 or > 1 || Column - player.Column is < -1 or > 1) && !PlayerIsTheSameAs(player))
                return true;
            else return false;
        }
    }
}

