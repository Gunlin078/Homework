﻿
namespace Homework
{
    internal class Program
    {        
		static void Main(string[] args)
        {

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
        {
            return new Point3D(tuple.x, tuple.y, tuple.z);
        }
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
            return Math.Sqrt(dx * dx + dy * dy);        
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
}
