
namespace Homework
{
    internal class Program
    {        
		static void Main(string[] args)
        {
            Console.WriteLine("=== ТЕСТИРОВАНИЕ POINT3D ===");

        // 1. Создание точек всеми способами
        Point3D pDefault = new Point3D(); // Дефолтный конструктор
        Point3D pConstructor = new Point3D(1.0, 2.0, 3.0); // Стандартный
        Point3D pCopy = new Point3D(pConstructor); // Конструктор копирования
        Point3D pImplicit = (4.0, 5.0, 6.0); // Неявное приведение из кортежа
        Point3D pExplicit = (Point3D)new double[] { 7.0, 8.0, 9.0 }; // Явное из массива
        
        Point3D pOut;
        Point3D.CreateNew(out pOut); // Через out-параметр
        Point3D pPolar = Point3D.FromPolar(5.0, 45.0, 30.0); // Из полярных координат

        Console.WriteLine($"Конструктор: {pConstructor}");
        Console.WriteLine($"Из кортежа: {pImplicit}");
        Console.WriteLine($"Из массива: {pExplicit}");
        Console.WriteLine($"Через Out: {pOut}");
        Console.WriteLine($"Из полярных: {pPolar}");

        // 2. Расстояния
        double dist1 = pConstructor.DistanceTo(pImplicit);
        double dist2 = Point3D.DistanceBetween((1, 2, 3), (4, 5, 6));
        Console.WriteLine($"DistanceTo: {dist1:F4}");
        Console.WriteLine($"DistanceBetween: {dist2:F4}");

        // 3. Счётчик экземпляров (вызов рефлексии для демонстрации приватного поля)
        // Так как поле приватное, выведем его через Reflection для теста
        var pointCounter = typeof(Point3D).GetField("_instanceCount", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?.GetValue(null);
        Console.WriteLine($"Счётчик инстансов Point3D: {pointCounter}");


        Console.WriteLine("\n=== ТЕСТИРОВАНИЕ VECTOR3D ===");

        // 4. Создание вектора по двум точкам
        Point3D startPoint = new Point3D(1, 1, 1);
        Point3D endPoint = new Point3D(4, 5, 6);
        Vector3D vFromPoints = Vector3D.FromPoints(startPoint, endPoint);
        Console.WriteLine($"Вектор по двум точкам (должен быть 3, 4, 5): {vFromPoints}");

        // 5. Арифметика с векторами и числами
        Vector3D v1 = new Vector3D(1, 2, 3);
        Vector3D v2 = new Vector3D(2, 4, 6);

        Vector3D vSum = v1 + v2;
        Vector3D vSub = v1 - v2;
        Vector3D vCompMult = v1 * v2; // Покомпонентное умножение (из вашего кода)
        Vector3D vScale1 = v1 * 2.0;
        Vector3D vScale2 = 3.0 * v1;
        Vector3D vNeg = -v1;
        Vector3D vDivScalar = v2 / 2.0;
        Vector3D vDivVector = v2 / v1;

        Console.WriteLine($"v1 + v2 = {vSum}");
        Console.WriteLine($"v1 - v2 = {vSub}");
        Console.WriteLine($"v1 * v2 (покомпонентно) = {vCompMult}");
        Console.WriteLine($"v1 * 2.0 = {vScale1}");
        Console.WriteLine($"2.0 * v1 = {vScale2}");
        Console.WriteLine($"-v1 = {vNeg}");
        Console.WriteLine($"v2 / 2.0 = {vDivScalar}");
        Console.WriteLine($"v2 / v1 = {vDivVector}");

        // 6. Операции «на месте» (демонстрация специфики struct)
        Vector3D vMutate = new Vector3D(1, 1, 1);
        int initialHash = vMutate.GetHashCode();
        
        // Операция += создает новый объект и перезаписывает переменную vMutate
        vMutate += new Vector3D(2, 2, 2); 
        int afterHash = vMutate.GetHashCode();

        Console.WriteLine($"Операция += : Результат = {vMutate}");
        Console.WriteLine($"Исходный Хэш-код данных: {initialHash} -> Новый Хэш-код данных: {afterHash}");
        Console.WriteLine("Примечание: Так как Vector3D — это struct, данные заменяются целиком (Value Type).");

        // 7. Все операции сравнения
        Vector3D vIdenticalA = new Vector3D(1.0000001, 2, 3);
        Vector3D vIdenticalB = new Vector3D(1.0000002, 2, 3);
        Vector3D vLarge = new Vector3D(10, 10, 10);

        Console.WriteLine($"vIdenticalA == vIdenticalB (с учетом epsilon): {vIdenticalA == vIdenticalB}");
        Console.WriteLine($"v1 != v2: {v1 != v2}");
        Console.WriteLine($"vLarge > v1 (по длине): {vLarge > v1}");
        Console.WriteLine($"v1 < vLarge (по длине): {v1 < vLarge}");
        Console.WriteLine($"vIdenticalA.Equals(vIdenticalB): {vIdenticalA.Equals(vIdenticalB)}");

        // 8. Длина, коллинеарность и компланарность
        Vector3D vCollinear1 = new Vector3D(1, 2, 3);
        Vector3D vCollinear2 = new Vector3D(2, 4, 6);
        Vector3D vNonCollinear = new Vector3D(1, 0, 5);

        Vector3D vCoplanar1 = new Vector3D(1, 0, 0);
        Vector3D vCoplanar2 = new Vector3D(0, 1, 0);
        Vector3D vCoplanar3 = new Vector3D(1, 1, 0); // Лежит в той же плоскости XY

        Console.WriteLine($"Длина v2: {v2.Len():F4}");
        Console.WriteLine($"Коллинеарность (vCollinear1 и vCollinear2): {Vector3D.IsCollinear(vCollinear1, vCollinear2)}");
        Console.WriteLine($"Коллинеарность (vCollinear1 и vNonCollinear): {Vector3D.IsCollinear(vCollinear1, vNonCollinear)}");
        Console.WriteLine($"Компланарность (Плоскость XY): {Vector3D.IsCoplanar(vCoplanar1, vCoplanar2, vCoplanar3)}");

        // 9. Индексация и перебор (GetEnumerator)
        Console.WriteLine($"Индексатор v1[1] (компонента Y): {v1[1]}");
        
        Console.Write("Перебор компонентов v1 через foreach: ");
        foreach (double component in v1)
        {
            Console.Write($"{component:F1} ");
        }
        Console.WriteLine();
        
        var vectorCounter = typeof(Vector3D).GetField("_instanceCount", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?.GetValue(null);
        Console.WriteLine($"Счётчик инстансов Vector3D: {vectorCounter}");
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
        //                          CONSTRUCTORS
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

        //                          ARITHMETIC OPERATORS

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


        //                          COMPARISON OPERATORS


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


        //                          ENUMERATIONS


        public readonly double this[int index] => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => throw new ArgumentOutOfRangeException(nameof(index))
        };

        // Warning: not the best implementation; creates unnecessary memory clutter
        public readonly Span<double>.Enumerator GetEnumerator()
        {
            return new[] { X, Y, Z }.AsSpan().GetEnumerator();
        }

        //                          GEOMETRIC

        public readonly double Len()
        {
            return Math.Sqrt(X*X + Y*Y + Z*Z);
        }

        public static bool IsCollinear(Vector3D a, Vector3D b)
        {
            double crossX = a.Y * b.Z - a.Z * b.Y;
            double crossY = a.Z * b.X - a.X * b.Z;
            double crossZ = a.X * b.Y - a.Y * b.X;

            double crossLengthSquared = crossX * crossX + crossY * crossY + crossZ * crossZ;

            const double epsilon = 1e-10; 

            return crossLengthSquared < epsilon;
        }

        public static bool IsCoplanar(Vector3D a, Vector3D b, Vector3D c)
        {
            double crossX = a.Y * b.Z - a.Z * b.Y;
            double crossY = a.Z * b.X - a.X * b.Z;
            double crossZ = a.X * b.Y - a.Y * b.X;

            double mixedProduct = crossX * c.X + crossY * c.Y + crossZ * c.Z;

            const double epsilon = 1e-10;
            return Math.Abs(mixedProduct) < epsilon;
        }


        //                          OTHER


        public readonly override string ToString()
        {
            return $"({X:F2}, {Y:F2}, {Z:F2})";
        }

        public readonly override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }


        //                          STORED DATA


        public double X { get; init; }
        public double Y { get; init; }
        public double Z { get; init; }
        private static int _instanceCount = 0;
    }
}