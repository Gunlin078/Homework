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
                List<int> numbers = Sorter.HeapSort(input);
                
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

        public static List<int> HeapSort(string input)
        {

            var segments = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var array = new List<int>();

            for (int i = 0; i < segments.Length; i++)
            {
                if (!HasExtraCharacters(segments[i]))
                {
                    array.Add(int.Parse(segments[i]));
                }
            }
            int n = array.Count;

            if (n == 0) return new List<int>();

            // Построение кучи (перегруппируем массив)
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(array, n, i);

            // Поочередно извлекаем элементы из кучи
            for (int i = n - 1; i > 0; i--)
            {
                // Перемещаем текущий корень в конец
                int temp = array[0];
                array[0] = array[i];
                array[i] = temp;

                // Вызываем heapify на уменьшенной куче
                Heapify(array, i, 0);
            }
            return array;
        }

        // Преобразование в двоичную кучу с корнем в индексе i
        private static void Heapify(List<int> array, int heapSize, int rootIndex)
        {
            int largest = rootIndex;
            int left = 2 * rootIndex + 1;  // Левый дочерний элемент
            int right = 2 * rootIndex + 2; // Правый дочерний элемент

            // Если левый дочерний элемент больше корня
            if (left < heapSize && array[left] > array[largest])
                largest = left;

            // Если правый дочерний элемент больше, чем самый большой элемент на данный момент
            if (right < heapSize && array[right] > array[largest])
                largest = right;

            // Если самый большой элемент не корень
            if (largest != rootIndex)
            {
                int swap = array[rootIndex];
                array[rootIndex] = array[largest];
                array[largest] = swap;

                // Рекурсия для поддерева
                Heapify(array, heapSize, largest);
            }
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