namespace SumTo13
{
    using System;
    using System.Linq;

    public class SumTo13
    {
        private const int TargetSum = 13;

        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            if (numbers[0] + numbers[1] + numbers[2] == TargetSum
                || numbers[0] + numbers[1] - numbers[2] == TargetSum
                || numbers[0] - numbers[1] + numbers[2] == TargetSum
                || numbers[0] - numbers[1] - numbers[2] == TargetSum
                || -numbers[0] + numbers[1] + numbers[2] == TargetSum
                || -numbers[0] + numbers[1] - numbers[2] == TargetSum
                || -numbers[0] - numbers[1] + numbers[2] == TargetSum
                || -numbers[0] - numbers[1] - numbers[2] == TargetSum)
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }
        }
    }
}
