namespace Figure.WebAPI.Utilities;

public class Date
{
    public int Year { get; set; }
    public Month Month { get; set; }
}

public enum Month
{
    January,
    February,
    March,
    April,
    May,
    June,
    July,
    August,
    September,
    October,
    November,
    December
}