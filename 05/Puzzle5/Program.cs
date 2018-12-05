using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Puzzle5
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = File.ReadAllText("./input.txt");
            var processed = ProcessText(text);
            var min = ProcessTextPart2(text);

            System.Console.WriteLine($"Result (Part 1): {processed.Length}");
            System.Console.WriteLine($"Result (Part 2): {min}");
        }

        // Part 1
        private static string ProcessText(string text)
        {
            var output = new StringBuilder();
            var textList = text.ToList();

            // 65 - 90

            for (int i = 0; i < textList.Count - 1; i++)
            {
                if (Math.Abs(textList[i] - textList[i + 1]) == 32)
                {
                    textList.RemoveAt(i);
                    textList.RemoveAt(i);
                    i -= 2;
                    if (i < 0)
                    {
                        i = -1;
                    }
                }

            }
            return new string(textList.ToArray());
        }

        // Part 2
        private static int ProcessTextPart2(string text)
        {
            var output = new StringBuilder();
            var textList = text.ToList();

            var exclude = Enumerable.Range(65, 25).ToList();
            var lengths = new List<int>();

            foreach (var c in exclude)
            {
                var input = textList.Where(x => x != c && x != (c + 32));
                lengths.Add(ProcessText(new string(input.ToArray())).Length);
            }

            return lengths.Min();
        }
    }
}
