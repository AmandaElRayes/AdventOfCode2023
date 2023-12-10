
using System.Data;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace day9
{
    public class Day9
    {
        public void Run()
        {
            using var sr = new StreamReader("input.txt");
            var input = sr.ReadToEnd().Split("\n");
            var sumOfAllValues = 0;
            var sumOfStartValues = 0;
            var rowCounter = 0;
            foreach (var history in input)
            {
                rowCounter++;
                Console.WriteLine(string.Join(",", history));
                var result = Predict(history, rowCounter); 
                sumOfAllValues += result.Item1;
                sumOfStartValues += result.Item2;
            }

            Console.WriteLine($"The sum of extrapolated last values: {sumOfAllValues}");
            Console.WriteLine($"The sum of extrapolated first values: {sumOfStartValues}");
        }

        private (int, int) Predict(string history, int row)
        {
            var steps = history.Split().Select(int.Parse).ToArray();
            var lastValues = new List<int>
            {
                steps.Last()
            };
            var firstValues = new List<int>
            {
                steps.First()
            };
            (lastValues, firstValues) = BuildTree(steps, lastValues, firstValues); // recursive
            var nextValue = CalculatePrediction(lastValues); // recursive
            var firstValue = ReversePrediction(firstValues);
            Console.WriteLine($"prediction for row {row}: first: {firstValue}, last: {nextValue},                      done in {lastValues.Count} loops");
            return (nextValue, firstValue);
        }


        private (List<int>, List<int>) BuildTree(int[] steps, List<int> result, List<int> firstResult)
        {
            var diff = GetDiff(steps);
            Console.WriteLine(string.Join(",", diff));

            bool allZero = diff.All(element => element.Equals(0));
            if (allZero) // check that all equal zero
            {
                return (result, firstResult);
            }
            else
            {
                result.Add(diff.Last());
                firstResult.Add(diff.First());
                (result, firstResult) = BuildTree(diff, result, firstResult);
            }
            return (result, firstResult);
        }

        private int[] GetDiff(int[] steps)
        {
            var res = new List<int>();
            for (int i = 0; i < steps.Length - 1; i++)
            {
                res.Add(steps[i + 1] - steps[i]); // turn to positive integer?
            }
            return res.ToArray();
        }

        private int CalculatePrediction(List<int> lastValues)
        {
            var resultRow = lastValues.Last();
            var counter = lastValues.Count - 1;
            while (counter > 0)
            {
                resultRow = resultRow + lastValues[counter - 1];
                counter--;
            }
            return resultRow;
        }

        private int ReversePrediction(List<int> lastValues)
        {
            var resultRow = 0;
            var counter = lastValues.Count - 1;
            while (counter >= 0)
            {
                resultRow = lastValues[counter] - resultRow;
                counter--;
            }
            return resultRow;
        }
    }

    public class Result
    {
        public int NoOfRecursions { get; set; } = 0;
        public List<int> LastValuePerRow { get; set; } = new List<int>();
    }
}
