using FigureDatabase.API.Models;
using FigureDatabase.API.ResourceParameters;

namespace FigureDatabase.API.Repositories;

public interface IFigureRepository
{
    Task<FigureModel?> GetFigureByName(string name);
    Task<IEnumerable<FigureModel>> GetListOfFigures();
    Task<IEnumerable<FigureModel>> GetListOfFigures(FiguresParameters figuresParameters);
}