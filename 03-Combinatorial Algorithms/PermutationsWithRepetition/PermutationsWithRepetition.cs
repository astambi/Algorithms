namespace PermutationsWithRepetition
{
    using System;
    using System.Collections.Generic;

    public class PermutationsWithRepetition
    {
        private static string[] elements;

        public static void Main()
        {
            ReadElements();
            Permute(0);
        }

        private static void Permute(int index)
        {
            if (index >= elements.Length)
            {
                Print();
            }
            else
            {
                var swappedElements = new HashSet<string>();

                for (int current = index; current < elements.Length; current++)
                {
                    var currentElement = elements[current];
                    if (!swappedElements.Contains(currentElement))
                    {
                        swappedElements.Add(currentElement);
                        Swap(index, current);

                        Permute(index + 1);

                        Swap(index, current);
                    }
                }
            }
        }

        private static void Print()
            => Console.WriteLine(string.Join(" ", elements));

        private static void Swap(int first, int second)
        {
            var temp = elements[first];
            elements[first] = elements[second];
            elements[second] = temp;
        }

        private static void ReadElements()
            => elements = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
    }
}
