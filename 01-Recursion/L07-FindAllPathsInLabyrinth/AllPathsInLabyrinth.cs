using System;
using System.Collections.Generic;
using System.Linq;

namespace L07_FindAllPathsInLabyrinth
{
    public class AllPathsInLabyrinth
    {
        static char[][] lab;
        static List<char> path = new List<char>();

        public static void Main()
        {
            lab = ReadLab();
            FindPaths(0, 0, 'S');
        }

        private static void FindPaths(int row, int col, char direction)
        {
            if (!IsValidCell(row, col))
            {
                return;
            }

            path.Add(direction);

            if (IsExit(row, col))
            {
                PrintPath();
            }
            else if (!IsVisited(row, col) && IsFree(row, col))
            {
                MarkRow(row, col);

                FindPaths(row, col + 1, 'R');
                FindPaths(row + 1, col, 'D');
                FindPaths(row, col - 1, 'L');
                FindPaths(row - 1, col, 'U');

                UnmarkRow(row, col);
            }
            path.RemoveAt(path.Count - 1);
        }

        private static bool IsFree(int row, int col)
        {
            return lab[row][col] == '-';
        }

        private static bool IsVisited(int row, int col)
        {
            return lab[row][col] == 'v';
        }

        private static void UnmarkRow(int row, int col)
        {
            lab[row][col] = '-';
        }

        private static void MarkRow(int row, int col)
        {
            lab[row][col] = 'v';
        }

        private static bool IsExit(int row, int col)
        {
            return lab[row][col] == 'e';
        }

        private static void PrintPath()
        {
            Console.WriteLine(string.Join("", path.Skip(1)));
        }

        private static bool IsValidCell(int row, int col)
        {
            return row >= 0 && row < lab.Length &&
                   col >= 0 && col < lab[row].Length;
        }

        private static char[][] ReadLab()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            lab = new char[rows][];
            for (int row = 0; row < rows; row++)
            {
                lab[row] = Console.ReadLine().ToCharArray();
            }
            return lab;
        }
    }
}