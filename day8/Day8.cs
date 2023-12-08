using System.ComponentModel;
using System.Text.RegularExpressions;

namespace day8
{
    public class Day8
    {
        public void Run()
        {
            using var sr = new StreamReader("input.txt");
            var instructions = sr.ReadLine();
            sr.ReadLine(); // reading empty line
            Dictionary<string, List<string>> tree = CreateTree(sr);

            var key = "AAA";
            
            var count = 0;

            NextNode(instructions, tree, key, ref count, 0);        

            Console.WriteLine(count);
        }

        private static void NextNode(string? instructions, Dictionary<string, List<string>> tree, string key, ref int count, int position)
        {
            var node = tree.GetValueOrDefault(key);
            if (key == "ZZZ")
            {
                Console.WriteLine(count);
                Environment.Exit(0);
            }
            if (position == instructions.Length)
            {
                position = 0;
            }
            count++;
            switch (instructions[position])
            {
                case 'L':
                    position += 1;
                    NextNode(instructions, tree, node.First(), ref count, position);
                    break;
                case 'R':
                    position += 1;
                    NextNode(instructions, tree, node.Last(), ref count, position);
                    break;
            }
        }


        private static Dictionary<string, List<string>> CreateTree(StreamReader sr)
        {
            var tree = new Dictionary<string, List<string>>();
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var elements = Regex.Matches(line, "[A-Z]{3}");
                tree.Add(elements[0].ToString(), new List<string>() { elements[1].ToString(), elements[2].ToString() });

            }
            return tree;
        }
    }
}
