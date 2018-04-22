namespace Prim
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class Prim
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
        private static HashSet<int> visitedNodes = new HashSet<int>();
        private static List<Edge> minSpanningTree = new List<Edge>();

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

            var nodes = graphEdges
                .Select(e => e.StartNode)
                .Union(graphEdges.Select(e => e.EndNode))
                .Distinct()
                .OrderBy(e => e)
                .ToHashSet();

            InitializeNodeToEdges(graphEdges);

            foreach (var node in nodes)
            {
                if (!visitedNodes.Contains(node))
                {
                    PrimAlgo(node);
                }
            }

            Print();
        }

        private static void Print()
        {
            Console.WriteLine("Minimum spanning forest weight: " +
                minSpanningTree.Sum(e => e.Weight));

            minSpanningTree
                .OrderBy(e => e.Weight)
                .ToList()
                .ForEach(e => Console.WriteLine(e));
        }

        private static void PrimAlgo(int startingNode)
        {
            visitedNodes.Add(startingNode);

            var priorityQueue = new OrderedBag<Edge>(
                Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));
            priorityQueue.AddMany(nodeToEdges[startingNode]);

            while (priorityQueue.Any())
            {
                var minEdge = priorityQueue.GetFirst();
                priorityQueue.Remove(minEdge);

                // Connect a non-tree node to the spanning tree, avoiding a cycle
                var nonTreeNode = -1;

                if (visitedNodes.Contains(minEdge.StartNode)
                    && !visitedNodes.Contains(minEdge.EndNode))
                {
                    nonTreeNode = minEdge.EndNode;
                }

                if (!visitedNodes.Contains(minEdge.StartNode)
                   && visitedNodes.Contains(minEdge.EndNode))
                {
                    nonTreeNode = minEdge.StartNode;
                }

                if (nonTreeNode == -1)
                {
                    continue;
                }

                // Connect non-tree node & spanning tree
                minSpanningTree.Add(minEdge);

                visitedNodes.Add(nonTreeNode);
                priorityQueue.AddMany(nodeToEdges[nonTreeNode]);
            }
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
