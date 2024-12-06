﻿using System.Diagnostics;

class Day6
{
    public enum Dir
    {
        Up, Right, Down, Left
    }

    public enum Step
    {
        Obstacle, OutOfBounds, Move
    }

    private static char[/*row*/][/*col*/] StringTo2DArray(string text)
    {
        var split = text.Split("\r\n");
        var res = split.Select(s => s.ToArray()).ToArray();

        return res;
    }

    private static (int row, int col) FindGuard(char[][] map)
    {
        for (int row = 0; row < map.Length; row++)
        {
            for (int col = 0; col < map[0].Length; col++)
            {
                if (map[row][col] == '^')
                {
                    return (row, col);
                }
            }
        }

        return (-1, -1);
    }

    private static int CountX(char[][] map)
    {
        var res = 0;
        for (int row = 0; row < map.Length; row++)
        {
            for (int col = 0; col < map[0].Length; col++)
            {
                if (map[row][col] == 'X')
                {
                    res++;
                }
            }
        }

        return res;
    }

    private static int DumpMap(char[][] map)
    {
        var res = 0;
        for (int row = 0; row < map.Length; row++)
        {
            for (int col = 0; col < map[0].Length; col++)
            {
                Console.Write(map[row][col]);
            }
            Console.WriteLine();
        }

        return res;
    }

    public static void Main()
    {
        // part 1
        {
            var map = StringTo2DArray(_input);
            var position = FindGuard(map);
            var direction = Dir.Up;

            while (true)
            {
                var nextRow = position.row;
                var nextCol = position.col;

                if (direction == Dir.Up)
                {
                    nextRow--;
                }
                else if (direction == Dir.Down)
                {
                    nextRow++;
                }
                else if (direction == Dir.Right)
                {
                    nextCol++;
                }
                else if (direction == Dir.Left)
                {
                    nextCol--;
                }

                // Check out of bounds
                if ((0 <= nextCol && nextCol < map[0].Length) &&
                    (0 <= nextRow && nextRow < map.Length))
                {
                    // ok
                }
                else
                {
                    map[position.row][position.col] = 'X';
                    break;
                }

                // Rotate if next step is an obstacle 
                if (map[nextRow][nextCol] == '#')
                {
                    direction++;
                    if (direction > Dir.Left)
                    {
                        direction = Dir.Up;
                    }
                    //Console.WriteLine($"Rotate into {Enum.GetName(typeof(Dir), direction)}");
                    continue;
                }

                // Move 
                //Console.WriteLine($"Go to row:{nextRow} col:{nextCol}");
                map[position.row][position.col] = 'X';
                position.row = nextRow;
                position.col = nextCol;
            }

            Console.WriteLine(CountX(map));
        }

        // part 2
        {
            var map = StringTo2DArray(_input);
            var position = FindGuard(map);
            var startPosition = position;
            var direction = Dir.Up;
            var loopCount = 0;

            var debugCount = 0;
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[row].Length; col++)
                {
                    if (map[row][col] == '^' || map[row][col] == '#')
                    {
                        continue;
                    }
                    map = StringTo2DArray(_input); // unfortunately we have to generate clean map

                    // place trap on map
                    var prevElement = map[row][col];
                    map[row][col] = '#';

                    // do magic
                    while (true)
                    {
                        debugCount++;

                        if (debugCount > 10_000_000)
                        {
                            Console.WriteLine($"row: {row} col: {col}");
                            DumpMap(map);
                            Debugger.Break();
                        }

                        var nextRow = position.row;
                        var nextCol = position.col;

                        if (direction == Dir.Up)
                        {
                            nextRow--;
                        }
                        else if (direction == Dir.Down)
                        {
                            nextRow++;
                        }
                        else if (direction == Dir.Right)
                        {
                            nextCol++;
                        }
                        else if (direction == Dir.Left)
                        {
                            nextCol--;
                        }

                        // Check out of bounds
                        if ((0 <= nextCol && nextCol < map[0].Length) &&
                            (0 <= nextRow && nextRow < map.Length))
                        {
                            // ok
                        }
                        else
                        {
                            map[position.row][position.col] = 'X';
                            break;
                        }

                        // Rotate if next step is an obstacle 
                        if (map[nextRow][nextCol] == '#')
                        {
                            direction++;
                            if (direction > Dir.Left)
                            {
                                direction = Dir.Up;
                            }
                            if (map[position.row][position.col] != 'O')
                            {
                                map[position.row][position.col] = '+';
                            }
                            //Console.WriteLine($"Rotate into {Enum.GetName(typeof(Dir), direction)}");
                            continue;
                        }

                        // Move 
                        //Console.WriteLine($"Go to row:{nextRow} col:{nextCol}");

                        //if (direction == Dir.Right || direction == Dir.Left)
                        //{
                        //    //var currentChar = map[position.row][position.col];
                        //    //if (currentChar == '|' || currentChar == '+')
                        //    //{
                        //    //    map[position.row][position.col] = '+';
                        //    //}
                        //    //else
                        //    //{
                        //    //    map[position.row][position.col] = '-';
                        //    //}
                        //}
                        //else if (direction == Dir.Up || direction == Dir.Down)
                        //{
                        //    //var currentChar = map[position.row][position.col];
                        //    //if (currentChar == '-' || currentChar == '+')
                        //    //{
                        //    //    map[position.row][position.col] = '+';
                        //    //}
                        //    //else
                        //    //{
                        //    //    map[position.row][position.col] = '|';
                        //    //}
                        //}

                        // Detect loop
                        if (map[nextRow][nextCol] == 'O'
                        )
                        {
                            loopCount++;

                            //Console.WriteLine($"row: {row} col: {col}");
                            //DumpMap(map);
                            //Console.WriteLine();
                            //Console.WriteLine();
                            //Console.WriteLine();

                            break;
                        }

                        if (map[nextRow][nextCol] == '+')
                        {
                            map[nextRow][nextCol] = 'O';
                            
                            //if (row == 6 && col == 3)
                            //    debugCount = 100000000;
                        }

                        position.row = nextRow;
                        position.col = nextCol;
                    }

                    // revert change
                    map[row][col] = prevElement;
                    direction = Dir.Up;
                    position = startPosition;
                    debugCount = 0;
                }
            }

            Console.WriteLine(loopCount);
        }
    }

    private const string _test =
"""
....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...
""";

    private const string _input =
"""
........#........................................#......#........#................................................................
....................................#......#.....#............#.............#..........#..........................................
......................#.......................................................#...................................................
.......#..#..#....#...#...#....#..............#......#.......#...#................#.......#.......................................
......................#....##...#.......#....#.......................................#....................#.......................
...#............................#........................................#..........................#.....................#.......
....................#............#...............#......#.........#...........#...................................................
............................#......#...#................#.............#...........................................................
.....#..#.........#....................#......................................................#........................#.........#
.........#..##.#.........#.............................................#...........#........#....................##...............
...............#....#.........................##......#.....................#..............................................#......
..................##...................................#...........#........#....#.............#..................#........#.#....
....................................#...................#..............................#............#.............................
.........#.....#................#..........................................#...................................#..............#...
...#....................#...................................#..##...................#.......#......................###.........#..
....................#............#....#.##....#.........#......#...#........................#.......................#..........#..
..............#...................................................................................#....................#..........
.........#................#..............................#............................#...#.................#...............#.....
............................................................................................#...#............................#....
............#....................#................................#....#...............................#....#.....................
........................................#.........#..................................................#..#..................#......
.............#.#............................#..#.....#............................................#....#..........................
................................................................................................#.........#..#..............#..#..
...........#..........#.#..#................#.#..#...#.........#..........................................#..........#..#.........
.............................#....................#.......#....#.....#....#......#....................#..#...............##.......
..........#..............................................................................#....#.........#..#................#.....
..#..#...............................................................#.......#........#...........................#...............
...........................................................................#...##....................#.#....##....................
.......................................#...............................................#.....................#.........#..........
.......................................................#.......#....#................#.....................................#......
.............#................#...................#.................#....................#..................#.#.........#.........
.....................................................................#.....................................#......................
........................#..........................................#....#..#.#..........................................#.........
............#.......#..................#.................................................#..............#.......................#.
.........................................................#...............#...#....#...........#.................................#.
.........................#..........................#..#........................................................#.................
...............#....................................#.......#......................................#............#.................
.#......................................................................................................#.........................
#...#........................................................#.................#....#....................#...........#............
..#.........#................................................................#................................#.............#.....
..................................#.........................................................................#................#....
..............#..............................................#........#..................................#........................
......#............................................#.................................#............................#...............
.......##..#.......................##............#...#...................#.#..........................................##..........
.#......#.....................................................................#..#..........................#......#.............#
.................#.....................#........##..#.........#........#................#.........................................
...........#.....#..........#........#............................................................................................
.........................#......#.......................................#..............................#..........................
............#............................................................#..............#..............................#..........
..................#.........#...........................................................................................#.........
#.#..................................#....................#......................#.............#.................................#
....#................#.................#...................#...........#......................................#...................
................#........................................................................................#....#.#.......#.....##..
..........#...#.......................................#........#.......................................#...#......................
.......#..##........................#......##.........................#.........#.......#.............................#.....#.....
................#...............................................#....#..........#.....#.........#.........#.........#.............
...............................#............#....................................#......#......................................#..
.#..#..................#............................................#....#............#...............##...#..........#...........
....#.............................................................................................................................
.............................................................#...........................#..........#.............................
.#........#..................#.....#.............#.....................................................#...#........#...........#.
.......................#........................................#.....#............................#.#..................#.........
................#....#................#.......#............................#.......#.................#............................
....#.........#....#........#.....................#........................#............#.........................................
.......#.......#.....................................................................##...........#...............................
...........#.........................................................#..........#............#....................................
..................#.............................#.......................................................................#.........
................#.....#........#.....#...#..........#.....................................#.....#........................#........
..........................................#.........#...........#.................................................................
...#.......................................................................................................#......................
....#..............#...........#..................................................#.................#.................#...........
.#................#.....#.#.................................................................#.........................#...........
............#.........................##....................................#..............#......................................
...##...........#...#............#..........................................................................#.....................
............................................#......................#......#.......................................................
............#............................................................................................................#........
...................##..............#.#....#.##...................................................#..................#.............
..#...................................................................................#.........#.........................#.....#.
........................#..............................................#......#................................................#..
................#............#............................#.#...................#.....................#...........................
..................#......#................#.#......................#...................#...#......................................
..#................................................##...................................................................#.........
...........................#......................................................................#......#...#......#.............
........................#...#........#......#.......#..........#.............................#........#.....#.....................
.................................................#..............................................#......#.....#....................
.....#....#.................#......#........#.#..............^...........................#...................#..#.................
.............................................................................#................................#...................
#..........................#..#..............#.......#..........................#.................................................
............#.............................................................................#...................#...................
..................#.............................#.........................................#................#.........#......#.....
...#...............................#.....#......#............###.#.#.....................................#....#.............#.....
...........#...........#...........................#..............................................................................
............#..........................................#.....#.............#..........................#....................#.....#
........................#......#..#............................#.......................................................#..........
..#...#...#.......#.#..........................................#.............#.........#....#..................#...........#......
..................#.......#.....................................................................................#........#........
......##................#...........................................#..............##.................................#...........
.................#................................................................................#.#....................#........
....................#.........#..........#...............#...#...#.#.#............................................................
#..................#.#..........#..#...................................................................................#..........
..........#........................................................#..........##..........................#..##...................
...........#...................................................................#..................................................
..................#........#............................................#..................#....#.......................#..#......
............#...................#......#..........................................................................................
...........................#.....##..........#.#..............#......................#.............#.......#...........#..........
............#..................................................#.......#.........#.#..................#..............##...........
#..................................................#...#......#..#......................#.............#........#............#.....
....#..................#..........#.........#.........................................#..................#................#.......
...#.................#.............................................................................#....................#.........
..........#.................................................................#............#....................#.#.#.....#.........
.....................#......................#...........#........#................................................................
.....#.......................................................................#......................#...#......#..................
.............##........#.....................#...........##........#............#.....#.....................#...............#.....
.....#.........#....#.................#....#...........#.......##..........#.........#.#......................................#...
.......#..................##.......#...#.#...#...................................#.......................................#.....#..
...............#.......#.................#........................................................................................
......#.......#.....#...............................#...........#.......#......................##...#....#........................
.##..........................##..................##................#..#....#...##.................................................
........#..............................#.#......#........#...............#....#........#.#........................................
...............................#..#.....................#.#...................#...................................................
.....................#.................................................##...#.......#.................##...............#.......#..
.............#..........................#.................................#..............#.......#..........#........#............
.........................................#...................#.........................................................#..........
...............................#..........#..............................#............#.....#.....................................
.............#..........................#....................#................................#...#............#..................
....#......#........#.......#......#................................................#...#......................................##.
..#...................................#........#.....................................#...#......#.........#..#..........#.........
.......#.........#................................................................................................................
...........#...............................##.........................................#..#....................#.....#.#.......##..
.........................#..#...............#............................#.............#..........................#..............#
""";
}
