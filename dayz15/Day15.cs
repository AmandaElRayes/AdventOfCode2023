namespace dayz15
{
    public class Day15
    {
        public void Run()
        {
            Console.WriteLine(123);

        }

        public static StreamReader ReadFile()
        {
            ///////////////HELPLINES///////////////////////
            // ADD THIS IN RUN
            //var sr = ReadFile();
            // THEN YOU CAN DO ONE OF THESE
            //var input = sr.ReadToEnd();
            //var line = sr.ReadLine();
            //while (!sr.EndOfStream)
            //{
            //    var line = sr.ReadLine();
            //}
            ///////////////////////////////////////////////
            using var sr = new StreamReader("input.txt");
            return sr;
        }
    }
}
