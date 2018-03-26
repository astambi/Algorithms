namespace PermutationsWithoutRepetitionIterative
{
    using System;

    public class PermutationsWithoutRepetitionIterative
    {
        private static string[] elements;

        public static void Main()
        {
            ReadElements();

            CountdownQuickPermAlgorithm();
            //CountingQuickPermAlgorithm();
        }

        private static void CountingQuickPermAlgorithm()
        {
            Print();

            // Initialize controller array
            var p = new int[elements.Length]; // n => [0,0,...,0]

            var upper = 1; // initialize upper index = 1

            while (upper < elements.Length) // n
            {
                if (p[upper] < upper)
                {
                    var lower = IsOdd(upper) ? p[upper] : 0;
                    Swap(lower, upper);
                    Print();

                    p[upper]++;
                    upper = 1; // reset upper index
                }
                else
                {
                    p[upper++] = 0; // reset to zero value
                }
            }
        }

        private static void CountdownQuickPermAlgorithm()
        {
            Print();
            
            // Initialize controller array
            var p = new int[elements.Length + 1]; // [0,1,...,n-1]
            for (int i = 0; i < p.Length; i++)
            {
                p[i] = i;
            }

            var upper = 1; // initialize upper index = 1

            while (upper < elements.Length) // n
            {
                p[upper]--;

                var lower = IsOdd(upper) ? p[upper] : 0;
                Swap(lower, upper);
                Print();

                upper = 1; // reset upper index
                while (p[upper] == 0)
                {
                    p[upper] = upper++; // reset zero values in controller array
                }
            }
        }

        private static bool IsOdd(int index)
            => index % 2 != 0;

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
