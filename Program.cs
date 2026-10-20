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
                List<int> numbers = Sorter.RadixSort(input);
                
                if (numbers.Count == 0)
                {
                    Console.WriteLine("No valid numbers found. Try again");
                    continue;
                }

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
        public static List<int> RadixSort(string input)
        {
            var segments = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int[] buffer = new int[segments.Length];
            int count = 0;

            for (int i = 0; i < segments.Length; i++)
            {
                if (!HasExtraCharacters(segments[i]))
                {
                    buffer[count++] = int.Parse(segments[i]);
                }
            }

            if (count == 0) return new List<int>();

            if (count < buffer.Length) //обрезка
            {
                Array.Resize(ref buffer, count);
            }

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] ^= int.MinValue;
            }

            int[] output = new int[buffer.Length];

            int[] counts = new int[256];

            for (int shift = 0; shift < 32; shift += 8)
            {
                Array.Clear(counts, 0, 256);

                for (int i = 0; i < buffer.Length; i++)
                {
                    int byteValue = (buffer[i] >> shift) & 0xFF;
                    counts[byteValue]++;
                }

                for (int i = 1; i < 256; i++)
                {
                    counts[i] += counts[i - 1];
                }

                for (int i = buffer.Length - 1; i >= 0; i--)
                {
                    int byteValue = (buffer[i] >> shift) & 0xFF;
                    output[counts[byteValue] - 1] = buffer[i];
                    counts[byteValue]--;
                }

                int[] temp = buffer;
                buffer = output;
                output = temp;
            }

            List<int> result = new List<int>(buffer.Length);
            for (int i = 0; i < buffer.Length; i++) //возврат минусов
            {
                buffer[i] ^= int.MinValue;
                result.Add(buffer[i]);
            }

            return result;
        }

        public static bool HasExtraCharacters(string input)
        {
            if (input.Length == 0) return true;
            
            int start = 0;
            if (input[0] == '-')
            {
                if (input.Length == 1) return true;
                start = 1;
            }

            for (int i = start; i < input.Length; i++)
            {
                char c = input[i];
                if (c < '0' || c > '9')
                {
                    return true;
                }
            }
            return false;
        }

        private static bool Swap<T>(this List<T> list, int index1, int index2)
        {
            (list[index1], list[index2]) = (list[index2], list[index1]);
            return true;
        }
    }
}
