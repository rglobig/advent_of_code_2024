using System.Text;

var lines = File.ReadAllLines("input.txt");

List<Equation> equations = new(lines.Length);

ParseLines(lines, equations);

long sum = 0;
bool print = false;

Parallel.ForEach(equations, new ParallelOptions { MaxDegreeOfParallelism = 4 }, CalculateEquation);

Console.WriteLine($"Sum: {sum}");

void CalculateEquation(Equation equation)
{
    int combinationLength = equation.Numbers.Length - 1;
    var combinations = GenerateCombinations(combinationLength);
    foreach (var combination in combinations)
    {
        var workNumbers = (long[])equation.Numbers.Clone();
        var result = Calculate(combination, workNumbers);
        if (result != equation.Result) continue;
        Print(equation, combination, result);
        Interlocked.Add(ref sum, equation.Result);
        break;
    }
}

void Print(Equation equation, string combination, long result)
{
    if (!print) return;
    StringBuilder stringBuilder = new();
    for (int j = 0; j < combination.Length; j++)
    {
        stringBuilder.Append(equation.Numbers[j]);
        stringBuilder.Append(combination[j]);
    }
    stringBuilder.Append(equation.Numbers[^1]);
    Console.WriteLine($"Found Combination: {result}={stringBuilder}");
}

static long Calculate(string combination, long[] numbers)
{
    for (int i = 0; i < combination.Length; i++)
    {
        long number = numbers[i];
        long nextNumber = numbers[i + 1];
        char operation = combination[i];
        long tempResult = operation switch
        {
            '*' => number * nextNumber,
            '+' => number + nextNumber,
            '|' => long.Parse(number.ToString() + nextNumber.ToString()),
            _ => throw new InvalidOperationException("Invalid operation")
        };
        numbers[i + 1] = tempResult;
    }
    return numbers[^1];
}

static List<string> GenerateCombinations(int length)
{
    char[] ops = ['*', '+', '|'];
    int baseValue = ops.Length;
    int max = (int)Math.Pow(baseValue, length);

    List<string> combinations = new(max);

    for (int i = 0; i < max; i++)
    {
        StringBuilder sb = new(length);
        int num = i;
        for (int j = 0; j < length; j++)
        {
            sb.Append(ops[num % baseValue]);
            num /= baseValue;
        }
        combinations.Add(sb.ToString());
    }

    return combinations;
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