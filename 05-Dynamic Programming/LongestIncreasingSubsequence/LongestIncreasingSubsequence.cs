namespace LongestIncreasingSubsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LongestIncreasingSubsequence
    {
        private static int maxSolutionLenIndex;

        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var prevIndices = FindLongestIncreasingSeq(numbers);

            ReconstructSolution(numbers, prevIndices);
        }

        private static int[] FindLongestIncreasingSeq(int[] numbers)
        {
            var solutionsLen = new int[numbers.Length];
            var prevIndices = new int[numbers.Length];

            var maxSolutionLen = 0;

            for (int current = 0; current < numbers.Length; current++)
            {
                var solutionLen = 1;
                var prevIndex = -1;

                var currentNumber = numbers[current];

                for (int prev = 0; prev < current; prev++)
                {
                    var prevNumber = numbers[prev];
                    var prevSolutionLen = solutionsLen[prev];

                    if (currentNumber > prevNumber
                        && solutionLen <= prevSolutionLen)
                    {
                        solutionLen = prevSolutionLen + 1;
                        prevIndex = prev;
                    }
                }

                solutionsLen[current] = solutionLen;
                prevIndices[current] = prevIndex;

                if (solutionLen > maxSolutionLen)
                {
                    maxSolutionLen = solutionLen;
                    maxSolutionLenIndex = current;
                }
            }

            return prevIndices;
        }

        private static void ReconstructSolution(int[] numbers, int[] prevIndices)
        {
            var longestSeq = new Stack<int>();

            var index = maxSolutionLenIndex;

            while (index != -1)
            {
                var currentNumber = numbers[index];
                longestSeq.Push(currentNumber);
                index = prevIndices[index];
            }

            Console.WriteLine(string.Join(" ", longestSeq));
        }
    }
}
