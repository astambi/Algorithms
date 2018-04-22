using System.Collections.Generic;
using System.Linq;

public class StronglyConnectedComponents // Kosaraju-Sharir Algorithm
{
    private static List<int>[] graph;
    private static List<int>[] reverseGraph;
    private static bool[] visited;
    private static Stack<int> nodesStack = new Stack<int>();

    private static List<List<int>> stronglyConnectedComponents;

    public static List<List<int>> FindStronglyConnectedComponents(List<int>[] targetGraph)
    {
        stronglyConnectedComponents = new List<List<int>>();
        graph = targetGraph;

        BuildReverseGraph();

        // Build nodes stack (starting node on top of stack)
        visited = new bool[graph.Length];

        for (int node = 0; node < graph.Length; node++)
        {
            if (!visited[node])
            {
                DFS(node);
            }
        }

        // Build strongly-connected components
        visited = new bool[graph.Length];

        while (nodesStack.Count > 0)
        {
            var node = nodesStack.Pop();

            if (!visited[node])
            {
                stronglyConnectedComponents.Add(new List<int>());

                ReverseDFS(node);
            }
        }

        return stronglyConnectedComponents;
    }

    private static void ReverseDFS(int node)
    {
        if (!visited[node])
        {
            visited[node] = true;
            stronglyConnectedComponents.Last().Add(node);

            foreach (var child in reverseGraph[node])
            {
                ReverseDFS(child);
            }
        }
    }

    private static void DFS(int node)
    {
        if (!visited[node])
        {
            visited[node] = true;

            foreach (var child in graph[node])
            {
                DFS(child);
            }

            nodesStack.Push(node);
        }
    }

    private static void BuildReverseGraph()
    {
        reverseGraph = new List<int>[graph.Length];

        for (int node = 0; node < reverseGraph.Length; node++)
        {
            reverseGraph[node] = new List<int>();
        }

        for (int node = 0; node < graph.Length; node++)
        {
            foreach (var child in graph[node])
            {
                reverseGraph[child].Add(node);
            }
        }
    }
}
