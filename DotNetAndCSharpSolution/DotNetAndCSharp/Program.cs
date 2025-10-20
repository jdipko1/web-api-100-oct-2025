//namespace DotNetAndCSharp;

/*
internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
*/

global using DotNetAndCSharp.Zoo;

//can add global before using only need to declare once

Console.WriteLine("Hello World");
var myBowlingScores = new List<int>()
{
    127,180,99,43
};

//No longer need using Systems.Collections.Generic;

//Eliminates need for top level statements

foreach (var score in myBowlingScores)
{
    Console.WriteLine(score);

}

var george = new Monkey() { Name = "George", Age = 3 };
var george2 = new Monkey() {Name = "George"};
var gorilla = new Gorilla("King", 32);
var updatedKong = gorilla with { Name = "King Kong" };


if (george == george2)
{
    Console.WriteLine("They are equivalent");
}
else
{
    Console.WriteLine("They are not equivalent");
}







if (george.Name is not null) Console.WriteLine($"{george.Name.ToUpper()}");
else Console.WriteLine("Monkey has no name!!");