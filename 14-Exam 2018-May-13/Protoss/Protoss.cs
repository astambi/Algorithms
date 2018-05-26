namespace Protoss
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Protoss
    {
        private static HashSet<int>[] graph;
        private static HashSet<int>[] hyperConnections;

        public static void Main()
        {
            var nodes = int.Parse(Console.ReadLine());
            InitializeGraph(nodes);

            ReadGraph();

            AddHyperConnections();

            var maxConnections = hyperConnections.Max(x => x.Count);
            Console.WriteLine(maxConnections);
        }

        private static void AddHyperConnections()
        {
            for (int node = 0; node < graph.Length; node++)
            {
                foreach (var directConnection in graph[node])
                {
                    foreach (var child in graph[directConnection])
                    {
                        if (child != node)
                        {
                            hyperConnections[node].Add(child);
                        }
                    }
                }
            }
        }

        private static void ReadGraph()
        {
            for (int node = 0; node < graph.Length; node++)
            {
                var tokens = Console.ReadLine().ToCharArray();

                for (int i = 0; i < tokens.Length; i++)
                {
                    if (tokens[i] == 'Y')
                    {
                        graph[node].Add(i);
                        hyperConnections[node].Add(i);
                    }
                }
            }
        }

        private static void InitializeGraph(int nodes)
        {
            graph = new HashSet<int>[nodes];
            hyperConnections = new HashSet<int>[nodes];

            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new HashSet<int>();
                hyperConnections[node] = new HashSet<int>();
            }
        }
    }
}
