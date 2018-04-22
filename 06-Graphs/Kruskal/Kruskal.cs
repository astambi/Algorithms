namespace Kruskal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Kruskal
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

        private static int[] parents;

        public static void Main()
        {
            var graphEdges = new List<Edge>
            {
                new Edge(0, 3, 9),
                new Edge(0, 5, 4),
                new Edge(0, 8, 5),
                new Edge(1, 4, 8),
                new Edge(1, 7, 7),
                new Edge(2, 6, 12),
                new Edge(3, 5, 2),
                new Edge(3, 6, 8),
                new Edge(3, 8, 20),
                new Edge(4, 7, 10),
                new Edge(6, 8, 7)
            };

            graphEdges.Sort(); // NB. SortedSet does not work!

            var nodes = graphEdges
                .Select(e => e.StartNode)
                .Union(graphEdges.Select(e => e.EndNode))
                .Distinct()
                .ToHashSet();

            InitializeForestOfUnconnectedNodes(nodes);

            var minSpanningTree = FindMinSpanningForest(graphEdges);

            Print(minSpanningTree);
        }

        private static void Print(List<Edge> minSpanningTree)
        {
            Console.WriteLine("Minimum spanning forest weight: " +
                minSpanningTree.Sum(e => e.Weight));

            minSpanningTree
                .OrderBy(e => e.Weight)
                .ToList()
                .ForEach(e => Console.WriteLine(e));
        }

        private static List<Edge> FindMinSpanningForest(List<Edge> graphEdges)
        {
            var minSpanningTree = new List<Edge>();

            foreach (var edge in graphEdges) // min weight edge
            {
                var rootStartNode = FindRoot(edge.StartNode);
                var rootEndNode = FindRoot(edge.EndNode);

                if (rootStartNode != rootEndNode) // different trees
                {
                    minSpanningTree.Add(edge);
                    parents[rootStartNode] = rootEndNode; // join trees
                }
            }

            return minSpanningTree;
        }

        private static int FindRoot(int node)
        {
            var root = node;
            while (parents[root] != root)
            {
                root = parents[root];
            }

            // Path compression optimization
            while (node != root)
            {
                var oldParent = parents[node];
                parents[node] = root;
                node = oldParent;
            }

            return root;
        }

        private static void InitializeForestOfUnconnectedNodes(HashSet<int> nodes)
        {
            parents = new int[nodes.Max() + 1];

            foreach (var node in nodes)
            {
                parents[node] = node;
            }
        }
    }
}
