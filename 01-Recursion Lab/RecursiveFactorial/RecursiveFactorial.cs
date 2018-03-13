namespace RecursiveFactorial
{
    using System;

    public class RecursiveFactorial
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());

            var factorial = Factorial(n);
            Console.WriteLine(factorial);
        }

        private static long Factorial(int n)
            => n == 0
            ? 1
            : n * Factorial(n - 1);
    }
}
