namespace RecursiveArraySum
{
    using System;
    using System.Linq;

    public class RecursiveArraySum
    {
        public static void Main()
        {
            var numbers = ReadNumbers();

            var sum = Sum(numbers, 0);
            Console.WriteLine(sum);
        }

        private static long Sum(int[] numbers, int index)
            => index == numbers.Length
            ? 0
            : numbers[index] + Sum(numbers, index + 1);

        private static int[] ReadNumbers()
            => Console.ReadLine()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
    }
}
