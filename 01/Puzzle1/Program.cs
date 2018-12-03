using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Puzzle1
{
    class Program
    {
        static void Main(string[] args)
        {
            var nums = File.ReadAllLines("./input.txt").Select(line =>
            {
                if (!int.TryParse(line, out var num))
                {
                    return 0;
                }
                return num;
            });

            Console.WriteLine($"Result part1: {nums.Sum()}");
            Console.WriteLine($"Result part2: {nums.ToList().EqSum()}");
        }
    }

    static class Extensions
    {
        public static int EqSum(this List<int> source)
        {
            var table = new HashSet<int>();
            var freq = source[0];
            table.Add(freq);

            var i = 1;
            while (true)
            {
                freq += source[(i++) % source.Count];
                if (table.Contains(freq))
                {
                    break;
                }
                else
                {
                    table.Add(freq);
                }
            }

            return freq;
        }
    }
}
