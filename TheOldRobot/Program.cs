// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


string? _input;
int _index = 0;
IRobotCommand _robotCommand;

Robot _robot = new();

do
{
    Console.Write("Enter a command > ");
    _input = Console.ReadLine();

    _robotCommand = _input switch
    {
        "on"    => new OnCommand(),
        "north" => new NorthCommand(),
        "south" => new SouthCommand(),
        "east"  => new EastCommand(),
        "west"  => new WestCommand(),
        "off"   => new OffCommand(),
        _       => new OffCommand()
    };

    //_robot.Commands[_index] = _robotCommand;
    _index++;

    _robot.Commands.Add(_robotCommand);

} while (_index <= 5);

Console.WriteLine();
_robot.Run();


public class Robot
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsPowered { get; set; }

    //public IRobotCommand?[] Commands { get; } = new IRobotCommand?[4];
    public List<IRobotCommand> Commands { get; } = new();

    public void Run()
    {
        foreach (var command in Commands)
        {
            command?.Run(this);
            Console.WriteLine($"[{X} {Y} {IsPowered}]");
        }
    }
}

public interface IRobotCommand
{
    void Run(Robot robot);
}

public class OnCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        robot.IsPowered = true;
    }
}

public class OffCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        robot.IsPowered = false;
    }
}

public class NorthCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered)
        {
            robot.Y += 1;
        }
    }
}

public class SouthCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered)
        {
            robot.Y += -1;
        }
    }
}

public class WestCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered)
        {
            robot.X += -1;
        }
    }
}

public class EastCommand : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (robot.IsPowered)
        {
            robot.X += 1;
        }
    }
}