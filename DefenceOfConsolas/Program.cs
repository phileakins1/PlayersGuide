// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

int _maxRow = 8;
int _maxColumn = 8;

Console.Title = "The Defence of Consolas";

Console.Write("Enter the Target Row > ");
int _row = int.Parse(Console.ReadLine()!);

Console.Write("Enter the Target Column > ");
int _column = int.Parse(Console.ReadLine()!);

// Don't run off the edges!
if (_row - 1 <= 1) _row = 2;
if (_row + 1 >= _maxRow) _row = _maxRow - 1;
if (_column - 1 <= 1) _column = 2;
if (_column + 1 >= _maxColumn) _column = _maxColumn - 1;

Console.WriteLine($"Target Row {_row}");
Console.WriteLine($"Target Column {_column}");

Console.ForegroundColor = ConsoleColor.Green;

//Console.Beep(440, 1000);

Console.WriteLine("Deploy to:-");
Console.WriteLine($"({_row}, {_column - 1})");
Console.WriteLine($"({_row - 1}, {_column})");
Console.WriteLine($"({_row}, {_column + 1})");
Console.WriteLine($"({_row + 1}, {_column})");

Console.ResetColor();

