namespace Parentheses
{
    using System;
    using System.Text;

    public class Parentheses
    {
        private const char LeftParenthesis = '(';
        private const char RightParenthesis = ')';

        private static StringBuilder result = new StringBuilder();

        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var variation = new char[n * 2];

            VariationsOptimized(0, variation, 0, 0);
            //VariationsOptimized(0, n, n, variation);

            Console.WriteLine(result.ToString().Trim());
        }

        private static void VariationsOptimized(int index, char[] variation, int usedLeft, int usedRight)
        {
            if (index >= variation.Length)
            {
                result.AppendLine(new string(variation));
                return;
            }

            if (usedLeft < variation.Length / 2)
            {
                variation[index] = LeftParenthesis;
                VariationsOptimized(index + 1, variation, usedLeft + 1, usedRight);
            }

            if (usedLeft > usedRight)
            {
                variation[index] = RightParenthesis;
                VariationsOptimized(index + 1, variation, usedLeft, usedRight + 1);
            }
        }

        private static void VariationsOptimized(int index, int remainingLeft, int remainingRight, char[] variation)
        {
            if (remainingLeft == 0 && remainingRight == 0)
            {
                result.AppendLine(new string(variation));
            }

            if (remainingLeft > 0)
            {
                variation[index] = LeftParenthesis;
                VariationsOptimized(index + 1, remainingLeft - 1, remainingRight, variation);
            }

            if (remainingLeft < remainingRight)
            {
                variation[index] = RightParenthesis;
                VariationsOptimized(index + 1, remainingLeft, remainingRight - 1, variation);
            }
        }
    }
}
