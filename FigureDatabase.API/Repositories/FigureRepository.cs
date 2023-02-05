using FigureDatabase.API.Context;
using FigureDatabase.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FigureDatabase.API.Repositories;

public class FigureRepository
{
    private readonly FigureDbContext _context;

    public FigureRepository(FigureDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<FigureModel?> GetFigureById(int id)
    {
        /*
        return await _context.Figures
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
            */
        return new FigureModel(1, "Miku");
    }
}