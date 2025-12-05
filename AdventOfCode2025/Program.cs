using AdventOfCode2025;

Console.Write("Enter the day number: ");
var day = Console.ReadLine();

var input = File.ReadAllText($@"..\inputs\input_{day}.txt");

var puzzle = PuzzleFactory.GetPuzzle(day);
puzzle.Solve(input);