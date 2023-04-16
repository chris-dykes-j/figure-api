using Figure.WebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Figure.WebAPI.Repositories;

public class FigureRepository
{
    public async Task<ActionResult<FigureDto?>> GetFigureById(int id)
    {
        return new FigureDto();
    }
}