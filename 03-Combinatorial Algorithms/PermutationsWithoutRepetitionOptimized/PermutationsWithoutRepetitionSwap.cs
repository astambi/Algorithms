namespace PermutationsWithoutRepetitionOptimized
{
    using System;

    public class PermutationsWithoutRepetitionSwap
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
                Permute(index + 1);

                for (int current = index + 1; current < elements.Length; current++)
                {
                    Swap(index, current);

                    Permute(index + 1);

                    Swap(index, current);
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
