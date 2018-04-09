namespace RodCutting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RodCutting
    {
        private static int[] prices; // index = length
        private static int[] bestPrices;
        private static int[] bestLengths;

        public static void Main()
        {
            prices = Console.ReadLine()
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var rodLength = int.Parse(Console.ReadLine());

            bestPrices = new int[rodLength + 1];
            bestLengths = new int[rodLength + 1];

            CutRodRecursion(rodLength);
            //CutRodIteration(rodLength);

            ReconstructSolution(rodLength);
        }

        private static int CutRodRecursion(int len)
        {
            if (bestPrices[len] > 0)
            {
                return bestPrices[len];
            }

            if (len == 0)
            {
                return 0;
            }

            for (int current = 1; current <= len; current++)
            {
                var currentBestPrice = Math.Max(bestPrices[len],
                    prices[current] + CutRodRecursion(len - current));

                if (currentBestPrice > bestPrices[len])
                {
                    bestPrices[len] = currentBestPrice;
                    bestLengths[len] = current;
                }
            }

            return bestPrices[len];
        }

        private static void CutRodIteration(int len)
        {
            for (int current = 1; current <= len; current++)
            {
                for (int prev = 1; prev <= current; prev++)
                {
                    var currentBestPrice = Math.Max(bestPrices[current],
                        prices[prev] + bestPrices[current - prev]);

                    if (currentBestPrice > bestPrices[current])
                    {
                        bestPrices[current] = currentBestPrice;
                        bestLengths[current] = prev;
                    }
                }
            }
        }

        private static void ReconstructSolution(int len)
        {
            if (bestPrices[len] == 0)
            {
                return;
            }

            Console.WriteLine(bestPrices[len]);

            var pieces = new List<int>();
            while (len != 0)
            {
                pieces.Add(bestLengths[len]);
                len -= bestLengths[len];
            }

            Console.WriteLine(string.Join(" ", pieces));
        }
    }
}
