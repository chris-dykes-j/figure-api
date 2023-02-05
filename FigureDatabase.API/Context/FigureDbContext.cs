using FigureDatabase.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FigureDatabase.API.Context;

public class FigureDbContext : DbContext
{
    public FigureDbContext(DbContextOptions<FigureDbContext> options) : base(options) { }

    public DbSet<FigureModel> Figures { get; set; } = null!;
}

