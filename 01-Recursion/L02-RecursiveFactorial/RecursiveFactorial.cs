using System;

namespace L02_RecursiveFactorial
{
    public class RecursiveFactorial
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var factorial = Factorial(n);
            Console.WriteLine(factorial);
        }

        private static long Factorial(int n)
        {
            if (n == 0)
            {
                return 1;
            }
            return n * Factorial(n - 1);
        }
    }
}
