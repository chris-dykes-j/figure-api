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
        return await (
            from figure in _context.Figures
            where figure.Id == id
            join figureName in _context.FigureNames on figure.Id equals figureName.FigureId into fnGroup
            from fn in fnGroup.DefaultIfEmpty().Where(x => x.LanguageCode == languageCode)
            join seriesName in _context.SeriesNames on figure.Id equals seriesName.FigureId into snGroup
            from sn in snGroup.DefaultIfEmpty().Where(x => x.LanguageCode == languageCode)
            join characterName in _context.CharacterNames on figure.Id equals characterName.FigureId into cnGroup
            from cn in cnGroup.DefaultIfEmpty().Where(x => x.LanguageCode == languageCode)
            join sculptor in _context.Sculptors on figure.Id equals sculptor.FigureId into sGroup
            from s in sGroup.DefaultIfEmpty().Where(x => x.LanguageCode == languageCode)
            join painter in _context.Painters on figure.Id equals painter.FigureId into pGroup
            from p in pGroup.DefaultIfEmpty().Where(x => x.LanguageCode == languageCode)
            join material in _context.Materials on figure.Id equals material.FigureId into mGroup
            from m in mGroup.DefaultIfEmpty()
            join measurement in _context.Measurements on figure.Id equals measurement.FigureId into meGroup
            from me in meGroup.DefaultIfEmpty().Where(x => x.LanguageCode == languageCode)
            join releaseDate in _context.ReleaseDates on figure.Id equals releaseDate.FigureId into rdGroup
            from rd in rdGroup.DefaultIfEmpty()
            join price in _context.Prices on figure.Id equals price.FigureId into prGroup
            from pr in prGroup.DefaultIfEmpty()
            join blogUrl in _context.BlogUrls on figure.Id equals blogUrl.FigureId into buGroup
            from bu in buGroup.DefaultIfEmpty()
            select new FigureDto
            {
                Id = figure.Id,
                Scale = figure.Scale,
                Brand = figure.Brand,
                OriginUrl = figure.OriginUrl,
                FigureName = fn.Text,
                SeriesName = sn.Text,
                CharacterName = cn.Text,
                Sculptors = sGroup.Where(x => x.LanguageCode == languageCode).Select(x => x.Text).ToList(),
                Painters = pGroup.Where(x => x.LanguageCode == languageCode).Select(x => x.Text).ToList(),
                Materials = new List<string>(),//mGroup.Select(x => x.Material).ToList(),
                Measurements = meGroup.Where(x => x.LanguageCode == languageCode).Select(x => x.Text).ToList(),
                ReleaseYears = rdGroup.Select(x => x.ReleaseYear).ToList(),
                ReleaseMonths = new List<int>(),//rdGroup.Select(x => x.ReleaseMonth).ToList(),
                PricesWithTax = prGroup.Select(x => x.PriceWithTax).ToList(),
                PricesWithoutTax = prGroup.Select(x => x.PriceWithoutTax).ToList(),
                Edition = pr.Edition,
                BlogUrls = new List<string>()//buGroup.Select(x => x.BlogUrl).ToList()
            })
            .FirstOrDefaultAsync();
    }
}