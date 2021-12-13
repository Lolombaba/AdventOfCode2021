using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day3
    {
        private List<string> _values;

        public Day3()
        {
            Init();
            Part1(_values);
            Part2(_values);
        }

        public void Init()
        {
            _values = File.ReadLines("../../inputs/input_day3.txt").ToList();
        }

        public void Part1(List<string> values)
        {
            var nbChar = values[0].Length;

            string binaryGamma = "";
            string binaryEpsilon = "";

            for (int i = 0; i < nbChar; i++)
            {
                var list = values.Select(x => x[i]).ToList();
                var nb1 = list.Count(x => x == '1');
                var nb0 = list.Count - nb1;
                binaryGamma += nb1 > nb0 ? '1' : '0';
                binaryEpsilon += nb1 < nb0 ? '1' : '0';
            }

            var decGamma = Convert.ToInt32(binaryGamma, 2);
            var decEspilon = Convert.ToInt32(binaryEpsilon, 2);

            var result = decEspilon * decGamma;
            Console.WriteLine(result);
        }

        public void Part2(List<string> values)
        {
            var nbChar = values[0].Length;
            var currentValues = values.ToList();

            for (int i = 0; i < nbChar; i++)
            {
                var list = currentValues.Select(x => x[i]).ToList();
                var nb1 = list.Count(x => x == '1');
                var nb0 = list.Count - nb1;

                var winner = nb1 >= nb0 ? '1' : '0';
                currentValues = currentValues.Where(x => x[i] == winner).ToList();

                if (currentValues.Count == 1)
                    break;
            }

            var oxygenRating = Convert.ToInt32(currentValues[0], 2);

            currentValues = values.ToList();

            for (int i = 0; i < nbChar; i++)
            {
                var list = currentValues.Select(x => x[i]).ToList();
                var nb1 = list.Count(x => x == '1');
                var nb0 = list.Count - nb1;

                var winner = nb1 < nb0 ? '1' : '0';
                currentValues = currentValues.Where(x => x[i] == winner).ToList();

                if (currentValues.Count == 1)
                    break;
            }

            var co2Rating = Convert.ToInt32(currentValues[0], 2);

            var result = oxygenRating * co2Rating;
            Console.WriteLine(result);
        }
    }
}
