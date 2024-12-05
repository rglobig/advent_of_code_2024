using System.Text;

var input = File.ReadAllLines("input.txt");

List<Rule> rules = [];
List<Update> updates = [];

var flip = false;

foreach (var line in input)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        flip = true;
        continue;
    }

    if (!flip)
        rules.Add(CreateRule(line));
    else
        updates.Add(CreateUpdate(line));
}

List<Update> incorrectUpdates;

new PartOne().CalculateAndPrint(rules, updates, out incorrectUpdates);
new PartTwo().CalculateAndPrint(rules, incorrectUpdates);

Rule CreateRule(string line)
{
    var splits = line.Split('|', 2);
    var left = int.Parse(splits[0]);
    var right = int.Parse(splits[1]);
    return new Rule(left, right);
}

Update CreateUpdate(string line)
{
    var sequence = line.Split(',').Select(int.Parse).ToList();
    return new Update(sequence);
}

record Rule(int Left, int Right);
record Update(List<int> Sequence)
{
    protected virtual bool PrintMembers(StringBuilder stringBuilder)
    {
        stringBuilder.Append("Sequence = " + string.Join(",", Sequence));
        return true;
    }
}