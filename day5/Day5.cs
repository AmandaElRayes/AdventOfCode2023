using System.Text;
using System.Text.RegularExpressions;

namespace day5
{
    public class Day5
    {
        public void Run()
        {
            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            using var sr = new StreamReader("input.txt");
            var seedsToBePlanted = sr.ReadLine().Split().Skip(1); // first element is text
            //seedranges
            sr.ReadLine(); // reading empty line
            var dictionary = MapData(sr);

            Part1(seedsToBePlanted, dictionary);

            //ReverseCheck(dictionary, seedsToBePlanted);

            //IfAllElseFails(seedsToBePlanted, dictionary);

            watch.Stop();

            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }

        private void ReverseCheck(Dictionary<string, List<Ranges>> dictionary, IEnumerable<string> seedsToBePlanted)
        {
            List<Ranges> seedRanges = GetSeedRanges(seedsToBePlanted);
            var success = dictionary.TryGetValue("humidity-to-location map:", out var locationMap);

            var min = locationMap.Select(x => x.DestRangeStart - x.RangeLength).Min(); // min max dont work gives wrong range
            var max = locationMap.Select(x => x.DestRangeStart).Max();
            uint location = 0;

            while (location < 213844866)
            {
                CheckReverseLocation(seedRanges, dictionary, location);
                Console.WriteLine("still running: " + location);
                location++;
                
            }


        }

        private void IfAllElseFails(IEnumerable<string> seedsToBePlanted, Dictionary<string, List<Ranges>> dictionary)
        {
            List<Ranges> seedRanges = GetSeedRanges(seedsToBePlanted);
            var minlocationPerRange = new List<uint>();
            foreach (Ranges seedRange in seedRanges)
            {
                var locationPerSeed = new List<uint>();
                for (uint i = seedRange.SourceRangeStart; i < seedRange.DestRangeStart; i++)
                {
                    var location = i;
                    foreach (var map in dictionary)
                    {
                        location = GetCorrespondingNumber(map, location);
                    }
                    locationPerSeed.Add(location);
                }
                minlocationPerRange.Add(locationPerSeed.Min());
                Console.WriteLine("still running Minimun location per seed... " + locationPerSeed.Min());
            }
            Console.WriteLine(minlocationPerRange.Min());
        }

        private static List<Ranges> GetSeedRanges(IEnumerable<string> seedsToBePlanted)
        {
            var seedRanges = new List<Ranges>();
            var count = 1;
            uint savedValue = 0;
            foreach (var range in seedsToBePlanted)
            {
                //if index is odd grab 2
                if (count % 2 == 0)
                {
                    var seedRange = new Ranges()
                    {
                        SourceRangeStart = savedValue,
                        RangeLength = uint.Parse(range),
                        DestRangeStart = savedValue + uint.Parse(range) // this is the endrange
                    };
                    seedRanges.Add(seedRange);
                }
                else
                {
                    savedValue = uint.Parse(range);
                }
                count++;
            }

            return seedRanges;
        }

        private uint CheckReverseLocation(List<Ranges> seedRanges, Dictionary<string, List<Ranges>> dictionary, uint loc)
        {
            var initialLocation = loc;
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            foreach (var map in dictionary.Reverse())
            {
                loc = GetCorrespondingNumberInReverse(map, loc);
            }
            foreach (var range in seedRanges)
            {
                if(loc >= range.SourceRangeStart && loc <= (range.SourceRangeStart + range.RangeLength))
                {
                    watch.Stop();
                    Console.WriteLine("Found location: " + initialLocation + $" Execution Time: {watch.ElapsedMilliseconds} ms");
                    Environment.Exit(0);
                }
            }            
            return loc;
        }

        private uint GetCorrespondingNumberInReverse(KeyValuePair<string, List<Ranges>> map, uint location)
        {
            var ranges = map.Value;
            foreach (var range in ranges)
            {
                if (location >= range.DestRangeStart && location < range.DestRangeStart + range.RangeLength)
                {
                    // check where in range
                    var index = location - range.DestRangeStart;
                    //get dest 
                    location = range.SourceRangeStart + index;
                    break;
                }
            }
            return location;
        }

        private void Part1(IEnumerable<string> seedsToBePlanted, Dictionary<string, List<Ranges>> dictionary)
        {
            var locationPerSeed = new List<uint>();
            foreach (var seed in seedsToBePlanted)
            {
                uint location = uint.Parse(seed);
                foreach (var map in dictionary)
                {
                    location = GetCorrespondingNumber(map, location);
                }
                locationPerSeed.Add(location);
            }
            var result = locationPerSeed.Min();

            Console.WriteLine(result);
        }

        private uint GetCorrespondingNumber(KeyValuePair<string, List<Ranges>> map, uint location)
        {
            var ranges = map.Value;
            foreach (var range in ranges)
            {
                if(location >= range.SourceRangeStart && location < range.SourceRangeStart + range.RangeLength)
                {
                    // check where in range
                    var index = location - range.SourceRangeStart;
                    //get dest 
                    location  = range.DestRangeStart + index;
                    break;
                }
            }
            return location;
        }

        private static Dictionary<string, List<Ranges>> MapData(StreamReader sr)
        {
            var dict = new Dictionary<string, List<Ranges>>();
            var keys = new List<string>();
            List<Ranges> values = new List<Ranges>();
            var currentKey = "";
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                if (line == "")
                {
                    dict.Add(currentKey, values);
                    values = new List<Ranges>();
                }
                else if (Regex.IsMatch(line, "[a-z]"))
                {
                    currentKey = line;
                    keys.Add(currentKey);
                }
                else
                {
                    var ranges = line.Split();
                    var x = new Ranges()
                    {
                        DestRangeStart = uint.Parse(ranges[0]),
                        SourceRangeStart = uint.Parse(ranges[1]),
                        RangeLength = uint.Parse(ranges[2]),
                    };
                    values.Add(x);
                }
            }
            return dict;
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

    public class Ranges
    {
        public uint DestRangeStart {  get; set; }
        public uint SourceRangeStart { get; set; }
        public uint RangeLength { get; set; }
    }
}
