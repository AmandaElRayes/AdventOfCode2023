using System.Diagnostics;
using System.Text.RegularExpressions;

namespace day8
{
    public class Day8
    {
        public async Task Run()
        {
            var watch = Stopwatch.StartNew();
            using var sr = new StreamReader("input.txt");
            var instructions = sr.ReadLine();
            Queue<int> direction = CreateQueueOfDirections(ref instructions);
            sr.ReadLine(); // reading empty line
            Dictionary<string, List<string>> dict = CreateDictionary(sr);

            Part1(watch, direction, dict);
            Part2(instructions.Length, direction, dict);
        }

        private static Queue<int> CreateQueueOfDirections(ref string? instructions)
        {
            instructions = instructions.Replace('L', '0');
            instructions = instructions.Replace('R', '1');

            var direction = new Queue<int>();
            for (int i = 0; i < instructions.Length; i++)
            {
                direction.Enqueue(int.Parse(instructions[i].ToString()));
            }

            return direction;
        }

        private static void Part2(int instructionLength, Queue<int> direction, Dictionary<string, List<string>> dict)
        {
            var keys = dict.Keys.Where(x => x.EndsWith('A')).ToList();
            var runCount = new List<int>();
            long factor = 1L;
            foreach (var key in keys)
            {
                var count = RunKey(key, direction, dict);
                factor *= count / instructionLength;
                runCount.Add(count);
            }

            // get gcd
            var gcd = GCD(runCount);

            var result = factor * gcd;


            Console.WriteLine("Part 2 Result: " + result);
        }

        static int GCD(List<int> numbers)
        {
            return numbers.Aggregate(GCD);
        }

        static int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        private static int RunKey(string key, Queue<int> direction, Dictionary<string, List<string>> dict)
        {
            var count = 0;
            while (!key.EndsWith('Z'))
            {
                int n = direction.Dequeue();
                key = dict.GetValueOrDefault(key)[n];
                count++;
                direction.Enqueue(n);
            }
            return count;
        }


        private static void Part1(Stopwatch watch, Queue<int> direction, Dictionary<string, List<string>> dict)
        {
            var count = 0;
            var key = "AAA";
            while (key != "ZZZ")
            {
                int n = direction.Dequeue();
                key = dict.GetValueOrDefault(key)[n];
                count++;
                direction.Enqueue(n);
            }

            Console.WriteLine($"Steps: {count}, elapsed time {watch.ElapsedMilliseconds} ms ");
        }

        private static Dictionary<string, List<string>> CreateDictionary(StreamReader sr)
        {
            var dict = new Dictionary<string, List<string>>();
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var elements = Regex.Matches(line, "\\w{3}");
                dict.Add(elements[0].ToString(), new List<string>() { elements[1].ToString(), elements[2].ToString() });
            }
            return dict;
        }
    }
}
