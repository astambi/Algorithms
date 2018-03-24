namespace ReverseArrayWithList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReverseArray
    {
        private static List<int> reversedArr = new List<int>();

        public static void Main()
        {
            var arr = ReadArray();
            Reverse(arr, 0);
            Console.WriteLine(string.Join(" ", reversedArr));
        }

        private static void Reverse(int[] arr, int index)
        {
            if (index == arr.Length)
            {
                return;
            }

            Reverse(arr, index + 1);

            reversedArr.Add(arr[index]);
        }

        private static int[] ReadArray()
            => Console.ReadLine()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
    }
}
