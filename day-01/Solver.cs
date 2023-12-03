namespace day_01;

public static class Solver<TPart> where TPart : parts.IPart, new()
{
    private static readonly TPart Part = new();

    public static int Solve(IEnumerable<string> lines) => GetCalibrationValues(lines).Sum();

    private static IEnumerable<int> GetCalibrationValues(IEnumerable<string> lines)
        => from line in lines select Part.GetLineCalibrationValue(line);



}