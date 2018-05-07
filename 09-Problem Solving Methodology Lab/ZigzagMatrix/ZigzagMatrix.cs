namespace ZigzagMatrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ZigzagMatrix
    {
        public static void Main()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            var matrix = ReadMatrix(rows);

            var maxPaths = new int[rows][];
            var prevRowIndices = new int[rows][];
            InitializeResultMatrices(matrix, maxPaths, prevRowIndices);

            FillMaxPathsDP(matrix, maxPaths, prevRowIndices);

            var path = ReconstructPath(matrix, maxPaths, prevRowIndices);

            Console.WriteLine($"{path.Sum()} = {string.Join(" + ", path)}");
        }

        private static Stack<int> ReconstructPath(int[][] matrix, int[][] maxPaths, int[][] prevRowIndices)
        {
            var path = new Stack<int>();
            var col = matrix[0].Length - 1;
            var row = FindMaxSumRowIndex(maxPaths);

            while (col >= 0)
            {
                path.Push(matrix[row][col]);

                row = prevRowIndices[row][col];
                col--;
            }

            return path;
        }

        private static int FindMaxSumRowIndex(int[][] maxPaths)
        {
            var rows = maxPaths.Length;
            var cols = maxPaths[0].Length;
            var globalMax = 0;
            var maxRowIndex = -1;

            for (int row = 0; row < rows; row++)
            {
                var currentMax = maxPaths[row][cols - 1];
                if (currentMax > globalMax)
                {
                    globalMax = currentMax;
                    maxRowIndex = row;
                }
            }

            return maxRowIndex;
        }

        private static void FillMaxPathsDP(int[][] matrix, int[][] maxPaths, int[][] prevRowIndices)
        {
            for (int col = 1; col < matrix[0].Length; col++)
            {
                var prevCol = col - 1;

                for (int row = 0; row < matrix.Length; row++)
                {
                    int prevMax = 0;

                    if (col % 2 != 0) // odd cols => compare with cells [row > current row, prev col]
                    {
                        for (int prevRow = row + 1; prevRow < matrix.Length; prevRow++)
                        {
                            if (maxPaths[prevRow][prevCol] > prevMax)
                            {
                                prevMax = maxPaths[prevRow][prevCol];
                                maxPaths[row][col] = prevMax + matrix[row][col];
                                prevRowIndices[row][col] = prevRow;
                            }
                        }
                    }
                    else // even cols => compare with cells [row < current row, prev col]
                    {
                        for (int prevRow = 0; prevRow < row; prevRow++)
                        {
                            if (maxPaths[prevRow][prevCol] > prevMax)
                            {
                                prevMax = maxPaths[prevRow][prevCol];
                                maxPaths[row][col] = prevMax + matrix[row][col];
                                prevRowIndices[row][col] = prevRow;
                            }
                        }
                    }
                }
            }
        }

        private static void InitializeResultMatrices(int[][] matrix, int[][] maxPaths, int[][] prevRowIndices)
        {
            for (int row = 0; row < matrix.Length; row++)
            {
                maxPaths[row] = new int[matrix[row].Length];
                prevRowIndices[row] = new int[matrix[row].Length];
            }

            // Initialize MaxPath first col
            for (int row = 1; row < matrix.Length; row++)
            {
                maxPaths[row][0] = matrix[row][0];
            }
        }

        private static int[][] ReadMatrix(int rows)
        {
            var matrix = new int[rows][];
            for (int row = 0; row < rows; row++)
            {
                matrix[row] = Console.ReadLine()
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
            }

            return matrix;
        }
    }
}
