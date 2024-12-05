using static System.Console;

class PartTwo
{
    public void CalculateAndPrint(List<Rule> rules, List<Update> updates)
    {
        WriteLine("====== PART TWO ======");
        var sum = 0;

        foreach (var update in updates)
        {
            var newUpdate = ApplyRulesToUpdate(rules, update);
            sum += GetMiddleValueInUpdate(newUpdate);
        }

        WriteLine($"Sum: {sum}");
        WriteLine("======================");
    }

    Update ApplyRulesToUpdate(List<Rule> rules, Update update)
    {
        List<int> sortedList = new(update.Sequence);

        bool redo;

        do
        {
            redo = false;
            foreach (var rule in rules)
            {
                if (!(update.Sequence.Contains(rule.Left) && update.Sequence.Contains(rule.Right)))
                {
                    continue;
                }

                var applied = ApplyRuleToUpdate(rule, sortedList);
                if (applied) redo = true;
            }
        } while (redo);

        return new Update(sortedList);
    }

    private bool ApplyRuleToUpdate(Rule rule, List<int> list)
    {
        var leftIndex = list.IndexOf(rule.Left);
        var rightIndex = list.IndexOf(rule.Right);
        if (leftIndex > rightIndex)
        {
            var temp = list[leftIndex];
            list[leftIndex] = list[rightIndex];
            list[rightIndex] = temp;
            return true;
        }
        return false;
    }

    int GetMiddleValueInUpdate(Update update)
    {
        return update.Sequence[update.Sequence.Count / 2];
    }
}
