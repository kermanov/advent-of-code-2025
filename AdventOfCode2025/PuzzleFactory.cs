using System.Reflection;
using AdventOfCode2025.Puzzles;

namespace AdventOfCode2025;

public static class PuzzleFactory
{
    public static PuzzleBase GetPuzzle(string day)
    {
        var typeName = $"AdventOfCode2025.Puzzles.Puzzle{day}";
        var type = Assembly.GetExecutingAssembly().GetType(typeName);

        if (type == null)
        {
            throw new ArgumentException($"Puzzle for day {day} not found.");
        }

        if (Activator.CreateInstance(type) is not PuzzleBase puzzle)
        {
             throw new InvalidOperationException($"Type {typeName} does not implement IPuzzle.");
        }

        return puzzle;
    }
}
