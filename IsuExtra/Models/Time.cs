using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Time
    {
        public Time(string time)
        {
            if (time.Length != 5)
                throw new IsuExtraException("Time is wrong");

            Hours = short.Parse(time.Substring(0, 2));
            Minutes = short.Parse(time.Substring(3, 2));

            if (Hours is > 24 or < 0)
                throw new IsuExtraException("There's no such time");
            if (Minutes is > 60 or < 0)
                throw new IsuExtraException("There's no such time");
        }

        public int Hours { get; private set; }
        public int Minutes { get; private set; }
    }
}