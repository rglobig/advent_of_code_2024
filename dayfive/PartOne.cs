using static System.Console;

class PartOne
{
    public void CalculateAndPrint(List<Rule> rules, List<Update> updates)
    {
        WriteLine("====== PART ONE ======");

        var sum = 0;

        foreach (var update in updates)
        {
            if (UpdateIsValid(update, rules))
            {
                sum += GetMiddleValueInUpdate(update);
            }
        }
        WriteLine($"Sum: {sum}");
        WriteLine("======================");
    }

    bool UpdateIsValid(Update update, List<Rule> rules)
    { 
        var sequence = update.sequence;
        foreach (var rule in rules)
        {
            if(sequence.Contains(rule.left) && sequence.Contains(rule.right))
            {
                var valid = CheckRule(rule, sequence);
                if (!valid) return false;
            }
        }
        return true;
    }

    bool CheckRule(Rule rule, List<int> sequence)
    {
        var leftIndex = sequence.FindIndex(number => number == rule.left);
        var rightIndex = sequence.FindIndex(number => number == rule.right);
        return leftIndex < rightIndex;
    }


    int GetMiddleValueInUpdate(Update update)
    {
        return update.sequence[update.sequence.Count / 2];
    }
}