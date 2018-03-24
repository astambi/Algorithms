namespace TowersOfHanoi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TowersOfHanoi
    {
        private static int stepsTaken = 0;
        private static Stack<int> source;
        private static Stack<int> destination = new Stack<int>();
        private static Stack<int> spare = new Stack<int>();

        public static void Main()
        {
            var numberOfDiscs = int.Parse(Console.ReadLine());
            source = new Stack<int>(Enumerable.Range(1, numberOfDiscs).Reverse()); // n .. 2 1

            PrintRods();
            MoveDiscs(numberOfDiscs, source, destination, spare);
            //var stepsNeeded = Math.Pow(2, numberOfDiscs) - 1; // check
        }


        private static void MoveDiscs(int bottomDisk, Stack<int> sourceRod, Stack<int> destinationRod, Stack<int> spareRod)
        {
            if (bottomDisk == 1)
            {
                destinationRod.Push(sourceRod.Pop());
                PrintMove(bottomDisk);
            }
            else
            {
                // Move discs on top of bottom disc from Source to Spare
                MoveDiscs(bottomDisk - 1, sourceRod, spareRod, destinationRod);

                // Move bottom disk to destination
                destinationRod.Push(sourceRod.Pop());
                PrintMove(bottomDisk);

                // Move disks from Spare to Destination
                MoveDiscs(bottomDisk - 1, spareRod, destinationRod, sourceRod);
            }
        }

        private static void PrintMove(int currentDisc)
        {
            Console.WriteLine($"Step #{++stepsTaken}: Moved disk");
            PrintRods();
        }

        private static void PrintRods()
        {
            Console.WriteLine($"Source: {string.Join(", ", source.Reverse())}");
            Console.WriteLine($"Destination: {string.Join(", ", destination.Reverse())}");
            Console.WriteLine($"Spare: {string.Join(", ", spare.Reverse())}");
            Console.WriteLine();
        }
    }
}
