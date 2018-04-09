namespace SubsetSumNoRepeats
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SubsetSumNoRepeats
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var targetSum = int.Parse(Console.ReadLine());

            var sums = CalcSums(numbers);
            if (!sums.ContainsKey(targetSum))
            {
                return;
            }

            ReconstructSubset(targetSum, sums);
        }

        private static void ReconstructSubset(int targetSum, Dictionary<int, int> sums)
        {
            Console.Write($"{targetSum} = ");

            var resultSubset = new Stack<int>();

            while (targetSum != 0)
            {
                var number = sums[targetSum];
                resultSubset.Push(number);
                targetSum -= number;
            }

            Console.WriteLine(string.Join(" + ", resultSubset.OrderBy(x => x)));
        }

        private static Dictionary<int, int> CalcSums(int[] numbers)
        {
            var sums = new Dictionary<int, int>(); // sum => last added number
            sums[0] = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                var number = numbers[i];

                foreach (var prevSum in sums.Keys.ToList())
                {
                    var newSum = prevSum + number;

                    if (!sums.ContainsKey(newSum))
                    {
                        sums[newSum] = number;
                    }
                }
            }

            return sums;
        }
    }
}
