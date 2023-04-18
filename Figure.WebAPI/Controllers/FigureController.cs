using Figure.WebAPI.Models;
using Figure.WebAPI.Services;
using Figure.WebAPI.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Figure.WebAPI.Controllers;

[ApiController]
[Route("/figure")]
public class FigureController : ControllerBase
{
    private readonly FigureService _figureService;
    
    private const string DefaultLanguage = Constants.DefaultLanguage;
    
    public FigureController(FigureService figureService)
    {
        _figureService = figureService;
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<FigureModel?>> GetFigureById(int id, [FromQuery] string? language = DefaultLanguage)
    {
        var figure = await _figureService.GetFigureById(id, language!);
        return figure != null ? Ok(figure) : NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<List<FigureModel>>> GetListOfFigures([FromQuery] FigureParameters figureParameters)
    {
        return Ok(await _figureService.GetListOfFigures(figureParameters));
    }
}