
using System.Text.RegularExpressions;

namespace day4
{
    public class Day4
    {
        public void Run()
        {
            using var sr = new StreamReader("input.txt");
            var cardPoints = new List<int>();
            var matchingNumbers = new Dictionary<int, string>();
            var copies = new List<int>();
            //var noOfScratchCards = 0;
            List<int> noOfScratchCards = new();

            var cards = sr.ReadToEnd().Split("\r\n");

            for (int i = 0; i < cards.Length; i++)
            {
                noOfScratchCards = RecursiveMethod(noOfScratchCards, cards, cards[i]);
            }
            Console.WriteLine(noOfScratchCards.Count());
        }

        private static List<int> RecursiveMethod(List<int> noOfScratchCards, string[] cards, string currentCard)
        {

            noOfScratchCards.Add(1);
            GetMatchCount(currentCard, out int matchCount, out int currentCardNumber);
            if (matchCount == 0)
            {
                return noOfScratchCards;
            }

            for (int i = 0; i < matchCount; i++)
            {
                RecursiveMethod(noOfScratchCards, cards, cards[currentCardNumber + i]);
            }

            return noOfScratchCards;
        }

        public static void GetMatchCount(string card, out int matchCount, out int currentCardNo)
        {
            var line = card.Split('|');
            var winningNumbers = line[0].Split(":")[1].Split(" ");
            var mynumbers = line[1].Split(" ");
            var matchingNumbers = winningNumbers.Intersect(mynumbers).Where(x => int.TryParse(x, out int _) == true);
            matchCount = matchingNumbers.Count();
            var x = line[0].Split(":")[0];
            var u = Regex.Match(x, "\\d+").ToString();
            currentCardNo = int.Parse(u);
        }
    }
}
