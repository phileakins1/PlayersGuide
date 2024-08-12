// See https://aka.ms/new-console-template for more information
// https://www.youtube.com/watch?v=dMiviA_VqKw


//Console.WriteLine("Hello, World!");

Console.Write("Enter the units of measure: ");
string? units = Console.ReadLine();
Console.Write("Enter the C# Type to use: ");
string? type = Console.ReadLine();
Console.WriteLine();

string code = $$"""
    Console.Write("Enter the width of (in {{units}}) of the triangle): ");
    {{type}} width = {{type}}.Parse(Console.ReadLine());
    Console.Write("Enter the height of (in {{units}}) of the triangle): ");
    {{type}} height = {{type}}.Parse(Console.ReadLine());
    {{type}} result = width * height / 2;
    Console.WriteLine($"{result} square {{units}}");
    """;

Console.WriteLine(code);

