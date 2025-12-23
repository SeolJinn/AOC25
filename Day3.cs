using System;

namespace Day3;

class Program
{
    // Day 3, Part 1
    private static long GetMaxJoltagePart1(string line)
    {
        long JoltageFirstDigit = 0;
        long JoltageSecondDigit = 0;
        
        for(int i = 0; i < line.Length - 1; i++)
        {
            var currentJoltage = long.Parse(line[i].ToString());
            if(currentJoltage > JoltageFirstDigit)
            {
                JoltageFirstDigit = currentJoltage;
                JoltageSecondDigit = 0;
            }
            else if(currentJoltage > JoltageSecondDigit)
            {
                JoltageSecondDigit = currentJoltage;
            }
        }

        var lastJoltage = long.Parse(line[line.Length - 1].ToString());
        if(lastJoltage > JoltageSecondDigit)
        {
            JoltageSecondDigit = lastJoltage;
        }

        return JoltageFirstDigit * 10 + JoltageSecondDigit;
    }

    // Day 3, Part 2
    private static long GetMaxJoltagePart2(string row, int count = 12)
    {
        int n = row.Length;
        if (n < count) return 0;
        
        Stack<char> stack = new Stack<char>();
        int toRemove = n - count;
        
        for (int i = 0; i < n; i++)
        {
            while (stack.Count > 0 && stack.Peek() < row[i] && toRemove > 0)
            {
                stack.Pop();
                toRemove--;
            }
            stack.Push(row[i]);
        }
        
        while (toRemove > 0)
        {
            stack.Pop();
            toRemove--;
        }
        
        char[] result = new char[count];
        for (int i = count - 1; i >= 0; i--)
        {
            result[i] = stack.Pop();
        }
        
        return long.Parse(new string(result));
    }

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("Day3.txt");
        long JoltageSum = 0;
        foreach (var line in lines)
        {
            JoltageSum += GetMaxJoltagePart2(line);
        }

        Console.WriteLine("Joltage Sum: {0}", JoltageSum);
    }
}