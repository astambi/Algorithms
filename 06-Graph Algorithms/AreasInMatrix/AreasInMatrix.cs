namespace AreasInMatrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AreasInMatrix
    {
        private static char[][] matrix;
        private static bool[][] visited;
        private static SortedDictionary<char, int> areas = new SortedDictionary<char, int>();

        public static void Main()
        {
            InitializeMatrices();

            FindConnectedAreas();

            PrintResult();
        }

        private static void PrintResult()
        {
            var totalAreas = areas.Values.Sum();
            Console.WriteLine($"Areas: {totalAreas}");

            foreach (var area in areas)
            {
                Console.WriteLine($"Letter '{area.Key}' -> {area.Value}");
            }
        }

        private static void FindConnectedAreas()
        {
            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    if (!visited[row][col])
                    {
                        var content = matrix[row][col];

                        // Start a new area
                        if (!areas.ContainsKey(content))
                        {
                            areas[content] = 0;
                        }

                        areas[content]++;

                        // Expand area
                        ExpandArea(content, row, col);
                    }
                }
            }
        }

        private static void ExpandArea(char content, int row, int col)
        {
            if (IsValid(row, col)
                && matrix[row][col] == content
                && !visited[row][col])
            {
                visited[row][col] = true;

                ExpandArea(content, row - 1, col);
                ExpandArea(content, row + 1, col);
                ExpandArea(content, row, col - 1);
                ExpandArea(content, row, col + 1);
            }
        }

        private static bool IsValid(int row, int col)
            => row >= 0 && row < matrix.Length
            && col >= 0 && col < matrix[row].Length;

        private static void InitializeMatrices()
        {
            var rows = int.Parse(Console.ReadLine());

            matrix = new char[rows][];
            visited = new bool[rows][];

            for (int row = 0; row < rows; row++)
            {
                matrix[row] = Console.ReadLine().ToCharArray();
                visited[row] = new bool[matrix[row].Length];
            }
        }
    }
}
