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
                List<int> numbers= CombSorter.CombSort(input);

                foreach (var n in numbers)
                {
                    Console.Write(n + " "); 
                }
                Console.WriteLine();
            }
        }
    }
    public static class CombSorter 
    {
        public static List<int> CombSort(string input)
        {
            List<int> numbers = [.. input
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Where( s => !HasExtraCharacters(s))
                    .Select(s => int.Parse(s))]; 
            double _reductionFactor = 1.247;
            bool _proceed = true;

            int _step = numbers.Count;
            
            while (_step > 1 || _proceed){
                _step = (int)(_step / _reductionFactor);
                _proceed = false;
                if (_step < 1)
                {
                    _step = 1;
                }

                for ( int i = 0; i + _step < numbers.Count; i++)
                {
                    if (numbers[i] > numbers[i+_step]) 
                    {
                        _proceed = numbers.Swap(i, i+_step); 
                    }
                }
                
            }
            return numbers;
        }
        private static bool Swap<T>(this List<T> list, int index1, int index2)
        {
            (list[index1], list[index2]) = (list[index2], list[index1]);
            return true;
        }
        public static bool HasExtraCharacters(string input)
        {
            ReadOnlySpan<char> span = input.AsSpan();

            for (int i = 0; i < span.Length; i++)
            {
                char c = span[i];

                if (i == 0 && c == '-') continue;
                
                if (c != ' ' && (c < '0' || c > '9'))
                {
                    return true;
                }
            }

            return false;
        }
    }
}