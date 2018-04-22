//namespace Dijkstra
//{
using System.Collections.Generic;

public static class DijkstraWithoutQueue
{
    public static List<int> DijkstraAlgorithm(int[,] graph, int sourceNode, int destinationNode)
    {
        var nodesCount = graph.GetLength(0);

        var distances = InitializeDistances(nodesCount);
        distances[sourceNode] = 0;

        var visitedNodes = new bool[nodesCount];

        var prevNodes = new int[nodesCount];
        for (int i = 0; i < prevNodes.Length; i++)
        {
            prevNodes[i] = -1; // no prev node
        }

        while (true)
        {
            // Find nearest unvisited node from source
            var minDistance = int.MaxValue;
            var minNode = 0;
            for (int node = 0; node < nodesCount; node++)
            {
                if (!visitedNodes[node]
                    && distances[node] < minDistance)
                {
                    minNode = node;
                    minDistance = distances[node];
                }
            }

            if (minDistance == int.MaxValue)
            {
                break; // all nodes traversed
            }

            visitedNodes[minNode] = true;
            ImproveDistanceToConnectedNodes(graph, distances, prevNodes, minNode);
        }

        return ReconstructPath(destinationNode, distances, prevNodes);
    }

    private static void ImproveDistanceToConnectedNodes(int[,] graph, int[] distances, int[] prevNodes, int minNode)
    {
        for (int current = 0; current < graph.GetLength(0); current++)
        {
            if (graph[minNode, current] > 0) // connected nodes
            {
                var newCurrentDistance = distances[minNode] + graph[minNode, current];
                if (newCurrentDistance < distances[current])
                {
                    distances[current] = newCurrentDistance;
                    prevNodes[current] = minNode;
                }
            }
        }
    }

    private static List<int> ReconstructPath(int destinationNode, int[] distances, int[] prevNodes)
    {
        if (distances[destinationNode] == int.MaxValue)
        {
            return null; // no path from source to destination
        }

        var path = new List<int>();
        while (destinationNode != -1)
        {
            path.Add(destinationNode);
            destinationNode = prevNodes[destinationNode];
        }

        path.Reverse();
        return path;
    }

    private static int[] InitializeDistances(int n)
    {
        var distances = new int[n];
        for (int i = 0; i < distances.Length; i++)
        {
            distances[i] = int.MaxValue;
        }

        return distances;
    }
}
//}
