namespace StarsInTheCube
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StarsInTheCube
    {
        public static void Main()
        {
            var cube = ReadCube();

            var stars = FindStars(cube);

            Print(stars);
        }

        private static void Print(SortedDictionary<char, int> stars)
        {
            Console.WriteLine(stars.Values.Sum());

            foreach (var star in stars)
            {
                Console.WriteLine($"{star.Key} -> {star.Value}");
            }
        }

        private static SortedDictionary<char, int> FindStars(char[][][] cube)
        {
            var stars = new SortedDictionary<char, int>(); // star => count
            var size = cube.Length;

            for (int row = 1; row < size - 1; row++)
            {
                for (int layer = 1; layer < size - 1; layer++)
                {
                    for (int col = 1; col < size - 1; col++)
                    {
                        var symbol = cube[row][layer][col];
                        if (IsStar(cube, row, layer, col, symbol))
                        {
                            if (!stars.ContainsKey(symbol))
                            {
                                stars[symbol] = 0;
                            }

                            stars[symbol]++;
                        }
                    }
                }
            }

            return stars;
        }

        private static bool IsStar(char[][][] cube, int row, int layer, int col, char cell)
            => cell == cube[row - 1][layer][col]
            && cell == cube[row + 1][layer][col]
            && cell == cube[row][layer - 1][col]
            && cell == cube[row][layer + 1][col]
            && cell == cube[row][layer][col - 1]
            && cell == cube[row][layer][col + 1];

        private static char[][][] ReadCube()
        {
            var size = int.Parse(Console.ReadLine());
            var cube = new char[size][][];

            for (int row = 0; row < size; row++)
            {
                var tokens = Console.ReadLine()
                    .Split(new[] { '|' })
                    .Select(x => x.Trim())
                    .ToArray();

                cube[row] = new char[tokens.Length][];
                for (int layer = 0; layer < tokens.Length; layer++)
                {
                    cube[row][layer] = tokens[layer]
                        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x[0])
                        .ToArray();
                }
            }

            return cube;
        }
    }
}
