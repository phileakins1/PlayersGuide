// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


(Type Type, Ingredients Ingredients, Seasoning Seasoning) _recipe;

Console.WriteLine();

_recipe.Type = GetType();
_recipe.Ingredients = GetIngredient();
_recipe.Seasoning = GetSeasoning();

Console.WriteLine($"\n{_recipe.Seasoning} {_recipe.Ingredients} {_recipe.Type}");

Console.ReadLine();

Type GetType()
{
    Console.WriteLine("Variation");
    int i = 1;
    foreach (var item in Enum.GetNames(typeof(Type)))
    {
        Console.WriteLine($"{i,2} {item, 8}");
        i++;
    }
    Console.Write("Select by number > ");
    string? input = Console.ReadLine();

    Type response;

    response = input switch
    {
        "1" => Type.Soup, 
        "2" => Type.Stew,
        "3" => Type.Gumbo,
        _ => Type.Gumbo
    };

    return response;
}

Ingredients GetIngredient()
{
    Console.WriteLine("\nMain ingredient");
    int i = 1;
    foreach (var item in Enum.GetNames(typeof(Ingredients)))
    {
        Console.WriteLine($"{i,2} {item,12}");
        i++;
    }
    Console.Write("Select by number > ");
    string? input = Console.ReadLine();

    Ingredients response;

    response = input switch
    {
        "1" => Ingredients.Mushroom,
        "2" => Ingredients.Chicken,
        "3" => Ingredients.Carrot,
        "4" => Ingredients.Potato,
        _ => Ingredients.Chicken
    };

    return response;
}

Seasoning GetSeasoning()
{
    Console.WriteLine("Seasoning");
    int i = 1;
    foreach (var item in Enum.GetNames(typeof(Seasoning)))
    {
        Console.WriteLine($"{i,2} {item,12}");
        i++;
    }
    Console.Write("Select by number > ");
    string? input = Console.ReadLine();

    Seasoning response;

    response = input switch
    {
        "1" => Seasoning.Spicy,
        "2" => Seasoning.Salty,
        "3" => Seasoning.Sweet,
        _ => Seasoning.Sweet
    };

    return response;
}


enum Type { Soup, Stew, Gumbo }
enum Ingredients { Mushroom, Chicken, Carrot, Potato}
enum Seasoning { Spicy, Salty, Sweet }
