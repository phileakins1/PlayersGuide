// See https://aka.ms/new-console-template for more information
// https://www.youtube.com/watch?v=IeDsW0S7j_k&t=3s

int[] numbers = [4, 8, 15, 16, 23, 42];

while (true)
{
    Console.WriteLine(numbers[0]);

    int[] copy = [.. numbers[1..], numbers[0]];
    numbers = copy;
    Console.ReadKey();
}
