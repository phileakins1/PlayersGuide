// See https://aka.ms/new-console-template for more information


Console.Title = "Co-ordinate Comparison";

Coordinate _coordinate = new(6, 9);
Coordinate _coordinate2 = new(4, 10);

Coordinate _coordinate3 = new(6, 9);


Console.WriteLine($"\nIt is {_coordinate.Adjacent(_coordinate2)} that the two rooms are adjacent");
Console.WriteLine($"\nIt is {_coordinate.IsTheSame(_coordinate3)} that the two rooms are the same");


Console.ReadKey();


public readonly struct Coordinate
{
    private int Row { get; }
    private int Column { get; }

    public Coordinate(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public bool Adjacent(Coordinate coordinate)
    {
        if (this.Row - coordinate.Row is < -1 or > 1 || this.Column - coordinate.Column is < -1 or > 1) return false;

        return true;
    }

    public bool IsTheSame(Coordinate coordinate)
    {
        if (this.Row == coordinate.Row && this.Column == coordinate.Column) return true;
        else return false;
    }
}