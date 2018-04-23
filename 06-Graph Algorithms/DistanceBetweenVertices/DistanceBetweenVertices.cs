namespace DistanceBetweenVertices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DistanceBetweenVertices
    {
        private static Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
        private static List<int[]> paths = new List<int[]>();

        public static void Main()
        {
            ReadInput();

            foreach (var path in paths)
            {
                var start = path[0];
                var end = path[1];

                var distance = FindShortestDistance(start, end);

                Console.WriteLine("{" + $"{start}, {end}" + "} -> " + distance);
            }
        }

        private static int FindShortestDistance(int start, int end)
        {
            // Initialize distances from start
            var distances = new Dictionary<int, int>();
            foreach (var node in graph.Keys)
            {
                distances[node] = -1; // unreachable
            }

            distances[start] = 0;

            // BFS traversal from start
            var queue = new Queue<int>();
            queue.Enqueue(start);

            while (queue.Any())
            {
                var node = queue.Dequeue();

                foreach (var child in graph[node])
                {
                    if (distances[child] == -1)
                    {
                        distances[child] = distances[node] + 1;
                        queue.Enqueue(child);

                        if (child == end)
                        {
                            break;
                        }
                    }
                }
            }

            return distances[end];
        }

        private static void ReadInput()
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var pairsCount = int.Parse(Console.ReadLine());

            // Read Edges
            for (int i = 0; i < nodesCount; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var node = int.Parse(tokens[0]);
                if (!graph.ContainsKey(node))
                {
                    graph[node] = new List<int>();
                }

                foreach (var token in tokens.Skip(1))
                {
                    var otherNode = int.Parse(token);
                    graph[node].Add(otherNode);
                }
            }

            // Read Pairs
            for (int i = 0; i < pairsCount; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                paths.Add(tokens);
            }
        }
    }
}
