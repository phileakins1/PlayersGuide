// See https://aka.ms/new-console-template for more information
// https://www.youtube.com/watch?v=UEwBSikaRfo&t=34s

//Console.WriteLine("Hello, World!");

CreatePotions();
PremixedPotions();

static void CreatePotions()
{
    Potion currentPotion = Potion.Water;

    while (true)
    {
        Console.WriteLine($"\nCurrent Potion is {currentPotion}");

        Console.Write("Do you want to add more ingredients? > ");
        string? input = Console.ReadLine();
        if (input == "no") break;
        Console.WriteLine();

        //Console.Write("\nAvailable ingredients are: \n Stardust, \n Snake Venom, \n Dragon Breath, \n Eyeshine Gem, \n Shadow Glass \n- make a choice > ");

        string choices = """
         Stardust
         Snake Venom
         Dragon Breath
         Eyeshine Gem
         Shadow Glass

         Make a choice :>  
        """;
        Console.Write(choices);

        Ingredients ingredient = Console.ReadLine()!.ToLower() switch
        {
            "stardust" => Ingredients.Stardust,
            "snake venom" => Ingredients.SnakeVenom,
            "dragon breath" => Ingredients.DragonBreath,
            "eyeshine gem" => Ingredients.EyeshineGem,
            "shadow glass" => Ingredients.ShadowGlass,
            _ => Ingredients.None
        };

        Potion potionType = (currentPotion, ingredient) switch
        {
            (Potion.Water, Ingredients.Stardust) => currentPotion = Potion.Elixir,
            (Potion.Elixir, Ingredients.SnakeVenom) => currentPotion = Potion.Poison,
            (Potion.Elixir, Ingredients.DragonBreath) => currentPotion = Potion.Flying,
            (Potion.Elixir, Ingredients.ShadowGlass) => currentPotion = Potion.Invisibility,
            (Potion.Elixir, Ingredients.EyeshineGem) => currentPotion = Potion.NightSight,
            (Potion.NightSight, Ingredients.ShadowGlass) => currentPotion = Potion.CloudyBrew,
            (Potion.Invisibility, Ingredients.EyeshineGem) => currentPotion = Potion.CloudyBrew,
            (Potion.CloudyBrew, Ingredients.Stardust) => currentPotion = Potion.Wraith,
            (_, _) => currentPotion = Potion.Ruined
        };

        if (currentPotion == Potion.Ruined)
        {
            Console.WriteLine("\nThe potion is ruined, start again with water!");
            currentPotion = Potion.Water;
        }
    }
}

static void PremixedPotions()
{
    List<Ingredients> potion = [Ingredients.None];

    string potionType = potion switch
    {
        [] => "water",
        [Ingredients.Water, Ingredients.Stardust] => "elixir",
        [.., Ingredients.DragonBreath] => "flying",
        [.., Ingredients.SnakeVenom] => "poison",
        [.., Ingredients.EyeshineGem, Ingredients.ShadowGlass] => "cloudy brew",
        [.., Ingredients.ShadowGlass] => "invisibility",
        [.., Ingredients.ShadowGlass, Ingredients.EyeshineGem] => "cloudy brew",
        [.., Ingredients.EyeshineGem] => "night sight",
        [.., Ingredients.ShadowGlass, Ingredients.EyeshineGem, Ingredients.Stardust] => "wraith",
        [.., Ingredients.EyeshineGem, Ingredients.ShadowGlass, Ingredients.Stardust] => "wraith",
        _ => "ruined"
    };

    Console.WriteLine($"The {potionType} potion has been brewed.");
}

enum Ingredients { None, Water, Stardust, SnakeVenom, DragonBreath, EyeshineGem, ShadowGlass }
enum Potion { Water, Elixir, Poison, Flying, Invisibility, NightSight, CloudyBrew, Wraith, Ruined }
