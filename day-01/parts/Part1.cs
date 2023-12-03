namespace day_01.parts;

public class Part1 : IPart
{
    public (int, int) GetLineCalibrationDigits(string line) => ParseDigits(line).ToList() switch
    {
        [var n] => (n, n),
        [var n, .., var m] => (n, m),
        _ => throw new Exception("Invalid input")
    };
    
    private IEnumerable<int> ParseDigits(string line) => from c in line where char.IsDigit(c) select c - '0';
}