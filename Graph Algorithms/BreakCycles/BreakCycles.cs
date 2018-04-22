namespace BreakCycles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BreakCycles
    {
        private static SortedDictionary<char, List<char>> graph =
            new SortedDictionary<char, List<char>>();

        public static void Main()
        {
            ReadGraph();

            var edgesToRemove = new List<string>();

            foreach (var nodeKvp in graph)
            {
                var startNode = nodeKvp.Key;
                var sortedEndNodes = graph[startNode].OrderBy(e => e);

                foreach (var endNode in sortedEndNodes)
                {
                    // Remove edge
                    graph[startNode].Remove(endNode);
                    graph[endNode].Remove(startNode);

                    if (HasPath(startNode, endNode)) // cycle
                    {
                        edgesToRemove.Add($"{startNode} - {endNode}");
                    }
                    else
                    {
                        // Restore edge
                        graph[startNode].Add(endNode);
                        graph[endNode].Add(startNode);
                    }
                }
            }

            Print(edgesToRemove);
        }

        private static void Print(List<string> edgesToRemove)
        {
            Console.WriteLine($"Edges to remove: {edgesToRemove.Count}");
            edgesToRemove.ForEach(Console.WriteLine);
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

                var tokens = input
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var node = tokens[0][0];
                var otherNodes = tokens
                    .Skip(2)
                    .Select(x => x[0])
                    .ToList();

                if (!graph.ContainsKey(node))
                {
                    graph[node] = new List<char>();
                }

                graph[node].AddRange(otherNodes);
            }
        }
    }
}
