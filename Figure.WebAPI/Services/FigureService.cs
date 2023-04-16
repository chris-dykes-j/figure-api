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

    public async Task<FigureDto?> GetFigureById(int id) => await _figureRepository.GetFigureById(id);

    public async Task<List<FigureDto>> GetListOfFigures() => await _figureRepository.GetListOfFigures();
}