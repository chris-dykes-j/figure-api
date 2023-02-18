using FigureDatabase.API.Models;

namespace FigureDatabase.API.Repositories;

public interface IFigureRepository
{
    Task<FigureModel?> GetFigureById(int id);
    Task<IEnumerable<FigureModel>> GetListOfFigures();
    Task<IEnumerable<FigureModel>> GetListOfFigures(string characterName, string brandName);
}