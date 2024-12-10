using static Helper;

class PartOne
{
    public void CalculateAndPrint(string input)
    {
        Console.WriteLine("====== PART ONE ======");

        var diskSpaces = ParseDiskMapToDiskSpace(input);

        PrintDiskSpace(diskSpaces);

        diskSpaces = MoveFiles(diskSpaces);

        PrintDiskSpace(diskSpaces);

        var sum = CalculateCheckSum(diskSpaces);
        Console.WriteLine("Sum is:" + sum);
        Console.WriteLine("======================");
    }

    SortedDictionary<int, DiskSpace> MoveFiles(SortedDictionary<int, DiskSpace> diskSpaces)
    {
        int frontIndex = 0;

        for (int i = diskSpaces.Count - 1; i >= 0; i--)
        {
            var space = diskSpaces[i];
            if (space is not DiskFile)
            {
                diskSpaces.Remove(i);
                continue;
            }

            if (frontIndex > i) break;

            for (var j = frontIndex; j < diskSpaces.Count; j++, frontIndex++)
            {
                if (j > i) break;
                var frontSpace = diskSpaces[j];
                if (frontSpace is not DiskFree) continue;
                var file = (DiskFile)space;
                diskSpaces[j] = file with { Moved = true };
                diskSpaces.Remove(i);
                break;
            }
        }

        return diskSpaces;
    }
}
