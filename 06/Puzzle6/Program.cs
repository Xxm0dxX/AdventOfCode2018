using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Puzzle6
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = new Stopwatch();
            watch.Start();

            var lines = File.ReadLines("./input.txt").Select((line, index) =>
            {
                var split = line.Split(", ");
                return (index: index, y: int.Parse(split[0]), x: int.Parse(split[1]));
            }).ToList();

            var maxX = lines.Max(x => x.x) + 2;
            var maxY = lines.Max(x => x.y) + 2;

            var closest = new int[maxX, maxY];

            var part2Count = 0;

            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    var distances = lines.Select((l, index) => (index, Distance(l.x, l.y, i, j)));
                    if (distances.Sum(x => x.Item2) < 10000)
                    {
                        part2Count++;
                    }

                    var _close = distances.OrderBy(x => x.Item2).First();
                    if (distances.Count(x => x.Item2 == _close.Item2) > 1)
                    {
                        closest[i, j] = -1;
                    }
                    else
                    {
                        closest[i, j] = _close.index;
                    }
                }
            }

            var viables = lines.Where((_, index) => IsViable(closest, index)).Select(x => x.index);
            var maxRegion = viables.Select(x => (x, RegionSize(closest, x))).OrderByDescending(x => x.Item2).First();
            System.Console.WriteLine($"Result (Part1): {maxRegion.Item2}");
            System.Console.WriteLine($"Result (Part2): {part2Count}");

            watch.Stop();
            System.Console.WriteLine($"Time elapsed: {watch.ElapsedMilliseconds}ms.");
        }
        private static int RegionSize(int[,] arr, int index)
        {
            var region = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == index)
                    {
                        region++;
                    }
                }
            }
            return region;
        }

        private static bool IsViable(int[,] arr, int index)
        {
            var minX = int.MaxValue;
            var maxX = 0;
            var minY = int.MaxValue;
            var maxY = 0;

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == index)
                    {
                        if (i > maxX)
                        {
                            maxX = i;
                        }
                        if (i < minX)
                        {
                            minX = i;
                        }
                        if (j > maxY)
                        {
                            maxY = j;
                        }
                        if (j < minY)
                        {
                            minY = j;
                        }
                    }
                }
            }

            return (minX > 0 && minY > 0 && maxX < (arr.GetLength(0) - 1) && maxY < (arr.GetLength(1) - 1));
        }
        private static int Distance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }
    }
}
