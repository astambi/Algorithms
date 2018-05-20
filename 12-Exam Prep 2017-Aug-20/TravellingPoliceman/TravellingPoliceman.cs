namespace TravellingPoliceman
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TravellingPoliceman
    {
        private static int[,] values;
        private static bool[,] isSelected;

        public static void Main()
        {
            var fuel = int.Parse(Console.ReadLine());
            var streets = ReadInput();

            FillKnapsack(streets, fuel);

            var streetsTaken = ReconstructSolution(streets, fuel);
            Print(fuel, streetsTaken);
        }

        private static void Print(int fuel, Stack<Street> streetsTaken)
        {
            Console.WriteLine(string.Join(" -> ", streetsTaken.Select(s => s.Name)));
            Console.WriteLine($"Total pokemons caught -> {streetsTaken.Sum(s => s.Pokemons)}");
            Console.WriteLine($"Total car damage -> {streetsTaken.Sum(s => s.CarDamage)}");
            Console.WriteLine($"Fuel Left -> {fuel - streetsTaken.Sum(s => s.Length)}");
        }

        private static Stack<Street> ReconstructSolution(List<Street> streets, int fuel)
        {
            var streetsTaken = new Stack<Street>();

            for (int row = streets.Count; row > 0; row--)
            {
                if (isSelected[row, fuel])
                {
                    var streetIndex = row - 1;
                    var street = streets[streetIndex];

                    streetsTaken.Push(street);
                    fuel -= street.Length;
                }
            }

            return streetsTaken;
        }

        private static void FillKnapsack(List<Street> streets, int fuel)
        {
            // Initialize
            values = new int[streets.Count + 1, fuel + 1];
            isSelected = new bool[streets.Count + 1, fuel + 1];

            // Fill
            for (int streetIndex = 0; streetIndex < streets.Count; streetIndex++)
            {
                var street = streets[streetIndex];
                var row = streetIndex + 1;

                for (int capacity = 0; capacity <= fuel; capacity++)
                {
                    var valueExcluding = values[row - 1, capacity];

                    if (street.Length > capacity) // insufficient fuel capacity
                    {
                        values[row, capacity] = valueExcluding;
                        continue;
                    }

                    var valueIncluding = street.Value + values[row - 1, capacity - street.Length];

                    if (valueIncluding > valueExcluding)
                    {
                        values[row, capacity] = valueIncluding;
                        isSelected[row, capacity] = true;
                    }
                    else
                    {
                        values[row, capacity] = valueExcluding;
                    }
                }
            }
        }

        private static List<Street> ReadInput()
        {
            var streets = new List<Street>();

            while (true)
            {
                var input = Console.ReadLine();
                if (input == "End")
                {
                    break;
                }

                var tokens = input
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray();

                var name = tokens[0];
                var carDamage = int.Parse(tokens[1]);
                var pokemons = int.Parse(tokens[2]);
                var length = int.Parse(tokens[3]);

                var street = new Street(name, carDamage, pokemons, length);
                streets.Add(street);
            }

            return streets;
        }

        private class Street
        {
            public Street(string name, int carDamage, int pokemons, int length)
            {
                this.Name = name;
                this.CarDamage = carDamage;
                this.Pokemons = pokemons;
                this.Length = length;
            }

            public string Name { get; }

            public int CarDamage { get; }

            public int Pokemons { get; }

            public int Length { get; } // fuel needed = length

            public int Value => this.Pokemons * 10 - this.CarDamage;
        }
    }
}
