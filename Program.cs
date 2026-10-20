
namespace Homework
{
    internal class Program
    {        
		static void Main(string[] args)
        {
            Point2D p1 = new (3.0, 4.0);
            Console.WriteLine(p1);

            Point2D p2 = new (p1);
            Console.WriteLine(p2);

            Point2D p3 = (5.0, 12.0); 
            Console.WriteLine(p3);

            try
            {
                double[] array = [ -1.0, 1.0 ];
                Point2D p4 = (Point2D)array;
                Console.WriteLine(p4);
                
                double[] invalidArray = [ 1.0, 2.0, 3.0, 4.0 ];
                Point2D pInvalid = (Point2D)invalidArray;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Point2D.CreateNew(out Point2D p5);
            Console.WriteLine(p5);

            Point2D p6 = Point2D.FromPolar(45.0, 10.0);
            Console.WriteLine(p6);

            double distanceInstance = p1.DistanceTo(p3);
            Console.WriteLine(distanceInstance.ToString("F2"));

            var tuple1 = (0.0, 0.0);
            var tuple2 = (3.0, 4.0);
            double distanceStatic = Point2D.DistanceBetween(tuple1, tuple2);
            Console.WriteLine(distanceStatic.ToString("F2"));

            Console.WriteLine($"{p1.X} {p1.Y}");
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

