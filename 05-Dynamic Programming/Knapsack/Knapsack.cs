namespace Knapsack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Knapsack
    {
        private class Item
        {
            public string Name { get; set; }

            public int Weight { get; set; }

            public int Price { get; set; }
        }

        private static List<Item> items = new List<Item>();
        private static int[][] values;
        private static bool[][] itemsSelected;

        public static void Main()
        {
            var maxCapacity = int.Parse(Console.ReadLine());
            ReadItems();

            InitializeMatrices(maxCapacity, items.Count);

            FillKnapsack(maxCapacity);

            ReconstructSolution(maxCapacity);
        }

        private static void ReconstructSolution(int maxCapacity)
        {
            var itemsTaken = new List<Item>();
            var capacity = maxCapacity;

            for (int row = items.Count; row > 0; row--)
            {
                if (itemsSelected[row][capacity])
                {
                    var itemIndex = row - 1;
                    var item = items[itemIndex];
                    itemsTaken.Add(item);

                    capacity -= item.Weight;
                }
            }

            Console.WriteLine($"Total Weight: {itemsTaken.Sum(i => i.Weight)}");
            Console.WriteLine($"Total Value: {values[items.Count][maxCapacity]}");
            Console.WriteLine(string.Join(Environment.NewLine,
                itemsTaken.Select(i => i.Name).OrderBy(x => x)));
        }

        private static void FillKnapsack(int maxCapacity)
        {
            for (int itemIndex = 0; itemIndex < items.Count; itemIndex++)
            {
                var item = items[itemIndex];
                var row = itemIndex + 1;

                for (int capacity = 0; capacity <= maxCapacity; capacity++)
                {
                    var valueExcluding = values[row - 1][capacity];

                    if (item.Weight > capacity)
                    {
                        values[row][capacity] = valueExcluding;
                        continue;
                    }

                    var valueIncluding = item.Price + values[row - 1][capacity - item.Weight];

                    if (valueIncluding > valueExcluding)
                    {
                        values[row][capacity] = valueIncluding;
                        itemsSelected[row][capacity] = true;
                    }
                    else
                    {
                        values[row][capacity] = valueExcluding;
                    }
                }
            }
        }

        private static void InitializeMatrices(int maxCapacity, int itemsCount)
        {
            values = new int[itemsCount + 1][];
            itemsSelected = new bool[itemsCount + 1][];

            for (int itemIndex = 0; itemIndex <= itemsCount; itemIndex++)
            {
                values[itemIndex] = new int[maxCapacity + 1];
                itemsSelected[itemIndex] = new bool[maxCapacity + 1];
            }
        }

        private static void ReadItems()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "end")
                {
                    break;
                }

                var tokens = input.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                items.Add(new Item
                {
                    Name = tokens[0],
                    Weight = int.Parse(tokens[1]),
                    Price = int.Parse(tokens[2])
                });
            }
        }
    }
}
