using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day4
    {
        public class Board
        {
            public List<Col> Columns;
            public List<Row> Rows;
            public bool Winner;

            public Board()
            {
                Columns = new List<Col> { new Col(), new Col(), new Col(), new Col(), new Col() };
                Rows = new List<Row>();
            }
        }

        public class Col
        {
            public List<Number> Numbers;
            public int NbFound;

            public Col()
            { Numbers = new List<Number>(); }
        }

        public class Row
        {
            public List<Number> Numbers;
            public int NbFound;
        }

        public class Number
        {
            public int Value;
            public bool Marked;
        }

        private List<int> _pickedNumbers;
        private List<Board> _boards = new List<Board>();

        public Day4()
        {
            Init();
            Part1();
            Part2();
        }

        public void Init()
        {
            var values = File.ReadLines("../../inputs/input_day4.txt").ToList();
            _pickedNumbers = values[0].Split(',').Select(x => Convert.ToInt32(x)).ToList();
            values.RemoveAt(0);
            values.RemoveAt(0);

            int idx = 0;
            int boardIdx = 0;
            _boards.Add(new Board());

            while(idx < values.Count)
            {
                var value = values[idx];

                if (value == "")
                {
                    var currentb = _boards[boardIdx];
                    foreach (var row in currentb.Rows)
                    {
                        for (int i = 0; i < 5; i++)
                            currentb.Columns[i].Numbers.Add(new Number { Value = row.Numbers[i].Value });
                    }

                    _boards.Add(new Board());
                    boardIdx++;
                }
                else
                    _boards[boardIdx].Rows.Add(new Row { Numbers = value.Split(' ').Where(x => x != "").Select(x => Convert.ToInt32(x)).Select(x => new Number { Value = x }).ToList() });

                idx++;
            }
        }

        public void Part1()
        {
            bool exit = false;
            int justCalledNb = 0;

            foreach (var pickedNb in _pickedNumbers)
            {
                if (exit)
                    break;

                foreach (var board in _boards)
                {
                    foreach (var column in board.Columns)
                    {
                        foreach (var nb in column.Numbers)
                        {
                            if (nb.Value == pickedNb)
                            {
                                nb.Marked = true;
                                column.NbFound++;
                            }
                        }
                    }

                    if (board.Columns.Any(c => c.NbFound == 5))
                    {
                        justCalledNb = pickedNb;
                        board.Winner = true;
                        exit = true;
                        break;
                    }

                    foreach (var row in board.Rows)
                    {
                        foreach (var nb in row.Numbers)
                        {
                            if (nb.Value == pickedNb)
                            {
                                nb.Marked = true;
                                row.NbFound++;
                            }
                        }
                    }

                    if (board.Rows.Any(c => c.NbFound == 5))
                    {
                        justCalledNb = pickedNb;
                        board.Winner = true;
                        exit = true;
                        break;
                    }
                }
            }

            var winningBoard = _boards.FirstOrDefault(b => b.Winner);

            var unMarkedSum = 0;
            foreach (var col in winningBoard.Columns)
            {
                unMarkedSum += col.Numbers.Where(x => !x.Marked).Select(x => x.Value).Sum();
            }

            var result = justCalledNb * unMarkedSum;
            Console.WriteLine(result);
        }

        public void Part2()
        {
            _boards.Clear();
            Init();

            var winningBoards = new List<Board>();
            bool exit = false;
            int justCalledNb = 0;

            foreach (var pickedNb in _pickedNumbers)
            {
                if (exit)
                    break;

                foreach (var board in _boards)
                {
                    if (winningBoards.Contains(board))
                        continue;

                    foreach (var column in board.Columns)
                    {
                        foreach (var nb in column.Numbers)
                        {
                            if (nb.Value == pickedNb)
                            {
                                nb.Marked = true;
                                column.NbFound++;
                            }
                        }
                    }

                    if (board.Columns.Any(c => c.NbFound == 5))
                    {
                        winningBoards.Add(board);

                        if (winningBoards.Count == _boards.Count)
                        {
                            board.Winner = true;
                            exit = true;
                            justCalledNb = pickedNb;
                            break;
                        }

                        continue;
                    }

                    foreach (var row in board.Rows)
                    {
                        foreach (var nb in row.Numbers)
                        {
                            if (nb.Value == pickedNb)
                            {
                                nb.Marked = true;
                                row.NbFound++;
                            }
                        }
                    }

                    if (board.Rows.Any(c => c.NbFound == 5))
                    {
                        winningBoards.Add(board);

                        if (winningBoards.Count == _boards.Count)
                        {
                            board.Winner = true;
                            exit = true;
                            justCalledNb = pickedNb;
                            break;
                        }
                        
                        continue;
                    }
                }
            }

            var winningBoard = winningBoards.FirstOrDefault(b => b.Winner);

            var unMarkedSum = 0;
            foreach (var col in winningBoard.Columns)
            {
                unMarkedSum += col.Numbers.Where(x => !x.Marked).Select(x => x.Value).Sum();
            }

            var result = justCalledNb * unMarkedSum;
            Console.WriteLine(result);
        }
    }
}
