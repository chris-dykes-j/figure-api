using Figure.WebAPI.DTOs;
using Figure.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Figure.WebAPI.Controllers;

[ApiController]
[Route("/figure")]
public class FigureController : ControllerBase
{
    private readonly FigureService _figureService;

    public FigureController(FigureService figureService)
    {
        _figureService = figureService;
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<FigureDto?>> GetFigureById(int id)
    {
        var figure = await _figureService.GetFigureById(id);
        return figure.Result != null ? Ok(figure) : NotFound();
    }

    // Get list of figures. Paginate results. Include search and filter queries.
    [HttpGet]
    public async Task<ActionResult<List<FigureDto>>> GetListOfFigures()
    {
        return Ok(await _figureService.GetListOfFigures());
    }
}