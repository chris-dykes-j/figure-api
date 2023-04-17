using Figure.WebAPI.DTOs;
using Figure.WebAPI.Models;
using Figure.WebAPI.Repositories;
using Figure.WebAPI.Utilities;

namespace Figure.WebAPI.Services;

public class FigureService
{
    private readonly FigureRepository _figureRepository;

    public FigureService(FigureRepository figureRepository)
    {
        _figureRepository = figureRepository;
    }

    public async Task<FigureDto?> GetFigureById(int id, string language)
    {
        return await _figureRepository.GetFigureById(id, language);
    }

    public async Task<List<FigureDto>> GetListOfFigures(FigureParameters figureParameters)
    {
        return await _figureRepository.GetListOfFigures(figureParameters);
    }

    private FigureModel MapToFigureModel(FigureDto figureDto)
    {
        return new FigureModel
        {
            Id = figureDto.Id,
            Scale = figureDto.Scale,
            Brand = figureDto.Brand,
            OriginUrl = figureDto.OriginUrl,
            FigureName = figureDto.FigureName,
            SeriesName = figureDto.SeriesName,
            Characters = figureDto.Characters,
            Sculptors = figureDto.Sculptors,
            Painters = figureDto.Painters,
            Materials = figureDto.Materials,
            Measurements = figureDto.Measurements,
            ReleaseYears = figureDto.ReleaseYears,
            ReleaseMonths = new List<Month>(), //figureDto.ReleaseMonths,
            PricesWithTax = figureDto.PricesWithTax,
            PricesWithoutTax = figureDto.PricesWithoutTax,
            Edition = figureDto.Edition,
            BlogUrls = figureDto.BlogUrls
        };
    }
}