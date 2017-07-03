using System;

namespace L04_GeneratingVectors
{
    public class GeneratingVectors
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var vector = new int[n];
            Gen(0, vector);
        }

        private static void Gen(int index, int[] vector)
        {
            if (index > vector.Length - 1)
            {
                Console.WriteLine(string.Join(string.Empty, vector));
                return;
            }

            vector[index] = 0;
            Gen(index + 1, vector);

            vector[index] = 1;
            Gen(index + 1, vector);
        }
    }
}
