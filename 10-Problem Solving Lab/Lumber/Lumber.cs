namespace Lumber
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Lumber
    {
        public static void Main()
        {
            var inputTokens = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var logsCount = inputTokens[0];
            var queriesCount = inputTokens[1];

            var logs = new List<Log>();
            var graph = new List<int>[logsCount + 1]; // log => intersecting logs

            ReadLogs(logsCount, logs, graph);

            var components = AssignNodesToComponents(logsCount, graph); // node (log) => component

            RunQueries(queriesCount, components);
        }

        private static void RunQueries(int queriesCount, int[] components)
        {
            for (int i = 0; i < queriesCount; i++)
            {
                var queryTokens = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var startLog = queryTokens[0];
                var endLog = queryTokens[1];

                Console.WriteLine(
                    components[startLog] == components[endLog] // same component
                    ? "YES"
                    : "NO");
            }
        }

        private static int[] AssignNodesToComponents(int logsCount, List<int>[] graph)
        {
            var visited = new bool[logsCount + 1];
            var components = new int[logsCount + 1]; // node (logId) => componentId
            var componentId = 0;

            for (int node = 1; node <= logsCount; node++)
            {
                if (!visited[node])
                {
                    DfsTraversal(node, visited, graph, components, componentId);
                    componentId++;
                }
            }

            return components;
        }

        private static void DfsTraversal(int node, bool[] visited, List<int>[] graph, int[] components, int componentId)
        {
            visited[node] = true;
            components[node] = componentId;

            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    DfsTraversal(child, visited, graph, components, componentId);
                }
            }
        }

        private static void ReadLogs(int logsCount, List<Log> logs, List<int>[] graph)
        {
            for (int i = 1; i <= logsCount; i++)
            {
                var logTokens = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var newLog = new Log(i, logTokens[0], logTokens[1], logTokens[2], logTokens[3]);

                // Mark intersecting logs
                graph[newLog.Id] = new List<int>();

                foreach (var prevLog in logs)
                {
                    if (prevLog.Intersects(newLog))
                    {
                        graph[prevLog.Id].Add(newLog.Id);
                        graph[newLog.Id].Add(prevLog.Id);
                    }
                }

                logs.Add(newLog);
            }
        }

        private class Log
        {
            public Log(int id, int x1, int y1, int x2, int y2)
            {
                this.Id = id;
                this.X1 = x1; // min x
                this.Y1 = y1;
                this.X2 = x2;
                this.Y2 = y2; // min y
            }

            public int Id { get; }
            public int X1 { get; }
            public int Y1 { get; }
            public int X2 { get; }
            public int Y2 { get; }

            public bool Intersects(Log other)
                => this.X1 <= other.X2 && this.X2 >= other.X1
                && this.Y1 >= other.Y2 && this.Y2 <= other.Y1;
        }
    }
}
