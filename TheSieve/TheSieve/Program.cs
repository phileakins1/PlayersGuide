
// Here we go ... !
int testMethod = Start.ChooseDelegateToUse();
Start.GetTestMethod(testMethod);
Start.TestEnteredNumbers();

public delegate bool IsItAGoodNumber(int number);

public static class Start
{
    public static IsItAGoodNumber? SieveMethod { get; set; }

    public static int ChooseDelegateToUse()
    {
        WriteMenu();

        do
        {
            string? entry = Console.ReadLine();
            if (int.TryParse(entry, out int choice) && (choice > 0 && choice <= 3))
                return choice;
            Console.Write(" Incorrect entry, try again: >  ");
        } while (true);
    }

    private static void WriteMenu()
    {
        string menu = """
            Which of the functions do you wish to use? Choose by entering its number
            
            1   Test whether a number is even
            2   Test whether a number is  positive 
            3   Test whether a number is a multiple of 10

            """;

        Console.WriteLine(menu);
        Console.Write(" Enter your selection here: >  ");
    }

    public static void GetTestMethod(int method)
    {
        IsItAGoodNumber response;

        SieveMethod = method switch
        {
            1 => response = Sieve.IsEven,
            2 => response = Sieve.IsPositive,
            3 => response = Sieve.IsMultipleOfTen,
            _ => response = Sieve.IsEven
        };

        SieveMethod = response;
    }

    public static void TestEnteredNumbers()
    {
        if (SieveMethod != null)
        {
            Sieve sieve = new(new IsItAGoodNumber(SieveMethod));
        }

        string? entry;

        do
        {
            Console.Write("\nEnter a number to test (enter 'end' to finish): >  ");
            entry = Console.ReadLine();

            if (entry == "end")
                break;

            if (int.TryParse(entry, out int numberToCheck))
            {
                Console.WriteLine($"\n{entry} is tested as being {Sieve.IsGood(numberToCheck)}");
            }

        } while (true);

    }
}

public class Sieve
{
    public static IsItAGoodNumber? IsGoodDelegate { get; set; }

    public Sieve(IsItAGoodNumber numberDelegate) => IsGoodDelegate = numberDelegate;

    public static bool IsGood(int number)
    {
        bool result = IsGoodDelegate!(number);
        return result;
    }

    public static bool IsEven(int number) => number % 2 == 0;

    public static bool IsPositive(int number) => number > 0;

    public static bool IsMultipleOfTen(int number) => number % 10 == 0;
}