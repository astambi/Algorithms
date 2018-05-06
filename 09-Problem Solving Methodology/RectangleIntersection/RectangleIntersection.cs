namespace RectangleIntersection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RectangleIntersection
    {
        public static void Main()
        {
            var rectangles = ReadRectangles();

            var xCoordinates = rectangles
                .Select(r => r.MinX)
                .Union(rectangles.Select(r => r.MaxX))
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            // Calc intersecting rectangles at each X interval
            var xRect = GetIntersectingRectanglesByX(rectangles, xCoordinates);

            long totalArea = 0;
            for (int i = 0; i < xRect.Length; i++)
            {
                var currentXRect = xRect[i];
                if (currentXRect.Count < 2) // no X intersecting rectangles
                {
                    continue;
                }

                var yCoordinates = currentXRect
                    .Select(r => r.MinY)
                    .Union(currentXRect.Select(r => r.MaxY))
                    .Distinct()
                    .OrderBy(y => y)
                    .ToList();

                // Calc intersecting rectangles at each Y interval
                var intersectingCount = CalcIntersectingCountByY(currentXRect, yCoordinates);

                for (int j = 0; j < intersectingCount.Length; j++)
                {
                    if (intersectingCount[j] < 2) // no Y intersecting rectangles
                    {
                        continue;
                    }

                    var intersection = new Rectangle(
                        xCoordinates[i], xCoordinates[i + 1],
                        yCoordinates[j], yCoordinates[j + 1]);

                    totalArea += intersection.Area;
                }
            }

            Console.WriteLine(totalArea);
        }

        private static int[] CalcIntersectingCountByY(List<Rectangle> currentXRect, List<int> yCoordinates)
        {
            var intersectingCount = new int[yCoordinates.Count - 1];
            for (int y = 0; y < yCoordinates.Count; y++)
            {
                foreach (var rectangle in currentXRect)
                {
                    if (rectangle.MaxY > yCoordinates[y]
                        && rectangle.MinY < yCoordinates[y + 1]) // intersecting by Y
                    {
                        intersectingCount[y]++;
                    }
                }
            }

            return intersectingCount;
        }

        private static List<Rectangle>[] GetIntersectingRectanglesByX(Rectangle[] rectangles, List<int> xCoordinates)
        {
            var xRect = new List<Rectangle>[xCoordinates.Count - 1];
            for (int i = 0; i < xRect.Length; i++)
            {
                xRect[i] = new List<Rectangle>();

                foreach (var rectangle in rectangles)
                {
                    if (rectangle.MinX < xCoordinates[i + 1]
                        && rectangle.MaxX > xCoordinates[i]) // intersecting by X
                    {
                        xRect[i].Add(rectangle);
                    }
                }
            }

            return xRect;
        }

        private static Rectangle[] ReadRectangles()
        {
            var n = int.Parse(Console.ReadLine());
            var rectangles = new Rectangle[n];

            for (int i = 0; i < n; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var minX = tokens[0];
                var maxX = tokens[1];
                var minY = tokens[2];
                var maxY = tokens[3];

                rectangles[i] = new Rectangle(minX, maxX, minY, maxY);
            }

            return rectangles;
        }

        private class Rectangle
        {
            public Rectangle(int minX, int maxX, int minY, int maxY)
            {
                this.MinX = minX;
                this.MaxX = maxX;
                this.MinY = minY;
                this.MaxY = maxY;
            }

            public int MinX { get; }

            public int MaxX { get; }

            public int MinY { get; }

            public int MaxY { get; }

            public int Area
                => Math.Abs((this.MaxX - this.MinX) * (this.MaxY - this.MinY));
        }
    }
}
