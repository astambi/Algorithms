//namespace SumOfCoins
//{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SumOfCoins
    {
        public static void Main(string[] args)
        {
            var availableCoins = new[] { 1, 2, 5, 10, 20, 50 };
            var targetSum = 923;

            var selectedCoins = ChooseCoins(availableCoins, targetSum);

            Console.WriteLine($"Number of coins to take: {selectedCoins.Values.Sum()}");
            foreach (var selectedCoin in selectedCoins)
            {
                Console.WriteLine($"{selectedCoin.Value} coin(s) with value {selectedCoin.Key}");
            }
        }

        public static Dictionary<int, int> ChooseCoins(IList<int> coins, int targetSum)
        {
            coins = coins.OrderByDescending(x => x).ToList();

            var selectedCoins = new Dictionary<int, int>();
            var currentSum = 0;
            var coinIndex = 0;

            while (currentSum != targetSum && coinIndex < coins.Count)
            {
                var coinValue = coins[coinIndex];
                var remaininigSum = targetSum - currentSum;
                var coinsToTake = remaininigSum / coinValue;

                if (coinsToTake != 0)
                {
                    selectedCoins[coinValue] = coinsToTake;
                    currentSum += coinValue * coinsToTake;
                }

                coinIndex++;
            }

            if (currentSum != targetSum)
            {
                throw new InvalidOperationException("Greedy algorithm cannot produce desired sum with specified coins.");
            }

            return selectedCoins;
        }
    }
//}