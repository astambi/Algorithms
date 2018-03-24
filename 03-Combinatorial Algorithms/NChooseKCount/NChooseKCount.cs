namespace NChooseKCount
{
    using System;

    public class NChooseKCount
    {
        private static long[][] triangle;

        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var k = int.Parse(Console.ReadLine());

            InitializePascalsTriangle(n);
            Console.WriteLine(BinomialCoefficient(n, k));
        }

        private static long BinomialCoefficient(int n, int k)
        {
            if (k > n)
            {
                return 0;
            }

            if (k == 0 || k == n)
            {
                return 1;
            }

            return triangle[n][k] != 0
                ? triangle[n][k]
                : triangle[n][k] = BinomialCoefficient(n - 1, k - 1) + BinomialCoefficient(n - 1, k);
        }

        private static void InitializePascalsTriangle(int n)
        {
            triangle = new long[n + 1][]; // rows [0, n]

            for (int row = 0; row <= n; row++)
            {
                triangle[row] = new long[row + 1]; // cols [0, row]
            }
        }
    }
}
