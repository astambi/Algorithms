namespace LongestIncreasingSubsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LongestIncreasingSubsequence
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var solutionsLen = new int[numbers.Length];
            var prevIndices = new int[numbers.Length];

            var maxSolutionLen = 0;
            var maxSolutionLenIndex = 0;

            for (int current = 0; current < numbers.Length; current++)
            {
                var solutionLen = 1;
                var prevIndex = -1;
                var currentNumber = numbers[current];

                for (int solIndex = 0; solIndex < current; solIndex++)
                {
                    var prevNumber = numbers[solIndex];
                    var prevSolutionLen = solutionsLen[solIndex];

                    if (currentNumber > prevNumber
                        && solutionLen <= prevSolutionLen)
                    {
                        solutionLen = prevSolutionLen + 1;
                        prevIndex = solIndex;
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

            // Reconstruct Longest Increasing Seq
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
