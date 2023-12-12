namespace dayz11
{
    public class Day11
    {
        public void Run()
        {
            using var sr = new StreamReader("input.txt");
            var input = sr.ReadToEnd().Split("\r\n");

            //Create grid, get starting index
            var noOfRows = input.Length;
            var noOfCols = input[0].ToArray().Length;
            char[,] grid = new char[noOfRows, noOfCols];

            var galaxies = CreateAndDisplayGrid(grid, input);
            // add spaces
            char[,] expandedGrid = AddSpace(grid, galaxies);

            long sum = 0;
            while(galaxies.Count != 0)
            {
                var currentGalaxy = galaxies.Dequeue();
                foreach(var gal in galaxies)
                {
                    sum += Math.Abs(currentGalaxy.rowPosition - gal.rowPosition) + Math.Abs(currentGalaxy.columnPosition - gal.columnPosition);
                }
            }



            Console.WriteLine($"Sum of shortest distance to all galaxies: {sum}");
        }

        private char[,] AddSpace(char[,] grid, Queue<Galaxy> galaxies)
        {
            //for rows
            var emptyRows = new Queue<int>();
            var emptyColumns = new Queue<int>();
            for (var i = 0; i < grid.GetLength(0); i++)
            {
                if (!grid.GetRow(i).Contains('#'))
                {
                        emptyRows.Enqueue(i);
                }
                if (!grid.GetCol(i).Contains('#'))
                {
                    emptyColumns.Enqueue(i);
                }
            }
            char[,] expandedGrid = 
                new char[emptyRows.Count + grid.GetLength(0), emptyColumns.Count + grid.GetLength(1)];

            Console.WriteLine("Expanded grid");

            // insert all the dots
            var increaseRowPositionOf = new List<int> { };
            while(emptyRows.Count != 0)
            {
                var gals = galaxies.Where(x => x.rowPosition >= emptyRows.First());
                foreach(var gal in gals)
                {
                    increaseRowPositionOf.Add(gal.Id);
                }
                emptyRows.Dequeue();
            }
            var increaseColPositionOf = new List<int> { };
            while (emptyColumns.Count != 0)
            {
                var gals = galaxies.Where(x => x.columnPosition >= emptyColumns.First());
                foreach (var gal in gals)
                {
                    increaseColPositionOf.Add(gal.Id);
                }
                emptyColumns.Dequeue();
            }

            foreach(var ID in increaseRowPositionOf)
            {
                galaxies.Where(x => x.Id == ID).First().rowPosition += 999999;
            }
            foreach (var ID in increaseColPositionOf)
            {
                galaxies.Where(x => x.Id == ID).First().columnPosition += 999999;
            }

            //var highestGalaxyColumn = galaxies.OrderByDescending(x => x.columnPosition).First();
            //var highestGalaxyRow = galaxies.OrderByDescending(x => x.rowPosition).First();

            //for (int i = 0; i < highestGalaxyRow.rowPosition; i++)
            //{
            //    for (int j = 0; j < highestGalaxyColumn.columnPosition; j++)
            //    {

            //        var galaxyInThisPosition = galaxies.Where(x => x.rowPosition == i && x.columnPosition == j);
            //        if (galaxyInThisPosition.Any())
            //        {
            //            //expandedGrid[i, j] = '#';
            //            Console.Write('#');
            //        }
            //        else
            //        {
            //            //expandedGrid[i, j] = '.';
            //            Console.Write('.');
            //        }

            //        //Console.Write(expandedGrid[i, j]);
            //    }
            //    Console.WriteLine("");
            //}

            return expandedGrid;
        }

        private Queue<Galaxy> CreateAndDisplayGrid(char[,] grid, string[] input)
        {
            var galaxies = new Queue<Galaxy>();
            var id = 1;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (input[i][j] == '#')
                    {
                        galaxies.Enqueue(new Galaxy
                        {
                            Id = id,
                            rowPosition = i,
                            columnPosition = j,
                        });
                        id++;
                    }
                    grid[i, j] = input[i][j];
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine("");
            }

            return galaxies;
        }
    }

    public class Galaxy
    {
        public int Id { get; set; }
        public long rowPosition { get; set; }
        public long columnPosition { get; set; }

    }

    public static class ArrayExt
    {
        public static T[] GetRow<T>(this T[,] array, int row)
        {
            int cols = array.GetUpperBound(1) + 1;
            T[] result = new T[cols];

            int size = 2;

            Buffer.BlockCopy(array, row * cols * size, result, 0, cols * size);

            return result;
        }

        /// <summary>
        /// Returns the column with number 'col' of this matrix as a 1D-Array.
        /// </summary>
        public static T[] GetCol<T>(this T[,] matrix, int col)
        {
            var colLength = matrix.GetLength(0);
            var colVector = new T[colLength];

            for (var i = 0; i < colLength; i++)
                colVector[i] = matrix[i, col];

            return colVector;
        }
    }

}
