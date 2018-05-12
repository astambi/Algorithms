namespace AbaspaBasapa
{
    using System;

    public class AbaspaBasapa
    {
        public static void Main()
        {
            var firstSec = Console.ReadLine();
            var secondSec = Console.ReadLine();

            var longestCommonSecs = new int[firstSec.Length, secondSec.Length];
            var maxLen = 0;
            var maxIndexF = -1;
            var maxIndexS = -1;

            for (int f = 0; f < firstSec.Length; f++)
            {
                for (int s = 0; s < secondSec.Length; s++)
                {
                    if (firstSec[f] == secondSec[s])
                    {
                        var currentLen = 1;

                        if (f - 1 >= 0 && s - 1 >= 0) // diagonal exists
                        {
                            currentLen = 1 + longestCommonSecs[f - 1, s - 1];
                        }

                        longestCommonSecs[f, s] = currentLen;

                        if (currentLen > maxLen)
                        {
                            maxLen = currentLen;
                            maxIndexF = f;
                            maxIndexS = s;
                        }
                    }
                    else
                    {
                        longestCommonSecs[f, s] = 0;
                    }
                }
            }

            var resultSec = firstSec.Substring(maxIndexF - maxLen + 1, maxLen);
            Console.WriteLine(resultSec);
        }
    }
}
