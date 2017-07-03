using System;
using System.Linq;

namespace L05_GeneratingCombinations
{
    public class GeneratingCombinations
    {
        public static void Main()
        {
            var set = Console.ReadLine()
                      .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                      .Select(int.Parse)
                      .ToArray();
            var k = int.Parse(Console.ReadLine());
            var vector = new int[k];
            Gen(set, vector, 0, 0); // index in vector, index in set
        }

        private static void Gen(int[] set, int[] vector, int index, int border)
        {
            if (index > vector.Length - 1)
            {
                Console.WriteLine(string.Join(" ", vector));
                return;
            }

            for (int i = border; i < set.Length; i++)
            {
                vector[index] = set[i];
                Gen(set, vector, index + 1, i + 1);
            }
        }
    }
}
