
namespace Homework
{
    internal class Program
    {        
		static void Main(string[] args)
        {
            var a = new Vector3D(1, 5, 9);
            var b = new Vector3D(0,0,0);
            Console.WriteLine( (a/b).ToString() );
            //Console.WriteLine( (a/0).ToString() );
            //Console.WriteLine( (a/2.1).ToString() );

            b+=a*2;

            Console.WriteLine( b.ToString() );
            Console.WriteLine( a.ToString() );


        }
    }



    public struct Point3D
    {
        public Point3D(double x, double y, double z)
        {
            (X, Y, Z) = (x, y, z);
            Interlocked.Increment(ref _instanceCount);
        }
        public Point3D(Point3D point)
        {
            (X, Y, Z) = (point.X, point.Y, point.Z);
            Interlocked.Increment(ref _instanceCount);
        }
        public static implicit operator Point3D((double x, double y, double z) tuple)
            => new(tuple.x, tuple.y, tuple.z);

        public static explicit operator Point3D(double[] array)
        {
            if (array is [double x, double y, double z])
            {
                return new Point3D(x, y, z);
            }

        throw new ArgumentException("The array must be non-null and contain exactly two elements.");
        }
        public static void CreateNew(out Point3D point)
        {
            point = new Point3D(0, 0, 0); 
        }
        public static Point3D FromPolar(double r, double thetaDegrees, double phiDegrees)
        {
            double thetaRadians = thetaDegrees * (Math.PI / 180.0);
            double phiRadians = phiDegrees * (Math.PI / 180.0);

            double x = r * Math.Cos(phiRadians) * Math.Cos(thetaRadians);
            double y = r * Math.Cos(phiRadians) * Math.Sin(thetaRadians);
            double z = r * Math.Sin(phiRadians);

            return new Point3D(x, y, z);
        }

        public readonly double DistanceTo(in Point3D point)
        {
            double dx = X - point.X;
            double dy = Y - point.Y;
            double dz = Z - point.Z;
            return Math.Sqrt(dx * dx + dy * dy +  dz * dz);        
        }
        public static double DistanceBetween((double x, double y, double z) p1, (double x, double y, double z) p2)
        {
            double dx = p1.x - p2.x;
            double dy = p1.y - p2.y;
            double dz = p1.z - p2.z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
        public readonly override string ToString()
        {
            return $"({X:F2}, {Y:F2}, {Z:F2})";
        }
        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        private static int _instanceCount = 0;
    }


    public struct Vector3D
    {
        public Vector3D(double x, double y, double z)
        {
            (X, Y, Z) = (x, y, z);
            Interlocked.Increment(ref _instanceCount);
        }
        public Vector3D(Vector3D point)
        {
            (X, Y, Z) = (point.X, point.Y, point.Z);
            Interlocked.Increment(ref _instanceCount);
        }
        public static implicit operator Vector3D((double x, double y, double z) tuple)
            => new(tuple.x, tuple.y, tuple.z);

        public static explicit operator Vector3D(double[] array)
        {
            if (array is [double x, double y, double z])
            {
                return new Vector3D(x, y, z);
            }

        throw new ArgumentException("The array must be non-null and contain exactly two elements.");
        }
        public static void CreateNew(out Vector3D point)
        {
            point = new Vector3D(0, 0, 0); 
        }
        public static Vector3D FromPoints(Point3D start, Point3D end)
        {
            double dx = end.X - start.X;
            double dy = end.Y - start.Y;
            double dz = end.Z - start.Z;
            return new Vector3D(dx, dy, dz);
        }

        //                  ARITHMETIC OPERATORS

        public static Vector3D operator +(Vector3D a, Vector3D b) { return new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z); }
        public static Vector3D operator -(Vector3D a, Vector3D b) { return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z); }
        public static Vector3D operator *(Vector3D a, Vector3D b) { return new Vector3D(a.X * b.X, a.Y * b.Y, a.Z * b.Z); }

        public static Vector3D operator *(Vector3D a, double scalar) { return new Vector3D(a.X * scalar, a.Y * scalar, a.Z * scalar); }
        public static Vector3D operator *(double scalar, Vector3D a) { return new Vector3D(a.X * scalar, a.Y * scalar, a.Z * scalar); }
        public static Vector3D operator -(Vector3D a)                { return new Vector3D(-a.X, -a.Y, -a.Z); }

        public static Vector3D operator /(Vector3D a, double scalar) 
        { 
            return scalar == 0 
                ? throw new DivideByZeroException("Dividing by 0 is bad!") 
                : new Vector3D(a.X / scalar, a.Y / scalar, a.Z / scalar); 
        }

        public static Vector3D operator /(Vector3D a, Vector3D b) =>
            new (
                b.X == 0 ? throw new DivideByZeroException("Dividing by 0 is bad!") : a.X / b.X,
                b.Y == 0 ? throw new DivideByZeroException("Dividing by 0 is bad!") : a.Y / b.Y,
                b.Z == 0 ? throw new DivideByZeroException("Dividing by 0 is bad!") : a.Z / b.Z
            );


        //                  COMPARISON OPERATORS


        public static bool operator ==(Vector3D a, Vector3D b)
        {
            const double epsilon = 0.000001;
            return Math.Abs(a.X - b.X) < epsilon && Math.Abs(a.Y - b.Y) < epsilon && Math.Abs(a.Z - b.Z) < epsilon;
        }

        public static bool operator !=(Vector3D a, Vector3D b)
        {
            return !(a == b);
        }

        public static bool operator >(Vector3D a, Vector3D b)
        {
            return (a.X * a.X + a.Y * a.Y + a.Z * a.Z) > (b.X * b.X + b.Y * b.Y + b.Z * b.Z);
        }

        public static bool operator <(Vector3D a, Vector3D b)
        {
            return (a.X * a.X + a.Y * a.Y + a.Z * a.Z) < (b.X * b.X + b.Y * b.Y + b.Z * b.Z);
        }

        public readonly override bool Equals(object? obj)
        {
            if (obj is Vector3D other)
            {
                const double epsilon = 0.000001;
                return  Math.Abs(X - other.X) < epsilon &&
                        Math.Abs(Y - other.Y) < epsilon &&
                        Math.Abs(Z - other.Z) < epsilon;
            }
            return false;
        }

        //                      INDEX
       public readonly double this[int index] => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => throw new ArgumentOutOfRangeException(nameof(index))
        };

        //                      OTHER
        public readonly override string ToString()
        {
            return $"({X:F2}, {Y:F2}, {Z:F2})";
        }

        public readonly override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }


        //                  STORED DATA


        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        private static int _instanceCount = 0;

    }
}