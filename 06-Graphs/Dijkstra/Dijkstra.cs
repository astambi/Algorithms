namespace Dijkstra
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Dijkstra
    {
        private class Edge : IComparable<Edge>
        {
            public Edge(int startNode, int endNode, int weight)
            {
                this.StartNode = startNode;
                this.EndNode = endNode;
                this.Weight = weight;
            }

            public int StartNode { get; }

            public int EndNode { get; }

            public int Weight { get; }

            public int CompareTo(Edge other)
                => this.Weight.CompareTo(other.Weight);

            public override string ToString()
                => $"({this.StartNode} {this.EndNode}) -> {this.Weight}";
        }

        private static Dictionary<int, List<Edge>> nodeToEdges = new Dictionary<int, List<Edge>>();

        public static void Main()
        {
            var graphEdges = new List<Edge>
            {
                new Edge(0, 6, 10),
                new Edge(0, 8, 12),
                new Edge(6, 4, 17),
                new Edge(6, 5, 6),
                new Edge(8, 5, 3),
                new Edge(8, 2, 14),
                new Edge(5, 4, 5),
                new Edge(5, 11, 33),
                new Edge(4, 1, 20),
                new Edge(4, 11, 11),
                new Edge(2, 11, 9),
                new Edge(2, 7, 15),
                new Edge(7, 9, 3),
                new Edge(7, 1, 26),
                new Edge(7, 11, 20),
                new Edge(1, 11, 6),
                new Edge(1, 9, 5),
                new Edge(3, 10, 7)
            };

            var nodes = graphEdges
                .Select(e => e.StartNode)
                .Union(graphEdges.Select(e => e.EndNode))
                .Distinct()
                .OrderBy(e => e)
                .ToHashSet();

            InitializeNodeToEdges(graphEdges);

            var distances = InitializeDistances(nodes);

            var queue = new SortedSet<int>(
                Comparer<int>.Create((f, s) => distances[f] - distances[s]));

            var startNode = nodes.First();
            distances[startNode] = 0;
            queue.Add(startNode);

            var prevNodes = new int[nodes.Max() + 1];
            for (int i = 0; i < prevNodes.Length; i++)
            {
                prevNodes[i] = -1; // no prev node
            }

            while (queue.Any())
            {
                var minNode = queue.Min;
                queue.Remove(minNode);

                foreach (var edge in nodeToEdges[minNode])
                {
                    var edgeOtherNode = edge.StartNode == minNode
                        ? edge.EndNode
                        : edge.StartNode;

                    // Enqueue unvisited nodes
                    if (distances[edgeOtherNode] == int.MaxValue)
                    {
                        queue.Add(edgeOtherNode);
                    }

                    // Improve distance from minNode to otherNode
                    var newDistance = distances[minNode] + edge.Weight;
                    if (newDistance < distances[edgeOtherNode])
                    {
                        distances[edgeOtherNode] = newDistance;
                        prevNodes[edgeOtherNode] = minNode;

                        // Reorder queue by min distance
                        queue = new SortedSet<int>(
                            queue,
                            Comparer<int>.Create((f, s) => distances[f] - distances[s]));
                    }
                }
            }

            var destinationNode = nodes.Max();
            ReconstructPath(destinationNode, distances, prevNodes);
        }

        private static void ReconstructPath(int destinationNode, int[] distances, int[] prevNodes)
        {
            if (distances[destinationNode] == int.MaxValue)
            {
                Console.WriteLine("No path");
                return;
            }

            var path = new Stack<int>();
            while (destinationNode != -1)
            {
                path.Push(destinationNode);
                destinationNode = prevNodes[destinationNode];
            }

            Console.WriteLine(string.Join("->", path));
        }

        private static int[] InitializeDistances(HashSet<int> nodes)
        {
            var distances = new int[nodes.Max() + 1];

            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = int.MaxValue; // infinity
            }

            return distances;
        }

        private static void InitializeNodeToEdges(List<Edge> graphEdges)
        {
            foreach (var edge in graphEdges)
            {
                if (!nodeToEdges.ContainsKey(edge.StartNode))
                {
                    nodeToEdges[edge.StartNode] = new List<Edge>();
                }

                if (!nodeToEdges.ContainsKey(edge.EndNode))
                {
                    nodeToEdges[edge.EndNode] = new List<Edge>();
                }

                nodeToEdges[edge.StartNode].Add(edge);
                nodeToEdges[edge.EndNode].Add(edge);
            }
        }
    }
}
