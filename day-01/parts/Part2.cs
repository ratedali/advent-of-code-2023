using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace day_01.parts;

public class Part2 : IPart
{
    private static readonly Regex DigitPattern = new(
        pattern: @"(?=(?<digit>zero|one|two|three|four|five|six|seven|eight|nine|\d))",
        options: RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private static readonly IDictionary<string, int> DigitMap;

    static Part2()
    {
        var builder = ImmutableDictionary.CreateBuilder<string, int>();
        builder.AddRange(new KeyValuePair<string, int>[]
        {
            new("zero", 0),
            new("0", 0),
            new("one", 1),
            new("1", 1),
            new("two", 2),
            new("2", 2),
            new("three", 3),
            new("3", 3),
            new("four", 4),
            new("4", 4),
            new("five", 5),
            new("5", 5),
            new("six", 6),
            new("6", 6),
            new("seven", 7),
            new("7", 7),
            new("eight", 8),
            new("8", 8),
            new("nine", 9),
            new("9", 9),
        });
        DigitMap = builder.ToImmutable();
    }

    public (int, int) GetLineCalibrationDigits(string line)
    {
        var matches = DigitPattern.Matches(line);
        
        if (matches.Count == 0)
        {
            throw new Exception("No digits found in line");
        }
        
        var m = DigitMap[matches.First().Groups["digit"].Value];
        var n = DigitMap[matches.Last().Groups["digit"].Value];
        return (m, n);
    }
}