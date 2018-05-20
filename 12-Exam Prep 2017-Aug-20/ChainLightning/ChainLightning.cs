namespace ChainLightning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class ChainLightning
    {
        private static List<Edge>[] graph;
        private static HashSet<int> visited = new HashSet<int>();
        private static int[] damages;
        private static List<Edge> minSpanningTree = new List<Edge>();

        public static void Main()
        {
            var nodes = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());
            var lightnings = int.Parse(Console.ReadLine());

            BuildGraph(nodes, edges);

            FindMinSpanningTree();

            CalcDamages(lightnings);

            Console.WriteLine(damages.Max());

            // TODO 60/100
        }

        private static void CalcDamages(int lightnings)
        {
            damages = new int[graph.Length];

            for (int i = 0; i < lightnings; i++)
            {
                var tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var node = tokens[0];
                var damage = tokens[1];

                CalcDamageDFS(node, node, damage);
            }
        }

        private static void CalcDamageDFS(int node, int parent, int damage)
        {
            damages[node] += damage;

            var closestChildren = graph[node]
                .Where(e => minSpanningTree.Contains(e)) // min weight
                .Select(e => e.Start == node ? e.End : e.Start)
                .ToArray();

            foreach (var child in closestChildren)
            {
                if (child != parent)
                {
                    CalcDamageDFS(child, node, damage / 2);
                }
            }
        }

        private static void FindMinSpanningTree()
        {
            for (int node = 0; node < graph.Length; node++)
            {
                Prim(node);
            }
        }

        private static void Prim(int startingNode)
        {
            visited.Add(startingNode);

            var priorityQueue = new OrderedBag<Edge>(
                Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));

            priorityQueue.AddMany(graph[startingNode]);

            while (priorityQueue.Any())
            {
                var minEdge = priorityQueue.GetFirst();
                priorityQueue.Remove(minEdge);

                // Connect a non-tree node to the spanning tree, avoiding a cycle
                var nonTreeNode = -1;

                if (visited.Contains(minEdge.Start)
                    && !visited.Contains(minEdge.End))
                {
                    nonTreeNode = minEdge.End;
                }

                if (!visited.Contains(minEdge.Start)
                   && visited.Contains(minEdge.End))
                {
                    nonTreeNode = minEdge.Start;
                }

                if (nonTreeNode == -1)
                {
                    continue;
                }

                // Connect non-tree node & spanning tree
                minSpanningTree.Add(minEdge);

                visited.Add(nonTreeNode);
                priorityQueue.AddMany(graph[nonTreeNode]);
            }
        }

        private static void BuildGraph(int nodes, int edges)
        {
            // Initialize
            graph = new List<Edge>[nodes];
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<Edge>();
            }

            for (int i = 0; i < edges; i++)
            {
                var tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();

                var start = tokens[0];
                var end = tokens[1];
                var weight = tokens[2];

                var edge = new Edge(start, end, weight);

                graph[start].Add(edge);
                graph[end].Add(edge);
            }
        }

        private class Edge
        {
            public Edge(int start, int end, int weight)
            {
                this.Start = start;
                this.End = end;
                this.Weight = weight;
            }

            public int Start { get; }

            public int End { get; }

            public int Weight { get; }
        }
    }
}
