using System.Text;

var lines = File.ReadAllLines("input.txt");

List<Equation> equations = new(lines.Length);

ParseLines(lines, equations);

long sum = 0;

foreach (var equation in equations)
{
    int length = equation.numbers.Length;
    int max = (int)Math.Pow(2, length);
    List<string> combinations = new(max);

    GenerateCombinations(string.Empty, length, combinations);

    for (int i = 0; i < max; i++)
    {
        var combination = combinations[i];
        var workNumbers = (long[])equation.numbers.Clone();
        var result = Calculate(combination, workNumbers);
        if(result == equation.result)
        {
            var resultString = equation.result + " = " + string.Join("", equation.numbers.Zip(combination.ToCharArray(), (n, c) => n.ToString() + c.ToString()));
            Console.WriteLine($"Found Combination: {resultString}");
            sum += equation.result;
            break;
        }
    }
}

Console.WriteLine($"Sum: {sum}");

static long Calculate(string combination, long[] numbers)
{
    for (int i = 1; i < combination.Length; i++)
    {
        long number = numbers[i - 1];
        long nextNumber = numbers[i];
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
        numbers[i] = tempResult;
    }
    return numbers[^1];
}

static void GenerateCombinations(string prefix, int length, List<string> combinations)
{
    if (length == 0)
    {
        combinations.Add(prefix);
        return;
    }
    GenerateCombinations(prefix + "*", length - 1, combinations);
    GenerateCombinations(prefix + "+", length - 1, combinations);
}

static void ParseLines(string[] lines, List<Equation> equations)
{
    foreach (var line in lines)
    {
        var newLine = line.Replace(":", string.Empty);
        var splitted = newLine.Split(' ');
        var equation = new Equation(long.Parse(splitted[0]), splitted[1..].Select(long.Parse).ToArray());
        equations.Add(equation);
    }
}

record Equation(long result, long[] numbers)
{
    protected virtual bool PrintMembers(StringBuilder stringBuilder)
    {
        stringBuilder.Append("Result = " + result);
        stringBuilder.Append(" Numbers = " + string.Join(",", numbers));
        return true;
    }
}