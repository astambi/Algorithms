namespace AbaspaBasapa
{
    using System;

    public class AbaspaBasapa
    {
        public static void Main()
        {
            var firstSec = Console.ReadLine();
            var secondSec = Console.ReadLine();

            var longestCommonSecs = new int[firstSec.Length, secondSec.Length]; // sec len
            var maxLen = 0;
            var maxIndexFirst = -1;

            for (int indexF = 0; indexF < firstSec.Length; indexF++)
            {
                for (int indexS = 0; indexS < secondSec.Length; indexS++)
                {
                    if (firstSec[indexF] == secondSec[indexS])
                    {
                        var currentLen = 1;
                        if (indexF - 1 >= 0 && indexS - 1 >= 0) // prev sec possible
                        {
                            currentLen = 1 + longestCommonSecs[indexF - 1, indexS - 1];
                        }

                        longestCommonSecs[indexF, indexS] = currentLen;

                        if (currentLen > maxLen) // leftmost subsec in firstSec
                        {
                            maxLen = currentLen;
                            maxIndexFirst = indexF;
                        }
                    }
                }
            }

            var resultSec = firstSec.Substring(maxIndexFirst - maxLen + 1, maxLen);
            Console.WriteLine(resultSec);
        }
    }
}
