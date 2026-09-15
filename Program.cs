namespace Homework
{
    internal class Program
    {
        private const double _reductionFactor = 1.247;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            while (true)
            {
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))   break;
                List<int> numbers = [.. input
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => int.Parse(s))];                                 
                
            }
        }
    }
}
