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

            var sums = CalcSums(numbers);

            var count = CountMatchingSums(targetSum, sums);
            Console.WriteLine(count);
        }

        private static BigInteger CountMatchingSums(int targetSum, BigInteger[] sums)
        {
            BigInteger count = 0;
            for (int sum = targetSum; sum < sums.Length; sum++)
            {
                count += sums[sum];
            }

            return count;
        }

        private static BigInteger[] CalcSums(int[] numbers)
        {
            var sums = new BigInteger[numbers.Sum() + 1]; // sums => possible combinations
            sums[0] = 1; // visited

            foreach (var number in numbers)
            {
                for (int i = sums.Length - 1; i >= 0; i--)
                {
                    if (sums[i] != 0)
                    {
                        sums[i + number] += sums[i];
                    }
                }
            }

            return sums;
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
