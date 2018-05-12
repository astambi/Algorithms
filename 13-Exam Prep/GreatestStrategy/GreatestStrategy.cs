namespace GreatestStrategy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GreatestStrategy
    {
        private static Dictionary<int, HashSet<int>> graph = new Dictionary<int, HashSet<int>>();
        private static Dictionary<int, HashSet<int>> modified = new Dictionary<int, HashSet<int>>();

        public static void Main()
        {
            var tokens = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var nodesCount = tokens[0];
            var edgesCount = tokens[1];
            var root = tokens[2];

            BuildGraph(nodesCount, edgesCount);

            SplitTreeDFS(root, 0, new HashSet<int>());

            var maxValue = GetMaxValue();
            Console.WriteLine(maxValue);
        }

        private static int GetMaxValue()
        {
            var maxValue = 0;
            var visited = new HashSet<int>();

            foreach (var node in modified.Keys)
            {
                if (!visited.Contains(node))
                {
                    var value = GetValueDFS(node, visited);
                    if (value > maxValue)
                    {
                        maxValue = value;
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

        private static int SplitTreeDFS(int node, int parent, HashSet<int> visited)
        {
            visited.Add(node);
            var totalNodes = 1;

            foreach (var child in graph[node])
            {
                if (!visited.Contains(child)
                    && child != parent)
                {
                    var subTreeNodes = SplitTreeDFS(child, node, visited);
                    totalNodes += subTreeNodes;

                    // Disconnect subtree of even nodes
                    if (subTreeNodes % 2 == 0)
                    {
                        modified[node].Remove(child);
                        modified[child].Remove(node);
                    }
                }
            }

            return totalNodes;
        }

        private static void BuildGraph(int nodesCount, int edgesCount)
        {
            // Initialize 
            for (int node = 1; node <= nodesCount; node++)
            {
                graph[node] = new HashSet<int>();
                modified[node] = new HashSet<int>();
            }

            // Read input
            for (int i = 0; i < edgesCount; i++)
            {
                var edgeTokens = Console.ReadLine()
                     .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(int.Parse)
                     .ToArray();

                var start = edgeTokens[0];
                var end = edgeTokens[1];

                graph[start].Add(end);
                graph[end].Add(start);

                modified[start].Add(end);
                modified[end].Add(start);
            }
        }
    }
}
