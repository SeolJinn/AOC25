using System;

namespace Day4;

class Program
{
    private static int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
    private static int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

    // Day 4, Part 1
    private static int CountAccessibleRolls(string[] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;
        int accessibleCount = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (grid[row][col] != '@')
                    continue;

                int adjacentRolls = 0;

                for (int dir = 0; dir < 8; dir++)
                {
                    int newRow = row + dx[dir];
                    int newCol = col + dy[dir];

                    if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                    {
                        if (grid[newRow][newCol] == '@')
                        {
                            adjacentRolls++;
                        }
                    }
                }

                if (adjacentRolls < 4)
                {
                    accessibleCount++;
                }
            }
        }

        return accessibleCount;
    }

    // Day 4, Part 2
    private static int CountTotalRemovableRolls(string[] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;
        int totalRemoved = 0;

        char[][] mutableGrid = new char[rows][];
        for (int i = 0; i < rows; i++)
        {
            mutableGrid[i] = grid[i].ToCharArray();
        }

        while (true)
        {
            List<(int row, int col)> accessible = new List<(int, int)>();

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (mutableGrid[row][col] != '@')
                        continue;

                    int adjacentRolls = 0;

                    for (int dir = 0; dir < 8; dir++)
                    {
                        int newRow = row + dx[dir];
                        int newCol = col + dy[dir];

                        if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                        {
                            if (mutableGrid[newRow][newCol] == '@')
                            {
                                adjacentRolls++;
                            }
                        }
                    }

                    if (adjacentRolls < 4)
                    {
                        accessible.Add((row, col));
                    }
                }
            }

            if (accessible.Count == 0)
                break;

            foreach (var (row, col) in accessible)
            {
                mutableGrid[row][col] = '.';
            }

            totalRemoved += accessible.Count;
        }

        return totalRemoved;
    }

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("Day4.txt");

        int part1 = CountAccessibleRolls(lines);
        Console.WriteLine("Part 1 - Accessible rolls: {0}", part1);

        int part2 = CountTotalRemovableRolls(lines);
        Console.WriteLine("Part 2 - Total removable rolls: {0}", part2);
    }
}
