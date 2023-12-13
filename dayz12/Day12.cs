using System.Text.RegularExpressions;

namespace dayz12
{
    public class Day12
    {
        public void Run()
        {
            using var sr = new StreamReader("testinput.txt");
            var combinationsCounter = 0;
            while (!sr.EndOfStream)
            {
                var x = GetNumberOfCombinations(sr.ReadLine());
                combinationsCounter += x;
            }

            Console.WriteLine($"Sum of combinations: {combinationsCounter}");
        }

        private int GetNumberOfCombinations(string? line)
        {
            var numberOfCombinations = 0;

            var record = line.Split().First();
            var conditions = line.Split().Last().Split(',');
            var conditionsSum = Regex.Matches(line.Split().Last(), "\\d+").Select(x => int.Parse(x.Value)).Sum();
            var commasCount = Regex.Matches(line.Split().Last(), "\\,").Count;
            var totalConditionCount = conditionsSum + commasCount;
            var conditionsQueue = new Queue<int>();
            foreach (var condition in conditions)
            {
                conditionsQueue.Enqueue(int.Parse(condition));
            }

            if (record.Length == totalConditionCount)
            {
                return 1; // alength == blength, only 1 possible combination if all numbers and commas are equal to length of string
            }
            else
            {
                var substringsBetweenDots = Regex.Matches(record, "[^\\.]+").ToArray();

                var currentCondition = conditionsQueue.Dequeue();


                foreach (var substring in substringsBetweenDots)
                {
                    SubstringCheck(ref numberOfCombinations, conditionsQueue, ref currentCondition, substring.Value);
                }
            }
            if (numberOfCombinations == 0)
            {
                return 1;
            }
            return numberOfCombinations;
        }

        private static void SubstringCheck(ref int numberOfCombinations, Queue<int> conditionsQueue, ref int currentCondition, string substring)
        {
            // can we fit the first condition inside the first substring?
            if (substring.Length >= currentCondition)
            {
                // get substrings of hashes, check conditions
                var insideHashtags = Regex.Matches(substring, "\\#+").ToArray();

                // does the substring have same amount of hashes as condition?
                if (insideHashtags.Length != 0 && insideHashtags[0].Length >= currentCondition) // or any condition???
                {
                    // condition is satisfied => go to next condition, add before and after condition
                    // split away this part from substring and continue 

                    var startposition = insideHashtags[0].Index + insideHashtags[0].Length;
                    var length = substring.Length - startposition - 1;
                    var newSubstring = substring.Substring(startposition, length);


                    SubstringCheck(ref numberOfCombinations, conditionsQueue, ref currentCondition, newSubstring);
                    if (conditionsQueue.Count != 0)
                    {
                        currentCondition = conditionsQueue.Dequeue();
                    }


                }
                else
                {
                    //check how many? marks are there compared to #
                    var questions = Regex.Matches(substring, "\\?+").ToArray();
                    if (questions.Select(x => x.Value.Length).Sum() > currentCondition)
                    {
                        // check if there are more conditions to satisfy
                        while (conditionsQueue.Count != 0)
                        {
                            currentCondition = conditionsQueue.Dequeue();
                            var commas = conditionsQueue.Count() - 1;
                            var w = conditionsQueue.Select(x => x).Sum() + commas;
                            var newSubstring = substring.Substring(currentCondition, w + 1);
                            // substring is currentcount - rest of queue count 
                            SubstringCheck(ref numberOfCombinations, conditionsQueue, ref currentCondition, newSubstring);

                            
                        }

                            //if (conditionsQueue.Count() == 1)
                            //{
                            //    numberOfCombinations += questions.Select(x => x.Value.Length).Sum() - currentCondition + 1;
                            //}
                            //else
                            //{
                            //    var commas = conditionsQueue.Count() - 1;
                            //    numberOfCombinations += conditionsQueue.Select(x => x).Sum() + commas;
                            //    conditionsQueue.Clear();
                            //    // sum up current conditions 
                            //}

                        }


                        if (conditionsQueue.Count != 0)
                        {
                            currentCondition = conditionsQueue.Dequeue();
                        }

                    }
                }
            }
        }
    }
