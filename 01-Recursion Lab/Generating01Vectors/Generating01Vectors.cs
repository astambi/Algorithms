namespace Generating01Vectors
{
    using System;

    public class Generating01Vectors
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            if (n < 0)
            {
                return;
            }

            var vector = new int[n];
            GenerateVector(vector, 0);
        }

        private static void GenerateVector(int[] vector, int index)
        {
            if (index == vector.Length)
            {
                Console.WriteLine(string.Join(string.Empty, vector));
            }
            else
            {
                for (int i = 0; i <= 1; i++)
                {
                    vector[index] = i;
                    GenerateVector(vector, index + 1);
                }
            }
        }
    }
}
