using Figure.WebAPI.DTOs;
using Figure.WebAPI.Utilities;

namespace Figure.WebAPI.Models;

public class FigureModel
{
    public int Id { get; private set; }
    public string Scale { get; private set; }
    public string Brand { get; private set; }
    public string OriginUrl { get; private set; }
    public string FigureName { get; private set; }
    public string SeriesName { get; private set; }
    public List<string> Characters { get; private set; }
    public List<string> Sculptors { get; private set; }
    public List<string> Painters { get; private set; }
    public List<string> Materials { get; private set; }
    public List<string> Measurements { get; private set; }
    public List<string> BlogUrls { get; private set; }
    public List<Date> ReleaseDates { get; private set; }
    public List<Release> ReleasePrices { get; private set; }

    public FigureModel(FigureDto figureDto)
    {

        Id = figureDto.Id;
        Scale = figureDto.Scale;
        Brand = figureDto.Brand;
        OriginUrl = figureDto.OriginUrl;
        FigureName = figureDto.FigureName;
        SeriesName = figureDto.SeriesName;
        Characters = figureDto.Characters;
        Sculptors = figureDto.Sculptors;
        Painters = figureDto.Painters;
        Materials = figureDto.Materials;
        Measurements = figureDto.Measurements;
        BlogUrls = figureDto.BlogUrls;
        ReleaseDates = GetReleaseDates(figureDto.ReleaseYears, figureDto.ReleaseMonths);
        ReleasePrices = GetReleasePrices(figureDto.PricesWithTax, figureDto.PricesWithoutTax, figureDto.Edition);
    }

    private List<Date> GetReleaseDates(List<int> releaseYears, List<short> releaseMonths)
    {
        return releaseYears.Select((year, i) => new Date(year, releaseMonths[i])).ToList();
    }

    private List<Release> GetReleasePrices(List<int?> pricesWithTax, List<int?> pricesWithoutTax, List<string> editions)
    {
        return editions.Select((edition, i) => new Release(edition, pricesWithTax[i], pricesWithoutTax[i])).ToList();
    }
}
