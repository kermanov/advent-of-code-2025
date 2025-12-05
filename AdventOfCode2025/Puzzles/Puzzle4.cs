namespace AdventOfCode2025.Puzzles;

public class Puzzle4 : IPuzzle
{
    public string Solve1(string input)
    {
        var rolls = ReadRolls(input);

        var sum = rolls
            .Where(roll => IsAccessible(roll, rolls))
            .Count();

        return sum.ToString();
    }

    public string Solve2(string input)
    {
        var rolls = ReadRolls(input);
        
        var removed = 0;
        var lastRemoved = -1;

        while (removed > lastRemoved)
        {
            lastRemoved = removed;
            
            foreach (var roll in rolls)
            {
                if (IsAccessible(roll, rolls))
                {
                    rolls.Remove(roll);
                    ++removed;
                    break;
                }
            }
        }

        return removed.ToString();
    }

    static bool IsAccessible((int X, int Y) roll, HashSet<(int X, int Y)> rolls)
    {
        Span<(int, int)> adjacentPositions = stackalloc (int, int)[8];
        GetAdjacentPositions(roll, adjacentPositions);
        return IsAdjacentCountAcceptable(adjacentPositions, rolls);
    }

    static bool IsAdjacentCountAcceptable(Span<(int, int)> positions, HashSet<(int X, int Y)> rolls)
    {
        var rollsAround = 0;

        foreach (var pos in positions)
        {
            if (rolls.Contains(pos))
            {
                ++rollsAround;
            }

            if (rollsAround >= 4)
            {
                return false;
            }
        }

        return true;
    }

    static void GetAdjacentPositions((int X, int Y) roll, Span<(int, int)> positions)
    {
        positions[0] = (roll.X - 1, roll.Y - 1);
        positions[1] = (roll.X, roll.Y - 1);
        positions[2] = (roll.X + 1, roll.Y - 1);
        positions[3] = (roll.X + 1, roll.Y);
        positions[4] = (roll.X + 1, roll.Y + 1);
        positions[5] = (roll.X, roll.Y + 1);
        positions[6] = (roll.X - 1, roll.Y + 1);
        positions[7] = (roll.X - 1, roll.Y);
    }

    static HashSet<(int X, int Y)> ReadRolls(string input)
    {
        var lines = input.Split("\r\n");
        var rolls = new HashSet<(int X, int Y)>();

        for (int y = 0; y < lines.Length; ++y)
        {
            var line = lines[y];

            for (int x = 0; x < line.Length; ++x)
            {
                if (line[x] == '@')
                {
                    rolls.Add((x, y));
                }
            }
        }

        return rolls;
    }
}
