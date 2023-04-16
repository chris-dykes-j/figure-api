using Figure.WebAPI.DTOs;
using Figure.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Figure.WebAPI.Services;

public class FigureService
{
    private readonly FigureRepository _figureRepository;

    public FigureService(FigureRepository figureRepository)
    {
        _figureRepository = figureRepository;
    }

    public async Task<FigureDto?> GetFigureById(int id)
    {
        return await _figureRepository.GetFigureById(id);
    }
    
    public async Task<ActionResult<List<FigureDto>>> GetListOfFigures()
    {
        return await _figureRepository.GetListOfFigures();
    }
}