namespace SubsetSumWithRepeats
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SubsetSumWithRepeats
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var targetSum = int.Parse(Console.ReadLine());

            var possibleSums = CalcSums(targetSum, numbers);
            if (!possibleSums[targetSum])
            {
                return;
            }

            ReconstructSubset(numbers, targetSum, possibleSums);
        }

        private static void ReconstructSubset(int[] numbers, int targetSum, bool[] possibleSums)
        {
            Console.Write($"{targetSum} = ");

            var resultSubset = new List<int>();
            while (targetSum != 0)
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    var number = numbers[i];
                    var remainingSum = targetSum - number;

                    if (remainingSum >= 0 && possibleSums[remainingSum])
                    {
                        resultSubset.Add(number);
                        targetSum = remainingSum;
                    }
                }
            }

            Console.WriteLine(string.Join(" + ", resultSubset.OrderBy(x => x)));
        }

        private static bool[] CalcSums(int targetSum, int[] numbers)
        {
            var possibleSums = new bool[targetSum + 1];
            possibleSums[0] = true;

            for (int sum = 0; sum < possibleSums.Length; sum++)
            {
                if (possibleSums[sum])
                {
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        var newSum = sum + numbers[i];
                        if (newSum < possibleSums.Length)
                        {
                            possibleSums[newSum] = true;
                        }
                    }
                }
            }

            return possibleSums;
        }
    }
}
