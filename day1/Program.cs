using System.Text.RegularExpressions;

namespace day1
{
    public partial class Program
    {
        static void Main()
        {
            using var sr = new StreamReader("input.txt");
            var count = 0;
            var finalDigit = 0;
            while (!sr.EndOfStream)
            {
                var input = sr.ReadLine() ?? throw new Exception();
                string digits = input;
                digits = Part2(input);
                digits = LowerCaseLetters().Replace(digits, "");
                switch (digits.Length)
                {
                    case 0:
                        // should not end up here
                        break;
                    case 1:
                        finalDigit = int.Parse(digits + digits);
                        break;
                    case 2:
                        finalDigit = int.Parse(digits);
                        break;
                    default:
                        finalDigit = int.Parse(digits[0].ToString() + digits[^1].ToString());
                        break;
                }
                count += finalDigit;
            }
            Console.WriteLine(count);
        }

        private static string Part2(string digits)
        {

            var matchCollection = NumbersAsString().Matches(digits);

            foreach (Match match in matchCollection.Cast<Match>())
            {

                switch (match.Groups[1].Value)
                {
                    case "one":
                        digits = One().Replace(digits, "1ne");
                        break;
                    case "two":
                        digits = Two().Replace(digits, "2wo");
                        break;
                    case "three":
                        digits = Three().Replace(digits, "3hree");
                        break;
                    case "four":
                        digits = Four().Replace(digits, "4our");
                        break;
                    case "five":
                        digits = Five().Replace(digits, "5ive");
                        break;
                    case "six":
                        digits = Six().Replace(digits, "6ix");
                        break;
                    case "seven":
                        digits = Seven().Replace(digits, "7even");
                        break;
                    case "eight":
                        digits = Eight().Replace(digits, "8ight");
                        break;
                    case "nine":
                        digits = Nine().Replace(digits, "9ine");
                        break;
                    default:
                        break;
                }
            }

            return digits;
        }

        [GeneratedRegex("four")]
        private static partial Regex Four();
        [GeneratedRegex("nine")]
        private static partial Regex Nine();
        [GeneratedRegex("eight")]
        private static partial Regex Eight();
        [GeneratedRegex("one")]
        private static partial Regex One();
        [GeneratedRegex("two")]
        private static partial Regex Two();
        [GeneratedRegex("three")]
        private static partial Regex Three();
        [GeneratedRegex("five")]
        private static partial Regex Five();
        [GeneratedRegex("six")]
        private static partial Regex Six();
        [GeneratedRegex("seven")]
        private static partial Regex Seven();
        [GeneratedRegex("(?=(one|two|three|four|five|six|seven|eight|nine))")]
        private static partial Regex NumbersAsString();
        [GeneratedRegex("[a-z]")]
        private static partial Regex LowerCaseLetters();
    }
}