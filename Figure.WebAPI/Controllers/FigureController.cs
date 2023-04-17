using Figure.WebAPI.DTOs;
using Figure.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Figure.WebAPI.Controllers;

[ApiController]
[Route("/figure")]
public class FigureController : ControllerBase
{
    private readonly FigureService _figureService;
    
    private const string DefaultLanguage = "ja";
    
    public FigureController(FigureService figureService)
    {
        _figureService = figureService;
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<FigureDto?>> GetFigureById(int id, [FromQuery] string? language = DefaultLanguage)
    {
        var figure = await _figureService.GetFigureById(id, language!);
        return figure != null ? Ok(figure) : NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<List<FigureDto>>> GetListOfFigures([FromQuery] string? language = DefaultLanguage)
    {
        return Ok(await _figureService.GetListOfFigures(language!));
    }
    
    // Filter (precise) ?language=en
    // searchQuery=nendoroid
}