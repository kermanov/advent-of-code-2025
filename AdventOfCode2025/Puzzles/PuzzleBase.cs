using System.Diagnostics;

namespace AdventOfCode2025.Puzzles;

public abstract class PuzzleBase
{
    public void Solve(string input)
    {
        var time1 = MeasureTime(Solve1, input, out var result1);
        Console.WriteLine($"Answer 1: {result1}. Time: {time1}");

        var time2 = MeasureTime(Solve2, input, out var result2);
        Console.WriteLine($"Answer 2: {result2}. Time: {time2}");
    }

    static TimeSpan MeasureTime(Func<string, string> solve, string input, out string result)
    {
        var stopWatch = new Stopwatch();

        stopWatch.Start();
        result = solve(input);
        stopWatch.Stop();

        return stopWatch.Elapsed;
    }

    protected abstract string Solve1(string input);
    protected abstract string Solve2(string input);
}
