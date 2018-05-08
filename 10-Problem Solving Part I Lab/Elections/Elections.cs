namespace Elections
{
    using System;
    using System.Linq;
    using System.Numerics;

    public class Elections
    {
        public static void Main()
        {
            var targetSum = int.Parse(Console.ReadLine());
            var numbers = ReadNumbers();

            var sumCounts = CalcSumCounts(numbers);

            var count = CountMatchingSums(targetSum, sumCounts);
            Console.WriteLine(count);
        }

        private static BigInteger CountMatchingSums(int targetSum, BigInteger[] sumCounts)
        {
            BigInteger count = 0;
            for (int sum = targetSum; sum < sumCounts.Length; sum++)
            {
                count += sumCounts[sum];
            }

            return count;
        }

        private static BigInteger[] CalcSumCounts(int[] numbers)
        {
            var sumCounts = new BigInteger[numbers.Sum() + 1]; // sum => count possible combinations
            sumCounts[0] = 1;

            foreach (var number in numbers)
            {
                for (int sum = sumCounts.Length - 1; sum >= 0; sum--) // traversing sums backwards
                {
                    if (sumCounts[sum] != 0)
                    {
                        sumCounts[sum + number] += sumCounts[sum];
                    }
                }
            }

            return sumCounts;
        }

        private static int[] ReadNumbers()
        {
            var numbersCount = int.Parse(Console.ReadLine());
            var numbers = new int[numbersCount];

            for (int i = 0; i < numbersCount; i++)
            {
                numbers[i] = int.Parse(Console.ReadLine());
            }

            return numbers;
        }
    }
}
