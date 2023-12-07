
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace day7
{
    public partial class Day7
    {
        public void Run()
        {
            var text = new StreamReader("input.txt").ReadToEnd();

            var input = ReplaceInput(text).Split("\r\n");

            ParseInput(input, out Dictionary<string, int>  dict, out Dictionary<Type, List<string>> typeDict);
            int totalWinnings = RankAndCount(dict, typeDict);

            Console.WriteLine("TotalWinnings: " + totalWinnings);
        }

        private string ReplaceInput(string input)
        {
            input = new Regex("A").Replace(input, "E");
            input = new Regex("K").Replace(input, "D");
            input = new Regex("Q").Replace(input, "C");
            input = new Regex("J").Replace(input, "!");
            input = new Regex("T").Replace(input, "A");
            return input;
        }

        private void ParseInput(string[] input, out Dictionary<string, int> dict, out Dictionary<Type, List<string>> typeDict)
        {
            dict = new Dictionary<string, int>();
            typeDict = new Dictionary<Type, List<string>>() //hand, rank
            {
                { Type.HighCard, new List<string>()},
                { Type.OnePair, new List<string>()},
                { Type.TwoPair, new List<string>()},
                { Type.ThreeOfAKind, new List<string>()},
                { Type.FullHouse, new List<string>()},
                { Type.FourOfAKind, new List<string>()},
                { Type.FiveOfAKind, new List<string>()},
            };
            foreach (var line in input)
            {
                var hand = line.Split();
                dict.Add(hand[0], int.Parse(hand[1]));
                CreateTypeDict(hand[0], typeDict);
            }
        }

        public int RankAndCount(Dictionary<string, int> dict, Dictionary<Type, List<string>> typeDict)
        {
            var RankSorter = new RankComparer();
            var rankCount = 1;
            var totalWinnings = 0;
            foreach (var key in typeDict.Keys)
            {
                var listOfHandsPerType = typeDict.GetValueOrDefault(key);
                if (listOfHandsPerType == null)
                {
                    continue;
                }

                var orderedHands = listOfHandsPerType.OrderBy(x => x, RankSorter);

                foreach (var hand in orderedHands)
                {
                    var bid = dict.GetValueOrDefault(hand);
                    totalWinnings += rankCount * bid;
                    rankCount++;
                }
            }
            return totalWinnings;
        }

        public void CreateTypeDict(string hand, Dictionary<Type, List<string>> typeDict)
        {
            var addedHand = false;
            var jOccurances = CountCharOccurances(hand, '!');
            switch (hand.Distinct().Count())
            {                
                case 1:
                    typeDict.GetValueOrDefault(Type.FiveOfAKind)?.Add(hand);
                    break;
                case 2:
                    foreach (var val in hand.Distinct())
                    {
                        var noOfChars = CountCharOccurances(hand, val);
                        if(val == '!')
                        {
                            typeDict.GetValueOrDefault(Type.FiveOfAKind)?.Add(hand);
                            addedHand = true;
                            break;
                        }
                        if (noOfChars == 1 && !addedHand)
                        {
                            typeDict.GetValueOrDefault(Type.FourOfAKind)?.Add(hand);
                            addedHand = true;
                            break;
                        }
                    }
                    if (!addedHand)
                    {
                            typeDict.GetValueOrDefault(Type.FullHouse)?.Add(hand);
                        
                    }                    
                    break;
                case 3:
                    foreach (var val in hand.Distinct())
                    {
                        var noOfChars = CountCharOccurances(hand, val);

                        if (!addedHand && noOfChars == 3)
                        {
                            switch (jOccurances)
                            {
                                case 1: case 3:
                                    typeDict.GetValueOrDefault(Type.FourOfAKind)?.Add(hand);
                                    addedHand = true;
                                    break;
                                default:
                                    typeDict.GetValueOrDefault(Type.ThreeOfAKind)?.Add(hand);
                                    addedHand = true;
                                    break;
                            }
                            break;
                        }
                    }
                    if (!addedHand)
                    {
                        switch (jOccurances)
                        {
                            case 2:
                                typeDict.GetValueOrDefault(Type.FourOfAKind)?.Add(hand);
                                break;
                            case 1:
                                typeDict.GetValueOrDefault(Type.FullHouse)?.Add(hand);
                                break;
                            default:
                                typeDict.GetValueOrDefault(Type.TwoPair)?.Add(hand);
                                break;
                        }
                        
                    }                                            
                    break;
                case 4:
                    switch (jOccurances)
                    {
                        case 2: case 1:
                            typeDict.GetValueOrDefault(Type.ThreeOfAKind)?.Add(hand);
                            break;
                        default:
                            typeDict.GetValueOrDefault(Type.OnePair)?.Add(hand);
                            break;

                    }
                    break;
                case 5:
                    switch (jOccurances)
                    {
                        case 1:
                            typeDict.GetValueOrDefault(Type.OnePair)?.Add(hand);
                            break;
                        default:
                            typeDict.GetValueOrDefault(Type.HighCard)?.Add(hand);
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        public int CountCharOccurances(string source, char toFind)
        {
            int count = 0;
            foreach (var ch in source)
            {
                if (ch == toFind)
                    count++;
            }
            return count;
        }

    }

    public class RankComparer : IComparer<string>
    { 
        public int Compare(string x, string y)
        {
            for (var i = 0; i < x.Length; i++)
            {
                if (x[i] == y[i])
                {
                    continue;
                }
                if (x[i] > y[i])
                    return 1;
                else
                    return -1;
            }
            return 0; 
        }
    }

    public enum Type
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind,
    }
}
