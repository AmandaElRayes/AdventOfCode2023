using System.Text;
using System.Text.RegularExpressions;

namespace day5
{
    public class Day5
    {
        public void Run()
        {
            using var sr = new StreamReader("input.txt");
            var seedsToBePlanted = sr.ReadLine().Split().Skip(1); // first element is text
            //seedranges
            sr.ReadLine(); // reading empty line
            var dictionary = MapData(sr);

            //Part1(seedsToBePlanted, dictionary);

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

            var locationMap = dictionary.TryGetValue("humidity-to-location map:", out List<Ranges> locations);
            var minLocations = locations.Select(x => x.SourceRangeStart);
            var seedPerLocation = new List<uint>();
            foreach (uint location in minLocations)
            {
                uint loc = location;
                foreach (var map in dictionary.Reverse())
                {
                    loc = GetCorrespondingNumberInReverse(map, loc);
                }
                // check if seed in range
                if(seedsToBePlanted.Contains(loc.ToString()))
                {
                    Console.WriteLine("true," + loc);
                }

            }
            //foreach (var map in dictionary.Reverse())
            //{
            //    foreach (var range in map.Value)
            //    {
            //        for (uint i = range.SourceRangeStart; i < range.RangeLength; i++)
            //        {
            //            var seedNo = i;
            //            seedNo = GetCorrespondingNumber(map, seedNo);
            //        }
            //        var locationPerSeed = new List<uint>();
            //    }
            //}

            Console.WriteLine("");
        }

        //private (uint, uint) IntersectSeedRanges(List<Ranges> seedRanges)
        //{
        //    var source = seedRanges.Select(x => x.SourceRangeStart);
        //    var dest = seedRanges.Select(x => x.DestRangeStart);
        //    //for (uint i = source; i < dest; i++)
        //    //{

        //    //    //if in seedranges => keep
        //    //    // else remove
        //    //}
        //    return (1, 1);
        //}

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
