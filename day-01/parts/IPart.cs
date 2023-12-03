namespace day_01.parts;

public interface IPart
{
    public int GetLineCalibrationValue(string line) => GetLineCalibrationDigits(line) switch
    {
        var (n, m) => n * 10 + m
    };
    
    public  (int, int) GetLineCalibrationDigits(string line);
}