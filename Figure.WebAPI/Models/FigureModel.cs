namespace Figure.WebAPI.Models;

public class FigureModel
{
    public int Id { get; set; }
    public string Scale { get; set; }
    public string Brand { get; set; }
    public string OriginUrl { get; set; }
    public string FigureName { get; set; }
    public string SeriesName { get; set; }
    public string CharacterName { get; set; }
    public List<string> Sculptor { get; set; }
    public List<string> Painter { get; set; }
    public List<string> Material { get; set; }
    public List<string> Measurement { get; set; }
    public List<int> ReleaseYears { get; set; }
    public List<int> ReleaseMonths { get; set; }
    public List<int?> PricesWithTax { get; set; }
    public List<int?> PricesWithoutTax { get; set; }
    public string Edition { get; set; }
    public List<string> BlogUrls { get; set; }
}
