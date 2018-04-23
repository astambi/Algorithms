namespace Salaries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Salaries
    {
        private static List<int>[] graph; // manager => employees
        private static long[] salaries; // employee => salary
        private static HashSet<int> visited = new HashSet<int>();

        public static void Main()
        {
            ReadGraph();

            // Initialize salaries (initial salary 1, assuming no managed employees)
            salaries = Enumerable.Repeat<long>(1, graph.Length).ToArray();

            for (int employee = 0; employee < graph.Length; employee++)
            {
                if (!visited.Contains(employee))
                {
                    CalcSalaryDFS(employee);
                }
            }

            Console.WriteLine(salaries.Sum());
        }

        private static void CalcSalaryDFS(int manager)
        {
            visited.Add(manager);

            if (!graph[manager].Any()) // not a manager => unchanged salary
            {
                return;
            }

            long salaryManager = 0;

            foreach (var managedEmployee in graph[manager])
            {
                if (!visited.Contains(managedEmployee))
                {
                    CalcSalaryDFS(managedEmployee);
                }

                salaryManager += salaries[managedEmployee];
            }

            salaries[manager] = salaryManager;
        }

        private static void ReadGraph()
        {
            var employeesCount = int.Parse(Console.ReadLine());
            graph = new List<int>[employeesCount];

            for (int manager = 0; manager < graph.Length; manager++)
            {
                graph[manager] = new List<int>();
                var tokens = Console.ReadLine().ToCharArray();

                for (int employee = 0; employee < tokens.Length; employee++)
                {
                    if (tokens[employee] == 'Y')
                    {
                        graph[manager].Add(employee);
                    }
                }
            }
        }
    }
}
