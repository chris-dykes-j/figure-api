using FigureDatabase.API.Context;
using FigureDatabase.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FigureDatabase.API.Repositories;

public class FigureRepository : IFigureRepository
{
    private readonly FigureDbContext _context;

    public FigureRepository(FigureDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<FigureModel?> GetFigureById(int id)
    {
        return await _context.Figures
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<FigureModel>> GetListOfFigures()
    {
        throw new NotImplementedException();
    }
    
    public async Task<IEnumerable<FigureModel>> GetListOfFigures(string characterName, string brandName)
    {
        if (string.IsNullOrWhiteSpace(characterName) && string.IsNullOrWhiteSpace(brandName))
            return await GetListOfFigures();

        characterName = characterName.Trim();
        
        return await _context.Figures
            .Where(x => x.CharacterName == characterName)
            .ToListAsync();
    }
}