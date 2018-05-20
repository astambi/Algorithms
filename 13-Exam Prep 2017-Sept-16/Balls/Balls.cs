namespace Balls
{
    using System;
    using System.Text;

    public class Balls
    {
        private static int pockets;
        private static int pocketCapacity;
        private static int[] result;
        private static StringBuilder builder = new StringBuilder();

        public static void Main()
        {
            pockets = int.Parse(Console.ReadLine());
            var balls = int.Parse(Console.ReadLine());
            pocketCapacity = int.Parse(Console.ReadLine());

            result = new int[pockets];

            Generate(0, balls);

            Console.WriteLine(builder.ToString().Trim());
        }

        private static void Generate(int pocketIndex, int remainingBalls)
        {
            if (pocketIndex == pockets)
            {
                if (remainingBalls == 0)
                {
                    builder.AppendLine(string.Join(", ", result));
                }

                return;
            }

            var ballsToUse = Math.Min(pocketCapacity, remainingBalls - (pockets - 1 - pocketIndex));

            for (int usedBalls = ballsToUse; usedBalls >= 1; usedBalls--)
            {
                result[pocketIndex] = usedBalls;
                Generate(pocketIndex + 1, remainingBalls - usedBalls);
            }
        }
    }
}
