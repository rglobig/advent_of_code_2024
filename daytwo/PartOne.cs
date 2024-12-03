using static System.Console;

class PartOne(int[][] reports)
{
    public void CalculateAndPrint()
    {
        int sumOfSafeReports = 0;

        for (int i = 0; i < reports.Length; i++)
        {
            var evaluation = Helper.CreateReportEvaluation([.. reports[i]]);
            bool reportIsSafe = Helper.CheckIfEvaluationIsSafe(evaluation);

            if (reportIsSafe)
            {
                sumOfSafeReports++;
            }
        }

        WriteLine($"{sumOfSafeReports} reports are safe. (Part 1)");
    }
}