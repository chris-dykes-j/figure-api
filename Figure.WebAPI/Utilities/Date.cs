namespace Figure.WebAPI.Utilities;

public class Date
{
    public int Year { get; set; }

    public int Month { get; set; }

    public Date(int year, int month)
    {
        Year = year;
        Month = month;
    }
}