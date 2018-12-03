using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace Puzzle2
{
    class Program
    {
        static void Main(string[] args)
        {
            var twoCount = 0;
            var threeCount = 0;

            var lines = File.ReadAllLines("./input.txt").ToList();
            lines.ForEach(line =>
            {
                var grouped = line.GroupBy(x => x);
                twoCount += grouped.Count(x => x.Count() == 2) > 0 ? 1 : 0;
                threeCount += grouped.Count(x => x.Count() == 3) > 0 ? 1 : 0;
            });

            Console.WriteLine($"Result (Part 1): {twoCount * threeCount}");
            Console.WriteLine($"Result (Part 2): {FindCommonBoxes(lines)}");
        }

        static string FindCommonBoxes(List<string> lines)
        {
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                for (var j = 0; j < lines.Count; j++)
                {
                    if (j != i)
                    {
                        var diff = 0;
                        for (var k = 0; k < line.Length; k++)
                        {
                            if (line[k] != lines[j][k])
                            {
                                diff++;
                            }
                            if (diff > 1)
                            {
                                break;
                            }
                        }
                        if (diff == 1)
                        {
                            var ret = new StringBuilder();
                            for (var k = 0; k < line.Length; k++)
                            {
                                if (line[k] == lines[j][k])
                                {
                                    ret.Append(line[k]);
                                }
                            }
                            return ret.ToString();
                        }
                    }
                }
            }

            return null;
        }
    }
}
