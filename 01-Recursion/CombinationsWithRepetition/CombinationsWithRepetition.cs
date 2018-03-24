namespace CombinationsWithRepetition
{
    using System;

    public class CombinationsWithRepetition
    {
        private static int[] elements;

        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var k = int.Parse(Console.ReadLine());

            GetElements(n);
            GenerateCombination(new int[k], 0, 0);
        }

        private static void GenerateCombination(int[] combination, int index, int border)
        {
            if (index == combination.Length) // k
            {
                Print(combination);
            }
            else
            {
                for (int current = border; current < elements.Length; current++)
                {
                    combination[index] = elements[current];

                    GenerateCombination(combination, index + 1, current);
                }
            }
        }

        private static void Print(int[] combination)
            => Console.WriteLine(string.Join(" ", combination));

        private static void GetElements(int n)
        {
            elements = new int[n];
            for (int i = 0; i < n; i++)
            {
                elements[i] = i + 1;
            }
        }
    }
}
