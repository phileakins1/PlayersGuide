// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

decimal _rope = 10m;
decimal _torches = 15m;
decimal _climbing = 25m;
decimal _water = 1m;
decimal _machete = 20m;
decimal _canoe = 200m;
decimal _food = 1m;

string _inventory = """
    
    1 - Rope
    2 - Torches
    3 - Climbing Equipment
    4 - Clean Water
    5 - Machete
    6 - Canoe
    7 - Food Supplies

    Which product price do you want to see (enter the number)? > 
    """;

Console.Write(_inventory);
int _choice = int.Parse(Console.ReadLine()!);

Console.Write("What is your name? > ");
string _user = Console.ReadLine()!.ToLower();

// Display user discounted price : full price
string response = _choice switch
{
    1 => $"Rope costs {(_user == "me" ? _rope / 2 : _rope)} gold",
    2 => $"Torches cost {(_user == "me" ? _torches / 2 : _torches)} gold",
    3 => $"Climbing Equipment costs {(_user == "me" ? _climbing / 2 : _climbing)} gold",
    4 => $"Clean Water costs {(_user == "me" ? _water / 2 : _water)} gold",
    5 => $"A machete costs {(_user == "me" ? _machete / 2 : _machete)} gold",
    6 => $"A canoe costs {(_user == "me" ? _canoe / 2 : _canoe)} gold",
    7 => $"Food supplies cost {(_user == "me" ? _food / 2 : _food)} gold",
    _ => "Unknown product"
};
Console.WriteLine($"\n {response}");

