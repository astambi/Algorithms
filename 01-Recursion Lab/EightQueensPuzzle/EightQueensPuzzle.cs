namespace EightQueensPuzzle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EightQueensPuzzle
    {
        private const int Size = 8;

        private static bool[][] board;
        private static HashSet<int> attackedCols = new HashSet<int>();
        private static HashSet<int> attackedLeftDiagonals = new HashSet<int>(); // row - col
        private static HashSet<int> attackedRightDiagonals = new HashSet<int>();// row + col
        //private static int solutionsFound; // check: 92 unique solutions

        public static void Main()
        {
            InitializeBoard();
            PlaceQueen(0);
            //Console.WriteLine(solutionsFound);
        }

        private static void PlaceQueen(int row)
        {
            if (row == Size)
            {
                PrintSolution();
            }
            else
            {
                for (int col = 0; col < Size; col++)
                {
                    if (CanPlaceQueen(row, col))
                    {
                        MarkAttackedPositions(row, col);
                        PlaceQueen(row + 1);
                        UnmarkAttackedPositions(row, col);
                    }
                }
            }
        }

        private static void UnmarkAttackedPositions(int row, int col)
        {
            board[row][col] = false;
            attackedCols.Remove(col);
            attackedLeftDiagonals.Remove(row - col);
            attackedRightDiagonals.Remove(row + col);
        }

        private static void MarkAttackedPositions(int row, int col)
        {
            board[row][col] = true;
            attackedCols.Add(col);
            attackedLeftDiagonals.Add(row - col);
            attackedRightDiagonals.Add(row + col);
        }

        private static bool CanPlaceQueen(int row, int col)
            => !attackedCols.Contains(col)
            && !attackedLeftDiagonals.Contains(row - col)
            && !attackedRightDiagonals.Contains(row + col);

        private static void PrintSolution()
        {
            for (int row = 0; row < board.Length; row++)
            {
                Console.WriteLine(string.Join(" ", board[row].Select(e => e ? '*' : '-')));
            }

            Console.WriteLine();
            //solutionsFound++;
        }

        private static void InitializeBoard()
        {
            board = new bool[Size][];
            for (int row = 0; row < board.Length; row++)
            {
                board[row] = new bool[Size];
            }
        }
    }
}
