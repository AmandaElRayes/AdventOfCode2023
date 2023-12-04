
using System.Text.RegularExpressions;

namespace day4
{
    public class Day4
    {
        public void Run()
        {
            using var sr = new StreamReader(@"input.txt");
            var cardPoints = new List<int>();


            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine().Split('|');
                var winningLine = line[0].Split(":")[1];
                var winningNumbers = winningLine.Split(" ");
                var mynumbers = line[1].Split(" ");

                var res = winningNumbers.Intersect(mynumbers);

                cardPoints.Add(CalculatePoints(res));
            }
            Console.WriteLine(cardPoints.Sum());
        }

        private int CalculatePoints(IEnumerable<string> res)
        {
            var count = 0;
            var firstSuccess = true;
            foreach (var item in res)
            {                
                var success = int.TryParse(item, out int result);
                
                if (success && result != 0 && firstSuccess)
                {
                    count = 1;
                    firstSuccess = false;
                }
                else if (success && result != 0)
                {
                    count = count * 2;                 
                }
            }
            return count;
        }

        public static StreamReader ReadFile()
        {
            ///////////////HELPLINES///////////////////////
            // ADD THIS IN RUN
            //var sr = ReadFile();
            // THEN YOU CAN DO ONE OF THESE
            //var input = sr.ReadToEnd();
            //var line = sr.ReadLine();
            //while (!sr.EndOfStream)
            //{
            //    var line = sr.ReadLine();
            //}
            ///////////////////////////////////////////////
            using var sr = new StreamReader("input.txt");
            return sr;
        }
    }
}
