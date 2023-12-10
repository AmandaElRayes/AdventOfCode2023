
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
            Console.WriteLine($"No of steps: {step}, currentIndex {currentRow}, {currentCol}.");
            CheckNorth(grid, currentRow, currentCol, step);

            CheckSouth(grid, currentRow, currentCol, step);

            CheckWest(grid, currentRow, currentCol, step);

            CheckEast(grid, currentRow, currentCol, step);
        }

        private static void CheckEast(char[,] grid, int row, int col, int step)
        {
            switch (grid[row, col + 1])
            {
                case '-':
                    CheckEast(grid, row, col+1,step+1);
                    break;
                case 'J':
                    CheckNorth(grid, row, col + 1, step + 1);
                    break;
                case '7':
                    CheckSouth(grid, row, col + 1, step + 1);
                    break;
                case 'S':
                    step++;
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
                    CheckWest(grid, row, col - 1, step + 1);
                    break;
                case 'F':
                    CheckSouth(grid, row, col - 1, step + 1);
                    break;
                case 'L':
                    CheckNorth(grid, row, col - 1, step + 1);
                    break;
                case 'S':
                    step++;
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
                    CheckSouth(grid, row + 1, col, step + 1);
                    break;
                case 'L':
                    CheckEast(grid, row + 1, col, step + 1);
                    break;
                case 'J':
                    CheckWest(grid, row + 1, col, step + 1);
                    break;
                case 'S':
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
                    CheckNorth(grid, row-1, col, step + 1);
                    break;
                case 'F':
                    CheckEast(grid, row - 1, col, step + 1);
                    break;
                case '7':
                    CheckWest(grid, row - 1, col, step + 1);
                    break;
                case 'S':
                    // back to start
                    step++;
                    Console.WriteLine($"steps {step}. Steps to farthest point {step / 2}");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
