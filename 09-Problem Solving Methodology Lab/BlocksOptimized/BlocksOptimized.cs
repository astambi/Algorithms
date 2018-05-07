namespace BlocksOptimized
{
    using System;

    public class BlocksOptimized
    {
        public static void Main()
        {
            var numberOfLetters = int.Parse(Console.ReadLine());

            Console.WriteLine("Number of blocks: " +
                numberOfLetters * (numberOfLetters - 1) * (numberOfLetters - 2) * (numberOfLetters - 3) / 4); // ignoring rotations

            FindBlocksIteration(numberOfLetters);
        }

        private static void FindBlocksIteration(int numberOfLetters)
        {
            var firstLetter = 'A';
            var lastLetter = firstLetter + numberOfLetters - 1;

            for (char i1 = firstLetter; i1 <= lastLetter; i1++)
            {
                for (char i2 = (char)(i1 + 1); i2 <= lastLetter; i2++)
                {
                    for (char i3 = (char)(i1 + 1); i3 <= lastLetter; i3++)
                    {
                        if (i2 == i3)
                        {
                            continue;
                        }

                        for (char i4 = (char)(i1 + 1); i4 <= lastLetter; i4++)
                        {
                            if (i2 == i4 || i3 == i4)
                            {
                                continue;
                            }

                            Console.WriteLine($"{i1}{i2}{i3}{i4}");
                        }
                    }
                }
            }
        }
    }
}
