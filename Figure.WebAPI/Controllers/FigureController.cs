using Figure.WebAPI.DTOs;
using Figure.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Figure.WebAPI.Controllers;

[ApiController]
[Route("/scale-figure")]
public class FigureController : ControllerBase
{
    private readonly FigureService _figureService;

    public FigureController(FigureService figureService)
    {
        _figureService = figureService;
    }
    
    [HttpGet]
    [Route("/{id:int}")]
    public async Task<ActionResult<FigureDto>> GetFigureById(int id) => 
        (ActionResult<FigureDto>) Ok(await _figureService.GetFigureById(id)) ?? NotFound();

    // Get list of figures. Paginate results. Include search and filter queries.
    
}