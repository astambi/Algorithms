using System;
using System.Collections.Generic;
using System.Linq;

public class TopologicalSorter
{
    private Dictionary<string, List<string>> graph; // node => children
    private Dictionary<string, int> predecessorsCount; // node => predecessors

    public TopologicalSorter(Dictionary<string, List<string>> graph)
    {
        this.graph = graph;
    }

    public ICollection<string> TopSort()
    {
        var sorted = new List<string>();
        this.GetPredecessorCount(this.graph);

        while (true)
        {
            var nodeWithoutPredecessors = this.predecessorsCount.Keys
                .Where(n => this.predecessorsCount[n] == 0)
                .FirstOrDefault();

            if (nodeWithoutPredecessors == null)
            {
                break;
            }

            var children = this.graph[nodeWithoutPredecessors];
            foreach (var child in children)
            {
                this.predecessorsCount[child]--;
            }

            this.graph.Remove(nodeWithoutPredecessors);
            this.predecessorsCount.Remove(nodeWithoutPredecessors);

            sorted.Add(nodeWithoutPredecessors);
        }

        if (this.graph.Any())
        {
            throw new InvalidOperationException();
        }

        return sorted;
    }

    private void GetPredecessorCount(Dictionary<string, List<string>> graph)
    {
        this.predecessorsCount = new Dictionary<string, int>();

        foreach (var nodeKvp in graph)
        {
            var node = nodeKvp.Key;
            var children = nodeKvp.Value;

            if (!this.predecessorsCount.ContainsKey(node))
            {
                this.predecessorsCount[node] = 0;
            }

            foreach (var child in children)
            {
                if (!this.predecessorsCount.ContainsKey(child))
                {
                    this.predecessorsCount[child] = 0;
                }

                this.predecessorsCount[child]++;
            }
        }
    }
}
