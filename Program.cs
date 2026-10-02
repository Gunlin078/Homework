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
        // 1. Ручной парсинг в один проход без LINQ для максимальной скорости
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

        // Отрезаем невалидные хвосты, если они были
        if (count < buffer.Length)
        {
            Array.Resize(ref buffer, count);
        }

        // 2. Трансформация знака: инвертируем старший бит (делаем все числа "положительными")
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] ^= unchecked((int)0x80000000);
        }

        // Вспомогательный массив для стабильной сортировки (аллокация один раз)
        int[] output = new int[buffer.Length];

        // 3. Побайтовая поразрядная сортировка (4 прохода по 8 бит = 32 бита целого числа)
        // Массив частот теперь размером 256 (так как в 1 байте 256 возможных значений)
        int[] counts = new int[256];

        for (int shift = 0; shift < 32; shift += 8)
        {
            // Очищаем массив частот
            Array.Clear(counts, 0, 256);

            // Подсчет вхождений байта
            for (int i = 0; i < buffer.Length; i++)
            {
                int byteValue = (buffer[i] >> shift) & 0xFF;
                counts[byteValue]++;
            }

            // Вычисление префиксных сумм (позиций)
            for (int i = 1; i < 256; i++)
            {
                counts[i] += counts[i - 1];
            }

            // Перенос элементов в выходной массив (идем с конца для стабильности)
            for (int i = buffer.Length - 1; i >= 0; i--)
            {
                int byteValue = (buffer[i] >> shift) & 0xFF;
                output[counts[byteValue] - 1] = buffer[i];
                counts[byteValue]--;
            }

            // Меняем массивы местами (без лишнего копирования)
            int[] temp = buffer;
            buffer = output;
            output = temp;
        }

        // 4. Обратная трансформация знака (возвращаем минусы на место)
        List<int> result = new List<int>(buffer.Length);
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] ^= unchecked((int)0x80000000);
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
                if (input.Length == 1) return true; // Одиночный минус — не число
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

        static void CountSort(List<int> list, int exp)
        {
            int n = list.Count;
            int[] output = new int[n];
            int[] count = new int[10];

            for (int i = 0; i < n; i++)
            {
                int digit = (list[i] / exp) % 10;
                count[digit]++;
            }

            for (int i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
            }

            for (int i = n - 1; i >= 0; i--)
            {
                int digit = (list[i] / exp) % 10;
                output[count[digit] - 1] = list[i];
                count[digit]--;
            }

            // Перезаписываем элементы в исходном List
            for (int i = 0; i < n; i++)
            {
                list[i] = output[i];
            }
        }

        private static bool Swap<T>(this List<T> list, int index1, int index2)
        {
            (list[index1], list[index2]) = (list[index2], list[index1]);
            return true;
        }
    }
}
