namespace CableNetwork
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class CableNetwork
    {
        private class Edge
        {
            public int First { get; set; }

            public int Second { get; set; }

            public int Cost { get; set; }
        }

        private static Dictionary<int, List<Edge>> graph = new Dictionary<int, List<Edge>>();
        private static HashSet<int> spanningTree = new HashSet<int>();
        private static int availableBudget;
        private static int usedBudget;

        public static void Main()
        {
            ReadInput();

            Prim();

            Console.WriteLine($"Budget used: {usedBudget}");
        }

        private static void Prim()
        {
            var queue = new OrderedBag<Edge>(
                Comparer<Edge>.Create((a, b) => a.Cost - b.Cost));

            queue.AddMany(spanningTree.SelectMany(n => graph[n])); // starting nodes existing network

            while (queue.Any())
            {
                var minEdge = queue.RemoveFirst();
                var nonTreeNode = -1;

                if (spanningTree.Contains(minEdge.First)
                    && !spanningTree.Contains(minEdge.Second))
                {
                    nonTreeNode = minEdge.Second;
                }

                if (!spanningTree.Contains(minEdge.First)
                    && spanningTree.Contains(minEdge.Second))
                {
                    nonTreeNode = minEdge.First;
                }

                if (nonTreeNode == -1)
                {
                    continue;
                }

                if (availableBudget < minEdge.Cost)
                {
                    break;
                }

                spanningTree.Add(nonTreeNode);
                queue.AddMany(graph[nonTreeNode]);

                availableBudget -= minEdge.Cost;
                usedBudget += minEdge.Cost;
            }
        }

        private static void ReadInput()
        {
            availableBudget = int.Parse(Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);
            var nodesCount = int.Parse(Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);
            var edgesCount = int.Parse(Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);

            for (int i = 0; i < edgesCount; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var edge = new Edge
                {
                    First = int.Parse(tokens[0]),
                    Second = int.Parse(tokens[1]),
                    Cost = int.Parse(tokens[2])
                };

                // Build graph of all customers
                if (!graph.ContainsKey(edge.First))
                {
                    graph[edge.First] = new List<Edge>();
                }

                if (!graph.ContainsKey(edge.Second))
                {
                    graph[edge.Second] = new List<Edge>();
                }

                graph[edge.First].Add(edge);
                graph[edge.Second].Add(edge);

                // Build existing network
                if (tokens.Length > 3)
                {
                    spanningTree.Add(edge.First);
                    spanningTree.Add(edge.Second);
                }
            }
        }
    }
}
