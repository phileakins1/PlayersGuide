// See https://aka.ms/new-console-template for more information


Sword _sword = new(Materials.Iron, Gemstones.None, 27, 3);
Sword _sword2 = _sword with { material = Materials.Steel, gemstone = Gemstones.Sapphire };
Sword _sword3 = _sword with { gemstone = Gemstones.Diamond, length = 30, guardWidth = 4 };

Console.WriteLine(_sword);
Console.WriteLine(_sword2);
Console.WriteLine(_sword3);

public record Sword(Materials material, Gemstones gemstone, int length, int guardWidth);

public enum Materials { Wood, bronze, Iron, Steel, Binarium }

public enum Gemstones { None, Emerald, Amber, Sapphire, Diamond, Bitstone }
