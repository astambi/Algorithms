namespace ParkingZones
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ParkingZones
    {
        public static void Main()
        {
            var zones = ReadParkingZones();
            var freeSpots = ReadParkingSpots(zones);
            var target = ReadTargetSpot();
            var traversalTime = int.Parse(Console.ReadLine()); // traversal time of 1 parking spot in secs

            var bestSpot = freeSpots
                .Where(s => s.CalcPrice(target, traversalTime)
                        == freeSpots.Select(x => x.CalcPrice(target, traversalTime)).Min()) // min price
                .OrderBy(s => s.CalcTime(target, traversalTime)) // min time
                .FirstOrDefault();

            if (bestSpot != null)
            {
                Console.WriteLine(bestSpot + $" Price: {bestSpot.CalcPrice(target, traversalTime):f2}");
            }
        }

        private static ParkingSpot ReadTargetSpot()
        {
            var tokens = Console.ReadLine()
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            return new ParkingSpot(tokens[0], tokens[1]);
        }

        private static List<ParkingSpot> ReadParkingSpots(Zone[] zones)
        {
            var tokens = Console.ReadLine()
                .Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var parkingSpots = new List<ParkingSpot>();
            for (int i = 0; i < tokens.Length; i += 2)
            {
                var parkingSpot = new ParkingSpot(tokens[i], tokens[i + 1]);

                // Set Parking Zone
                foreach (var zone in zones)
                {
                    if (zone.Contains(parkingSpot))
                    {
                        parkingSpot.ParkingZone = zone;
                        break;
                    }
                }

                parkingSpots.Add(parkingSpot);
            }

            return parkingSpots;
        }

        private static Zone[] ReadParkingZones()
        {
            var n = int.Parse(Console.ReadLine());
            var zones = new Zone[n];

            for (int i = 0; i < n; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new char[] { ':', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var name = tokens[0];
                var x = int.Parse(tokens[1]);
                var y = int.Parse(tokens[2]);
                var width = int.Parse(tokens[3]);
                var height = int.Parse(tokens[4]);
                var price = double.Parse(tokens[5]);

                zones[i] = new Zone(name, x, y, width, height, price);
            }

            return zones;
        }

        private class Zone
        {
            public Zone(string name, int x, int y, int width, int height, double price)
            {
                this.Name = name;
                this.X = x;
                this.Y = y;
                this.Width = width;
                this.Height = height;
                this.PricePerMin = price;
            }

            public string Name { get; }

            public int X { get; } // left

            public int Y { get; } // top

            public int Width { get; }

            public int Height { get; }

            public double PricePerMin { get; }

            public bool Contains(ParkingSpot spot)
                => this.X <= spot.X && spot.X <= this.X + this.Width
                && this.Y <= spot.Y && spot.Y <= this.Y + this.Height;
        }

        private class ParkingSpot
        {
            public ParkingSpot(int x, int y, Zone zone = null)
            {
                this.X = x;
                this.Y = y;
                this.ParkingZone = zone;
            }

            public int X { get; }

            public int Y { get; }

            public Zone ParkingZone { get; set; }

            public double CalcDistance(ParkingSpot other)
                => Math.Abs(this.X - other.X) + Math.Abs(this.Y - other.Y) - 1;

            public double CalcTime(ParkingSpot other, int traversalTime)
                => this.CalcDistance(other) * 2 * traversalTime;

            public double CalcPrice(ParkingSpot other, int traversalTime)
                => Math.Ceiling(this.CalcTime(other, traversalTime) / 60.0) * this.ParkingZone.PricePerMin;

            public override string ToString()
                => $"Zone Type: {this.ParkingZone.Name}; X: {this.X}; Y: {this.Y};";
        }
    }
}