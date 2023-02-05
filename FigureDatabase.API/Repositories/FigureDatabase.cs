using Microsoft.EntityFrameworkCore;

namespace FigureDatabase.API.Repositories;

public class FigureDatabase : DbContext
{
    public FigureDatabase(DbContextOptions<FigureDatabase> options) : base(options) { }

    public DbSet<FigureModel> Figures { get; set; } = null!;
}

