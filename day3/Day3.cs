using System.Text.RegularExpressions;

namespace day3
{
    public class Day3
    {
        public void Run()
        {
            using var sr = new StreamReader("input.txt");
            var input = sr.ReadToEnd().Split("\r\n");

            // match numbers
            var count = 0;
            var matches = new List<NoInfo>();
            foreach (var line in input)
            {
                var noMatches = Regex.Matches(line, "\\d+");
                foreach (Match match in noMatches)
                {
                    var info = new NoInfo()
                    {
                        Number = int.Parse(match.Value),
                        Index = match.Index,
                        Length = match.Length,
                        Line = count

                    };
                    matches.Add(info);
                }
                count++;
            }

            //Part1(input, matches);


            //part 2
            var gearRatios = new List<int>();

            var gearSymbolMatches = new List<NoInfo>();
            var Gearcounter = 0;
            foreach (var line in input)
            {
                var characterMatches = Regex.Matches(line, "(\\*+)");
                foreach (Match match in characterMatches)
                {
                    // check adj digits here.
                    var adjacentDigits = matches.Where(x => x.Line == Gearcounter | x.Line == Gearcounter + 1 | x.Line == Gearcounter - 1);

                    var savedDigits = new List<int>();
                    foreach (var digit in adjacentDigits)
                    {
                        var firstIndex = digit.Index - 1;
                        var lastIndex = digit.Index + digit.Length;
                        if (match.Index >= firstIndex && match.Index <= lastIndex)
                        {
                            //save digit.
                            savedDigits.Add(digit.Number);
                        }
                    }
                    if (savedDigits.Count == 2)
                    {
                        gearRatios.Add(savedDigits.First() * savedDigits.Last());
                        savedDigits = new List<int>();

                    }
                }
                Gearcounter++;
            }

            Console.WriteLine(gearRatios.Sum());

        }

        private static void Part1(string[] input, List<NoInfo> matches)
        {
            // match symbols
            var symbolMatches = new List<NoInfo>();
            var counter = 0;
            foreach (var line in input)
            {
                var characterMatches = Regex.Matches(line, "([^(.|\\w)])");
                foreach (Match match in characterMatches)
                {
                    var charInfo = new NoInfo()
                    {
                        Number = -1,
                        Index = match.Index,
                        Length = match.Length,
                        Line = counter
                    };
                    symbolMatches.Add(charInfo);
                }
                counter++;
            }

            //get indexes to check
            var numbersToKeep = new List<int>();
            //1.
            for (int i = 0; i < input.Length; i++)
            {
                var adjSymbols = symbolMatches.Where(x => x.Line == i | x.Line == i + 1 | x.Line == i - 1);

                var digitsOnLine = matches.Where(x => x.Line == i);

                foreach (var digit in digitsOnLine)
                {
                    var index = digit.Index - 1;
                    var endIndex = digit.Index + digit.Length;

                    if (adjSymbols.Any(x => x.Index >= index && x.Index <= endIndex))
                    {
                        numbersToKeep.Add(digit.Number);
                    }
                }
            }
            Console.WriteLine(numbersToKeep.Sum());
        }
    }

    public class NoInfo
    {
        public int Number { get; set; }
        public int Index { get; set; }
        public int Length { get; set; }

        public int Line { get; set; }
    }
}
