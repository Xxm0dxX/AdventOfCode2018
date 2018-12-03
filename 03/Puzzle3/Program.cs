using System;
using System.IO;
using System.Linq;

namespace Puzzle3
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("./input.txt");
            var claims = lines.Select(line =>
            {
                var data = line.Split(' ');
                var id = int.Parse(data[0].Substring(1, data[0].Length - 1));

                var _pos = data[2].Split(',');
                var left = int.Parse(_pos[0]);
                var top = int.Parse(_pos[1].Substring(0, _pos[1].Length - 1));

                var _size = data[3].Split('x');
                var x = int.Parse(_size[0]);
                var y = int.Parse(_size[1]);

                return (id, left, top, x, y);
            });

            var sizeX = claims.Max(x => x.left) + claims.Max(x => x.x);
            var sizeY = claims.Max(x => x.top) + claims.Max(x => x.y);

            var arr = new int[sizeX * sizeY];

            foreach (var claim in claims)
            {
                for (int i = 0; i < claim.x; i++)
                {
                    for (int j = 0; j < claim.y; j++)
                    {
                        var x = claim.left + i;
                        var y = claim.top + j;

                        arr[y * sizeY + x]++;
                    }
                }
            }

            var nonOverlapId = 0;
            foreach (var claim in claims)
            {
                var _break = false;
                for (int i = 0; i < claim.x; i++)
                {
                    for (int j = 0; j < claim.y; j++)
                    {
                        var x = claim.left + i;
                        var y = claim.top + j;

                        if (arr[y * sizeY + x] > 1)
                        {
                            _break = true;
                            break;
                        }
                    }
                    if (_break)
                    {
                        break;
                    }
                }
                if (_break)
                {
                    _break = false;
                }
                else
                {
                    nonOverlapId = claim.id;
                    break;
                }
            }

            var res = arr.Count(x => x > 1);
            Console.WriteLine($"Result (Part 1): {res}");
            Console.WriteLine($"Result (Part 2): {nonOverlapId}");
        }
    }
}
