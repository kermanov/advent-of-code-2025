namespace AdventOfCode2025.Puzzles;

public class Puzzle1 : PuzzleBase
{
    protected override string Solve1(string input)
    {
        var current = 50;
        var password = 0;

        foreach (var rotation in input.Split("\r\n"))
        {
            var sign = rotation[0] == 'R' ? 1 : -1;
            current = (current + sign * int.Parse(rotation[1..])) % 100;

            if (current == 0)
            {
                password++;
            }
        }

        return password.ToString();
    }

    protected override string Solve2(string input)
    {
        var safe = new Safe();

        foreach (var rotation in input.Split("\r\n"))
        {
            var sign = rotation[0] == 'R' ? 1 : -1;
            var distance = int.Parse(rotation[1..]);

            safe.Move(distance, sign);
        }

        return safe.ZeroTransitions.ToString();
    }

    class Safe
    {
        int _current = 50;
        public int ZeroTransitions { get; set; }

        public void Move(int distance, int sign)
        {
            for (var i = 0; i < distance; ++i)
            {
                _current += sign;

                if (_current < 0)
                {
                    _current = 99;
                }
                else if (_current > 99)
                {
                    _current = 0;
                }

                if (_current == 0)
                {
                    ++ZeroTransitions;
                }
            }
        }
    }
}
