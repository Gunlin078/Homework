using System.Globalization;

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
                List<int> numbers= Sorter.ShellSort(input);

                foreach (var n in numbers)
                {
                    Console.Write(n + " "); 
                }
                Console.WriteLine();
            }
        }
    }
    public static class Sorter 
    {
        public static List<int> ShellSort(string input)
        {
            List<int> numbers = [..input
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Where(s => !HasExtraCharacters(s))
                    .Select(int.Parse)]; 

            List<int> d = [1, 4, 10, 23, 57, 132, 301, 701, 1750];
            int count = numbers.Count;

            if (count > 3937){
                for (int i = 3937; i < count; i = (int)(i*2.25))
                {
                    d.Add(i);
                }
            }
            for (int iN = d.Count - 1; iN >= 0; iN--)
            {
                int n = d[iN];
                for (int i = 1; i < numbers.Count; i+=n)
                {
                    int key = numbers[i];
                    int j = i - 1;
                    while (j >= 0 && numbers[j] > key)
                    {
                        numbers[j + 1] = numbers[j];
                        j--;
                    }
                    numbers[j + 1] = key;
                }
            }
            for (int i = 1; i < numbers.Count; i++)
            {
                int key = numbers[i];
                int j = i - 1;
                while (j >= 0 && numbers[j] > key)
                {
                    numbers[j + 1] = numbers[j];
                    j--;
                }
                numbers[j + 1] = key;
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