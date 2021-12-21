using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day9
    {
        public class Point
        {
            public int Value { get; set; }
            public int Row { get; set; }
            public int Col { get; set; }
            public bool AlreadyAdded { get; set; }
        }

        private List<string> _values;
        private Point[][] _myArray;
        private int _nbRows;
        private int _nbCols;
        private List<Point> _lowPoints = new List<Point>();


        public Day9()
        {
            Init();
            Part1();
            Part2();
        }

        public void Init()
        {
            _values = File.ReadLines("../../inputs/input_day9.txt").ToList();
            _nbRows = _values.Count;
            _nbCols = _values[0].Length;
            _myArray = new Point[_nbRows][];

            // Parse input and build array
            for (int i = 0; i < _nbRows; i++)
            {
                _myArray[i] = new Point[_nbCols];

                var row = _values[i];

                for (int j = 0; j < _nbCols; j++)
                {
                    _myArray[i][j] = new Point { Value = Convert.ToInt32(row[j].ToString()), Row = i, Col = j };
                }
            }
        }

        public void Part1()
        {
            int sum = 0;

            // Compute low points
            for (int i = 0; i < _nbRows; i++)
            {
                for (int j = 0; j < _nbCols; j++)
                {
                    var point = _myArray[i][j];

                    var topLeft = i > 0 && j > 0 ? _myArray[i - 1][j - 1].Value : int.MaxValue;
                    var top = i > 0 ? _myArray[i - 1][j].Value : int.MaxValue;
                    var topRight = i > 0 && j < _nbCols-1 ? _myArray[i - 1][j + 1].Value : int.MaxValue;

                    var left = j > 0 ? _myArray[i][j - 1].Value : int.MaxValue;
                    var right = j < _nbCols-1 ? _myArray[i][j + 1].Value : int.MaxValue;

                    var bottomLeft = i < _nbRows-1 && j > 0 ? _myArray[i + 1][j - 1].Value : int.MaxValue;
                    var bottom = i < _nbRows-1 ? _myArray[i + 1][j].Value : int.MaxValue;
                    var bottomRight = i < _nbRows-1 && j < _nbCols-1 ? _myArray[i + 1][j + 1].Value : int.MaxValue;

                    var nb = point.Value;

                    if (nb < topLeft && nb < top && nb < topRight && nb < left && nb < right && nb < bottomLeft && nb < bottom && nb < bottomRight)
                    {
                        _lowPoints.Add(point);
                        sum += point.Value + 1;
                    }
                }
            }

            Console.WriteLine(sum);
        }

        public void Part2()
        {
            var basins = new List<List<Point>>();

            foreach (var lowPoint in _lowPoints)
            {
                var basin = new List<Point>();

                basin.Add(lowPoint);
                lowPoint.AlreadyAdded = true;

                GetBasinPoints(basin, lowPoint);

                basins.Add(basin);
            }

            var sorted = basins.OrderByDescending(x => x.Count).Take(3).ToList();
            var result = sorted[0].Count * sorted[1].Count * sorted[2].Count;
            Console.WriteLine(result);
        }


        private void GetBasinPoints(List<Point> basin, Point p)
        {
            if (p == null || p.Value == 9)
                return;

            var top = p.Row > 0 ? _myArray[p.Row - 1][p.Col] : null;
            if (top != null && top.Value > p.Value)
            {
                if (top.Value != 9 && !top.AlreadyAdded)
                    AddPoint(basin, top);

                GetBasinPoints(basin, top);
            }

            var left = p.Col > 0 ? _myArray[p.Row][p.Col - 1] : null;
            if (left != null && left.Value > p.Value)
            {
                if (left.Value != 9 && !left.AlreadyAdded)
                    AddPoint(basin, left);

                GetBasinPoints(basin, left);
            }

            var right = p.Col < _nbCols - 1 ? _myArray[p.Row][p.Col + 1] : null;
            if (right != null && right.Value > p.Value)
            {
                if (right.Value != 9 && !right.AlreadyAdded)
                    AddPoint(basin, right);

                GetBasinPoints(basin, right);
            }

            var bottom = p.Row < _nbRows - 1 ? _myArray[p.Row + 1][p.Col] : null;
            if (bottom != null && bottom.Value > p.Value)
            {
                if (bottom.Value != 9 && !bottom.AlreadyAdded)
                    AddPoint(basin, bottom);

                GetBasinPoints(basin, bottom);
            }
        }

        private void AddPoint(List<Point> basin, Point p)
        {
            basin.Add(p);
            p.AlreadyAdded = true;
        }
    }
}
