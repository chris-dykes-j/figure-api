using FigureDatabase.API.Context;
using FigureDatabase.API.Models;
using FigureDatabase.API.ResourceParameters;
using Microsoft.EntityFrameworkCore;

namespace FigureDatabase.API.Repositories;

public class FigureRepository : IFigureRepository
{
    private readonly FigureDbContext _context;

    public FigureRepository(FigureDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<FigureModel?> GetFigureByName(string name)
    {
        return await _context.Figures
            .Where(x => x.Name!.ToLower() == name.ToLower())
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<FigureModel>> GetListOfFigures()
    {
        return await _context.Figures.ToListAsync();
    }
    
    public async Task<IEnumerable<FigureModel>> GetListOfFigures(FiguresParameters figuresParameters)
    {
        if (figuresParameters == null)
        {
            return await GetListOfFigures();
        }

        var collection = _context.Figures as IQueryable<FigureModel>;

        if (!string.IsNullOrWhiteSpace(figuresParameters.Character))
        {
            var character = figuresParameters.Character.ToLower().Replace(" ", "");
            collection = collection.Where(x=> x.Character.ToLower().Replace(" ", "").Contains(character)
                || x.Name.ToLower().Replace(" ", "").Contains(character));
        }

        if (!string.IsNullOrWhiteSpace(figuresParameters.Brand))
        {
            var brand = figuresParameters.Brand.ToLower().Replace(" ", "");
            collection = collection.Where(x => x.Brand.ToLower().Replace(" ", "").Contains(brand));
        }
        
        return await collection.ToListAsync();
    }
}