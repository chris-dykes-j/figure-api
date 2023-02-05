using Microsoft.EntityFrameworkCore;

namespace FigureDatabase.API.Repositories;

public class FigureRepository
{
    private readonly FigureDatabase _context;

    public FigureRepository(FigureDatabase context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<FigureModel?> GetFigureById(int id)
    {
        return await _context.Figures
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }
}