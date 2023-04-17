using Figure.WebAPI.DTOs;
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
}