static class Helper
{
    public static SortedDictionary<int, DiskSpace> ParseDiskMapToDiskSpace(string diskMap)
    {
        SortedDictionary<int, DiskSpace> diskSpace = [];

        DiskSpace lastSpace = new DiskFree('.', 0);

        var diskSpaceIndex = 0;
        var fileId = -1;

        for (int i = 0; i < diskMap.Length; i++)
        {
            var type = diskMap[i];
            var size = type - '0';

            if (size == 0)
            {
                if (lastSpace is DiskFree)
                    lastSpace = new DiskFile(0, '0', false, 0, 0);
                else if (lastSpace is DiskFile)
                    lastSpace = new DiskFree('.', 0);
                continue;
            }

            if (lastSpace is DiskFree) fileId++;

            for (var j = 0; j < size; j++)
            {
                if (lastSpace is DiskFree)
                    diskSpace[diskSpaceIndex + j] = new DiskFile(fileId, type, false, size, diskSpaceIndex);
                else if (lastSpace is DiskFile)
                    diskSpace[diskSpaceIndex + j] = new DiskFree('.', size);
            }
            diskSpaceIndex += size;
            lastSpace = diskSpace[diskSpaceIndex - 1];
        }
        return diskSpace;
    }

    public static void PrintDiskSpace(SortedDictionary<int, DiskSpace> diskSpaces)
    {
        Console.WriteLine();
        for (int i = 0; i < diskSpaces.Count; i++)
        {
            var disk = diskSpaces[i];
            string write = disk.Type.ToString();

            if (disk is DiskFree) Console.ForegroundColor = ConsoleColor.Yellow;
            if (disk is DiskFile file)
            {
                write = file.Id.ToString();
                if(file.Moved) Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.Write(write);
            Console.ResetColor();
        }
        Console.WriteLine();
        Console.WriteLine();
    }

    public static void Print(SortedDictionary<int, DiskSpace> diskSpaces)
    {
        foreach (var (i, space) in diskSpaces)
        {
            Console.WriteLine($"{i}: {space}");
        }
    }

    public static long CalculateCheckSum(SortedDictionary<int, DiskSpace> diskSpaces)
    {
        long sum = 0;

        for (int i = 0; i < diskSpaces.Count; i++)
        {
            if (!diskSpaces.TryGetValue(i, out DiskSpace? space)) break;
            if (space is DiskFile file)
            {
                sum += (i * file.Id);
            }
        }

        return sum;
    }
}
