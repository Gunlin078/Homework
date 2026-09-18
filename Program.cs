namespace Homework
{
    internal class Program
    {        
        public static bool HasExtraCharacters(string input)
        {
            ReadOnlySpan<char> span = input.AsSpan();

            for (int i = 0; i < span.Length; i++)
            {
                char c = span[i];
                if (c != ' ' && (c < '0' || c > '9'))   return true;
            }
            return false;
        }

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
            }
        }
    }
    class CombSorter 
    {
        private static long _step;
        private static long _currentNumber;
        private static double _reductionFactor = 1.247f;
        
        static void CombSort(List<int> numbers)
        {
            while (true){
                _step = (long)Math.Round(numbers.Count / _reductionFactor, MidpointRounding.AwayFromZero);
                
            }
        }
    }
}
