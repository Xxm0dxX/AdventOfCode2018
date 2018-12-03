using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Puzzle3
{
    class Program
    {
        static void Main(string[] args)
        {
            var claims = File.ReadAllLines("./input.txt").Select(line =>
            {
                var data = Regex.Split(line, @"\D+").Skip(1).Select(x => int.Parse(x)).ToList();
                return (data[0], data[1], data[2], data[3], data[4], new HashSet<int>());
            }).ToList();
            var sizeX = claims.Max(x => x.Item2) + claims.Max(x => x.Item4);
            var sizeY = claims.Max(x => x.Item3) + claims.Max(x => x.Item5);
            var arr = new int[sizeX * sizeY];

            for (int i = 0; i < claims.Count; i++)
            {
                for (int j = 0; j < claims[i].Item4; j++)
                {
                    for (int k = 0; k < claims[i].Item5; k++)
                    {
                        var index = (claims[i].Item3 + k) * sizeY + claims[i].Item2 + j;
                        arr[index]++;
                        claims[i].Item6.Add(index);
                    }
                }
            }
            var newT = claims.FirstOrDefault(claim => claim.Item6.Where(x => arr[x] > 1).Count() == 0).Item1;
            var res = arr.Count(x => x > 1);

            Console.WriteLine($"Result (Part 1): {res}");
            Console.WriteLine($"Result (Part 2): {newT}");
        }
    }
}
