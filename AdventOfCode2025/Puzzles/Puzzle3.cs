using System.Data;

namespace AdventOfCode2025.Puzzles;

public class Puzzle3 : IPuzzle
{
    public string Solve1(string input)
    {
        return Solve(input, 2);
    }

    public string Solve2(string input)
    {
        return Solve(input, 12);
    }

    static string Solve(string input, int length)
    {
        var sum = input.Split("\r\n")
            .Select(bank =>
            {
                var digits = bank
                    .Select(digit => (byte) (digit - 48))
                    .ToArray();

                var uniqueSorted = digits
                    .Distinct()
                    .OrderDescending()
                    .ToArray();

                var finalDigits = new List<(byte, int)>(length);
                var usedIndexes = new HashSet<int>(digits.Length);

                TrySolve(digits, finalDigits, usedIndexes, uniqueSorted, length);

                return long.Parse(string.Join("", finalDigits.Select(x => x.Item1)));
            })
            .Sum();

        return sum.ToString();
    }

    static bool TrySolve(byte[] digits, List<(byte, int)> finalDigits, HashSet<int> usedIndexes, byte[] uniqueSorted, int length)
    {
        foreach (var digit in uniqueSorted)
        {
            var index = GetNextIndex(digits, digit, finalDigits, usedIndexes);

            if (index != -1)
            {
                usedIndexes.Add(index);
                finalDigits.Add((digits[index], index));
                
                if (finalDigits.Count == length || TrySolve(digits, finalDigits, usedIndexes, uniqueSorted, length))
                {
                    return true;
                }

                usedIndexes.Remove(index);
                finalDigits.Remove((digits[index], index));
            }
        }

        return false;
    }

    static int GetNextIndex(byte[] digits, byte digit, List<(byte, int)> finalDigits, HashSet<int> usedIndexes)
    {
        for (var i = 0; i < digits.Length; ++i)
        {
            if (digits[i] == digit && !usedIndexes.Contains(i) && (!finalDigits.Any() || finalDigits.Last().Item2 < i))
            {
                return i;
            }
        }

        return -1;
    }
}
