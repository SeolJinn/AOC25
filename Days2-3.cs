using System;

namespace Day2;

class Program
{
    // Day 2
    private static bool IsDoubled(string number)
    {
        if (number.Length % 2 != 0) 
            return false;
        
        int half = number.Length / 2;
        return number[..half] == number[half..];
    }

    // Day 3
    private static bool IsRepeatingPattern(string number)
    {
        int length = number.Length;
        
        for (int patternLen = 1; patternLen <= length / 2; patternLen++)
        {
            if (length % patternLen != 0)
                continue;
            
            string pattern = number[..patternLen];
            int repeatCount = length / patternLen;
            
            if (string.Concat(Enumerable.Repeat(pattern, repeatCount)) == number)
                return true;
        }
        
        return false;
    }

    static void Main(string[] args)
    {        
        var input = File.ReadAllText("Days2-3.txt");
        var ranges = input.Split(",");

        long sum = 0;

        foreach (var range in ranges)
        {
            var number1 = long.Parse(range.Split("-")[0]);
            var number2 = long.Parse(range.Split("-")[1]);

            for(long i = number1; i <= number2; i++)
            {
                if(IsRepeatingPattern(i.ToString()))
                    sum += i;
            }
        }

        Console.WriteLine($"Sum: {sum}");
    }
}