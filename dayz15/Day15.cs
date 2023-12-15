
using System.Text.RegularExpressions;

namespace dayz15
{
    public class Day15
    {
        public void Run()
        {
            using var sr = new StreamReader("testinput.txt");
            var input = sr.ReadToEnd().Split(',');
            Part1(input);
            Part2(input);

        }

        private static void Part2(string[] input)
        {            
            var boxes = CreateBoxes(input);
            var focusingPower = 0;

            foreach (var box in boxes)
            {
                var labels = box.labelAndFocalLength;
                var values = new List<int>();
                foreach (var label in labels)
                {
                    values.Add(label.Value);
                }

                var slot = 1;
                foreach (var value in values)
                {
                    var focus = (1 + box.number) * slot * value;
                    focusingPower += (1 + box.number) * slot * value;
                    slot++;

                }
            }

            Console.WriteLine($"Part 2: total focusingPower is {focusingPower}");
        }

        private static List<Box> CreateBoxes(string[] input)
        {
            var boxes = new List<Box>();
            foreach (var line in input)
            {
                var key = Regex.Match(line, "[a-z]+");
                var boxNumber = HashFunction(0, key.Value);
                var getDigits = Regex.Match(line, "\\d+");

                if (getDigits.Success)
                {
                    var isBoxInBoxes = boxes.Where(x => x.number == boxNumber);
                    if (isBoxInBoxes.Count() > 0)
                    {
                        // add to dictionary
                        var success = isBoxInBoxes.First().labelAndFocalLength.TryAdd(key.Value, int.Parse(getDigits.Value));
                        if (!success)
                        {
                            // there is another of the same label there must be replace
                            isBoxInBoxes.First().labelAndFocalLength.Remove(key.Value);
                            isBoxInBoxes.First().labelAndFocalLength.Add(key.Value, int.Parse(getDigits.Value));

                        }
                    }
                    else
                    {
                        var newBox = new Box();
                        newBox.number = boxNumber;
                        newBox.labelAndFocalLength.Add(key.Value, int.Parse(getDigits.Value));

                        boxes.Add(newBox);
                    }
                }
                else
                {
                    // remove label
                    var box = boxes.Where(x => x.number == boxNumber);
                    if (box.Count() > 0 && box.First().labelAndFocalLength.ContainsKey(key.Value))
                    {
                        box.First().labelAndFocalLength.Remove(key.Value);
                    }
                }
            }
            return boxes;
        }

        private static void Part1(string[] input)
        {
            var sum = 0;
            foreach (var line in input)
            {
                sum = HashFunction(sum, line);
            }

            Console.WriteLine($"Sum of results: {sum}");
        }

        private static int HashFunction(int sum, string line)
        {
            var currentValue = 0;
            foreach (var el in line)
            {
                currentValue += el;
                currentValue *= 17;
                currentValue = currentValue % 256;
            }
            sum += currentValue;
            return sum;
        }
    }

    public class Box
    {
        public int number;
        public Dictionary<string, int> labelAndFocalLength = new Dictionary<string, int>();
    }

    public class Element
    {
        public int Slot { get; set; }
        public int Value { get; set; }
}
