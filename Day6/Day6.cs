using System;

namespace Day6;

class Program
{
    public static long SolvePart1(string[] lines)
    {
        int maxLength = lines.Max(l => l.Length);

        string[] paddedLines = lines.Select(l => l.PadRight(maxLength)).ToArray();

        string[] dataLines = paddedLines.Take(paddedLines.Length - 1).ToArray();
        string operatorLine = paddedLines[paddedLines.Length - 1];

        var problems = new List<(List<long> numbers, char op)>();
        var currentNumbers = new List<string>();
        int colStart = 0;
        bool inProblem = false;

        for (int col = 0; col <= maxLength; col++)
        {
            bool isSpaceColumn = col == maxLength;

            if (col < maxLength)
            {
                isSpaceColumn = true;
                for (int row = 0; row < paddedLines.Length; row++)
                {
                    if (col < paddedLines[row].Length && paddedLines[row][col] != ' ')
                    {
                        isSpaceColumn = false;
                        break;
                    }
                }
            }

            if (isSpaceColumn)
            {
                if (inProblem)
                {
                    var numbers = new List<long>();
                    char op = ' ';

                    for (int row = 0; row < dataLines.Length; row++)
                    {
                        string segment = dataLines[row].Substring(colStart, col - colStart).Trim();
                        if (!string.IsNullOrEmpty(segment))
                        {
                            numbers.Add(long.Parse(segment));
                        }
                    }

                    string opSegment = operatorLine.Substring(colStart, col - colStart).Trim();
                    if (!string.IsNullOrEmpty(opSegment))
                    {
                        op = opSegment[0];
                    }

                    if (numbers.Count > 0 && (op == '+' || op == '*'))
                    {
                        problems.Add((numbers, op));
                    }

                    inProblem = false;
                }
            }
            else
            {
                if (!inProblem)
                {
                    colStart = col;
                    inProblem = true;
                }
            }
        }

        long grandTotal = 0;
        foreach (var (numbers, op) in problems)
        {
            long result;
            if (op == '+')
            {
                result = numbers.Sum();
            }
            else
            {
                result = numbers.Aggregate(1L, (acc, n) => acc * n);
            }
            grandTotal += result;
        }

        return grandTotal;
    }

    public static long SolvePart2(string[] lines)
    {
        int maxLength = lines.Max(l => l.Length);

        string[] paddedLines = lines.Select(l => l.PadRight(maxLength)).ToArray();

        string[] dataLines = paddedLines.Take(paddedLines.Length - 1).ToArray();
        string operatorLine = paddedLines[paddedLines.Length - 1];

        var problemRanges = new List<(int start, int end)>();
        int colStart = -1;
        bool inProblem = false;

        for (int col = 0; col <= maxLength; col++)
        {
            bool isSpaceColumn = col == maxLength;

            if (col < maxLength)
            {
                isSpaceColumn = true;
                for (int row = 0; row < paddedLines.Length; row++)
                {
                    if (paddedLines[row][col] != ' ')
                    {
                        isSpaceColumn = false;
                        break;
                    }
                }
            }

            if (isSpaceColumn)
            {
                if (inProblem)
                {
                    problemRanges.Add((colStart, col));
                    inProblem = false;
                }
            }
            else
            {
                if (!inProblem)
                {
                    colStart = col;
                    inProblem = true;
                }
            }
        }

        long grandTotal = 0;

        foreach (var (start, end) in problemRanges)
        {
            char op = ' ';
            for (int c = start; c < end; c++)
            {
                if (operatorLine[c] == '+' || operatorLine[c] == '*')
                {
                    op = operatorLine[c];
                    break;
                }
            }

            var numbers = new List<long>();
            for (int col = end - 1; col >= start; col--)
            {
                var digits = new List<char>();
                for (int row = 0; row < dataLines.Length; row++)
                {
                    char c = dataLines[row][col];
                    if (char.IsDigit(c))
                    {
                        digits.Add(c);
                    }
                }

                if (digits.Count > 0)
                {
                    long number = long.Parse(new string(digits.ToArray()));
                    numbers.Add(number);
                }
            }

            if (numbers.Count > 0 && (op == '+' || op == '*'))
            {
                long result;
                if (op == '+')
                {
                    result = numbers.Sum();
                }
                else
                {
                    result = numbers.Aggregate(1L, (acc, n) => acc * n);
                }
                grandTotal += result;
            }
        }

        return grandTotal;
    }

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("Day6.txt")
            .Where(l => !string.IsNullOrEmpty(l))
            .ToArray();

        long part1 = SolvePart1(lines);
        Console.WriteLine("Part 1 - Grand Total: {0}", part1);

        long part2 = SolvePart2(lines);
        Console.WriteLine("Part 2 - Grand Total: {0}", part2);
    }
}
