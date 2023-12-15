
using System.Text.RegularExpressions;

namespace dayz15
{
    public class Day15
    {
        public void Run()
        {
            using var sr = new StreamReader("input.txt");
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
                var slotmultvalue = new List<int>();
                var slots = new List<int>();
                foreach (var label in labels)
                {
                    slotmultvalue.Add(label.Value.Value * label.Value.Slot);
                }
                foreach (var value in slotmultvalue)
                {
                    var focus = (1 + box.number) * value;
                    focusingPower += focus;
                }
            }

            Console.WriteLine($"Part 2: total focusingPower is {focusingPower}");
        }

        private static List<Box> CreateBoxes(string[] input)
        {
            var boxes = new List<Box>();
            int slot;
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
                        var noOfSlots = isBoxInBoxes.First().labelAndFocalLength.Count();
                        var element = new Element
                        {
                            Slot = noOfSlots + 1,
                            Value = int.Parse(getDigits.Value)
                        };
                        var success = isBoxInBoxes.First().labelAndFocalLength.TryAdd(key.Value, element);
                        if (!success)
                            ReplaceLabel(key, getDigits, isBoxInBoxes);
                    }
                    else
                    {
                        AddLabelToNewBox(boxes, key, boxNumber, getDigits);
                    }
                }
                else
                {
                    // remove label , rearrange slots
                    boxes = RemoveLabel(boxes, key, boxNumber);
                }
            }
            return boxes;
        }

        private static void ReplaceLabel(Match key, Match getDigits, IEnumerable<Box> isBoxInBoxes)
        {
            // there is another of the same label there must be replaced
            var slotNoOfLabelToRemove = isBoxInBoxes.First().labelAndFocalLength
                .GetValueOrDefault(key.Value).Slot;


            isBoxInBoxes.First().labelAndFocalLength.Remove(key.Value);

            var element = new Element()
            {
                Slot = slotNoOfLabelToRemove,
                Value = int.Parse(getDigits.Value)
            };

            isBoxInBoxes.First().labelAndFocalLength.Add(key.Value, element);

        }

        private static List<Box> RemoveLabel(List<Box> boxes, Match key, int boxNumber)
        {
            var box = boxes.Where(x => x.number == boxNumber);

            if (box.Count() > 0 && box.First().labelAndFocalLength.ContainsKey(key.Value))
            {
                var slotNoOfLabelToRemove = box.First().labelAndFocalLength.GetValueOrDefault(key.Value).Slot;
                box.First().labelAndFocalLength.Remove(key.Value);

                foreach (var item in box.First().labelAndFocalLength)
                {
                    if (item.Value.Slot > slotNoOfLabelToRemove)
                    {
                        item.Value.Slot--;
                    }
                }
            }
            return boxes;
        }

        private static void AddLabelToNewBox(List<Box> boxes, Match key, int boxNumber, Match getDigits)
        {
            int slot = 1;
            var element = new Element() { Slot = slot, Value = int.Parse(getDigits.Value) };
            var newBox = new Box();
            newBox.number = boxNumber;
            newBox.labelAndFocalLength.Add(key.Value, element);

            boxes.Add(newBox);
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
        public Dictionary<string, Element> labelAndFocalLength = new Dictionary<string, Element>();
    }

    public class Element
    {
        public int Slot { get; set; }
        public int Value { get; set; }
    }
}
