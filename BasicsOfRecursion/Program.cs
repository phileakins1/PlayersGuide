// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");



Countdown(10);

int Countdown(int number)
{
    Console.WriteLine(number);
    if (number == 1) return 1;

    return Countdown(number - 1);
}

