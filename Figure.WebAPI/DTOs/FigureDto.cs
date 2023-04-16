namespace Figure.WebAPI.DTOs;

public class FigureDto
{
    public int Id { get; set; }
    public string Scale { get; set; }
    public string Brand { get; set; }
    public string OriginUrl { get; set; }
    public string FigureName { get; set; }
    public string SeriesName { get; set; }
    public List<string> Characters { get; set; }
    public List<string> Sculptors { get; set; }
    public List<string> Painters { get; set; }
    public List<string> Materials { get; set; }
    public List<string> Measurements { get; set; }
    public List<int> ReleaseYears { get; set; }
    public List<short> ReleaseMonths { get; set; }
    public List<int?> PricesWithTax { get; set; }
    public List<int?> PricesWithoutTax { get; set; }
    public string Edition { get; set; }
    public List<string> BlogUrls { get; set; }
}
