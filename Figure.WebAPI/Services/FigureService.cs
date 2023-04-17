using Figure.WebAPI.DTOs;
using Figure.WebAPI.Repositories;

namespace Figure.WebAPI.Services;

public class FigureService
{
    private readonly FigureRepository _figureRepository;

    public FigureService(FigureRepository figureRepository)
    {
        _figureRepository = figureRepository;
    }

    public async Task<FigureDto?> GetFigureById(int id, string languageCode)
    {
        return await _figureRepository.GetFigureById(id, languageCode);
    }

    public async Task<List<FigureDto>> GetListOfFigures(string languageCode)
    {
        return await _figureRepository.GetListOfFigures(languageCode);
    }
}