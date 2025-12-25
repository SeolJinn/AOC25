using System;

namespace Day7;

class Program
{
    public static int SolvePart1(string[] lines)
    {
        int width = lines[0].Length;
        int height = lines.Length;

        int startCol = -1;
        for (int col = 0; col < width; col++)
        {
            if (lines[0][col] == 'S')
            {
                startCol = col;
                break;
            }
        }

        HashSet<int> activeBeams = new HashSet<int> { startCol };
        int splitCount = 0;

        for (int row = 1; row < height; row++)
        {
            HashSet<int> newBeams = new HashSet<int>();

            foreach (int col in activeBeams)
            {
                if (col < 0 || col >= width) continue;

                char cell = lines[row][col];
                if (cell == '^')
                {
                    splitCount++;
                    newBeams.Add(col - 1);
                    newBeams.Add(col + 1);
                }
                else
                {
                    newBeams.Add(col);
                }
            }

            newBeams.RemoveWhere(c => c < 0 || c >= width);
            activeBeams = newBeams;

            if (activeBeams.Count == 0) break;
        }

        return splitCount;
    }

    public static long SolvePart2(string[] lines)
    {
        int width = lines[0].Length;
        int height = lines.Length;

        int startCol = -1;
        for (int col = 0; col < width; col++)
        {
            if (lines[0][col] == 'S')
            {
                startCol = col;
                break;
            }
        }

        Dictionary<int, long> timelines = new Dictionary<int, long> { { startCol, 1 } };

        for (int row = 1; row < height; row++)
        {
            Dictionary<int, long> newTimelines = new Dictionary<int, long>();

            foreach (var (col, count) in timelines)
            {
                if (col < 0 || col >= width) continue;

                char cell = lines[row][col];
                if (cell == '^')
                {
                    int leftCol = col - 1;
                    int rightCol = col + 1;

                    if (leftCol >= 0)
                    {
                        if (!newTimelines.ContainsKey(leftCol))
                            newTimelines[leftCol] = 0;
                        newTimelines[leftCol] += count;
                    }

                    if (rightCol < width)
                    {
                        if (!newTimelines.ContainsKey(rightCol))
                            newTimelines[rightCol] = 0;
                        newTimelines[rightCol] += count;
                    }
                }
                else
                {
                    if (!newTimelines.ContainsKey(col))
                        newTimelines[col] = 0;
                    newTimelines[col] += count;
                }
            }

            timelines = newTimelines;

            if (timelines.Count == 0) break;
        }

        return timelines.Values.Sum();
    }

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("Day7.txt")
            .Where(l => !string.IsNullOrEmpty(l))
            .ToArray();

        int part1 = SolvePart1(lines);
        Console.WriteLine("Part 1 - Total splits: {0}", part1);

        long part2 = SolvePart2(lines);
        Console.WriteLine("Part 2 - Total timelines: {0}", part2);
    }
}
