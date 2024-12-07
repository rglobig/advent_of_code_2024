using System.Text;

var lines = File.ReadAllLines("input.txt");

List<Equation> equations = new(lines.Length);

ParseLines(lines, equations);

long sum = 0;

foreach (var equation in equations)
{
    int length = equation.Numbers.Length - 1;
    int max = (int)Math.Pow(3, length);
    List<string> combinations = new(max);

    GenerateCombinations(string.Empty, length, combinations);

    for (int i = 0; i < combinations.Count; i++)
    {
        var combination = combinations[i];
        var workNumbers = (long[])equation.Numbers.Clone();

        var result = Calculate(combination, workNumbers);
        if (result == equation.Result)
        {
            StringBuilder stringBuilder = new();
            for (int j = 0; j < combination.Length; j++)
            {
                stringBuilder.Append(equation.Numbers[j]);
                stringBuilder.Append(combination[j]);
            }
            stringBuilder.Append(equation.Numbers[^1]); 
            Console.WriteLine($"Found Combination: {result}={stringBuilder}");
            sum += equation.Result;
            break;
        }
    }
}

Console.WriteLine($"Sum: {sum}");

static long Calculate(string combination, long[] numbers)
{
    for (int i = 0; i < combination.Length; i++)
    {
        long number = numbers[i];
        long nextNumber = numbers[i + 1];
        char operation = combination[i];
        long tempResult = 0;
        if (operation == '*')
        {
            tempResult = number * nextNumber;
        }
        else if (operation == '+')
        {
            tempResult = number + nextNumber;
        }
        else if (operation == '|')
        {
            tempResult = long.Parse(number.ToString() + nextNumber.ToString());
        }
        numbers[i + 1] = tempResult;
    }
    return numbers[^1];
}

static void GenerateCombinations(string prefix, int length, List<string> combinations)
{
    if (prefix.Length == length)
    {
        combinations.Add(prefix);
        return;
    }
    GenerateCombinations(prefix + "*", length, combinations);
    GenerateCombinations(prefix + "+", length, combinations);
    GenerateCombinations(prefix + "|", length, combinations);
}

static void ParseLines(string[] lines, List<Equation> equations)
{
    foreach (var line in lines)
    {
        var newLine = line.Replace(":", string.Empty);
        var split = newLine.Split(' ');
        var equation = new Equation(long.Parse(split[0]), split[1..].Select(long.Parse).ToArray());
        equations.Add(equation);
    }
}

record Equation(long Result, long[] Numbers)
{
    protected virtual bool PrintMembers(StringBuilder stringBuilder)
    {
        stringBuilder.Append("Result = " + Result);
        stringBuilder.Append(" Numbers = " + string.Join(",", Numbers));
        return true;
    }
}