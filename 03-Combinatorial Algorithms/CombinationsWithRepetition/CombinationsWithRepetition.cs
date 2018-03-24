namespace CombinationsWithRepetition
{
    using System;

    public class CombinationsWithRepetition
    {
        private static string[] elements;
        private static string[] combination;

        public static void Main()
        {
            ReadElements();
            InitializeCombination();
            Combinations(0, 0);
        }

        private static void Combinations(int index, int start)
        {
            if (index >= combination.Length) // k
            {
                Print();
            }
            else
            {
                for (int current = start; current < elements.Length; current++)
                {
                    combination[index] = elements[current];
                    Combinations(index + 1, current);
                }
            }
        }

        private static void Print()
           => Console.WriteLine(string.Join(" ", combination));

        private static void InitializeCombination()
        {
            var k = int.Parse(Console.ReadLine());
            combination = new string[k];
        }

        private static void ReadElements()
            => elements = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
    }
}
