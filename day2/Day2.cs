

namespace day2
{
    public class Day2
    {
        public void Run()
        {
            var dict = ReadFile();
            int counter = Part1(dict);
            int sumOfPowers = Part2(dict);

            Console.WriteLine("Part 1: " + counter);
            Console.WriteLine("Part 2: " + sumOfPowers);
        }

        private static int Part1(Dictionary<string, List<string>> dict)
        {
            var counter = 0;
            foreach (var game in dict)
            {
                bool possible = CheckIfPossible(game.Value);
                if (possible)
                {
                    counter += int.Parse(game.Key);
                }
            }
            return counter;
        }

        private int Part2(Dictionary<string, List<string>> dict)
        {
            var sumOfPowers = 0;
            foreach (var game in dict)
            {
                sumOfPowers += GetPowerOfCubes(game.Value);
            }

            return sumOfPowers;
        }

        private int GetPowerOfCubes(List<string> game)
        {
            List<int> redList = [];
            List<int> greenList = [];
            List<int> blueList = [];
            Dictionary<string, List<int>> cubesByColor = [];

            foreach (var g in game)
            {
                var x = g.Split(',');
                for (int i = 0; i < x.Length; i++)
                {
                    var y = x[i].Split(" ");
                    switch (y[^1])
                    {
                        case "red":
                            redList.Add(int.Parse(y[1]));
                            break;
                        case "blue":
                            blueList.Add(int.Parse(y[1]));
                            break;
                        case "green":
                            greenList.Add(int.Parse(y[1]));
                            break;
                        default:
                            break;
                    }
                }
            }
            var power = redList.Max() * blueList.Max() * greenList.Max();
            return power;
        } 

        private static bool CheckIfPossible(List<string> game)
        {
            var redCubes = 12;
            var greenCubes = 13;
            var blueCubes = 14;
            var possible = true;

            foreach(var g in game)
            {
                var x = g.Split(',');
                for (int i = 0; i < x.Length; i++)
                {
                    var y = x[i].Split(" ");
                    switch (y[^1])
                    {
                        case "red":
                            if (int.Parse(y[1]) > redCubes)
                            {
                                possible = false;
                            }                            
                            break;
                        case "blue":
                            if (int.Parse(y[1]) > blueCubes)
                            {
                                possible = false;
                            }
                            break;
                        case "green":
                            if (int.Parse(y[1]) > greenCubes)
                            {
                                possible = false;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return possible;
        }

        public static Dictionary<string, List<string>> ReadFile()
        {
            var dictionary = new Dictionary<string, List<string>>();
            using var sr = new StreamReader("input.txt");
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var splitForGameNo = line?.Split(':') ?? throw new NullReferenceException();
                var gameNo = splitForGameNo[0].Split(" ")[1].Replace(":", "");
                var gameSets = splitForGameNo[1].Split(";").ToList();

                dictionary.Add(gameNo, gameSets);
            }
            return dictionary;
        }
    }
}