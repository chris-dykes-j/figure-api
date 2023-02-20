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
        return await _context.Figures.ToListAsync();
    }
    
    public async Task<IEnumerable<FigureModel>> GetListOfFigures(string character, string brand)
    {
        if (string.IsNullOrWhiteSpace(character) && string.IsNullOrWhiteSpace(brand))
        {
            return await GetListOfFigures();
        }

        var collection = _context.Figures as IQueryable<FigureModel>;

        if (!string.IsNullOrWhiteSpace(character))
        {
            character = character.ToLower().Replace(" ", "");
            collection = collection.Where(x=> x.Character.ToLower().Replace(" ", "").Contains(character)
                || x.Name.ToLower().Replace(" ", "").Contains(character));
        }

        if (!string.IsNullOrWhiteSpace(brand))
        {
            brand = brand.ToLower().Replace(" ", "");
            collection = collection.Where(x => x.Brand.ToLower().Replace(" ", "").Contains(brand));
        }
        
        return await collection.ToListAsync();
    }
}