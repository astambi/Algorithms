namespace ShootingRange
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ShootingRange
    {
        public static void Main()
        {
            var targets = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            var targetScore = int.Parse(Console.ReadLine());

            GeneratePermutations(0, targets, targetScore, new bool[targets.Length]);
        }

        private static void GeneratePermutations(int index, int[] elements, int targetScore, bool[] marked)
        {
            var currentScore = GetScore(elements, marked);

            if (currentScore == targetScore)
            {
                Print(elements, marked);
                return;
            }

            if (index >= elements.Length || currentScore >= targetScore)
            {
                return;
            }

            var swappedElements = new HashSet<int>();

            for (int current = index; current < elements.Length; current++)
            {
                var currentElement = elements[current];
                if (!swappedElements.Contains(currentElement))
                {
                    swappedElements.Add(currentElement);
                    Swap(index, current, elements);
                    marked[index] = true;

                    GeneratePermutations(index + 1, elements, targetScore, marked);

                    Swap(index, current, elements);
                    marked[index] = false;
                }
            }
        }

        private static void Print(int[] elements, bool[] marked)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < elements.Length; i++)
            {
                if (marked[i])
                {
                    builder.Append(elements[i] + " ");
                }
            }

            Console.WriteLine(builder.ToString().Trim());
        }

        private static int GetScore(int[] elements, bool[] marked)
        {
            var score = 0;
            var multiplier = 1;

            for (int i = 0; i < elements.Length; i++)
            {
                if (marked[i])
                {
                    score += elements[i] * multiplier++;
                }
            }

            return score;
        }

        private static void Swap(int first, int second, int[] elements)
        {
            var temp = elements[first];
            elements[first] = elements[second];
            elements[second] = temp;
        }
    }
}
