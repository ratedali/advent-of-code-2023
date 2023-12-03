using day_01.parts;

namespace day_01
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var path = args[0];
            var input = File.ReadAllLines(path);
            
            Console.WriteLine($"Part 1 Answer: {Solver<Part1>.Solve(input)}");
            Console.WriteLine($"Part 2 Answer: {Solver<Part2>.Solve(input)}");

        }
    }
}