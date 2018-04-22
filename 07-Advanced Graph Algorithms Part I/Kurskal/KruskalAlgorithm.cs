//namespace Kurskal
//{
    using System.Collections.Generic;
    using System.Linq;

    public class KruskalAlgorithm
    {
        public static List<Edge> Kruskal(int numberOfVertices, List<Edge> edges)
        {
            edges.Sort();

            var nodes = edges
               .Select(e => e.StartNode)
               .Union(edges.Select(e => e.EndNode))
               .Distinct()
               .ToList();

            var parents = InitializeForestOfUnconnectedNodes(nodes);

            var minSpanningTree = new List<Edge>();

            foreach (var minEdge in edges)
            {
                var rootStartNode = FindRoot(minEdge.StartNode, parents);
                var rootEndNode = FindRoot(minEdge.EndNode, parents);

                if (rootStartNode != rootEndNode) // prevent cycles in spanning tree
                {
                    minSpanningTree.Add(minEdge);
                    parents[rootStartNode] = rootEndNode;
                }
            }

            return minSpanningTree;
        }

        public static int FindRoot(int node, int[] parents)
        {
            var root = node;
            while (parents[root] != root)
            {
                root = parents[root];
            }

            //Path compression optimization
            while (node != root)
            {
                var oldParent = parents[node];
                parents[node] = root;
                node = oldParent;
            }

            return root;
        }

        private static int[] InitializeForestOfUnconnectedNodes(List<int> nodes)
        {
            var parents = new int[nodes.Max() + 1];

            foreach (var node in nodes)
            {
                parents[node] = node;
            }

            return parents;
        }
    }
//}
