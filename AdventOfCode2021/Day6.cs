using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day6
    {
        List<int> _values;

        public Day6()
        {
            Init();
            Part1();
            Part2();
        }

        public void Init()
        {
            var values = File.ReadLines("../../inputs/input_day6.txt").ToList()[0];
            _values = values.Split(',').Select(x => Convert.ToInt32(x)).ToList();
        }

        public void Part1()
        {
            var day = 0;
            var initialValues = _values.ToList();

            while (day < 80)
            {
                var nbFishToAdd = 0;

                for (int i = 0; i < initialValues.Count; i++)
                {
                    var value = initialValues[i];

                    if (value == 0)
                    {
                        initialValues[i] = 6;
                        nbFishToAdd++;
                    }
                    else
                        initialValues[i]--;
                }

                for (int i = 0; i < nbFishToAdd; i++)
                    initialValues.Add(8);

                day++;
            }

            var result = initialValues.Count;
            Console.WriteLine(result);
        }

        public void Part2()
        {
            // CAUTION /!\ This algorithm was done by a professional, do not reproduce at home /!\.

            var day = 0;
            var nbFishPerDays = new double[9];

            for (int i = 0; i < _values.Count; i++)
            {
                nbFishPerDays[_values[i]]++;
            }

            while (day < 256)
            {
                var day0 = nbFishPerDays[0];
                var day1 = nbFishPerDays[1];
                var day2 = nbFishPerDays[2];
                var day3 = nbFishPerDays[3];
                var day4 = nbFishPerDays[4];
                var day5 = nbFishPerDays[5];
                var day6 = nbFishPerDays[6];
                var day7 = nbFishPerDays[7];
                var day8 = nbFishPerDays[8];

                nbFishPerDays[8] = day0;
                nbFishPerDays[7] = day8;
                nbFishPerDays[6] = day7;
                nbFishPerDays[6] += day0;
                nbFishPerDays[5] = day6;
                nbFishPerDays[4] = day5;
                nbFishPerDays[3] = day4;
                nbFishPerDays[2] = day3;
                nbFishPerDays[1] = day2;
                nbFishPerDays[0] = day1;

                day++;
            }

            double total = nbFishPerDays.Sum();
            Console.WriteLine(total);
        }
    }
}
