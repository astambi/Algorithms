namespace ConnectedAreasInMatrix
{
    using System;
    using System.Collections.Generic;

    public class ConnectedAreasInMatrix
    {
        private const char Wall = '*';
        private const char VisitedCell = 'v';

        private static char[][] matrix;
        private static SortedSet<Area> areas = new SortedSet<Area>();

        public static void Main()
        {
            ReadMatrix();
            FindSolutions();
            Print();
        }

        private static void FindSolutions()
        {
            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    FindArea(row, col);
                }
            }
        }

        private static void FindArea(int row, int col)
        {
            if (matrix[row][col] == Wall
                || matrix[row][col] == VisitedCell)
            {
                return;
            }

            var area = new Area(row, col);
            ExpandArea(area, row, col);

            areas.Add(area);
        }

        private static void ExpandArea(Area area, int row, int col)
        {
            if (!IsValidCell(row, col)
                || matrix[row][col] == Wall
                || matrix[row][col] == VisitedCell)
            {
                return;
            }

            matrix[row][col] = VisitedCell;
            area.Size++;

            // Expand Area (horizontally & vertically)
            ExpandArea(area, row - 1, col);
            ExpandArea(area, row + 1, col);
            ExpandArea(area, row, col - 1);
            ExpandArea(area, row, col + 1);
        }

        private static bool IsValidCell(int row, int col)
            => 0 <= row && row < matrix.Length
            && 0 <= col && col < matrix[row].Length;

        private static void Print()
        {
            Console.WriteLine($"Total areas found: {areas.Count}");

            var areasFound = 0;
            foreach (var area in areas)
            {
                Console.WriteLine($"Area #{++areasFound} at ({area.Row}, {area.Col}), size: {area.Size}");
            }
        }

        private static void ReadMatrix()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());
            matrix = new char[rows][];

            for (int row = 0; row < rows; row++)
            {
                matrix[row] = Console.ReadLine().ToCharArray();
            }
        }

        private class Area : IComparable<Area>
        {
            public Area(int row, int col)
            {
                this.Row = row;
                this.Col = col;
            }

            public int Row { get; }

            public int Col { get; }

            public int Size { get; set; } = 0;

            public int CompareTo(Area other)
            {
                var compare = other.Size.CompareTo(this.Size); // size DESC

                if (compare == 0)
                {
                    compare = this.Row.CompareTo(other.Row); // row ASC
                }

                if (compare == 0)
                {
                    compare = this.Col.CompareTo(other.Col); // col ASC
                }

                return compare;
            }
        }
    }
}
