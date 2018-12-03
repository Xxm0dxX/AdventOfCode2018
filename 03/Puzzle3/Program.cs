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
                return (id: data[0], left: data[1], top: data[2], x: data[3], y: data[4], indexes: new HashSet<int>());
            }).ToList();
            var sizeX = claims.Max(x => x.left) + claims.Max(x => x.x);
            var sizeY = claims.Max(x => x.top) + claims.Max(x => x.y);
            var arr = new int[sizeX * sizeY];

            for (int i = 0; i < claims.Count; i++)
            {
                for (int j = 0; j < claims[i].x; j++)
                {
                    for (int k = 0; k < claims[i].y; k++)
                    {
                        var index = (claims[i].top + k) * sizeY + claims[i].left + j;
                        arr[index]++;
                        claims[i].indexes.Add(index);
                    }
                }
            }
            var newT = claims.FirstOrDefault(claim => claim.indexes.Where(x => arr[x] > 1).Count() == 0).id;
            var res = arr.Count(x => x > 1);

            Console.WriteLine($"Result (Part 1): {res}");
            Console.WriteLine($"Result (Part 2): {newT}");
        }
    }
}
