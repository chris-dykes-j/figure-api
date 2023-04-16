using Figure.WebAPI.Context;
using Figure.WebAPI.DTOs;
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
        var query = 
            from figure in _context.Figures
            where figure.Id == id
            from figureName in _context.FigureNames.Where(x => x.FigureId == figure.Id && x.LanguageCode == languageCode)
                .DefaultIfEmpty()
            from seriesName in _context.SeriesNames.Where(x => x.FigureId == figure.Id && x.LanguageCode == languageCode)
                .DefaultIfEmpty()
            select new FigureDto
            {
                Id = figure.Id,
                Scale = figure.Scale,
                Brand = figure.Brand,
                OriginUrl = figure.OriginUrl,
                FigureName = figureName.Text,
                SeriesName = seriesName.Text,
                Characters = _context.CharacterNames.Where(x => x.FigureId == figure.Id && x.LanguageCode == languageCode)
                    .Select(x => x.Text).ToList(),
                Sculptors = _context.Sculptors.Where(x => x.FigureId == figure.Id && x.LanguageCode == languageCode)
                    .Select(x => x.Text).ToList(),
                Painters = _context.Painters.Where(x => x.FigureId == figure.Id && x.LanguageCode == languageCode)
                    .Select(x => x.Text).ToList(),
                Materials = _context.Materials.Where(x => x.FigureId == figure.Id)
                    .Select(x => x.MaterialType).ToList(),
                Measurements = _context.Measurements.Where(x => x.FigureId == figure.Id && x.LanguageCode == languageCode)
                    .Select(x => x.Text).ToList(),
                ReleaseYears = _context.ReleaseDates.Where(x => x.FigureId == figure.Id)
                    .Select(x => x.ReleaseYear).ToList(),
                ReleaseMonths = _context.ReleaseDates.Where(x => x.FigureId == figure.Id)
                    .Select(x => x.ReleaseMonth).ToList(),
                PricesWithTax =
                    _context.Prices.Where(x => x.FigureId == figure.Id)
                        .Select(x => x.PriceWithTax).ToList(),
                PricesWithoutTax = _context.Prices.Where(x => x.FigureId == figure.Id)
                    .Select(x => x.PriceWithoutTax).ToList(),
                Edition = _context.Prices.Where(x => x.FigureId == figure.Id)
                    .Select(x => x.Edition).FirstOrDefault(),
                BlogUrls = _context.BlogUrls.Where(x => x.FigureId == figure.Id).Select(x => x.Url).ToList()
            };
        return await query.FirstOrDefaultAsync();
    }

    public async Task<List<FigureDto>> GetListOfFigures(string languageCode = "ja")
    {
        var query = 
            from figure in _context.Figures
            from figureName in _context.FigureNames.Where(x => x.FigureId == figure.Id && x.LanguageCode == languageCode)
                .DefaultIfEmpty()
            from seriesName in _context.SeriesNames.Where(x => x.FigureId == figure.Id && x.LanguageCode == languageCode)
                .DefaultIfEmpty()
            select new FigureDto
            {
                Id = figure.Id,
                Scale = figure.Scale,
                Brand = figure.Brand,
                OriginUrl = figure.OriginUrl,
                FigureName = figureName.Text,
                SeriesName = seriesName.Text,
                Characters = _context.CharacterNames.Where(x => x.FigureId == figure.Id && x.LanguageCode == languageCode)
                    .Select(x => x.Text).ToList(),
                Sculptors = _context.Sculptors.Where(x => x.FigureId == figure.Id && x.LanguageCode == languageCode)
                    .Select(x => x.Text).ToList(),
                Painters = _context.Painters.Where(x => x.FigureId == figure.Id && x.LanguageCode == languageCode)
                    .Select(x => x.Text).ToList(),
                Materials = _context.Materials.Where(x => x.FigureId == figure.Id)
                    .Select(x => x.MaterialType).ToList(),
                Measurements = _context.Measurements.Where(x => x.FigureId == figure.Id && x.LanguageCode == languageCode)
                    .Select(x => x.Text).ToList(),
                ReleaseYears = _context.ReleaseDates.Where(x => x.FigureId == figure.Id)
                    .Select(x => x.ReleaseYear).ToList(),
                ReleaseMonths = _context.ReleaseDates.Where(x => x.FigureId == figure.Id)
                    .Select(x => x.ReleaseMonth).ToList(),
                PricesWithTax = _context.Prices.Where(x => x.FigureId == figure.Id)
                    .Select(x => x.PriceWithTax).ToList(),
                PricesWithoutTax = _context.Prices.Where(x => x.FigureId == figure.Id)
                    .Select(x => x.PriceWithoutTax).ToList(),
                Edition = _context.Prices.Where(x => x.FigureId == figure.Id)
                    .Select(x => x.Edition).FirstOrDefault(),
                BlogUrls = _context.BlogUrls.Where(x => x.FigureId == figure.Id)
                    .Select(x => x.Url).ToList()
            };
        return await query.ToListAsync();
    }
}