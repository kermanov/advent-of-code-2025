using AdventOfCode2025;

Console.Write("Enter the day number: ");
var day = Console.ReadLine();

var input = File.ReadAllText($"D:\\Repos\\advent-of-code-2025\\inputs\\input_{day}.txt");
var puzzle = PuzzleFactory.GetPuzzle(day);

var answer1 = puzzle.Solve1(input);
Console.WriteLine($"Answer 1: {answer1}");

var answer2 = puzzle.Solve2(input);
Console.WriteLine($"Answer 2: {answer2}");