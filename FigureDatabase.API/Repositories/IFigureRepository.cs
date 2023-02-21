using FigureDatabase.API.Models;
using FigureDatabase.API.ResourceParameters;

namespace FigureDatabase.API.Repositories;

public interface IFigureRepository
{
    Task<FigureModel?> GetFigureById(int id);
    Task<IEnumerable<FigureModel>> GetListOfFigures();
    Task<IEnumerable<FigureModel>> GetListOfFigures(FiguresParameters figuresParameters);
}