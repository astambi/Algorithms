namespace KnightsTour
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class KnightsTour
    {
        private static int[] adjacentRows = { 1, -1, 1, -1, 2, 2, -2, -2 }; // NB! Order!
        private static int[] adjacentCols = { 2, 2, -2, -2, 1, -1, 1, -1 };
        private static int[][] board;

        private class Move
        {
            public Move(int row, int col, int moves)
            {
                this.Row = row;
                this.Col = col;
                this.OnwardMoves = moves;
            }

            public int Row { get; }

            public int Col { get; }

            public int OnwardMoves { get; }
        }

        public static void Main()
        {
            var size = int.Parse(Console.ReadLine());
            InitializeBoard(size);

            var maxIndex = size * size;
            var index = 1;
            var row = 0;
            var col = 0;
            board[row][col] = index;

            while (index < maxIndex)
            {
                // Warnsdorf's rule: The knight proceeds to the square 
                // from which the knight will have the fewest onward moves. 

                var moves = new List<Move>();
                for (int i = 0; i < adjacentRows.Length; i++)
                {
                    MarkNextMove(row + adjacentRows[i], col + adjacentCols[i], moves);
                }

                var nextMove = moves
                    .Where(m => m.OnwardMoves == moves.Min(x => x.OnwardMoves)) // min onward moves
                    .FirstOrDefault();

                row = nextMove.Row;
                col = nextMove.Col;
                board[row][col] = ++index;
            }

            Print();
        }

        private static void MarkNextMove(int row, int col, List<Move> moves)
        {
            if (IsFreeCell(row, col))
            {
                // Mark visited
                board[row][col] = -1;

                // Count next moves
                var movesCount = 0;
                for (int i = 0; i < adjacentRows.Length; i++)
                {
                    if (IsFreeCell(row + adjacentRows[i], col + adjacentCols[i]))
                    {
                        movesCount++;
                    }
                }

                // Unmark visited
                board[row][col] = 0;

                moves.Add(new Move(row, col, movesCount));
            }
        }

        private static void Print()
        {
            for (int r = 0; r < board.Length; r++)
            {
                Console.WriteLine(string.Join(string.Empty, board[r].Select(c => $"{c}".PadLeft(4))));
            }
        }

        private static bool IsFreeCell(int row, int col)
            => 0 <= row && row < board.Length
            && 0 <= col && col < board[row].Length
            && board[row][col] == 0;

        private static void InitializeBoard(int size)
        {
            board = new int[size][];
            for (int row = 0; row < size; row++)
            {
                board[row] = new int[size];
            }
        }
    }
}
