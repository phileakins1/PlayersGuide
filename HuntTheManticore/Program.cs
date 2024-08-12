// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

Random _rand = new();
int _distance = _rand.Next(0, 101);
int _cityHealth = 15;
int _maxCity = 15;
int _enemyHealth = 10;
int _maxEnemy = 10;
int _round = 1;
int _damage;
int _guess;
string _status;
string _drawLine = new('-', 55);

Console.Title = "Hunt the Manticore";

do
{
    _damage = ComputeDamage(_round);

    _status = $"""

        STATUS: Round: {_round}  City: {_cityHealth}/{_maxCity} Manticore: {_enemyHealth}/{_maxEnemy}
        The cannon is expected to deal {_damage} damage this round.
        """;

    Console.WriteLine(_status);

    _guess = GetGuess();

    if (_guess >= 0) // A legitimate guess has been made
    {
        if (_guess > _distance)
        {
            Console.WriteLine("That round OVERSHOT the target.");
        }
        else if (_guess < _distance)
        {
            Console.WriteLine("That round FELL SHORT of the target.");
        }
        else
        {
            Console.WriteLine("That round was a DIRECT HIT!");
            _enemyHealth -= _damage;
        }

        // Don't penalise the City if the Manticore has just been destroyed
        if (_enemyHealth > 0)
            _cityHealth --;
    }

    _round++;

    Console.WriteLine(_drawLine);

} while (_enemyHealth > 0 && _cityHealth > 0);


if (_cityHealth <= 0)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("The City of Consolas has been destroyed!");
    Console.ResetColor();
}
else
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("The Manticore has been destroyed. The City of Consolas has been saved!");
    Console.ResetColor();
}



int GetGuess()
{
    Console.Write("Enter desired cannon range: > ");
    if (int.TryParse(Console.ReadLine(), out int input))
    {
        return input;
    }
    return 0;
}



int ComputeDamage(int round)
{
    int _damage;

    if (round % 5 == 0 && round % 3 == 0)
    {
        _damage = 10;
    }
    else if (round % 5 == 0)
    {
        _damage = 5;
    }
    else if (round % 3 == 0)
    {
        _damage = 3;
    }
    else
        _damage = 1;

    return _damage;
}

