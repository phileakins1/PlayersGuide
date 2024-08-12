// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

BoxState _boxState = BoxState.Locked;
string? _choice;

Chest();
void Chest()
{
    do
    {
        Console.Write($"The chest is {_boxState}. What do you want to do? > ");
        _choice = Console.ReadLine()?.ToLower();

        if (_boxState == BoxState.Locked && _choice == "unlock")
        {
            _boxState = BoxState.Unlocked;
        }
        else if (_boxState == BoxState.Unlocked || _boxState == BoxState.Closed && _choice == "open")
        {
            _boxState = BoxState.Open;
        }
        else if (_boxState == BoxState.Open && _choice == "close")
        {
            _boxState = BoxState.Closed;
        }
        else if (_boxState == BoxState.Closed && _choice == "lock")
        {
            _boxState = BoxState.Locked;
        }
        else
            Console.WriteLine("Sorry, can't do that ...");

    } while (true);
}



enum BoxState { Open, Closed, Locked, Unlocked };