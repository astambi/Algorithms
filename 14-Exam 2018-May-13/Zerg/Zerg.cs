namespace Zerg
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    public class Zerg
    {
        private static BigInteger[,] matrix;
        private static HashSet<int>[] enemies;

        public static void Main()
        {
            var tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var rows = tokens[0];
            var cols = tokens[1];
            matrix = new BigInteger[rows, cols]; // 0

            tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var destinationRow = tokens[0];
            var destinationCol = tokens[1];

            ReadEnemies(rows);

            CalcMatrixValuesDP(rows, cols, destinationRow, destinationCol);

            Console.WriteLine(matrix[destinationRow, destinationCol]);
        }

        private static void CalcMatrixValuesDP(int rows, int cols, int destinationRow, int destinationCol)
        {
            // Initialize first col
            for (int row = 0; row < rows; row++)
            {
                if (IsEnemy(row, 0))
                {
                    break;
                }

                matrix[row, 0] = 1;
            }

            // Initialize first row
            for (int col = 0; col < cols; col++)
            {
                if (IsEnemy(0, col))
                {
                    break;
                }

                matrix[0, col] = 1;
            }

            // Calc paths DP
            for (int row = 1; row <= destinationRow; row++)
            {
                for (int col = 1; col <= destinationCol; col++)
                {
                    if (!IsEnemy(row, col))
                    {
                        matrix[row, col] = matrix[row - 1, col] + matrix[row, col - 1]; // down & right only
                    }
                }
            }
        }

        private static void ReadEnemies(int rows)
        {
            // Initialize enemies
            enemies = new HashSet<int>[rows];

            for (int row = 0; row < enemies.Length; row++)
            {
                enemies[row] = new HashSet<int>();
            }

            // Mark enemies
            var enemiesCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < enemiesCount; i++)
            {
                var tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();

                var row = tokens[0];
                var col = tokens[1];

                enemies[row].Add(col);
            }
        }

        private static bool IsEnemy(int row, int col)
            => enemies[row].Contains(col);
    }
}
