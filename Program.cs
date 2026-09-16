namespace Homework
{
    internal class Program
    {        
		static void Main(string[] args)
        {
            Console.WriteLine("Enter numbers-—and only numbers—-separated by spaces");
            while (true)
            {
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))   break;
                List<int> numbers = [.. input
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => int.TryParse(s))];                                 
            }
        }
    }
    class CombSorter 
    {
        private static long _step;
        private static double _reductionFactor = 1.247f;
        
        static void CombSort(List<int> numbers)
        {
            _step = (long)Math.Round(numbers.Count / _reductionFactor, MidpointRounding.AwayFromZero);
        }
    }
}
