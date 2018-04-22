namespace Demo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Demo
    {
        private static List<int>[] graph;
        private static List<int>[] directedGraph;
        private static bool[] visited;

        public static void Main(string[] args)
        {
            graph = new List<int>[] // node => List<int>
            {
                new List<int> {3, 6},           // 0
                new List<int> {2, 3, 4, 5, 6},  // 1
                new List<int> {1, 4, 5},        // 2
                new List<int> {0, 1, 5},        // 3
                new List<int> {1, 2, 6},        // 4
                new List<int> {1, 2, 3},        // 5
                new List<int> {0, 1, 4},        // 6
                new List<int> {8},              // 7
                new List<int> {7}               // 8
            };

            visited = new bool[graph.Length];

            //TraversingGraphs();
            //FindConnectedComponents();

            //// Topological Sorting
            directedGraph = new List<int>[]
            {
                new List<int> { 1, 2 },
                new List<int> { 3, 4 },
                new List<int> { 5 },
                new List<int> { 2, 5 },
                new List<int> { 3 },
                new List<int> { }
            };

            SourceRemovalAlgorithm();
        }

        private static void SourceRemovalAlgorithm()
        {


            var result = new List<int>();
            var nodes = new HashSet<int>(); // without incoming edges
            var nodesWithIncomingEdges = GetNodesWithIncomingEdges();

            // Find nodes without incoming edges
            for (int i = 0; i < directedGraph.Length; i++)
            {
                if (!nodesWithIncomingEdges.Contains(i))
                {
                    nodes.Add(i);
                }
            }

            while (nodes.Any())
            {
                var currentNode = nodes.First();
                nodes.Remove(currentNode);
                result.Add(currentNode);

                var children = directedGraph[currentNode].ToList();
                directedGraph[currentNode] = new List<int>();
                nodesWithIncomingEdges = GetNodesWithIncomingEdges();

                foreach (var child in children)
                {
                    if (!nodesWithIncomingEdges.Contains(child))
                    {
                        nodes.Add(child);
                    }
                }
            }

            if (directedGraph.SelectMany(x => x).Any())
            {
                Console.WriteLine("Error: graph has at least one cycle");
            }
            else
            {
                Console.WriteLine(string.Join(" ", result));
            }
        }

        private static HashSet<int> GetNodesWithIncomingEdges()
            => directedGraph.SelectMany(s => s).ToHashSet();

        private static void FindConnectedComponents()
        {
            var components = 0;

            for (int n = 0; n < graph.Length; n++)
            {
                if (!visited[n])
                {
                    Console.Write($"Connected component #{++components}: ");
                    DFS(n);
                    Console.WriteLine();
                }
            }
        }

        private static void TraversingGraphs()
        {
            for (int n = 0; n < graph.Length; n++)
            {
                DFS(n);
                //DFSIterative(n);
                //BFS(n);
            }
        }

        private static void BFS(int node)
        {
            if (visited[node])
            {
                return;
            }

            var queue = new Queue<int>();
            queue.Enqueue(node);
            visited[node] = true;

            while (queue.Any())
            {
                var currentNode = queue.Dequeue();
                Console.Write(currentNode + " ");

                foreach (var child in graph[currentNode])
                {
                    if (!visited[child])
                    {
                        queue.Enqueue(child);
                        visited[child] = true;
                    }
                }
            }
        }

        private static void DFSIterative(int node)
        {
            if (visited[node])
            {
                return;
            }

            var stack = new Stack<int>();
            stack.Push(node);
            visited[node] = true;

            while (stack.Any())
            {
                var currentNode = stack.Pop();
                Console.Write(currentNode + " ");

                foreach (var child in graph[currentNode])
                {
                    if (!visited[child])
                    {
                        stack.Push(child);
                        visited[child] = true;
                    }
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

                Console.Write(node + " ");
            }
        }
    }
}
