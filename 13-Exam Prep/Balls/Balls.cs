namespace Balls
{
    using System;
    using System.Text;

    public class Balls
    {
        private static int pockets;
        private static int capacity;
        private static int[] result;
        private static StringBuilder builder = new StringBuilder();

        public static void Main()
        {
            pockets = int.Parse(Console.ReadLine());
            var balls = int.Parse(Console.ReadLine());
            capacity = int.Parse(Console.ReadLine());

            result = new int[pockets];

            Generate(0, balls);

            Console.WriteLine(builder.ToString().Trim());
        }

        private static void Generate(int index, int ballsLeft)
        {
            if (index == pockets)
            {
                if (ballsLeft == 0)
                {
                    builder.AppendLine(string.Join(", ", result));
                }

                return;
            }

            var ballsToPut = ballsLeft - (pockets - (index + 1));
            if (ballsToPut > capacity)
            {
                ballsToPut = capacity;
            }

            for (int i = ballsToPut; i >= 1; i--)
            {
                result[index] = i;
                Generate(index + 1, ballsLeft - i);
            }
        }
    }
}
