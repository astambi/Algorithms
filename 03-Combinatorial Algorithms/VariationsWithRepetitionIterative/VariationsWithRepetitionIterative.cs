namespace VariationsWithRepetitionIterative
{
    using System;

    public class VariationsWithRepetitionIterative
    {
        private static int[] variation;

        public static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var k = int.Parse(Console.ReadLine());
            variation = new int[k];

            while (true)
            {
                Print();

                int index = k - 1;
                while (index >= 0 && variation[index] == n - 1)
                {
                    index--;
                }

                if (index < 0)
                {
                    break;
                }

                variation[index]++;
                for (int i = index + 1; i < k; i++)
                {
                    variation[i] = 0;
                }
            }
        }

        private static void Print()
           => Console.WriteLine(string.Join(" ", variation));
    }
}
