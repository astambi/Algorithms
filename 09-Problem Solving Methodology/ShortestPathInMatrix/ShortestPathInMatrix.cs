namespace ShortestPathInMatrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class ShortestPathInMatrix
    {
        private const int Infinity = int.MaxValue;

        private static int[][] matrix;
        private static int[][] distances;
        private static Node[][] prevNodes;

        public static void Main()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            ReadMatrix(rows);

            CalcShortestPathsDijkstra();

            ReconstructPath(rows - 1, cols - 1);
        }

        private static void ReconstructPath(int row, int col)
        {
            var destinationNode = new Node(row, col);
            if (distances[row][col] == Infinity)
            {
                return; // no path
            }

            var path = new Stack<int>();
            while (destinationNode != null)
            {
                path.Push(matrix[destinationNode.Row][destinationNode.Col]);
                destinationNode = prevNodes[destinationNode.Row][destinationNode.Col];
            }

            Console.WriteLine($"Length: {path.Sum()}");
            Console.WriteLine($"Path: {string.Join(" ", path)}");
        }

        private static void CalcShortestPathsDijkstra()
        {
            InitializeDistances(); // infinity
            InitializePrevNodes(); // null

            var priorityQueue = new OrderedBag<Node>(
                Comparer<Node>.Create((a, b) =>
                {
                    var compare = distances[a.Row][a.Col] - distances[b.Row][b.Col]; // min distance
                    if (compare == 0) { compare = a.Row - b.Row; } // top to bottom
                    if (compare == 0) { compare = a.Col - b.Col; } // left to right
                    return compare;
                }));

            priorityQueue.Add(new Node(0, 0)); // starting node

            while (priorityQueue.Any())
            {
                var minNode = priorityQueue.RemoveFirst();

                var minNodeDistance = distances[minNode.Row][minNode.Col];
                if (minNodeDistance == Infinity)
                {
                    break; // all nodes traversed
                }

                var neighbours = GetNeighbours(minNode.Row, minNode.Col);

                foreach (var neighbour in neighbours)
                {
                    // Enqueque unvisited nodes
                    var neighbourDistance = distances[neighbour.Row][neighbour.Col];
                    if (neighbourDistance == Infinity)
                    {
                        priorityQueue.Add(neighbour);
                    }

                    // Improve distance from minNode to neighbour
                    var newDistance = minNodeDistance + matrix[neighbour.Row][neighbour.Col];
                    if (newDistance < neighbourDistance)
                    {
                        distances[neighbour.Row][neighbour.Col] = newDistance;
                        prevNodes[neighbour.Row][neighbour.Col] = minNode;

                        // Reorder Queue
                        priorityQueue = new OrderedBag<Node>(priorityQueue,
                            Comparer<Node>.Create((a, b) =>
                            {
                                var compare = distances[a.Row][a.Col] - distances[b.Row][b.Col];
                                if (compare == 0) { compare = a.Row - b.Row; }
                                if (compare == 0) { compare = a.Col - b.Col; }
                                return compare;
                            }));
                    }
                }
            }
        }

        private static void InitializePrevNodes()
        {
            prevNodes = new Node[matrix.Length][];
            for (int row = 0; row < matrix.Length; row++)
            {
                prevNodes[row] = new Node[matrix[row].Length]; // null => no prev node
            }
        }

        private static void InitializeDistances()
        {
            distances = new int[matrix.Length][];
            for (int row = 0; row < matrix.Length; row++)
            {
                distances[row] = Enumerable.Repeat(Infinity, matrix[row].Length).ToArray();
            }

            distances[0][0] = 0; // starting node
        }

        private static List<Node> GetNeighbours(int row, int col)
        {
            var neighbours = new List<Node>();

            AddNeighbour(row, col - 1, neighbours);
            AddNeighbour(row, col + 1, neighbours);
            AddNeighbour(row - 1, col, neighbours);
            AddNeighbour(row + 1, col, neighbours);

            return neighbours;
        }

        private static void AddNeighbour(int row, int col, List<Node> neighbours)
        {
            if (IsValidCell(row, col))
            {
                neighbours.Add(new Node(row, col));
            }
        }

        private static bool IsValidCell(int row, int col)
            => row >= 0 && row < matrix.Length
            && col >= 0 && col < matrix[row].Length;

        private static void ReadMatrix(int rows)
        {
            matrix = new int[rows][];
            for (int row = 0; row < rows; row++)
            {
                matrix[row] = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
            }
        }

        private class Node
        {
            public Node(int row, int col)
            {
                this.Row = row;
                this.Col = col;
            }

            public int Row { get; }

            public int Col { get; }
        }
    }
}
