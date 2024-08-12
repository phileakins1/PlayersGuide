// Ignore Spelling: Droid

using IField;
using McDroid;


internal class Program
{
    private static void Main(string[] args)
    {
        Sheep sheep = new Sheep();
        IField.Pig pig = new IField.Pig();
        Cow cow = new Cow();
        McDroid.Pig mcPig = new McDroid.Pig();
    }
}


namespace IField
{
    public class Sheep {}

    public class Pig {}
}

namespace McDroid
{
    public class Cow {}

    public class Pig {}
}