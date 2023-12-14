using System.Data.SqlTypes;
using System.Text.RegularExpressions;

namespace dayz14
{
    public class Day14
    {
        public void Run()
        {
            using var sr = new StreamReader("input.txt");
            var input = sr.ReadToEnd();
            var grid = CreateGrid(input);

            List<char[]> columns = GetColumns(input, grid);

            List<string> newGrid = TiltColumns(columns);

            // Calculate load

            var totalLoad = 0;
            foreach (var col in newGrid)
            {
                var matches = Regex.Matches(col, "O").ToList();
                foreach (var match in matches)
                {
                    totalLoad += col.Length - match.Index;
                }
            }

            Console.WriteLine(totalLoad);

        }

        private static List<string> TiltColumns(List<char[]> columns)
        {
            var newGrid = new List<string>();
            foreach (var col in columns)
            {
                var colstring = new string(col);
                var newString = "";
                int startPosition = 0;
                var substr = colstring;

                var match = Regex.Matches(substr, "\\#+").ToList();
                var matchCounter = 0;
                foreach (var m in match)
                {
                    matchCounter++;
                    if (m.Index == startPosition)
                    {
                        newString += m.Value;
                        continue;
                    }
                    substr = colstring.Substring(startPosition, m.Index - startPosition);
                    substr = Tilt(substr);
                    newString += substr + m.Value;
                    startPosition = m.Index + m.Length;

                    if (newString.Length == colstring.Length)
                    {
                        break;
                    }
                    else if (newString.Length < colstring.Length && matchCounter == match.Count)
                    {
                        substr = colstring.Substring(startPosition, col.Length - startPosition);
                        substr = Tilt(substr);
                        newString += substr;
                    }

                }
                if (match.Count == 0)
                {
                    newString = Tilt(colstring);
                }
                newGrid.Add(newString);
            }

            return newGrid;
        }

        private static List<char[]> GetColumns(string input, char[,] grid)
        {
            var noOfCols = input.Split("\r\n")[0].ToArray().Length;
            var noOfRows = input.Split("\r\n").Length;

            var columns = new List<char[]>();
            for (int j = 0; j < noOfCols; j++)
            {
                var tempCols
                    = new List<char>();
                for (int i = 0; i < noOfRows; i++)
                {
                    tempCols.Add(grid[i, j]);
                }
                columns.Add(tempCols.ToArray());
                tempCols.Clear();
            }

            return columns;
        }

        private static string Tilt(string substr)
        {
            var os = string.Concat(substr.Where(x => x == 'O'));
            var dots = string.Concat(substr.Where(x => x == '.'));
            return os + dots; 
        }

        private char[,] CreateGrid(string pattern)
        {
            var input = pattern.Split("\r\n");
            var noOfRows = input.Length;
            var noOfCols = input[0].ToArray().Length;
            char[,] grid = new char[noOfRows, noOfCols];
            for (int i = 0; i < noOfRows; i++)
            {
                var row = input[i].ToArray();
                for (int j = 0; j < noOfCols; j++)
                {
                    grid[i, j] = row[j];
                }
            }
            return grid;
        }
    }
}
