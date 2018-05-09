namespace ShortestPath
{
    using System;
    using System.Collections.Generic;

    public class ShortestPath
    {
        private static readonly char[] directions = new[] { 'L', 'R', 'S' };
        private static readonly List<int> missingIndices = new List<int>();
        private static readonly List<string> paths = new List<string>();

        public static void Main()
        {
            var path = Console.ReadLine().ToCharArray();

            FindMissingIndices(path);
            GeneratePath(0, path);

            Print();
        }

        private static void Print()
        {
            Console.WriteLine(paths.Count);
            paths.ForEach(Console.WriteLine);
        }

        private static void GeneratePath(int index, char[] path)
        {
            if (index >= missingIndices.Count)
            {
                paths.Add(new string(path));
                return;
            }

            foreach (var direction in directions)
            {
                path[missingIndices[index]] = direction;
                GeneratePath(index + 1, path);
            }
        }

        private static void FindMissingIndices(char[] path)
        {
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] == '*')
                {
                    missingIndices.Add(i);
                }
            }
        }
    }
}
