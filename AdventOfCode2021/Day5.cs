using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day5
    {
        public class Line
        {
            public List<Point> Points = new List<Point>();
        }

        public class Point
        {
            public int X;
            public int Y;

            public string Key { get => $"{X},{Y}"; }
        }

        List<string> _values;

        public Day5()
        {
            Init();
            Part1();
            Part2();
        }

        public void Init()
        {
            _values = File.ReadLines("../../inputs/input_day5.txt").ToList();
        }

        public void Part1()
        {
            var lines = new List<Line>();

            foreach (var value in _values)
            {
                var line = new Line();

                var tmp = value.Split(new string[] { " -> " }, StringSplitOptions.None);

                var beginX = Convert.ToInt32(tmp[0].Split(',')[0]);
                var endX = Convert.ToInt32(tmp[1].Split(',')[0]);

                var beginY = Convert.ToInt32(tmp[0].Split(',')[1]);
                var endY = Convert.ToInt32(tmp[1].Split(',')[1]);

                // Keep only horizontal or vertical
                if (beginX != endX && beginY != endY)
                    continue;

                var realBeginX = endX > beginX ? beginX : endX;
                var realEndX = endX > beginX ? endX : beginX;
                var realBeginY = endY > beginY ? beginY : endY;
                var realEndY = endY > beginY ? endY : beginY;

                for (int x = realBeginX; x <= realEndX; x++)
                {
                    for (int y = realBeginY; y <= realEndY; y++)
                    {
                        line.Points.Add(new Point() { X = x, Y = y, });
                    }
                }

                lines.Add(line);
            }

            var dico = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                foreach (var point in line.Points)
                {
                    if (dico.ContainsKey(point.Key))
                        dico[point.Key]++;
                    else
                        dico.Add(point.Key, 1);
                }
            }

            var result = dico.Count(x => x.Value > 1);
            Console.WriteLine(result);
        }

        public void Part2()
        {
            var lines = new List<Line>();

            foreach (var value in _values)
            {
                var line = new Line();

                var tmp = value.Split(new string[] { " -> " }, StringSplitOptions.None);

                var beginX = Convert.ToInt32(tmp[0].Split(',')[0]);
                var endX = Convert.ToInt32(tmp[1].Split(',')[0]);

                var beginY = Convert.ToInt32(tmp[0].Split(',')[1]);
                var endY = Convert.ToInt32(tmp[1].Split(',')[1]);

                if (beginX == endX || beginY == endY || (Math.Abs(beginX-endX) == Math.Abs(beginY-endY)))
                {
                    var X1 = endX > beginX ? beginX : endX;
                    var X2 = endX > beginX ? endX : beginX;
                    var Y1 = endY > beginY ? beginY : endY;
                    var Y2 = endY > beginY ? endY : beginY;

                    for (int x = X1; x <= X2; x++)
                    {
                        for (int y = Y1; y <= Y2; y++)
                        {
                            // the point
                            if (x == beginX && y == beginY || x == endX && y == endY)
                            {
                                line.Points.Add(new Point() { X = x, Y = y, });
                                continue;
                            }

                            // an horizontal or vertical point on the line
                            if (beginX == endX || beginY == endY)
                            {
                                line.Points.Add(new Point() { X = x, Y = y, });
                                continue;
                            }

                            // a 45 degree point on the line
                            if (Math.Abs(endX - x) != 0 && (double)(Math.Abs((double)(endY - y)) / Math.Abs((double)(endX - x))) == 1d)
                                line.Points.Add(new Point() { X = x, Y = y, });
                        }
                    }

                    lines.Add(line);
                }
            }

            var dico = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                foreach (var point in line.Points)
                {
                    if (dico.ContainsKey(point.Key))
                        dico[point.Key]++;
                    else
                        dico.Add(point.Key, 1);
                }
            }

            var result = dico.Count(x => x.Value > 1);
            Console.WriteLine(result);
        }
    }
}
