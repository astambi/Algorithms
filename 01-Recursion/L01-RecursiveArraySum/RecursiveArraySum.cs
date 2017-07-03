using System;
using System.Linq;

namespace L01_RecursiveArraySum
{
    public class RecursiveArraySum
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
                         .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                         .Select(int.Parse)
                         .ToArray();
            var sum = Sum(numbers, 0);
            Console.WriteLine(sum);
        }

        private static int Sum(int[] numbers, int index)
        {
            if (index == numbers.Length - 1)
            {
                return numbers[index];
            }
            return numbers[index] + Sum(numbers, index + 1);
        }
    }
}
