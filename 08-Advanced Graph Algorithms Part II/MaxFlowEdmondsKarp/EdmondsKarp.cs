using System.Collections.Generic;
using System.Linq;

public class EdmondsKarp // Ford–Fulkerson Algorithm with BFS
{
    private static int[][] graph;
    private static int[] parents;

    public static int FindMaxFlow(int[][] targetGraph)
    {
        graph = targetGraph;
        parents = Enumerable.Repeat(-1, graph.Length).ToArray();

        var start = 0;
        var end = graph.Length - 1;
        var maxFlow = 0;

        while (FindAugmentedPathBFS(start, end))
        {
            // Calc path flow (min capacity)
            var pathFlow = int.MaxValue;

            var currentNode = end;
            while (currentNode != start)
            {
                var prevNode = parents[currentNode];
                var currentFlow = graph[prevNode][currentNode];

                if (currentFlow > 0
                    && currentFlow < pathFlow)
                {
                    pathFlow = currentFlow;
                }

                currentNode = prevNode;
            }

            maxFlow += pathFlow;

            // Update used & spare capacities
            currentNode = end;
            while (currentNode != start)
            {
                var prevNode = parents[currentNode];

                graph[prevNode][currentNode] -= pathFlow; // spare capacity
                graph[currentNode][prevNode] += pathFlow; // used capacity

                currentNode = prevNode;
            }
        }

        return maxFlow;
    }

    private static bool FindAugmentedPathBFS(int start, int end)
    {
        var visited = new bool[graph.Length]; // reset on each traversal
        var queue = new Queue<int>();

        visited[start] = true;
        queue.Enqueue(start);

        while (queue.Any())
        {
            var node = queue.Dequeue();

            for (int child = 0; child < graph[node].Length; child++)
            {
                if (graph[node][child] != 0
                    && !visited[child])
                {
                    queue.Enqueue(child);
                    visited[child] = true;
                    parents[child] = node;
                }
            }
        }

        return visited[end];
    }
}
