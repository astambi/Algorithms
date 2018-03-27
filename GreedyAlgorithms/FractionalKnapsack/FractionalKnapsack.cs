namespace FractionalKnapsack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FractionalKnapsack
    {
        private class Item : IComparable<Item>
        {
            public Item(decimal price, decimal weight)
            {
                this.Price = price;
                this.Weight = weight;
            }

            public decimal Price { get; }

            public decimal Weight { get; set; }

            public int CompareTo(Item other)
                => (this.Price / this.Weight).CompareTo(other.Price / other.Weight); // ASC

            public override string ToString()
                => $"price {this.Price:F2} and weight {this.Weight:F2}";
        }

        public static void Main()
        {
            var tokens = SplitInputBy(":");
            var capacity = decimal.Parse(tokens[1]);
            var items = ReadItems();

            var totalPrice = FillKnapsack(capacity, items);
            Console.WriteLine($"Total price: {totalPrice:F2}");
        }

        private static decimal FillKnapsack(decimal capacity, SortedSet<Item> items)
        {
            var totalPrice = 0m;
            while (capacity > 0 && items.Any())
            {
                var item = items.Last();
                var weightTaken = Math.Min(capacity, item.Weight);
                var proportionTaken = weightTaken / item.Weight;
                capacity -= weightTaken;
                totalPrice += item.Price * proportionTaken;

                Print(item, proportionTaken * 100);

                if (weightTaken < item.Weight)
                {
                    item.Weight -= weightTaken;
                }
                else
                {
                    items.Remove(item);
                }
            }

            return totalPrice;
        }

        private static SortedSet<Item> ReadItems()
        {
            var tokens = SplitInputBy(":");
            var itemsCount = int.Parse(tokens[1]);

            var items = new SortedSet<Item>();
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

            return items;
        }

        private static string[] SplitInputBy(string separator)
            => Console.ReadLine().Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries).ToArray();

        private static void Print(Item item, decimal proportion)
            => Console.WriteLine(proportion < 100
                ? $"Take {proportion:F2}% of item with {item.ToString()}"
                : $"Take {proportion}% of item with {item.ToString()}");
    }
}