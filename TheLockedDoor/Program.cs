// See https://aka.ms/new-console-template for more information

// Experimental static using!
using static System.Console;

Console.Title = "The Locked Door";

Door _door = new("pass");


_door.ShowCurrentState();

_door.UnlockDoor("pass");
_door.ShowCurrentState();

_door.OpenDoor();
_door.ShowCurrentState();

_door.CloseDoor();
_door.ShowCurrentState();

_door.OpenDoor();
_door.ShowCurrentState();

_door.CloseDoor();
_door.ShowCurrentState();

_door.LockDoor();
_door.ShowCurrentState();

_door.ChangePassCode("pass", "newPass");

_door.UnlockDoor("newPass");
_door.ShowCurrentState();

_door.ChangePassCode("newPass", "newPass");


_door.UnlockDoor("fred");

_door.ShowCurrentState();

_door.CloseDoor();
_door.ShowCurrentState();

Console.ReadKey();


public class Door
{
    public string DoorState { get; set; } = "Locked";
    public string PassCode { get; private set; }

    public Door(string passCode)
    {
        PassCode = passCode;
    }

    public void UnlockDoor(string passCode)
    {
        if (DoorState == "Locked" && passCode == PassCode)
        {
            DoorState = "Closed";
        }
        else
            WriteLine("The door cannot be unlocked.");
    }

    public void OpenDoor()
    {
        if (DoorState == "Closed")
        {
            DoorState = "Open";
        }
        else
            WriteLine("The door cannot be opened.");
    }

    public void CloseDoor()
    {
        if (DoorState == "Open")
        {
            DoorState = "Closed";
        }
        else
            WriteLine("The door cannot be closed.");
    }

    public void LockDoor()
    {
        if (DoorState == "Closed")
        {
            DoorState = "Locked";
        }
        else
            WriteLine("The door cannot be locked.");
    }

    public void ChangePassCode(string oldPassCode, string newPassCode)
    {
        if (oldPassCode == PassCode && newPassCode != oldPassCode)
        {
            PassCode = newPassCode;
            WriteLine("The passcode has successfully been changed.");
        }
        else
            WriteLine("The passcode cannot be changed.");
    }

    public void ShowCurrentState()
    {
        WriteLine($"The door is {DoorState}.");
    }
}