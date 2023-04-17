namespace Figure.WebAPI.Utilities;

public class FigureParameters
{
    public string? SearchQuery { get; set; }
    public string Language { get; set; } = Constants.DefaultLanguage;
}