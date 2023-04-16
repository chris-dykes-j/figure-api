using Figure.WebAPI.Context;
using Figure.WebAPI.DTOs;
using Figure.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Figure.WebAPI.Repositories;

public class FigureRepository
{
    private readonly FiguresDbContext _context;

    public FigureRepository(FiguresDbContext context)
    {
        _context = context;
    }
    
    public async Task<FigureDto?> GetFigureById(int id, string languageCode = "ja")
    {
        return await GetFiguresQuery(_context.Figures.Where(x => x.Id == id),
            languageCode).FirstOrDefaultAsync();
    }

    public async Task<List<FigureDto>> GetListOfFigures(string languageCode = "ja")
    {
        return await GetFiguresQuery(_context.Figures, languageCode).ToListAsync();
    }

    private IQueryable<FigureDto> GetFiguresQuery(IQueryable<AnimeFigure> figures, string languageCode = "ja")
    {
        return from figure in figures
            let figureName = figure.FigureNames.FirstOrDefault(x => x.LanguageCode == languageCode)
            let seriesName = figure.SeriesNames.FirstOrDefault(x => x.LanguageCode == languageCode)
            let characterNames = figure.CharacterNames.Where(x => x.LanguageCode == languageCode).Select(x => x.Text).ToList()
            let sculptors = figure.Sculptors.Where(x => x.LanguageCode == languageCode).Select(x => x.Text).ToList()
            let painters = figure.Painters.Where(x => x.LanguageCode == languageCode).Select(x => x.Text).ToList()
            let materials = figure.Materials.Select(x => x.MaterialType).ToList()
            let measurements = figure.Measurements.Where(x => x.LanguageCode == languageCode).Select(x => x.Text).ToList()
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
}