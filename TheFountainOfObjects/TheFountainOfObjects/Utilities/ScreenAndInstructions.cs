using static System.Console;


namespace TheFountainOfObjects.Utilities;

public static class ScreenAndInstructions
{
    /// <summary>
    /// Beginning setup
    /// </summary>
    public static void StartupScreens()
    {
        Console.Title = "The quest for the Fountain of Objects";
        Console.Clear();

        WriteLine(StartUpMenu());

        Write("Press any key to continue:  ");
        Console.ReadLine();
    }

    public static string StartUpMenu()
    {
        string _menu = """

     Once upon a time, in a galaxy far, far away ...

     You, a fearless adventurer, enter the Cavern of Objects, a maze of caves filled with dangerous pits to seek and activate the Fountain of Objects.

     Light is visible only in the entrance, little other light is to be found in the cavern.  You must navigate the caves with your senses. Find the Fountain of Objects, activate it, and return to the entrance.

     Look out for Pits, you will feel a breeze if a pit is nearby. If you enter a cave with a pit, you will fall eternally.

     Maelstroms are violent forces of sentient wind. Enter a cave with one and you will be violently ejected into another cave. You will be able to hear the growling and groaning from nearby caves.

     Amaroks roam the caverns. Encountering one is certain death, but you can smell their rotten stench for caves around.

     If you carry with you a bow and a quiver of arrows, you can shoot at the monsters in adjacent caves but beware, you have a limited supply of arrows so don't waste them.

     Typing 'Help' or ('H' for short) will show a list of commands which will help you navigate the Cavern.

     """;
        return _menu;
    }
}
