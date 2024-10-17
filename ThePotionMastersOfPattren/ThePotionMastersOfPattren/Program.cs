// See https://aka.ms/new-console-template for more information

// c# 11 expansion for pre-mixed potions challenge -  p 12 

var newPotion = new CreatePotion();
newPotion.BrewPotion();


public class CreatePotion
{
    private Potion _potion;

    public CreatePotion()
    {
        Console.Title = "The Potion Masters of Pattren";
        _potion = Potion.water;
    }

    public void BrewPotion()
    {
        do
        {
            // Not ideal but works to apply colour to potion display.
            Console.Write("\n    Your potion is currently "); Beautify(_potion);

            var userChoice = GetUserInput();

            if (userChoice!.ToLower() == "y") break;

            // Filter out incorrect non-numeric entries
            if (int.TryParse(userChoice, out int choice) && (choice > 0 && choice <= 5))
            {
                Ingredient next = GetIngredientChoice(choice);

                _potion = StirItAllTogtherAndSimmerOverALowHeat(next, _potion);

                if (_potion == Potion.ruined_potion)
                {
                    Console.WriteLine("\n    You have ruined your potion. Start again!");
                    _potion = Potion.water;
                }
            }

        } while (true);
    }

    private static Potion StirItAllTogtherAndSimmerOverALowHeat(Ingredient ingredient, Potion potion)
    {
        return (ingredient, potion) switch
        {
            (Ingredient.stardust, Potion.water) => Potion.elixir,
            (Ingredient.snake_venom, Potion.elixir) => Potion.poison,
            (Ingredient.dragon_breath, Potion.elixir) => Potion.flying,
            (Ingredient.shadow_glass, Potion.elixir) => Potion.invisibility,
            (Ingredient.eyeshine_gem, Potion.elixir) => Potion.night_sight,
            (Ingredient.shadow_glass, Potion.night_sight) => Potion.cloudy_brew,
            (Ingredient.eyeshine_gem, Potion.invisibility) => Potion.cloudy_brew,
            (Ingredient.stardust, Potion.cloudy_brew) => Potion.wraith,

            _ => Potion.ruined_potion,
        };
    }

    private static string? GetUserInput()
    {
        PrintMenu();

        Console.Write("\n   Enter your choice here:  > ");
        string? choice = Console.ReadLine();

        return choice;
    }

    private static Ingredient GetIngredientChoice(int userChose)
    {
        return userChose switch
        {
            1 => Ingredient.stardust,
            2 => Ingredient.snake_venom,
            3 => Ingredient.dragon_breath,
            4 => Ingredient.shadow_glass,
            5 => Ingredient.eyeshine_gem,
            _ => Ingredient.none
        };
    }

    private static void PrintMenu()
    {
        Console.WriteLine(
"""
            Select your next ingredient from the list (by number)

            1. Stardust
            2. Snake Venom
            3. Dragon Breath
            4. Shadow Glass
            5. Eyeshine Gem

            Or enter 'y' if your potion is complete. 
        """);
    }

    private static void Beautify(Potion potion)
    {
        string response = potion switch
        {
            Potion.water => "just plain water.",
            Potion.elixir => "an elixir potion.",
            Potion.poison => "a poison potion.",
            Potion.flying => "a flying potion.",
            Potion.invisibility => "an invisibility potion.",
            Potion.night_sight => "a night sight potion.",
            Potion.cloudy_brew => "a cloudy brew.",
            Potion.wraith => "a wraith potion.",

            _ => "a ruined potion"
        };

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(response + "\n");
        Console.ResetColor();
    }
}

enum Potion { water, elixir, poison, flying, invisibility, night_sight, cloudy_brew, wraith, ruined_potion }
enum Ingredient { stardust, snake_venom, dragon_breath, shadow_glass, eyeshine_gem, water, none }
