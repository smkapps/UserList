using System;

public class TimeConverter
{
    public static double DateTimeToDouble(DateTime time)
    {
        DateTime begin = new DateTime(1970, 1, 1);
        return (time - begin).TotalSeconds;
    }

    public static DateTime SecondsToDateTime(double seconds)
    {
        DateTime begin = new DateTime(1970, 1, 1);
        return begin.AddSeconds(seconds);
    }


}