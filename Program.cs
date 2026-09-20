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
    public static class CombSorter 
    {

        public static void Swap<T>(this List<T> list, int index1, int index2)
        {
            (list[index1], list[index2]) = (list[index2], list[index1]);
        }
        static void CombSort(List<int> numbers)
        {
            int _step;
            double _reductionFactor = 1.247;

            while (true){
                _step = (int)Math.Round(numbers.Count / _reductionFactor, MidpointRounding.AwayFromZero);

                for ( int i = 0; i<numbers.Count - 2; i++)
                {
                    if (numbers[i] > numbers[i+_step]) {}
                }
            }
        }
    }
}