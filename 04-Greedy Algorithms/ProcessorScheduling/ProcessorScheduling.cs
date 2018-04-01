namespace ProcessorScheduling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProcessorScheduling
    {
        private class Task
        {
            public Task(int id, int value, int deadline)
            {
                this.Id = id;
                this.Value = value;
                this.Deadline = deadline;
            }

            public int Id { get; }

            public int Value { get; }

            public int Deadline { get; }
        }

        public static void Main()
        {
            var tasks = ReadTasks();

            var schedule = new List<Task>();
            foreach (var task in tasks)
            {
                if (CanCompleteSchedule(schedule, task))
                {
                    schedule.Add(task);
                }
            }

            Print(schedule);
        }

        private static void Print(List<Task> schedule)
        {
            schedule = schedule
                .OrderBy(t => t.Deadline)
                .ThenByDescending(t => t.Value)
                .ToList();

            Console.WriteLine($"Optimal schedule: {string.Join(" -> ", schedule.Select(t => t.Id))}");
            Console.WriteLine($"Total value: {schedule.Sum(t => t.Value)}");
        }

        private static bool CanCompleteSchedule(List<Task> schedule, Task task)
        {
            var testSchedule = new List<Task>(schedule) { task };
            testSchedule = testSchedule
                .OrderBy(t => t.Deadline)
                .ToList();

            for (int i = 0; i < testSchedule.Count; i++)
            {
                var time = i + 1;
                if (testSchedule[i].Deadline < time)
                {
                    return false;
                }
            }

            return true;
        }

        private static List<Task> ReadTasks()
        {
            var tasksCount = int.Parse(Console.ReadLine()
                .Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries)
                [1]);

            var tasks = new List<Task>();
            for (int i = 0; i < tasksCount; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var value = tokens[0];
                var deadline = tokens[1];

                tasks.Add(new Task(i + 1, value, deadline));
            }

            return tasks
                .OrderByDescending(t => t.Value) // best value DESC
                .ToList();
        }
    }
}
