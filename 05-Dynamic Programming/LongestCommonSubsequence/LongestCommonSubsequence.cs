namespace LongestCommonSubsequence
{
    using System;
    using System.Collections.Generic;

    public class LongestCommonSubsequence
    {
        public static void Main()
        {
            //var firstSeq = "ABCBDAB"; 
            //var secondSeq = "BDCABA";

            //var firstSeq = "GCGCAATG";
            //var secondSeq = "GCCCTAGCG";

            var firstSeq = Console.ReadLine();
            var secondSeq = Console.ReadLine();

            var lcs = CalcLongestCommonSeq(firstSeq, secondSeq);

            ReconstructSeq(firstSeq, secondSeq, lcs);
        }

        private static void ReconstructSeq(string firstSeq, string secondSeq, int[][] lcs)
        {
            var longestSeq = new Stack<char>();
            var row = firstSeq.Length;
            var col = secondSeq.Length;

            while (row != 0 && col != 0)
            {
                if (firstSeq[row - 1] == secondSeq[col - 1]         // equal elements &&
                    && lcs[row][col] == 1 + lcs[row - 1][col - 1])  // current = 1 + diagonal
                {
                    longestSeq.Push(firstSeq[row - 1]);
                    row--; // move diagonally
                    col--;
                }
                else if (lcs[row - 1][col] >= lcs[row][col - 1]) // top >= left => move up
                {
                    row--;
                }
                else // left > top => move left
                {
                    col--;
                }
            }

            Console.WriteLine(string.Join(string.Empty, longestSeq));
        }

        private static int[][] CalcLongestCommonSeq(string firstSeq, string secondSeq)
        {
            // Initialize matrix
            var rows = firstSeq.Length + 1;
            var cols = secondSeq.Length + 1;

            var lcs = new int[rows][];
            for (int row = 0; row < rows; row++)
            {
                lcs[row] = new int[cols];
            }

            // Calc LCS
            for (int row = 1; row < rows; row++)
            {
                for (int col = 1; col < cols; col++)
                {
                    var top = lcs[row - 1][col];
                    var left = lcs[row][col - 1];
                    var bestResult = Math.Max(top, left);

                    if (firstSeq[row - 1] == secondSeq[col - 1]) // equal elements
                    {
                        var diagonalTopLeft = lcs[row - 1][col - 1];
                        bestResult = Math.Max(bestResult, 1 + diagonalTopLeft);
                    }

                    lcs[row][col] = bestResult;
                }
            }

            Console.WriteLine(lcs[rows - 1][cols - 1]);
            return lcs;
        }
    }
}
