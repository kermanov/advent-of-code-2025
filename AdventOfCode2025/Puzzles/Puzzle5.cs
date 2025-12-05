using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace AdventOfCode2025.Puzzles;

public class Puzzle5 : PuzzleBase
{
    protected override string Solve1(string input)
    {
        var (ranges, ids) = ReadInput(input);

        var count = ids
            .Where(id => ranges.Any(range => range.Item1 <= id && range.Item2 >= id))
            .Count();

        return count.ToString();
    }

    protected override string Solve2(string input)
    {
        var (ranges, _) = ReadInput(input);
        var idsCount = ranges.Sum(range => range.Item2 - range.Item1 + 1);

        for (var i = 0; i < ranges.Length; ++i)
        {
            for (var j = i + 1; j < ranges.Length; ++j)
            {
                var range1 = ranges[i];
                var range2 = ranges[j];

                var overlap = OverlapDistance(range1, range2);

                if (overlap == -1)
                {
                    overlap = OverlapDistance(range2, range1);
                }

                if (overlap == -1)
                {
                    continue;
                }

                idsCount -= overlap;
            }
        }

        return idsCount.ToString();
    }

    static long OverlapDistance((long, long) range1, (long, long) range2)
    {
        if (range1.Item1 <= range2.Item1 && range1.Item2 >= range2.Item2 || range1.Item1 == range2.Item1 && range1.Item2 == range2.Item2)
        {
            return range2.Item2 - range2.Item1 + 1;
        }

        if (range1.Item1 <= range2.Item1 && range1.Item2 >= range2.Item1 && range1.Item2 <= range2.Item2)
        {
            return range1.Item2 - range2.Item1 + 1;
        }

        return -1;
    }

    static ((long, long)[] Ranges, long[] ids) ReadInput(string input)
    {
        var lines = input.Split("\r\n");
        var ranges = new List<(long, long)>();

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                break;
            }

            var range = line.Split('-');
            ranges.Add((long.Parse(range[0]), long.Parse(range[1])));
        }

        var ids = new long[lines.Length - ranges.Count - 1];

        for (int i = 0; i < ids.Length; ++i)
        {
            ids[i] = long.Parse(lines[i + ranges.Count + 1]);
        }

        return (ranges.ToArray(), ids);
    }
}
