using System;

namespace Day8;

class Program
{
    private static int[] parent = null!;
    private static int[] rank = null!;

    private static void InitUnionFind(int n)
    {
        parent = new int[n];
        rank = new int[n];
        for (int i = 0; i < n; i++)
        {
            parent[i] = i;
            rank[i] = 0;
        }
    }

    private static int Find(int x)
    {
        if (parent[x] != x)
            parent[x] = Find(parent[x]);
        return parent[x];
    }

    private static void Union(int x, int y)
    {
        int rootX = Find(x);
        int rootY = Find(y);
        if (rootX == rootY) return;

        if (rank[rootX] < rank[rootY])
            parent[rootX] = rootY;
        else if (rank[rootX] > rank[rootY])
            parent[rootY] = rootX;
        else
        {
            parent[rootY] = rootX;
            rank[rootX]++;
        }
    }

    private static List<(int x, int y, int z)> ParsePoints(string[] lines)
    {
        var points = new List<(int x, int y, int z)>();
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            int z = int.Parse(parts[2]);
            points.Add((x, y, z));
        }
        return points;
    }

    private static List<(double dist, int i, int j)> CalculateDistances(List<(int x, int y, int z)> points)
    {
        int n = points.Count;
        var distances = new List<(double dist, int i, int j)>();
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                double dx = points[i].x - points[j].x;
                double dy = points[i].y - points[j].y;
                double dz = points[i].z - points[j].z;
                double dist = Math.Sqrt(dx * dx + dy * dy + dz * dz);
                distances.Add((dist, i, j));
            }
        }
        distances.Sort((a, b) => a.dist.CompareTo(b.dist));
        return distances;
    }

    public static long SolvePart1(string[] lines, int connectionsToMake = 1000)
    {
        var points = ParsePoints(lines);
        int n = points.Count;
        var distances = CalculateDistances(points);

        InitUnionFind(n);

        int connections = 0;
        foreach (var (dist, i, j) in distances)
        {
            if (connections >= connectionsToMake)
                break;

            Union(i, j);
            connections++;
        }

        var circuitSizes = new Dictionary<int, int>();
        for (int i = 0; i < n; i++)
        {
            int root = Find(i);
            if (!circuitSizes.ContainsKey(root))
                circuitSizes[root] = 0;
            circuitSizes[root]++;
        }

        var sortedSizes = circuitSizes.Values.OrderByDescending(x => x).Take(3).ToList();

        long result = 1;
        foreach (var size in sortedSizes)
            result *= size;

        return result;
    }

    public static long SolvePart2(string[] lines)
    {
        var points = ParsePoints(lines);
        int n = points.Count;
        var distances = CalculateDistances(points);

        InitUnionFind(n);

        int numCircuits = n;
        int lastI = -1, lastJ = -1;

        foreach (var (dist, i, j) in distances)
        {
            if (Find(i) != Find(j))
            {
                lastI = i;
                lastJ = j;
                Union(i, j);
                numCircuits--;

                if (numCircuits == 1)
                    break;
            }
        }

        return (long)points[lastI].x * points[lastJ].x;
    }

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("Day8.txt")
            .Where(l => !string.IsNullOrEmpty(l))
            .ToArray();

        long part1 = SolvePart1(lines);
        Console.WriteLine("Part 1 - Product of three largest circuits: {0}", part1);

        long part2 = SolvePart2(lines);
        Console.WriteLine("Part 2 - Product of X coordinates: {0}", part2);
    }
}
