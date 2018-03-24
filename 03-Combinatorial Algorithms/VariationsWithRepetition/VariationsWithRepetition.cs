namespace VariationsWithRepetition
{
    using System;

    public class VariationsWithRepetition
    {
        private static string[] elements;
        private static string[] variation;

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
                    variation[index] = elements[current];
                    Variations(index + 1);
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
