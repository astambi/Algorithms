namespace CyclesInGraph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CyclesInGraph
    {
        private static Dictionary<char, List<char>> graph = new Dictionary<char, List<char>>();
        private static bool isAcyclic = true;

        public static void Main(string[] args)
        {
            ReadGraph();

            foreach (var startNode in graph.Keys)
            {
                foreach (var endNode in graph[startNode].ToList())
                {
                    // Remove edge between nodes
                    graph[startNode].Remove(endNode);
                    graph[endNode].Remove(startNode);

                    if (HasPath(startNode, endNode)) // cycle
                    {
                        isAcyclic = false;
                        break;
                    }
                    else
                    {
                        // Restore edge
                        graph[startNode].Add(endNode);
                        graph[endNode].Add(startNode);
                    }
                }

                if (!isAcyclic)
                {
                    break;
                }
            }

            Console.WriteLine($"Acyclic: {(isAcyclic ? "Yes" : "No")}");
        }

        private static bool HasPath(char start, char end)
        {
            var queue = new Queue<char>();
            var visited = new HashSet<char>();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Any())
            {
                var node = queue.Dequeue();

                foreach (var child in graph[node])
                {
                    if (!visited.Contains(child))
                    {
                        visited.Add(child);
                        queue.Enqueue(child);

                        if (child == end)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static void ReadGraph()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                var tokens = input.ToCharArray();
                var first = tokens[0];
                var second = tokens[2];

                if (!graph.ContainsKey(first))
                {
                    graph[first] = new List<char>();
                }

                if (!graph.ContainsKey(second))
                {
                    graph[second] = new List<char>();
                }

                graph[first].Add(second);
                graph[second].Add(first);
            }
        }
    }
}
