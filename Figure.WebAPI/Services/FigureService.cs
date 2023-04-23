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

    public async Task<FigureModel?> GetFigureById(int id, string language)
    {
        return new FigureModel((await _figureRepository.GetFigureById(id, language))!);
    }

    public async Task<PagedList<FigureModel>> GetListOfFigures(FigureParameters figureParameters)
    {
        var figureDtos = await _figureRepository.GetListOfFigures(figureParameters);
        var figureModels = figureDtos.Select(figureDto => new FigureModel(figureDto)).ToList();
        return new PagedList<FigureModel>(figureModels, figureDtos.TotalCount, figureDtos.CurrentPage, figureDtos.PageSize);
    }
}