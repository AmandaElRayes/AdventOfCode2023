using System.Text.RegularExpressions;

namespace day6
{
    public class Day6
    {
        public void Run()
        {
            var input = "Time:        46857582\r\nDistance:   208141212571410".Split("\r\n");
            //var testInput = "Time:      7  15   30\r\nDistance:  9  40  200".Split("\r\n");

            var times = Regex.Matches(input[0], "\\d+").ToArray();
            var distances = Regex.Matches(input[1], "\\d+").ToArray();
            var dict = new Dictionary<long, long>();
            for (int i = 0; i < times.Length; i++)
            {
                dict.Add(long.Parse(times[i].Value), long.Parse(distances[i].Value));
            }

            var totalNumberOfWaysToWin = new List<int>();
            var numberOfWaysToWin = 0;
            foreach (var race in dict)
            {
                //var myTime = race.Key;
                //var myDistance = race.Value;
                for (int buttonHold = 0; buttonHold < race.Key +1; buttonHold++)
                {
                    // if buttonhold == 0 then distance == 0ms
                    // if buttonhold == 1 then distance is 1mm/ms for 6 second (time - buttonhold) => distance = 6mm
                    // WIN if buttonhold == 2 then distance is 2mm/ms for 5 seconds => distance = 10 mm (2(speed)*5(time remaining))
                    // WIN if buttonhold == 3 then distance is 3mm/ms for 4 seconds => distance = 12mm 
                    // WIN if buttonhold == 4 then distance is 4mm/ms for 3 seconds => distance = 12mm 
                    // WIN if buttonhold == 5 then distance is 5mm/ms for 2 seconds => distance = 10mm
                    // if buttonhold == 6 then distance is 6mm/ms for 1 seconds => distance = 6mm
                    // if buttonhold == 7 then distance is 7mm/ms for 0 seconds => distance = 0mm (total time of race)
                    // TOTAL 4 WINS
                    // var myDistance = speed * timesRemaining
                    var mytime = (race.Key - buttonHold);
                    var mydistance = buttonHold * mytime;
                    if (mytime < race.Key && mydistance > race.Value)
                    {
                        numberOfWaysToWin++;                        
                    }
                }
                totalNumberOfWaysToWin.Add(numberOfWaysToWin);
                numberOfWaysToWin = 0;
            }


            //var numberOfWaysToWin = new int[1, 2, 4];
            var mult = 1;
            foreach (var winSum in totalNumberOfWaysToWin)
            {
                mult *= winSum;
            }

            Console.WriteLine(mult);
        }    
    }
}
