using System;

namespace L03_RecursiveDrawing
{
    public class RecursiveDrawing
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            PrintFigure(n);
        }

        private static void PrintFigure(int n)
        {
            if (n <= 0)
            {
                return;
            }

            Console.WriteLine(new string('*', n));
            PrintFigure(n - 1);
            Console.WriteLine(new string('#', n));
        }
    }
}
