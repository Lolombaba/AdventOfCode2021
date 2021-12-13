using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day7
    {
        private List<int> _positions;

        public Day7()
        {
            Init();
            Part1();
            Part2();
        }

        public void Init()
        {
            var values = File.ReadLines("../../inputs/input_day7.txt").ToList()[0];
            _positions = values.Split(',').Select(x => Convert.ToInt32(x)).ToList();
        }

        public void Part1()
        {
            var max = _positions.Max();

            var minValue = int.MaxValue;

            for (int i = 0; i <= max; i++)
            {
                var sumGap = 0;

                var toCompare = _positions.Where(x => x != i).ToList();

                for (int j = 0; j < toCompare.Count; j++)
                {
                    sumGap += Math.Abs(i - toCompare[j]);
                }

                if (sumGap < minValue)
                    minValue = sumGap;
            }

            Console.WriteLine(minValue);
        }

        public void Part2()
        {
            var max = _positions.Max();

            var minValue = int.MaxValue;

            for (int i = 0; i <= max; i++)
            {
                var sumGap = 0;

                var toCompare = _positions.Where(x => x != i).ToList();

                for (int j = 0; j < toCompare.Count; j++)
                {
                    sumGap += GetFuelCost(Math.Abs(i - toCompare[j]));
                }

                if (sumGap < minValue)
                    minValue = sumGap;
            }

            Console.WriteLine(minValue);
        }

        private int GetFuelCost(int value)
        {
            var sum = 0;
            for (int i = 1; i <= value; i++)
            {
                sum += i;
            }
            return sum;
        }
    }
}
