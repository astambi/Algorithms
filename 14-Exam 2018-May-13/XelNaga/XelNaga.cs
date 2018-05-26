namespace XelNaga
{
    using System;
    using System.Linq;

    public class XelNaga
    {
        public static void Main()
        {
            var answers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .Where(x => x != -1) // end of input excluded
                .ToArray();

            // input answers (2 2 2) (2 2 2) (2) (44) (444) => species in group = 3, 45, 445
            // total species = (3 groups * 3 species/group) + (45) + (445) = 499

            var totalCount = 0;

            foreach (var answer in answers.Distinct())
            {
                var speciesCount = answer + 1; // min count including respondent
                var answersCount = answers.Count(x => x == answer);

                var groups = 1;
                while (answersCount > speciesCount) // several groups with same age
                {
                    groups++;
                    answersCount -= speciesCount;
                }

                totalCount += speciesCount * groups;
            }

            Console.WriteLine(totalCount);
        }
    }
}
