// See https://aka.ms/new-console-template for more information
// https://www.youtube.com/watch?v=3rGDMsLCwa4

using System.Numerics;

int damage = 200;
int distance = 20;


Console.WriteLine(ComputeDamage<decimal>(damage, distance));
Console.WriteLine(ComputeDamage<long>(damage, distance));
Console.WriteLine(ComputeDamage<double>(damage, distance));
Console.WriteLine(ComputeDamage<float>(damage, distance));
Console.WriteLine(ComputeDamage<int>(damage, distance));


static T ComputeDamage<T>(T initialDamage, T distance) where T : INumberBase<T> => initialDamage / (distance * distance);

