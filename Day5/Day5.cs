using System;

namespace Day5;

class Program
{
    // Day 5, Part 1
    private static int CountFreshIngredients(List<(long start, long end)> freshRanges, List<long> ingredientIds)
    {
        int freshCount = 0;

        foreach (long id in ingredientIds)
        {
            bool isFresh = false;
            foreach (var range in freshRanges)
            {
                if (id >= range.start && id <= range.end)
                {
                    isFresh = true;
                    break;
                }
            }

            if (isFresh)
            {
                freshCount++;
            }
        }

        return freshCount;
    }

    // Day 5, Part 2
    private static long CountTotalFreshIds(List<(long start, long end)> freshRanges)
    {
        if (freshRanges.Count == 0)
            return 0;

        var sortedRanges = freshRanges.OrderBy(r => r.start).ThenBy(r => r.end).ToList();

        var mergedRanges = new List<(long start, long end)>();
        var current = sortedRanges[0];

        for (int i = 1; i < sortedRanges.Count; i++)
        {
            var next = sortedRanges[i];

            if (next.start <= current.end + 1)
            {
                current = (current.start, Math.Max(current.end, next.end));
            }
            else
            {
                mergedRanges.Add(current);
                current = next;
            }
        }

        mergedRanges.Add(current);

        long totalIds = 0;
        foreach (var range in mergedRanges)
        {
            totalIds += range.end - range.start + 1;
        }

        return totalIds;
    }

    private static (List<(long start, long end)> ranges, List<long> ids) ParseInput(string[] lines)
    {
        var freshRanges = new List<(long start, long end)>();
        var ingredientIds = new List<long>();
        bool parsingRanges = true;

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                parsingRanges = false;
                continue;
            }

            if (parsingRanges)
            {
                var parts = line.Split('-');
                long start = long.Parse(parts[0]);
                long end = long.Parse(parts[1]);
                freshRanges.Add((start, end));
            }
            else
            {
                ingredientIds.Add(long.Parse(line));
            }
        }

        return (freshRanges, ingredientIds);
    }

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("Day5.txt");
        var (freshRanges, ingredientIds) = ParseInput(lines);

        int part1 = CountFreshIngredients(freshRanges, ingredientIds);
        Console.WriteLine("Part 1 - Fresh ingredients: {0}", part1);

        long part2 = CountTotalFreshIds(freshRanges);
        Console.WriteLine("Part 2 - Total fresh IDs in ranges: {0}", part2);
    }
}
