using FigureDatabase.API.Models;
using FigureDatabase.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FigureDatabase.API.Controllers;

[Route("/")]
[ApiController]
public class FigureController : ControllerBase
{
    private readonly FigureRepository _repository;

    public FigureController(FigureRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet]
    [Route("/{id:int}")]
    public async Task<ActionResult<FigureModel>> GetFigureById(int id)
    {
        var figure = await _repository.GetFigureById(id);
        if (figure == null)
            return NotFound();
        return Ok(figure);
    }
}