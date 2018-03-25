namespace Activities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Activities
    {
        private class Activity
        {
            public int StartTime { get; set; }

            public int EndTime { get; set; }
        }

        public static void Main()
        {
            var activities = InitializeActivities();
            SelectActivities(activities);
        }

        private static void SelectActivities(List<Activity> activities)
        {
            if (activities.Any())
            {
                var earliestEndingActivity = activities.First();
                Print(earliestEndingActivity);

                for (int i = 1; i < activities.Count; i++)
                {
                    var currentActivity = activities[i];
                    if (currentActivity.StartTime >= earliestEndingActivity.EndTime)
                    {
                        earliestEndingActivity = currentActivity;
                        Print(earliestEndingActivity);
                    }
                }
            }
        }

        private static void Print(Activity activity)
            => Console.WriteLine($"Activity: {activity.StartTime} - {activity.EndTime}");

        private static List<Activity> InitializeActivities()
        {
            var startingTimes = new[] { 1, 3, 0, 5, 3, 5, 6, 8, 8, 2, 12 };
            var endingTimes = new[] { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            var activities = new List<Activity>();

            for (int i = 0; i < startingTimes.Length; i++)
            {
                activities.Add(new Activity
                {
                    StartTime = startingTimes[i],
                    EndTime = endingTimes[i]
                });
            }

            return activities.OrderBy(a => a.EndTime).ToList();
        }
    }
}
