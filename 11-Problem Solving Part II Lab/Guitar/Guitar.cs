namespace Guitar
{
    using System;
    using System.Linq;

    public class Guitar
    {
        public static void Main()
        {
            var intervals = Console.ReadLine()
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            var initialVolume = int.Parse(Console.ReadLine());
            var maxVolume = int.Parse(Console.ReadLine());

            var possibleVolumes = CalcPossibleVolumes(intervals, initialVolume, maxVolume);

            FindMaxPossibleVolume(possibleVolumes);
        }

        private static void FindMaxPossibleVolume(bool[][] possibleVolumes)
        {
            var maxResult = -1;
            var lastRow = possibleVolumes.Last();

            for (int col = lastRow.Length - 1; col >= 0; col--) // volume
            {
                if (lastRow[col])
                {
                    maxResult = col;
                    break;
                }
            }

            Console.WriteLine(maxResult);
        }

        private static bool[][] CalcPossibleVolumes(int[] intervals, int initialVolume, int maxVolume)
        {
            // Initialize matrix
            var possibleVolumes = new bool[intervals.Length + 1][];
            for (int row = 0; row < possibleVolumes.Length; row++) // [0, intervals]
            {
                possibleVolumes[row] = new bool[maxVolume + 1];    // [0, maxVol]
            }

            possibleVolumes[0][initialVolume] = true;

            // Calc possible volumes
            for (int row = 1; row <= intervals.Length; row++) // interval indices
            {
                for (int prevVolume = 0; prevVolume <= maxVolume; prevVolume++)
                {
                    if (!possibleVolumes[row - 1][prevVolume])
                    {
                        continue;
                    }

                    var currentInterval = intervals[row - 1];
                    var upVolume = prevVolume + currentInterval;
                    var downVolume = prevVolume - currentInterval;

                    if (upVolume <= maxVolume)
                    {
                        possibleVolumes[row][upVolume] = true;
                    }

                    if (downVolume >= 0)
                    {
                        possibleVolumes[row][downVolume] = true;
                    }
                }
            }

            return possibleVolumes;
        }
    }
}
