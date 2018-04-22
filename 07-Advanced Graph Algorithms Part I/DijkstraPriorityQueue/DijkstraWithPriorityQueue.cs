//namespace Dijkstra
//{
using System.Collections.Generic;

public static class DijkstraWithPriorityQueue
{
    public static List<int> DijkstraAlgorithm(
        Dictionary<Node, Dictionary<Node, int>> graph,
        Node sourceNode, Node destinationNode)
    {
        var visitedNodes = new bool[graph.Count];

        var prevNodes = new int[graph.Count];
        for (int i = 0; i < prevNodes.Length; i++)
        {
            prevNodes[i] = -1; // no prev node
        }

        var priorityQueue = new PriorityQueue<Node>();
        foreach (var edge in graph)
        {
            var currentNode = edge.Key;
            currentNode.DistanceFromStart = double.PositiveInfinity;
        }

        sourceNode.DistanceFromStart = 0;
        priorityQueue.Enqueue(sourceNode);

        while (priorityQueue.Count != 0)
        {
            // Find nearest unvisited node from source
            var minNode = priorityQueue.ExtractMin();
            if (minNode.DistanceFromStart == double.PositiveInfinity)
            {
                break; // all nodes traversed
            }

            foreach (var edge in graph[minNode])
            {
                var connectedNode = edge.Key;

                // Enqueue unvisited connected nodes
                if (!visitedNodes[connectedNode.Id])
                {
                    priorityQueue.Enqueue(connectedNode);
                    visitedNodes[connectedNode.Id] = true;
                }

                // Improve distance from source to connected nodes
                var newDistance = minNode.DistanceFromStart + edge.Value;
                if (newDistance < connectedNode.DistanceFromStart)
                {
                    connectedNode.DistanceFromStart = newDistance;
                    prevNodes[connectedNode.Id] = minNode.Id;

                    // Reorder priority queue (by node min distance)
                    priorityQueue.DecreaseKey(connectedNode);
                }
            }
        }

        return ReconstructPath(destinationNode, prevNodes);
    }

    private static List<int> ReconstructPath(Node destinationNode, int[] prevNodes)
    {
        if (destinationNode.DistanceFromStart == double.PositiveInfinity)
        {
            return null; // no path from source to destination
        }

        var path = new List<int>();
        var destinationNodeIt = destinationNode.Id;

        while (destinationNodeIt != -1)
        {
            path.Add(destinationNodeIt);
            destinationNodeIt = prevNodes[destinationNodeIt];
        }

        path.Reverse();
        return path;
    }
}
//}
