namespace Figure.WebAPI.Utilities;

public class FigureParameters
{
    public string? SearchQuery { get; set; }
    public string? FigureName { get; set; }
    public string? Character { get; set; }
    public string? Series { get; set; }
    public int? Year { get; set; }
    public string? Month { get; set; }
    public string? Brand { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public string? Scale { get; set; }
    public string? Sculptor { get; set; }
    public string? Painter { get; set; }
    public string Language { get; set; } = Constants.DefaultLanguage;
    public string SortBy { get; set; } = "Date";
    public string Order { get; set; } = "Desc";
    
    public int PageNumber { get; set; } = 1;

    private int _pageSize = Constants.DefaultPageSize;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > Constants.MaxPageSize ? Constants.MaxPageSize : value;
    }
}