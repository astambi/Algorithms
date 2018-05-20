namespace GreatestStrategy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GreatestStrategy
    {
        private static HashSet<int>[] graph;
        private static HashSet<int>[] modified;

        public static void Main()
        {
            var tokens = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var nodes = tokens[0];
            var connections = tokens[1];
            var root = tokens[2];

            BuildGraph(nodes, connections);

            SplitTreeDFS(root, new HashSet<int>());

            Console.WriteLine(GetMaxValue());
        }

        private static int GetMaxValue()
        {
            var visited = new HashSet<int>();
            var maxValue = 0;

            for (int node = 1; node < modified.Length; node++)
            {
                if (!visited.Contains(node))
                {
                    var currentValue = GetValueDFS(node, visited);
                    if (currentValue > maxValue)
                    {
                        maxValue = currentValue;
                    }
                }
            }

            return maxValue;
        }

        private static int GetValueDFS(int node, HashSet<int> visited)
        {
            visited.Add(node);
            var value = node;

            foreach (var child in modified[node])
            {
                if (!visited.Contains(child))
                {
                    value += GetValueDFS(child, visited);
                }
            }

            return value;
        }

        private static int SplitTreeDFS(int node, HashSet<int> visited)
        {
            visited.Add(node);
            var totalNodes = 1;

            foreach (var child in graph[node])
            {
                if (!visited.Contains(child))
                {
                    var subtreeNodes = SplitTreeDFS(child, visited);

                    totalNodes += subtreeNodes;

                    // Disconnect subtree of even nodes
                    if (subtreeNodes % 2 == 0)
                    {
                        modified[node].Remove(child);
                        modified[child].Remove(node);
                    }
                }
            }

            return totalNodes;
        }

        private static void BuildGraph(int nodes, int connections)
        {
            // Initialize
            graph = new HashSet<int>[nodes + 1];
            modified = new HashSet<int>[nodes + 1];

            for (int node = 1; node <= nodes; node++)
            {
                graph[node] = new HashSet<int>();
                modified[node] = new HashSet<int>();
            }

            // Read Input
            for (int connection = 0; connection < connections; connection++)
            {
                var tokens = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var start = tokens[0];
                var end = tokens[1];

                graph[start].Add(end);
                graph[end].Add(start);

                modified[start].Add(end);
                modified[end].Add(start);
            }
        }
    }
}
