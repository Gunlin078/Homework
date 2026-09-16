namespace Homework
{
    internal class Program
    {
        private const double _reductionFactor = 1.247;
        private static long _blockCount = 0;

		static void Main(string[] args)
        {
            Console.WriteLine("Enter numbers-—and only numbers—-separated by spaces");
            while (true)
            {
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))   break;
                List<int> numbers = [.. input
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => int.Parse(s))];                                 
                _blockCount = numbers.Count;
            }
        }
    }
}
