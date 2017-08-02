using System;
using System.Linq;

namespace L01_RecursiveArraySum
{
    public class RecursiveArrayCalculator
    {
        private readonly string _numbersSpaceDelimitedText;

        public RecursiveArrayCalculator(string numbersSpaceDelimitedText)
        {
            _numbersSpaceDelimitedText = numbersSpaceDelimitedText;
        }

        public long Calculate()
        {
            var array = GetNumbersArray();
            return Sum(array);
        }

        private static long Sum(int[] numbers)
        {
            long sum = 0;
            for (var i = numbers.Length; i --> 0;)
            {
                sum += numbers[i];
            }
            return sum;
        }

        private int[] GetNumbersArray()
        {
            return _numbersSpaceDelimitedText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }
    }

    public class RecursiveArraySum
    {
        public static void Main()
        {
            var numbersSpaceDelimitedText = Console.ReadLine();
            var calculator = new RecursiveArrayCalculator(numbersSpaceDelimitedText);
            var sum = calculator.Calculate();
            Console.WriteLine(sum);
        }
    }
}
