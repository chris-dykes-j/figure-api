using FigureDatabase.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FigureDatabase.API.Repositories;

public class MockFigureRepository : IFigureRepository
{
    private readonly List<FigureModel> _figures = new()
    {
        new()
        {
            Id = 1,
            CharacterName = "Miku",
            BrandName = "Good Smile"
        },
        new()
        {
            Id = 1,
            CharacterName = "Snow Miku",
            BrandName = "Good Smile"
        },
        new()
        {
            Id = 2,
            CharacterName = "Ryza",
            BrandName = "Wonderful Works"
        }
    };
    
    public async Task<FigureModel?> GetFigureById(int id)
    {
        return _figures[id];
    }

    public async Task<IEnumerable<FigureModel>> GetListOfFigures()
    {
        return _figures;
    }

    public async Task<IEnumerable<FigureModel>> GetListOfFigures(string characterName, string brandName)
    {
        if (string.IsNullOrWhiteSpace(characterName) && string.IsNullOrWhiteSpace(brandName))
            return await GetListOfFigures();

        var collection = _figures.ToList();

        if (!string.IsNullOrWhiteSpace(brandName))
        {
            brandName = brandName.ToLower().Replace(" ", "");
            collection = collection
                .Where(x => x.BrandName.ToLower().Replace(" ", "") == brandName)
                .ToList();
        }
        
        if (!string.IsNullOrWhiteSpace(characterName))
        {
            characterName = characterName.ToLower().Replace(" ", "");
            collection = collection
                .Where(x => x.CharacterName.ToLower().Replace(" ", "").Contains(characterName))
                .ToList();
        }
        
        return collection;
    }
}