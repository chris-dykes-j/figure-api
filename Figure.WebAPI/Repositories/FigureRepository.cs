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
    
    public async Task<PagedList<FigureDto>> GetListOfFigures(FigureParameters figureParameters)
    {
        var languageCode = await GetValidLanguageCode(figureParameters.Language);
        var figuresQuery = SelectFigureQuery(_context.Figures, languageCode);
        figuresQuery = ApplySearchToQuery(figuresQuery, figureParameters.SearchQuery);
        figuresQuery = ApplyFiltersToQuery(figuresQuery, figureParameters);
        figuresQuery = ApplyOrderByToQuery(figuresQuery, figureParameters.SortBy, figureParameters.Order);

        return await PagedList<FigureDto>.CreateAsync(figuresQuery, figureParameters.PageNumber,
            figureParameters.PageSize);
    }

    private IQueryable<FigureDto> ApplyOrderByToQuery(IQueryable<FigureDto> query, string sortBy, string order)
    {
        if (sortBy.ToUpper().Equals("DATE") && order.ToUpper().Equals("DESC"))
        {
            query = query.OrderByDescending(x => x.ReleaseYears.Max())
                .ThenByDescending(x => x.ReleaseMonths.Max());
        }
        if (sortBy.ToUpper().Equals("DATE") && order.ToUpper().Equals("ASC"))
        {
            query = query.OrderBy(x => x.ReleaseYears.Max())
                .ThenBy(x => x.ReleaseMonths.Max());
        }
        return query;
    }

    #region Helper Methods
    
    private async Task<bool> LanguageCodeExists(string? languageCode)
    {
        return await _context.Languages.AnyAsync(x => x.LanguageCode == languageCode);
    }
    
    private async Task<string> GetValidLanguageCode(string languageCode)
    {
        return await LanguageCodeExists(languageCode) ? languageCode : DefaultLanguage; // Could also send 406, but this is nicer
    }

    private IQueryable<FigureDto> ApplyFiltersToQuery(IQueryable<FigureDto> query, FigureParameters figureParameters)
    {
        var filters = new List<Func<IQueryable<FigureDto>, IQueryable<FigureDto>>>
        {
            q => figureParameters.FigureName != null 
                ? q.Where(x => x.FigureName.ToUpper().Contains(figureParameters.FigureName.ToUpper())) 
                : q,
            q => figureParameters.Character != null 
                ? q.Where(x => x.Characters.Any(y => y.ToUpper().Contains(figureParameters.Character.ToUpper()))) 
                : q,
            q => figureParameters.Sculptor != null 
                ? q.Where(x => x.Sculptors.Any(y => y.ToUpper().Contains(figureParameters.Sculptor.ToUpper()))) 
                : q,
            q => figureParameters.Painter != null 
                ? q.Where(x => x.Painters.Any(y => y.ToUpper().Contains(figureParameters.Painter.ToUpper()))) 
                : q,
            q => figureParameters.Series != null 
                ? q.Where(x => x.SeriesName.ToUpper().Contains(figureParameters.Series.ToUpper())) 
                : q,
            q => figureParameters.Brand != null
                ? q.Where(x => x.Brand.ToUpper().Equals(figureParameters.Brand.ToUpper())) 
                : q,
            q => figureParameters.Year != null
                ? q.Where(x => x.ReleaseYears.Any(y => y.Equals(figureParameters.Year))) 
                : q,
            q => figureParameters.Month != null 
                ? q.Where(x => x.ReleaseMonths.Any(y => y.Equals(figureParameters.Month))) 
                : q,
            q => figureParameters.MinPrice != null
                ? q.Where(x => (x.PricesWithoutTax.Any() 
                    ? x.PricesWithoutTax.Min() 
                    : x.PricesWithTax.Min()) >= figureParameters.MinPrice)
                : q,
            q => figureParameters.MaxPrice != null 
                ? q.Where(x => (x.PricesWithTax.Any() 
                    ? x.PricesWithTax.Max() 
                    : x.PricesWithoutTax.Max()) <= figureParameters.MaxPrice) 
                : q,
            q => figureParameters.Scale != null
                ? q.Where(x => x.Scale.Equals(figureParameters.Scale))
                : q
        };

        return filters.Aggregate(query, (current, filter) => filter(current));
    }

    private IQueryable<FigureDto> ApplySearchToQuery(IQueryable<FigureDto> query, string? searchQuery)
    {
        if (string.IsNullOrWhiteSpace(searchQuery)) return query;
        return query.Where(x => x.FigureName.ToUpper().Contains(searchQuery.ToUpper())
                                || x.SeriesName.ToUpper().Contains(searchQuery.ToUpper())
                                || x.Characters.Any(y => y.ToUpper().Contains(searchQuery.ToUpper()))
                                || x.Scale.Contains(searchQuery)
                                || x.Brand.ToUpper().Contains(searchQuery.ToUpper())
                                || x.Sculptors.Any(y => y.ToUpper().Contains(searchQuery.ToUpper()))
                                || x.Painters.Any(y => y.ToUpper().Contains(searchQuery.ToUpper()))
                                || x.Edition.Any(y => y.ToUpper().Contains(searchQuery.ToUpper())));
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
            let releaseYears = figure.ReleaseDates.Select(x => x.ReleaseYear).ToList()
            let releaseMonths = figure.ReleaseDates.Select(x => x.ReleaseMonth).ToList()
            let pricesWithTax = figure.Prices.Select(x => x.PriceWithTax).ToList()
            let pricesWithoutTax = figure.Prices.Select(x => x.PriceWithoutTax).ToList()
            let edition = figure.Prices.Select(x => x.Edition).ToList()
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