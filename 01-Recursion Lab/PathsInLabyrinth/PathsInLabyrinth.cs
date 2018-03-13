namespace PathsInLabyrinth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PathsInLabyrinth
    {
        private const char ExitCell = 'e';
        private const char PassableCell = '-';
        private const char VisitedCell = 'v';

        private static List<char> path = new List<char>();
        private static char[][] labyrinth;

        public static void Main()
        {
            labyrinth = ReadLabyrinth();
            FindPath(0, 0, 'S');
        }

        private static void FindPath(int row, int col, char direction)
        {
            if (!IsValid(row, col))
            {
                return;
            }

            path.Add(direction);

            if (IsExit(row, col))
            {
                PrintPath();
            }
            else if (!IsVisited(row, col) && IsPassable(row, col))
            {
                MarkVisited(row, col);

                // Find Paths
                FindPath(row, col - 1, 'L');
                FindPath(row, col + 1, 'R');
                FindPath(row - 1, col, 'U');
                FindPath(row + 1, col, 'D');

                UnmarkVisited(row, col);
            }

            path.RemoveAt(path.Count - 1);
        }

        private static void PrintPath()
            => Console.WriteLine(string.Join(string.Empty, path.Skip(1)));

        private static void UnmarkVisited(int row, int col)
            => labyrinth[row][col] = PassableCell;

        private static void MarkVisited(int row, int col)
            => labyrinth[row][col] = VisitedCell;

        private static bool IsVisited(int row, int col)
            => labyrinth[row][col] == VisitedCell;

        private static bool IsExit(int row, int col)
            => labyrinth[row][col] == ExitCell;

        private static bool IsPassable(int row, int col)
            => labyrinth[row][col] == PassableCell;

        private static bool IsValid(int row, int col)
            => 0 <= row && row < labyrinth.Length
            && 0 <= col && col < labyrinth[row].Length;

        private static char[][] ReadLabyrinth()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            var labyrinth = new char[rows][];
            for (int row = 0; row < rows; row++)
            {
                labyrinth[row] = Console.ReadLine().ToCharArray();
            }

            return labyrinth;
        }
    }
}
