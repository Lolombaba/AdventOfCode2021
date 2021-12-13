using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day1
    {
        private List<int> _intValues;

        public Day1()
        {
            Init();
            Part1(_intValues);
            Part2(_intValues);
        }

        public void Init()
        {
            var values = File.ReadLines("../../inputs/input_day1.txt").ToList();
            _intValues = values.Select(v => Convert.ToInt32(v)).ToList();
        }

        public void Part1(List<int> intValues)
        {
            var increasedNb = 0;

            for (int i = 0; i < intValues.Count; i++)
            {
                if (i == 0)
                    continue;

                if (intValues[i] > intValues[i - 1])
                    increasedNb++;
            }

            Console.WriteLine(increasedNb);
        }

        public void Part2(List<int> intValues)
        {
            var results = new List<int>();

            for (int i = 0; i < intValues.Count; i++)
            {
                if (i + 3 > intValues.Count)
                    break;

                var sum = 0;

                for (int j = i; j < i + 3; j++)
                {
                    sum += intValues[j];
                }

                results.Add(sum);
            }

            Part1(results);
        }
    }
}
