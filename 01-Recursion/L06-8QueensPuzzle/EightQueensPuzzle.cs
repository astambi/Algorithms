using System;
using System.Collections.Generic;

namespace L06_8QueensPuzzle
{
    public class EightQueensPuzzle
    {
        static bool[][] board = new bool[8][];
        static HashSet<int> attackedRows = new HashSet<int>();
        static HashSet<int> attackedCols = new HashSet<int>();
        static HashSet<int> attackedLeftDiagonals = new HashSet<int>(); // row + col
        static HashSet<int> attackedRightDiagonals = new HashSet<int>(); // row - col

        public static void Main()
        {
            PlaceQueen(0);
        }

        private static void PlaceQueen(int row)
        {
            if (row == 8)
            {
                PrintBoard();
                return;
            }

            board[row] = new bool[8];
            for (int col = 0; col < 8; col++)
            {
                if (CanPlaceQueen(row, col))
                {
                    Mark(row, col);
                    PlaceQueen(row + 1);
                    Unmark(row, col);
                }
            }
        }

        private static void Unmark(int row, int col)
        {
            board[row][col] = false;

            attackedRows.Remove(row);
            attackedCols.Remove(col);
            attackedLeftDiagonals.Remove(row + col);
            attackedRightDiagonals.Remove(row - col);
        }

        private static void Mark(int row, int col)
        {
            board[row][col] = true;

            attackedRows.Add(row);
            attackedCols.Add(col);
            attackedLeftDiagonals.Add(row + col);
            attackedRightDiagonals.Add(row - col);
        }

        private static bool CanPlaceQueen(int row, int col)
        {
            return !attackedRows.Contains(row) &&
                   !attackedCols.Contains(col) &&
                   !attackedLeftDiagonals.Contains(row + col) &&
                   !attackedRightDiagonals.Contains(row - col);
        }

        private static void PrintBoard()
        {
            for (int row = 0; row < board.Length; row++)
            {
                for (int col = 0; col < board[row].Length; col++)
                {
                    if (board[row][col])
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
