using System;
using System.Text.RegularExpressions;

namespace Day1;

class Program
{
    private static int currentValue = 50;
    private static int Password = 0;

    private static void Move(string direction, int value)
    {
        switch (direction)
        {
            case "L":
                currentValue = ((currentValue - value) % 100 + 100) % 100;
                break;
            case "R":
                currentValue = (currentValue + value) % 100;
                break;
        }

        if(currentValue == 0)
            Password++;
    }

    static void Main(string[] args)
    {
        
        var lines = File.ReadAllLines("Day1.txt");
        foreach (var line in lines)
        {
            var match = Regex.Match(line, @"^([LR])(\d+)$");
            if (match.Success)
            {
                var direction = match.Groups[1].Value;
                var value = int.Parse(match.Groups[2].Value);
                
                Move(direction, value);
            }
        }

        Console.WriteLine($"Password: {Password}");
    }
}