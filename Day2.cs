using System;

namespace Day2;

class Program
{
    private static bool IsDoubled(string number)
    {
        if (number.Length % 2 != 0) 
            return false;
        
        int half = number.Length / 2;
        return number[..half] == number[half..];
    }

    static void Main(string[] args)
    {        
        var input = File.ReadAllText("Day2.txt");
        var ranges = input.Split(",");

        long sum = 0;

        foreach (var range in ranges)
        {
            var number1 = long.Parse(range.Split("-")[0]);
            var number2 = long.Parse(range.Split("-")[1]);

            for(long i = number1; i <= number2; i++)
            {
                if(IsDoubled(i.ToString()))
                    sum += i;
            }
        }

        Console.WriteLine($"Sum: {sum}");
    }
}