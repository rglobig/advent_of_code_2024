using static Helper;

class PartTwo
{
    public void CalculateAndPrint(string input)
    {
        Console.WriteLine("====== PART TWO ======");

        var diskSpaces = ParseDiskMapToDiskSpace(input);

        Print(diskSpaces);

        PrintDiskSpace(diskSpaces);

        diskSpaces = MoveFiles(diskSpaces);

        Print(diskSpaces);

        PrintDiskSpace(diskSpaces);

        var sum = CalculateCheckSum(diskSpaces);
        Console.WriteLine("Sum is:" + sum);
        Console.WriteLine("======================");
    }

    SortedDictionary<int, DiskSpace> MoveFiles(SortedDictionary<int, DiskSpace> diskSpaces)
    {
        for (int i = diskSpaces.Count - 1; i >= 0; i--)
        {
            if (!diskSpaces.ContainsKey(i)) continue;

            var diskSpace = diskSpaces[i];

            if (diskSpace is DiskFree) continue;

            var file = (diskSpace as DiskFile)!;

            if (!TryFindFreePlace(diskSpaces, file.Size, file.BlockIndex - file.Size, out int startFreeIndex)) continue;

            var freeIndex = startFreeIndex;

            for (var j = file.BlockIndex + file.Size - 1; j >= file.BlockIndex; j--, freeIndex++)
            {
                var currentFile = (diskSpaces[j] as DiskFile)!;
                currentFile = currentFile with { BlockIndex = startFreeIndex, Moved = true };
                diskSpaces[freeIndex] = currentFile;
                diskSpaces[j] = new DiskFree('.', file.Size);
            }
            AdjustFreeSpaceFromIndexTillNextFile(diskSpaces, startFreeIndex + file.Size);
        }

        return diskSpaces;
    }

    void AdjustFreeSpaceFromIndexTillNextFile(SortedDictionary<int, DiskSpace> diskSpaces, int index)
    {
        var size = 0;
        for (int i = index; i < diskSpaces.Count; i++)
        {
            if (diskSpaces[i] is DiskFile) break;
            size++;
        }

        for (int i = index; i < index + size; i++)
        {
            diskSpaces[i] = new DiskFree('.', size);
        }
    }

    bool TryFindFreePlace(SortedDictionary<int, DiskSpace> diskSpaces, int size, int maxIndex, out int index)
    {
        for (int i = 0; i < maxIndex; i++)
        {
            if(diskSpaces[i] is DiskFree free && free.Size >=size)
            {
                index = i;
                return true;
            }
        }
        index = -1;
        return false;
    }
}
