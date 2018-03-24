namespace VariationsWithoutRepetition
{
    using System;
    using System.Collections.Generic;

    public class VariationsWithoutRepetition
    {
        private static string[] elements;
        private static string[] variation;
        private static List<int> usedIndices = new List<int>();

        public static void Main()
        {
            ReadElements();
            InitializeVariation();
            Variations(0);
        }

        private static void Variations(int index)
        {
            if (index >= variation.Length) // k
            {
                Print();
            }
            else
            {
                for (int current = 0; current < elements.Length; current++)
                {
                    if (!usedIndices.Contains(current))
                    {
                        usedIndices.Add(current);

                        variation[index] = elements[current];
                        Variations(index + 1);

                        usedIndices.Remove(current);
                    }
                }
            }
        }

        private static void Print()
           => Console.WriteLine(string.Join(" ", variation));

        private static void InitializeVariation()
        {
            var k = int.Parse(Console.ReadLine());
            variation = new string[k];
        }

        private static void ReadElements()
            => elements = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
    }
}
