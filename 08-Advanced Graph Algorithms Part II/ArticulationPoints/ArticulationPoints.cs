using System;
using System.Collections.Generic;

public class ArticulationPoints // Hopcroft-Tarjan Algorithm
{
    private static List<int>[] graph;
    private static bool[] visited;
    private static int[] depths;
    private static int[] lowpoints; // earliest reachable node via an alternative path
    private static int?[] parents;
    private static List<int> articulationPoints;

    public static List<int> FindArticulationPoints(List<int>[] targetGraph)
    {
        graph = targetGraph;

        visited = new bool[graph.Length];
        depths = new int[graph.Length];
        lowpoints = new int[graph.Length];
        parents = new int?[graph.Length];
        articulationPoints = new List<int>();

        for (int node = 0; node < graph.Length; node++)
        {
            if (!visited[node])
            {
                FindArticulationPointsDFS(node, 1);
            }
        }

        return articulationPoints;
    }

    private static void FindArticulationPointsDFS(int node, int depth)
    {
        visited[node] = true;
        depths[node] = depth;
        lowpoints[node] = depth;

        var childrenCount = 0;
        var isArticulationPoint = false;

        foreach (var child in graph[node])
        {
            if (!visited[child])
            {
                parents[child] = node;

                FindArticulationPointsDFS(child, depth + 1);
                childrenCount++;

                // No alternative path found from child to a prev node (other than via current node)
                if (lowpoints[child] >= depths[node])
                {
                    isArticulationPoint = true;
                }

                // Improve current lowpoint if a child's lowpoint is better
                lowpoints[node] = Math.Min(lowpoints[node], lowpoints[child]);
            }
            else if (child != parents[node]) // alternative path to a prev node (other than parent) via child
            {
                lowpoints[node] = Math.Min(lowpoints[node], depths[child]);
            }
        }

        if ((parents[node] == null && childrenCount > 1)        // root with min 2 unconnected children
            || (parents[node] != null && isArticulationPoint))  // non-root articulation point
        {
            articulationPoints.Add(node);
        }
    }
}
