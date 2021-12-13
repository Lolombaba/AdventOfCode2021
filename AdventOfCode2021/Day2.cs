using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day2
    {
        private List<string> _values;

        public Day2()
        {
            Init();
            Part1(_values);
            Part2(_values);
        }

        public void Init()
        {
            _values = File.ReadLines("../../inputs/input_day2.txt").ToList();
        }

        public void Part1(List<string> values)
        {
            var forwardList = values.Where(x => x.Contains("forward")).Select(x => Convert.ToInt32(x.Split(' ')[1])).ToList();
            var upList = values.Where(x => x.Contains("up")).Select(x => Convert.ToInt32(x.Split(' ')[1])).ToList();
            var downList = values.Where(x => x.Contains("down")).Select(x => Convert.ToInt32(x.Split(' ')[1])).ToList();

            var xPosition = forwardList.Sum();
            var depth = downList.Sum() - upList.Sum();

            var result = xPosition * depth;
            Console.WriteLine(result);
        }

        public void Part2(List<string> values)
        {
            var xPosition = 0;
            var aim = 0;
            var depth = 0;

            foreach (var value in values)
            {
                var action = value.Split(' ')[0];
                var nb = Convert.ToInt32(value.Split(' ')[1]);

                switch (action)
                {
                    case "forward":
                        xPosition += nb;
                        depth += aim * nb;
                        break;

                    case "up":
                        aim -= nb;
                        break;

                    case "down":
                        aim += nb;
                        break;
                }
            }

            var result = xPosition * depth;
            Console.WriteLine(result);
        }
    }
}
