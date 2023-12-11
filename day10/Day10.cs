
using System.Net.WebSockets;

namespace dayz10
{
    public class Day10
    {
        public void Run()
        {
            // input to grid
            using var sr = new StreamReader("input.txt");
            var input = sr.ReadToEnd().Split("\r\n");

            //Create grid, get starting index
            var noOfRows = input.Length;
            var noOfCols = input[0].ToArray().Length;
            char[,] grid = new char[noOfRows, noOfCols];
            var currentRow = 0;
            var currentCol = 0;
            for (int i = 0; i < noOfRows; i++)
            {
                var row = input[i].ToArray();
                for (int j = 0; j < noOfCols; j++)
                {
                    if (row[j] == 'S')
                    {
                        currentRow = i;
                        currentCol = j;
                    }
                    grid[i, j] = row[j];
                }
            }
            var step = 0;
            CheckNorth(grid, currentRow, currentCol, step);

            CheckSouth(grid, currentRow, currentCol, step);

            CheckWest(grid, currentRow, currentCol, step);

            CheckEast(grid, currentRow, currentCol, step);
        }

        private static void DisplayGrid(char[,] grid)
        {
            // left to right
            //for (int i = 0; i < grid.GetLength(0) - 1; i++)
            //{
            //    for (int j = 0; j < grid.GetLength(1) - 1; j++)
            //    {
            //        if (grid[i, j] == '0')
            //        {
            //            break;
            //        }
            //        grid[i, j] = '*';
            //    }
            //}


            ////reverse
            //for (int i = grid.GetLength(0) - 1; i > 0; i--)
            //{
            //    for (int j = grid.GetLength(1) - 1; j > 0; j--)
            //    {
            //        if (grid[i, j] == '0')
            //        {
            //            break;
            //        }
            //        grid[i, j] = '*';
            //    }
            //}

            //// up down
            //for (int j = 0; j < grid.GetLength(1) - 1; j++)
            //{
            //    for (int i = 0; i < grid.GetLength(0) - 1; i++)
            //    {
            //        if (grid[i, j] == '0')
            //        {
            //            break;
            //        }
            //        grid[i, j] = '*';
            //    }
            //}
            //// down up
            //for (int j = grid.GetLength(1) - 1; j > 0; j--)
            //{
            //    for (int i = grid.GetLength(0) - 1; i > 0; i--)
            //    {
            //        if (grid[i, j] == '0')
            //        {
            //            break;
            //        }
            //        grid[i, j] = '*';
            //    }
            //}

            //while (SetStars(grid, false))
            //{
            //    SetStars(grid, false);
            //}


            var enclosedCount = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] != '*' && grid[i, j] != '0')
                    {
                        enclosedCount++;
                    }
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine("");
            }
            Console.WriteLine($"EnclosedCount {enclosedCount}");
        }

        private static bool SetStars(char[,] grid, bool starSet)
        {

            for (int i = 0; i < grid.GetLength(0)-1; i++)
            {
                for (int j = 0; j < grid.GetLength(1)-1; j++)
                {
                    // check if adjacent to *
                    if (grid[i, j] != '0' && grid[i, j] != '*')
                    {
                        // check top row

                        if (grid[i - 1, j - 1] == '*' || grid[i - 1, j] == '*' || grid[i - 1, j + 1] == '*'
                            || grid[i, j - 1] == '*' || grid[i - 1, j + 1] == '*'
                            || grid[i + 1, j - 1] == '*' || grid[i + 1, j] == '*' || grid[i + 1, j + 1] == '*')
                        {
                            grid[i, j] = '*';
                            starSet = true;
                        }
                    }
                }
            }

            return starSet;
        }

        private static void CheckEast(char[,] grid, int row, int col, int step)
        {
            switch (grid[row, col + 1])
            {
                case '-':
                    grid[row, col + 1] = '0'; // means we passed it
                    CheckEast(grid, row, col+1,step+1);
                    break;
                case 'J':
                    grid[row, col + 1] = '0';
                    CheckNorth(grid, row, col + 1, step + 1);
                    break;
                case '7':
                    grid[row, col + 1] = '0';
                    CheckSouth(grid, row, col + 1, step + 1);
                    break;
                case 'S':
                    step++;
                    DisplayGrid(grid);
                    Console.WriteLine($"steps {step}. Steps to farthest point {step/2}");
                    Environment.Exit(0);                    
                    break;
            }
        }

        private static void CheckWest(char[,] grid, int row, int col, int step)
        {
            switch (grid[row, col - 1])
            {
                case '-':
                    grid[row, col - 1] = '0';
                    CheckWest(grid, row, col - 1, step + 1);
                    break;
                case 'F':
                    grid[row, col - 1] = '0';
                    CheckSouth(grid, row, col - 1, step + 1);
                    break;
                case 'L':
                    grid[row, col - 1] = '0';
                    CheckNorth(grid, row, col - 1, step + 1);
                    break;
                case 'S':
                    step++;
                    DisplayGrid(grid);
                    Console.WriteLine($"steps {step}. Steps to farthest point {step / 2}");
                    Environment.Exit(0);
                    break;
            }
        }

        private static void CheckSouth(char[,] grid, int row, int col, int step)
        {            
            switch (grid[row + 1, col])
            {
                case '|':
                    grid[row + 1, col] = '0';
                    CheckSouth(grid, row + 1, col, step + 1);
                    break;
                case 'L':
                    grid[row + 1, col] = '0';
                    CheckEast(grid, row + 1, col, step + 1);
                    break;
                case 'J':
                    grid[row + 1, col] = '0';
                    CheckWest(grid, row + 1, col, step + 1);
                    break;
                case 'S':
                    step++;
                    DisplayGrid(grid);
                    Console.WriteLine($"steps farthest point {step}. Steps to farthest point {step / 2}");
                    Environment.Exit(0);
                    break;
            }
        }

        private static void CheckNorth(char[,] grid, int row, int col, int step)
        {
            switch (grid[row - 1, col])
            {
                case '|':
                    grid[row - 1, col] = '0';
                    CheckNorth(grid, row-1, col, step + 1);
                    break;
                case 'F':
                    grid[row - 1, col] = '0';
                    CheckEast(grid, row - 1, col, step + 1);
                    break;
                case '7':
                    grid[row - 1, col] = '0';
                    CheckWest(grid, row - 1, col, step + 1);
                    break;
                case 'S':
                    step++;
                    DisplayGrid(grid);
                    Console.WriteLine($"steps {step}. Steps to farthest point {step / 2}");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
