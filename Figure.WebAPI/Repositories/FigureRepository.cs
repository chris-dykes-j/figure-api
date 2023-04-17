using Figure.WebAPI.Context;
using Figure.WebAPI.DTOs;
using Figure.WebAPI.Entities;
using Figure.WebAPI.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Figure.WebAPI.Repositories;

public class FigureRepository
{
    private readonly FiguresDbContext _context;

    private const string DefaultLanguage = Constants.DefaultLanguage;
    
    public FigureRepository(FiguresDbContext context)
    {
        _context = context;
    }
    
    public async Task<FigureDto?> GetFigureById(int id, string language)
    {
        language = await GetValidLanguageCode(language); 
        return await SelectFigureQuery(_context.Figures.Where(x => x.Id == id), language)
            .FirstOrDefaultAsync();
    }
    
    public async Task<List<FigureDto>> GetListOfFigures(FigureParameters figureParameters)
    {
        var languageCode = await GetValidLanguageCode(figureParameters.Language);
        var figuresQuery = SelectFigureQuery(_context.Figures, languageCode);
        if (!string.IsNullOrWhiteSpace(figureParameters.SearchQuery))
            figuresQuery = ApplySearchFilter(figuresQuery, figureParameters.SearchQuery);
        
        return await figuresQuery.ToListAsync();
    }
    
    #region Helper Methods
    
    private async Task<bool> LanguageCodeExists(string? languageCode)
    {
        return await _context.Languages.AnyAsync(x => x.LanguageCode == languageCode);
    }
    
    private async Task<string> GetValidLanguageCode(string languageCode)
    {
        return await LanguageCodeExists(languageCode) ? languageCode : DefaultLanguage;
    }

    private IQueryable<FigureDto> ApplySearchFilter(IQueryable<FigureDto> query, string searchQuery)
    {
        return query.Where(x => x.FigureName.ToUpper().Contains(searchQuery.ToUpper())
                                || x.SeriesName.ToUpper().Contains(searchQuery.ToUpper())
                                || x.Characters.Any(y => y.ToUpper().Contains(searchQuery.ToUpper()))
                                || x.Scale.Contains(searchQuery)
                                || x.Brand.ToUpper().Contains(searchQuery.ToUpper())
                                || x.Sculptors.Any(y => y.ToUpper().Contains(searchQuery.ToUpper()))
                                || x.Painters.Any(y => y.ToUpper().Contains(searchQuery.ToUpper()))
                                || x.Edition.ToUpper().Contains(searchQuery.ToUpper()));
    }

    private IQueryable<FigureDto> SelectFigureQuery(IQueryable<AnimeFigure> figures, string languageCode)
    {
        return from figure in figures
            let figureName = figure.FigureNames.FirstOrDefault(x => x.LanguageCode == languageCode)
            let seriesName = figure.SeriesNames.FirstOrDefault(x => x.LanguageCode == languageCode)
            let characterNames = figure.CharacterNames.Where(x => x.LanguageCode == languageCode)
                .Select(x => x.Text).ToList()
            let sculptors = figure.Sculptors.Where(x => x.LanguageCode == languageCode)
                .Select(x => x.Text).ToList()
            let painters = figure.Painters.Where(x => x.LanguageCode == languageCode)
                .Select(x => x.Text).ToList()
            let materials = figure.Materials.Select(x => x.MaterialType).ToList()
            let measurements = figure.Measurements.Where(x => x.LanguageCode == languageCode)
                .Select(x => x.Text).ToList()
            let releaseYears = figure.ReleaseDates.Select(x => x. ReleaseYear).ToList()
            let releaseMonths = figure.ReleaseDates.Select(x => x.ReleaseMonth).ToList()
            let pricesWithTax = figure.Prices.Select(x => x.PriceWithTax).ToList()
            let pricesWithoutTax = figure.Prices.Select(x => x.PriceWithoutTax).ToList()
            let edition = figure.Prices.Select(x => x.Edition).FirstOrDefault()
            let blogUrls = figure.BlogUrls.Select(x => x.Url).ToList()
            select new FigureDto
            {
                Id = figure.Id,
                Scale = figure.Scale,
                Brand = figure.Brand,
                OriginUrl = figure.OriginUrl,
                FigureName = figureName.Text,
                SeriesName = seriesName.Text,
                Characters = characterNames,
                Sculptors = sculptors,
                Painters = painters,
                Materials = materials,
                Measurements = measurements,
                ReleaseYears = releaseYears,
                ReleaseMonths = releaseMonths,
                PricesWithTax = pricesWithTax,
                PricesWithoutTax = pricesWithoutTax,
                Edition = edition, 
                BlogUrls = blogUrls
            }; 
    }

    #endregion
}