namespace TourDeSofia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TourDeSofia
    {
        private const int Infinity = int.MaxValue;

        private static HashSet<int>[] graph;
        private static int[] distances;
        private static bool[] visited;

        public static void Main()
        {
            var nodes = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());
            var root = int.Parse(Console.ReadLine());

            // Initialize
            graph = new HashSet<int>[nodes];
            distances = new int[nodes];
            visited = new bool[nodes];

            BuildGraph(edges);

            FindDistancesFromRootBFS(root);

            int minDistance = FindMinDistanceToRoot(root);

            Console.WriteLine(minDistance < Infinity
                ? minDistance
                : visited.Count(x => x)); // reachable nodes
        }

        private static int FindMinDistanceToRoot(int root)
        {
            var minDistance = Infinity;

            for (int node = 0; node < graph.Length; node++)
            {
                var currentDistance = distances[node] + 1;

                if (visited[node]
                    && graph[node].Contains(root)   // path to root exists
                    && currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                }
            }

            return minDistance;
        }

        private static void FindDistancesFromRootBFS(int root)
        {
            var queue = new Queue<int>();
            queue.Enqueue(root);
            distances[root] = 0;
            visited[root] = true;

            while (queue.Any())
            {
                var node = queue.Dequeue();

                foreach (var child in graph[node])
                {
                    if (!visited[child])
                    {
                        visited[child] = true;
                        queue.Enqueue(child);

                        distances[child] = distances[node] + 1;
                    }
                }
            }
        }

        private static void BuildGraph(int edges)
        {
            // Initialize
            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new HashSet<int>();
            }

            for (int i = 0; i < edges; i++)
            {
                var tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();

                var from = tokens[0];
                var to = tokens[1];

                graph[from].Add(to);
            }
        }
    }
}
