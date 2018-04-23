namespace MostReliablePath
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class MostReliablePath
    {
        private class Edge
        {
            public int First { get; set; }

            public int Second { get; set; }

            public int Weight { get; set; } // reliability
        }

        private static Dictionary<int, List<Edge>> graph = new Dictionary<int, List<Edge>>();
        private static int startNode;
        private static int endNode;
        private static double[] reliability;
        private static int[] prev;

        public static void Main()
        {
            ReadInput();

            DijsktraMostReliablePath();

            var path = ReconstructPath();

            Console.WriteLine($"Most reliable path reliability: {reliability[endNode]:f2}%");
            Console.WriteLine(string.Join(" -> ", path));
        }

        private static Stack<int> ReconstructPath()
        {
            var path = new Stack<int>();
            var currentNode = endNode;

            while (currentNode != -1)
            {
                path.Push(currentNode);
                currentNode = prev[currentNode];
            }

            return path;
        }

        private static void DijsktraMostReliablePath()
        {
            reliability = Enumerable.Repeat<double>(-1, graph.Count).ToArray();
            reliability[startNode] = 100;

            var visited = new bool[graph.Count];
            visited[startNode] = true;

            prev = new int[graph.Count];
            prev[startNode] = -1;

            var queue = new OrderedBag<int>(
                Comparer<int>.Create((a, b) => (int)(reliability[b] - reliability[a]))); // max reliability
            queue.Add(startNode);

            while (queue.Any())
            {
                var maxNode = queue.RemoveFirst();

                if (reliability[maxNode] == -1) // no path
                {
                    break;
                }

                foreach (var edge in graph[maxNode])
                {
                    var otherNode = edge.First == maxNode
                        ? edge.Second
                        : edge.First;

                    if (!visited[otherNode])
                    {
                        visited[otherNode] = true;
                        queue.Add(otherNode);
                    }

                    // Improve realiability
                    var newReliability = reliability[maxNode] * edge.Weight / 100;
                    if (newReliability > reliability[otherNode])
                    {
                        reliability[otherNode] = newReliability;

                        prev[otherNode] = maxNode;

                        // Reorder bag
                        queue = new OrderedBag<int>(
                            queue,
                            Comparer<int>.Create((a, b) => (int)(reliability[b] - reliability[a])));
                    }
                }
            }
        }

        private static void ReadInput()
        {
            var nodes = int.Parse(Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);
            var pathTokens = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            startNode = int.Parse(pathTokens[1]);
            endNode = int.Parse(pathTokens[3]);

            var edges = int.Parse(Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);

            for (int i = 0; i < edges; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var edge = new Edge
                {
                    First = tokens[0],
                    Second = tokens[1],
                    Weight = tokens[2]
                };

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
            }
        }
    }
}
