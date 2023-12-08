using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace day8
{
    public class Day8
    {
        public void Run()
        {
            var watch = Stopwatch.StartNew();
            using var sr = new StreamReader("input.txt");
            var instructions = sr.ReadLine();
            instructions = instructions.Replace('L', '0');
            instructions = instructions.Replace('R', '1');

            var queue = new Queue<int>();
            for (int i = 0; i < instructions.Length; i++)
            {
                queue.Enqueue(int.Parse(instructions[i].ToString()));
            }
            sr.ReadLine(); // reading empty line
            Dictionary<string, List<string>> dict = CreateDictionary(sr);
           
            var count = 0;
            var key = "AAA";

            while(key != "ZZZ")
            {
                var i = queue.Dequeue();
                key = dict.GetValueOrDefault(key)[i];
                count++;
                queue.Enqueue(i);
            }

            Console.WriteLine($"Steps: {count}, elapsed time {watch.ElapsedMilliseconds} ms ");
        }

        private static Dictionary<string, List<string>> CreateDictionary(StreamReader sr)
        {
            var dict = new Dictionary<string, List<string>>();
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var elements = Regex.Matches(line, "[A-Z]{3}");
                dict.Add(elements[0].ToString(), new List<string>() { elements[1].ToString(), elements[2].ToString() });

            }
            return dict;
        }
    }
}
