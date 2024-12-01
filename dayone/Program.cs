using static System.Console;

var lines = File.ReadAllLines("input.txt");
int count = lines.Length;

var left = new int[count];
var right = new int[count];

for (int i = 0; i < count; i++)
{
    var line = lines[i];
    var strings = line.Split(' ', 2);
    left[i] = int.Parse(strings[0]);
    right[i] = int.Parse(strings[1]);
}

Array.Sort(left);
Array.Sort(right);

var distanceSum = 0;

for (int i = 0; i < count; i++)
{
    var distance = Math.Abs(left[i] - right[i]);
    WriteLine($"Left is {left[i]} and Right is {right[i]} Distance is: {distance}");
    distanceSum += distance;
}

WriteLine($"Distance Sum is {distanceSum}");

var similarityScore = 0;

for (int i = 0; i < count; i++)
{
    var search = left[i];
    var found = 0;
    for (int j = 0; j < count; j++)
    {
        var number = right[j];
        if(search == number)
        {
            found++;
        }
    }
    var multiply = search * found;
    WriteLine($"Searched for {search} and found it {found}, so {search} * {found} = {multiply}");

    similarityScore += multiply;
}

WriteLine($"Similarity Score is {similarityScore}");