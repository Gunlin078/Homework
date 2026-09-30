
namespace Homework
{
    internal class Program
    {        
		static void Main(string[] args)
        {

        }
    }
    public struct Point2D
    {
        public Point2D(double x, double y)
        {
            (X, Y) = (x, y);
            Interlocked.Increment(ref _instanceCount);
        }
        public Point2D(Point2D point)
        {
            (X, Y) = (point.X, point.Y);
            Interlocked.Increment(ref _instanceCount);
        }
        public static implicit operator Point2D((double x, double y) tuple)
        {
            return new Point2D(tuple.x, tuple.y);
        }
        public static explicit operator Point2D(double[] array)
        {
            if (array is [double x, double y])
            {
                return new Point2D(x, y);
            }

        throw new ArgumentException("The array must be non-null and contain exactly two elements.");
        }
        public static void CreateNew(out Point2D point)
        {
            point = new Point2D(0, 0); 
        }
        public static Point2D FromPolar(double angleDegrees, double radius)
        {
            double radians = angleDegrees * (Math.PI / 180.0);
            double x = radius * Math.Cos(radians);
            double y = radius * Math.Sin(radians);
            return new Point2D(x, y);
        }
        public readonly double DistanceTo(in Point2D point)
        {
            double dx = X - point.X;
            double dy = Y - point.Y;
            return Math.Sqrt(dx * dx + dy * dy);        
        }
        public static double DistanceBetween((double x, double y) p1, (double x, double y) p2)
        {
            double dx = p1.x - p2.x;
            double dy = p1.y - p2.y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        public readonly override string ToString()
        {
            return $"({X:F2}, {Y:F2})";
        }
        public double X { get; }
        public double Y { get; }
        private static int _instanceCount = 0;
    }
}



/*
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
                
                for (int i = n; i < numbers.Count; i++)
                {
                    int key = numbers[i];
                    int j = i;
                    
                    while (j >= n && numbers[j - n] > key)
                    {
                        numbers[j] = numbers[j - n];
                        j -= n;
                    }
                    numbers[j] = key;
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
*/