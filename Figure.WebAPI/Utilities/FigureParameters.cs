namespace Figure.WebAPI.Utilities;

public class FigureParameters
{
    public string? SearchQuery { get; set; }
    public string Language { get; set; } = Constants.DefaultLanguage; 

    public int PageNumber { get; set; } = 1;

    private int _pageSize = Constants.DefaultPageSize;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > Constants.MaxPageSize ? Constants.MaxPageSize : value;
    }
}