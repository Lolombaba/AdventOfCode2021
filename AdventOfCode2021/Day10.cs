using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day10
    {
        private List<string> _values;
        private List<string> _incompleteValues = new List<string>();

        public Day10()
        {
            Init();
            Part1();
            Part2();
        }

        public void Init()
        {
            _values = File.ReadLines("../../inputs/input_day10.txt").ToList();
        }

        public void Part1()
        {
            var wrongCharacters = new List<char>();
            int sum = 0;

            foreach (var value in _values)
            {
                var corrupted = false;
                var tmp = new List<char>();

                foreach (var character in value)
                {
                    if (character == '(' || character == '[' || character == '{' || character == '<')
                        tmp.Add(character);

                    else
                    {
                        var previous = tmp[tmp.Count - 1];
                        tmp.RemoveAt(tmp.Count - 1);

                        if (character == ')' && previous != '(' ||
                            character == ']' && previous != '[' ||
                            character == '}' && previous != '{' ||
                            character == '>' && previous != '<')
                        {
                            wrongCharacters.Add(character);
                            sum += GetPointsPart1(character);
                            corrupted = true;
                            break;
                        }
                    }
                }

                if (!corrupted)
                    _incompleteValues.Add(value);
            }

            Console.WriteLine(sum);
        }

        public void Part2()
        {
            var totalScores = new List<double>();

            foreach (var value in _incompleteValues)
            {
                var tmp = new List<char>();

                foreach (var character in value)
                {
                    if (character == '(' || character == '[' || character == '{' || character == '<')
                        tmp.Add(character);

                    else
                        tmp.RemoveAt(tmp.Count - 1);
                }

                double totalScore = 0;

                for (int i = tmp.Count-1; i >= 0; i--)
                {
                    totalScore *= 5;
                    totalScore += GetPointsPart2(GetOpposite(tmp[i]));
                }

                totalScores.Add(totalScore);
            }

            totalScores.Sort();
            Console.WriteLine(totalScores[totalScores.Count / 2]);
        }

        private int GetPointsPart1(char c)
        {
            switch(c)
            {
                case ')':
                    return 3;
                case ']':
                    return 57;
                case '}':
                    return 1197;
                case '>':
                    return 25137;
                default:
                    throw new Exception("unknown character");
            }
        }

        private char GetOpposite(char c)
        {
            switch (c)
            {
                case '(':
                    return ')';
                case '[':
                    return ']';
                case '{':
                    return '}';
                case '<':
                    return '>';
                default:
                    throw new Exception("unknown character");
            }
        }

        private int GetPointsPart2(char c)
        {
            switch (c)
            {
                case ')':
                    return 1;
                case ']':
                    return 2;
                case '}':
                    return 3;
                case '>':
                    return 4;
                default:
                    throw new Exception("unknown character");
            }
        }
    }
}
