using static System.Console;

class PartOne
{
    public void CalculateAndPrint(List<Rule> rules, List<Update> updates, out List<Update> incorrectUpdates)
    {
        WriteLine("====== PART ONE ======");

        var sum = 0;

        incorrectUpdates = [];

        foreach (var update in updates)
        {
            if (UpdateIsValid(update, rules))
            {
                sum += GetMiddleValueInUpdate(update);
                continue;
            }
            incorrectUpdates.Add(update);
        }
        WriteLine($"Sum: {sum}");
        WriteLine("======================");
    }

    bool UpdateIsValid(Update update, List<Rule> rules)
    {
        var sequence = update.Sequence;
        foreach (var rule in rules)
        {
            if (!(sequence.Contains(rule.Left) && sequence.Contains(rule.Right))) continue;

            var valid = CheckRule(rule, update);
            if (!valid) return false;
        }
        return true;
    }

    bool CheckRule(Rule rule, Update update)
    {
        var leftIndex = update.Sequence.FindIndex(number => number == rule.Left);
        var rightIndex = update.Sequence.FindIndex(number => number == rule.Right);
        return leftIndex < rightIndex;
    }

    int GetMiddleValueInUpdate(Update update)
    {
        return update.Sequence[update.Sequence.Count / 2];
    }
}