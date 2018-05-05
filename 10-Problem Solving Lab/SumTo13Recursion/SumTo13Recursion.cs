namespace SumTo13Recursion
{
    using System;
    using System.Linq;

    public class SumTo13Recursion
    {
        private const int TargetSum = 13;

        private static bool isTargetSum;

        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            CalcSum(0, 0, numbers);
            Console.WriteLine(isTargetSum ? "Yes" : "No");
        }

        private static void CalcSum(int sum, int index, int[] numbers)
        {
            if (index >= numbers.Length)
            {
                if (sum == TargetSum)
                {
                    isTargetSum = true;
                }

                return;
            }

            CalcSum(sum + numbers[index], index + 1, numbers);
            CalcSum(sum - numbers[index], index + 1, numbers);
        }
    }
}
