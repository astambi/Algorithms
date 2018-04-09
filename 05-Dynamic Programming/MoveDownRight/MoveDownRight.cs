namespace MoveDownRight
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MoveDownRight
    {
        public static void Main()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            var numbers = new int[rows][];
            var sums = new int[rows][];

            for (int row = 0; row < rows; row++)
            {
                numbers[row] = Console.ReadLine()
                    .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                sums[row] = new int[cols];
            }

            // Calculate Max Sums
            sums[0][0] = numbers[0][0];
            for (int col = 1; col < cols; col++)
            {
                sums[0][col] = sums[0][col - 1] + numbers[0][col]; // top row
            }

            for (int row = 1; row < rows; row++)
            {
                sums[row][0] = sums[row - 1][0] + numbers[row][0]; // left col
            }

            for (int row = 1; row < rows; row++)
            {
                for (int col = 1; col < cols; col++)
                {
                    var topSum = sums[row - 1][col];
                    var leftSum = sums[row][col - 1];
                    sums[row][col] = Math.Max(topSum, leftSum) + numbers[row][col];
                }
            }

            // Reconstruct path
            var path = new Stack<string>();
            var currentRow = rows - 1;
            var currentCol = cols - 1;
            path.Push($"[{currentRow}, {currentCol}]");

            while (currentRow != 0 || currentCol != 0)
            {
                if (currentRow == 0)
                {
                    currentCol--;
                }
                else if (currentCol == 0)
                {
                    currentRow--;
                }
                else if (sums[currentRow - 1][currentCol] > sums[currentRow][currentCol - 1])
                {
                    currentRow--;
                }
                else
                {
                    currentCol--;
                }

                path.Push($"[{currentRow}, {currentCol}]");
            }

            Console.WriteLine(string.Join(" ", path));
        }
    }
}
