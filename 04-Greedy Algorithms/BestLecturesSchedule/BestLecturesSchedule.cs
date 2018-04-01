namespace BestLecturesSchedule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BestLecturesSchedule
    {
        private class Lecture
        {
            public Lecture(string name, int start, int finish)
            {
                this.Name = name;
                this.Start = start;
                this.Finish = finish;
            }

            public string Name { get; }

            public int Start { get; }

            public int Finish { get; }
        }

        public static void Main()
        {
            var lectures = ReadLectures();

            var lecturesSchedule = new List<Lecture>();
            var currentLecture = lectures[0];
            lecturesSchedule.Add(currentLecture);

            for (int i = 1; i < lectures.Count; i++)
            {
                var nextLecture = lectures[i];
                if (currentLecture.Finish <= nextLecture.Start)
                {
                    currentLecture = nextLecture;
                    lecturesSchedule.Add(currentLecture);
                }
            }

            Print(lecturesSchedule);
        }

        private static void Print(List<Lecture> lecturesSchedule)
        {
            Console.WriteLine($"Lectures ({lecturesSchedule.Count}):");
            foreach (var lecture in lecturesSchedule)
            {
                Console.WriteLine($"{lecture.Start}-{lecture.Finish} -> {lecture.Name}");
            }
        }

        private static List<Lecture> ReadLectures()
        {
            var lecturesCount = int.Parse(
                Console.ReadLine()
                .Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries)
                [1]);

            var lectures = new List<Lecture>();
            for (int i = 0; i < lecturesCount; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new[] { ':', '-' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray();

                var name = tokens[0];
                var start = int.Parse(tokens[1]);
                var finish = int.Parse(tokens[2]);

                lectures.Add(new Lecture(name, start, finish));
            }

            return lectures
                .OrderBy(x => x.Finish) // min finish time to maximise number of lectures held
                .ToList();
        }
    }
}
