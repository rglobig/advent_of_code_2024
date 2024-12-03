static class Helper
{
    public static ReportEvaluation CreateReportEvaluation(List<int> report)
    {
        List<int> gradients = new(report.Count - 1);
        List<int> differences = new(report.Count - 1);
        for (int x = 0, y = 1; y < report.Count; x++, y++)
        {
            var now = report[x];
            var next = report[y];
            gradients.Add(Math.Sign(next - now));
            differences.Add(Math.Abs(next - now));
        }
        return new ReportEvaluation(report, gradients, differences);
    }

    public static bool CheckIfEvaluationIsSafe(ReportEvaluation evaluation)
    {
        var allGradientsAreSame = evaluation.gradients.Count(gradient => gradient == evaluation.gradients[0]) == evaluation.gradients.Count;
        if (!allGradientsAreSame) return false;

        var allDifferencesAreOkay = evaluation.differences.Count(difference => 1 <= difference && difference <= 3) == evaluation.differences.Count;
        return allDifferencesAreOkay;
    }
}
