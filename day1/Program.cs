using System.Text.RegularExpressions;

namespace day1
{
    internal class Program
    {
        static void Main()
        {
            using var sr = new StreamReader("input.txt");
            var count = 0;
            var finalDigit = 0;
            while (!sr.EndOfStream)
            {
                var input = sr.ReadLine();
                Console.WriteLine(input);
                // get numbers
                string digits = input;
                digits = Part2(input);
                Console.WriteLine(digits);
                digits = Regex.Replace(digits, "[a-z]", "");
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
                        finalDigit = int.Parse(digits[0].ToString() + digits[digits.Length - 1].ToString());
                        break;
                }
                Console.WriteLine(finalDigit);
                Console.WriteLine('\n');
                count += finalDigit;
            }
            Console.WriteLine(count);
        }

        private static string Part2(string digits)
        {

            var matchCollection = Regex.Matches(digits, "(?=(one|two|three|four|five|six|seven|eight|nine))");

            //foreach(Match m in matchCollection)
            //{
            //    var u = m.Groups[1].Value;
            //}
            //var x = "M";
            foreach (Match match in matchCollection)
            {

                switch (match.Groups[1].Value)
                {
                    case "one":
                        digits = Regex.Replace(digits, "one", "1ne");
                        break;
                    case "two":
                        digits = Regex.Replace(digits, "two", "2wo");
                        break;
                    case "three":
                        digits = Regex.Replace(digits, "three", "3hree");
                        break;
                    case "four":
                        digits = Regex.Replace(digits, "four", "4our");
                        break;
                    case "five":
                        digits = Regex.Replace(digits, "five", "5ive");
                        break;
                    case "six":
                        digits = Regex.Replace(digits, "six", "6ix");
                        break;
                    case "seven":
                        digits = Regex.Replace(digits, "seven", "7even");
                        break;
                    case "eight":
                        digits = Regex.Replace(digits, "eight", "8ight");
                        break;
                    case "nine":
                        digits = Regex.Replace(digits, "nine", "9ine");
                        break;
                    default:
                        break;
                }
            }

            return digits;
        }
    }
}