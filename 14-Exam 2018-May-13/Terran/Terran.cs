namespace Terran
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    public class Terran
    {
        private static char[] elements;
        private static BigInteger permutationsCount;

        public static void Main()
        {
            elements = Console.ReadLine().ToCharArray();

            //Permute(0);  // Slow -> 45/100
            CalcPermutationsWithRepetition(); // 100/100

            Console.WriteLine(permutationsCount);
        }

        private static void CalcPermutationsWithRepetition() // n! / (k1! * k2! * ... ki!)
        {
            permutationsCount = Factorial(elements.Count()); // n!

            foreach (var uniqueElem in elements.Distinct())
            {
                var k = elements.Count(e => e == uniqueElem);
                permutationsCount /= Factorial(k); // k!
            }
        }

        private static BigInteger Factorial(int n)
        {
            BigInteger factorial = 1;
            for (int i = 2; i <= n; i++)
            {
                factorial *= i;
            }

            return factorial;
        }

        private static void Permute(int index)
        {
            if (index >= elements.Length)
            {
                permutationsCount++;
                return;
            }

            var swappedElements = new HashSet<char>();
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

        private static void Swap(int first, int second)
        {
            var temp = elements[first];
            elements[first] = elements[second];
            elements[second] = temp;
        }
    }
}
