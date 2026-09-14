namespace Homework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            while (true)
            {
                string? input = Console.ReadLine()?.Replace(" ", "");

                if (string.IsNullOrWhiteSpace(input))   break; 


            }
        }
    }
}
