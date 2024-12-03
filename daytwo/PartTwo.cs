using static System.Console;

class PartTwo(int[][] reports)
{
    public void CalculateAndPrint()
    {
        int sumOfSafeReports = 0;

        for (int i = 0; i < reports.Length; i++)
        {
            var evaluation = Helper.CreateReportEvaluation([.. reports[i]]);
            bool reportIsSafe = CheckIfEvaluationIsSafeWithDampener(evaluation);

            if (reportIsSafe)
            {
                sumOfSafeReports++;
            }
        }

        WriteLine($"{sumOfSafeReports} reports are safe. (Part 2)");
    }

    bool CheckIfEvaluationIsSafeWithDampener(ReportEvaluation evaluation)
    {
        if (Helper.CheckIfEvaluationIsSafe(evaluation)) return true;

        for (int i = 0; i < evaluation.report.Count; i++)
        {
            List<int> copy = new(evaluation.report);
            copy.RemoveAt(i);
            var newEvaluation = Helper.CreateReportEvaluation(copy);
            var isSafe = Helper.CheckIfEvaluationIsSafe(newEvaluation);
            if (isSafe) return true;
        }

        return false;
    }
}