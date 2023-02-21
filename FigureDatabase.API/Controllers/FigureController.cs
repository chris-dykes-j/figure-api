using FigureDatabase.API.Models;
using FigureDatabase.API.Repositories;
using FigureDatabase.API.ResourceParameters;
using Microsoft.AspNetCore.Mvc;

namespace FigureDatabase.API.Controllers;

[ApiController]
[Route("/figures")]
public class FigureController : ControllerBase
{
    private readonly IFigureRepository _repository;

    public FigureController(IFigureRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet]
    public async Task<ActionResult<List<FigureModel>>> GetListOfFigures([FromQuery] FiguresParameters figuresParameters) 
    {
        return Ok (await _repository.GetListOfFigures(figuresParameters));
    }
    
    [HttpGet]
    [Route("/{id:int}")]
    public async Task<ActionResult<FigureModel>> GetFigureById(int id)
    {
        var figure = await _repository.GetFigureById(id);
        return figure == null ? NotFound() : Ok(figure);
    }

}