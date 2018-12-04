using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Puzzle4
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("./input.txt")
                            .OrderBy(line =>
                            {
                                var dateS = Regex.Match(line, @"\[.+\]").ToString();
                                dateS = dateS.Substring(1, dateS.Length - 2);
                                return DateTime.Parse(dateS);
                            });
            // var groups = 
            var groups = GroupLines(lines.ToList());
            var max = groups.Where(g => g.Value.Count == groups.Max(x => x.Value.Count)).FirstOrDefault();

            var mostSleptMinute = max.Value.GroupBy(x => x)
                                           .Select(x => (x, x.Count()))
                                           .OrderByDescending(x => x.Item2)
                                           .First().Item1.First();

            var mostMinutesSleptP2 = groups.Select(group => (group.Key, group.Value.GroupBy(x => x)
                                           .Select(x => (x, x.Count()))
                                           .OrderByDescending(x => x.Item2)
                                           .First())).OrderByDescending(x => x.Item2.Item2).First();

            System.Console.WriteLine($"Answer (Part 1): {mostSleptMinute * max.Key}");
            System.Console.WriteLine($"Answer (Part 2): {mostMinutesSleptP2.Item1 * mostMinutesSleptP2.Item2.Item1.First()}");
        }

        private static Dictionary<int, List<int>> GroupLines(List<string> lines)
        {
            var ret = new Dictionary<int, List<int>>();

            var currentId = int.Parse(Regex.Split(lines[0], @"\D+")[6]);

            for (var i = 1; i < lines.Count; i++)
            {
                var split = Regex.Split(lines[i], @"\D+");
                if (split.Length > 7)
                {
                    currentId = int.Parse(split[6]);
                    continue;
                }

                var fallsAsleep = int.Parse(split[5]);
                var wakesUp = int.Parse(Regex.Split(lines[++i], @"\D+")[5]);
                var range = Enumerable.Range(fallsAsleep, wakesUp - fallsAsleep);
                if (ret.ContainsKey(currentId))
                {
                    ret[currentId].AddRange(range);
                }
                else
                {
                    ret[currentId] = new List<int>();
                    ret[currentId].AddRange(range);
                }
            }
            return ret;
        }
    }
}
