namespace EgyptianFractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EgyptianFractions
    {
        public static void Main()
        {
            var tokens = Console.ReadLine()
                .Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToList();

            var numerator = tokens[0]; // a
            var denominator = tokens[1]; // b

            if (numerator >= denominator)
            {
                Console.WriteLine("Error (fraction is equal to or greater than 1)");
                return;
            }

            var fractions = CalculateFractions(numerator, denominator);

            Console.WriteLine($"{numerator}/{denominator} = " +
                $"{string.Join(" + ", fractions.Select(x => $"1/{x}"))}");
        }

        private static List<int> CalculateFractions(long numerator, long denominator)
        {
            // a/b = 1/c1 + 1/c2 + .....
            var fractions = new List<int>(); // c
            var nextDenominator = 2; // c

            while (numerator != 0)
            {
                var resultNumerator = numerator * nextDenominator - denominator; // a * c - b
                if (resultNumerator >= 0)
                {
                    fractions.Add(nextDenominator);
                    numerator = resultNumerator; // a * c - b
                    denominator *= nextDenominator; // b * c 
                }

                nextDenominator++;
            }

            return fractions;
        }
    }
}
