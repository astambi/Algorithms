namespace Blocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Blocks
    {
        private const int LettersToChoose = 4;

        private static readonly HashSet<string> usedCombinations = new HashSet<string>();

        public static void Main()
        {
            var numberOfLetters = int.Parse(Console.ReadLine());
            var results = FindBlocksRecursion(numberOfLetters);
            Print(results);
        }

        private static void Print(HashSet<string> results)
        {
            Console.WriteLine($"Number of blocks: {results.Count}");

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        private static HashSet<string> FindBlocksRecursion(int numberOfLetters)
        {
            var letters = GenerateLetters(numberOfLetters);
            var usedLetters = new bool[numberOfLetters];

            var results = new HashSet<string>();
            GenerateVariations(new char[LettersToChoose], letters, usedLetters, results);
            return results;
        }

        private static void GenerateVariations(char[] combination, char[] letters, bool[] usedLetters, HashSet<string> results, int index = 0)
        {
            if (index >= combination.Length)
            {
                AddResult(combination, results);
            }
            else
            {
                for (int i = 0; i < letters.Length; i++)
                {
                    if (!usedLetters[i])
                    {
                        // Mark used
                        usedLetters[i] = true;

                        combination[index] = letters[i];
                        GenerateVariations(combination, letters, usedLetters, results, index + 1);

                        // Unmark used
                        usedLetters[i] = false;
                    }
                }
            }
        }

        private static void AddResult(char[] result, HashSet<string> results)
        {
            var rotatedStr = RotateBlock(result);
            if (!usedCombinations.Contains(rotatedStr))
            {
                results.Add(rotatedStr);
                usedCombinations.Add(rotatedStr);
            }
        }

        private static string RotateBlock(char[] block)
        {
            var startLetter = block.Min(e => e);
            var rotated = block.ToArray();

            while (rotated[0] != startLetter) // ordered alphabetically
            {
                rotated = new[] { rotated[2], rotated[0], rotated[3], rotated[1] };
            }

            return new string(rotated);
        }

        private static char[] GenerateLetters(int n)
        {
            var letters = new char[n];
            for (int i = 0; i < n; i++)
            {
                letters[i] = (char)(i + 'A');
            }

            return letters;
        }
    }
}
