namespace ReverseArray
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReverseArray
    {
        public static void Main()
        {
            var arr = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var reversedArr = new Stack<int>();
            Reverse(arr, reversedArr, 0);
        }

        private static void Reverse(int[] arr, Stack<int> reversedArr, int index)
        {
            if (index == arr.Length)
            {
                Console.WriteLine(string.Join(" ", reversedArr));
            }
            else
            {
                reversedArr.Push(arr[index]);
                Reverse(arr, reversedArr, index + 1);
            }
        }
    }
}
