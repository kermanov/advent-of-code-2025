namespace AdventOfCode2025.Puzzles;

public class Puzzle2 : IPuzzle
{
    public string Solve1(string input)
    {
        return CommonSolution(input, IdIsInvalid);
    }

    public string Solve2(string input)
    {
        return CommonSolution(input, IdIsInvalid2);
    }

    static string CommonSolution(string input, Func<long, bool> validator)
    {
        var ranges = GetRanges(input);

        long result = 0;

        foreach (var (first, last) in ranges)
        {
            for (long number = first; number <= last; ++number)
            {
                if (validator(number))
                {
                    result += number;
                }
            }
        }

        return result.ToString();
    }

    static (long, long)[] GetRanges(string input)
    {
        return input.Split(',')
            .Select(range =>
            {
                var numbers = range.Split('-');
                return (long.Parse(numbers[0]), long.Parse(numbers[1]));
            })
            .ToArray();
    }

    static int GetDigitsCount(long number)
    {
        var digits = 1;

        while (number / (long) Math.Pow(10, digits) > 0)
        {
            digits++;
        }

        return digits;
    }

    static void GetDigits(long number, int digitsCount, Span<byte> digits)
    {
        for (int i = 0; i < digitsCount; ++i)
        {
            var baseNumber = (long) Math.Pow(10, digitsCount - 1 - i);
            digits[i] = (byte) (number / baseNumber);
            number -= digits[i] * baseNumber;
        }
    }

    static bool IdIsInvalid(long number)
    {
        var digitsCount = GetDigitsCount(number);
                
        if (digitsCount % 2 != 0)
        {
            return false;
        }

        Span<byte> digits = stackalloc byte[digitsCount];
        GetDigits(number, digitsCount, digits);

        var halfLength = digitsCount / 2;
        return digits.Slice(0, halfLength).SequenceEqual(digits.Slice(halfLength, halfLength));
    }

    static bool IdIsInvalid2(long number)
    {
        var digitsCount = GetDigitsCount(number);
                
        Span<byte> digits = stackalloc byte[digitsCount];
        GetDigits(number, digitsCount, digits);

        return HasRepeatedSequence(digits);
    }

    static bool HasRepeatedSequence(Span<byte> digits)
    {
        for (int seqLength = 1; seqLength <= digits.Length / 2; ++seqLength)
        {
            if (digits.Length % seqLength != 0)
            {
                continue;
            }

            var found = true;
            var sequence = digits.Slice(0, seqLength);

            for (int i = 1; i < digits.Length / seqLength; ++i)
            {
                var nextSequence = digits.Slice(i * seqLength, seqLength);

                if (!sequence.SequenceEqual(nextSequence))
                {
                    found = false;
                    break;
                }
            }

            if (found)
            {
                return true;
            }
        }

        return false;
    }
}
