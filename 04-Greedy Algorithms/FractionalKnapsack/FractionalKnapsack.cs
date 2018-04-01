namespace FractionalKnapsack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FractionalKnapsack
    {
        private class Item
        {
            public Item(decimal price, decimal weight)
            {
                this.Price = price;
                this.Weight = weight;
            }

            public decimal Price { get; }

            public decimal Weight { get; }
        }

        public static void Main()
        {
            var capacity = decimal.Parse(SplitInputBy(":")[1]);
            var items = ReadItems();

            var totalPrice = FillKnapsack(capacity, items);
            Console.WriteLine($"Total price: {totalPrice:F2}");
        }

        private static decimal FillKnapsack(decimal capacity, List<Item> items)
        {
            var totalPrice = 0m;
            var index = 0;

            while (capacity > 0 && index < items.Count)
            {
                var item = items[index++];
                var weightTaken = Math.Min(capacity, item.Weight);
                var proportionTaken = weightTaken / item.Weight;

                capacity -= weightTaken;
                totalPrice += item.Price * proportionTaken;

                Print(item, proportionTaken * 100); // no need to reduce item weight
            }

            return totalPrice;
        }

        private static void Print(Item item, decimal proportion)
        {
            var proportionAsStr = proportion == 100
                ? $"{proportion}"
                : $"{proportion:F2}";

            Console.WriteLine($"Take {proportionAsStr}% of item with price {item.Price:F2} and weight {item.Weight:F2}");
        }

        private static List<Item> ReadItems()
        {
            var itemsCount = int.Parse(SplitInputBy(":")[1]);

            var items = new List<Item>();
            for (int i = 0; i < itemsCount; i++)
            {
                var item = SplitInputBy("->").Select(int.Parse).ToArray();
                var price = item[0];
                var weight = item[1];

                if (weight > 0)
                {
                    items.Add(new Item(price, weight));
                }
            }

            return items
                .OrderByDescending(x => x.Price / x.Weight) // best item
                .ToList();
        }

        private static string[] SplitInputBy(string separator)
            => Console.ReadLine()
            .Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
    }
}