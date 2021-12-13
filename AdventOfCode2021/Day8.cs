using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day8
    {
        private List<string> _values;

        public Day8()
        {
            Init();
            Part1();
            Part2();
        }

        public void Init()
        {
            _values = File.ReadLines("../../inputs/input_day8.txt").ToList();
        }

        public void Part1()
        {
            var sum = 0;
            var keptNbs = new List<int>{ 2, 4, 3, 7 };

            foreach (var value in _values)
            {
                var letterDigits = value.Split(new string[] { " | " }, StringSplitOptions.None)[1].Split(' ');

                sum += letterDigits.Count(ld => keptNbs.Contains(ld.Length));
            }

            Console.WriteLine(sum);
        }

        public void Part2()
        {
            var dico = new Dictionary<int, string>();
            int sum = 0;

            foreach (var value in _values)
            {
                var splitedValues = value.Split(new string[] { " | " }, StringSplitOptions.None);

                var letterDigits = splitedValues[0].Split(' ').ToList();
                var outputs = splitedValues[1].Split(' ').ToList();

                dico[1] = letterDigits.FirstOrDefault(x => x.Length == 2);
                letterDigits.Remove(dico[1]);

                dico[4] = letterDigits.FirstOrDefault(x => x.Length == 4);
                letterDigits.Remove(dico[4]);

                dico[7] = letterDigits.FirstOrDefault(x => x.Length == 3);
                letterDigits.Remove(dico[7]);

                dico[8] = letterDigits.FirstOrDefault(x => x.Length == 7);
                letterDigits.Remove(dico[8]);

                dico[6] = letterDigits.FirstOrDefault(x => x.Length == 6 && NotContainsAllLetters(dico[1], x));
                letterDigits.Remove(dico[6]);

                dico[3] = letterDigits.FirstOrDefault(x => x.Length == 5 && ContainsAllLetters(dico[7], x));
                letterDigits.Remove(dico[3]);

                dico[9] = letterDigits.FirstOrDefault(x => x.Length == 6 && ContainsAllLetters(dico[3], x));
                letterDigits.Remove(dico[9]);

                dico[0] = letterDigits.FirstOrDefault(x => x.Length == 6);
                letterDigits.Remove(dico[0]);

                var topRight = GetListOfLetters(dico[1]).Except(GetListOfLetters(dico[6])).FirstOrDefault();

                dico[2] = letterDigits.FirstOrDefault(x => x.Contains(topRight));
                letterDigits.Remove(dico[2]);

                dico[5] = letterDigits.FirstOrDefault();

                string outputStrNumber = "";
                foreach (var output in outputs)
                {
                    outputStrNumber += dico.FirstOrDefault(x => MatchList(GetListOfLetters(x.Value), GetListOfLetters(output))).Key;
                }

                sum += Convert.ToInt32(outputStrNumber);
            }

            Console.WriteLine(sum);
        }

        private List<string> GetListOfLetters(string value)
        {
            return value.Select(c => c.ToString()).ToList();
        }

        private bool ContainsAllLetters(string list1, string list2)
        {
            return GetListOfLetters(list1).Intersect(GetListOfLetters(list2)).Count() == GetListOfLetters(list1).Count();
        }

        private bool NotContainsAllLetters(string list1, string list2)
        {
            return !ContainsAllLetters(list1, list2);
        }

        private bool MatchList(List<string> list1, List<string> list2)
        {
            var firstNotSecond = list1.Except(list2).ToList();
            var secondNotFirst = list2.Except(list1).ToList();

            return !firstNotSecond.Any() && !secondNotFirst.Any();
        }
    }
}
