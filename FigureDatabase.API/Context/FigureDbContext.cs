using FigureDatabase.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FigureDatabase.API.Context;

public class FigureDbContext : DbContext
{
    public FigureDbContext(DbContextOptions<FigureDbContext> options) : base(options) { }

    public DbSet<FigureModel> Figures { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FigureModel>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("alter_figures");

            entity.Property(e => e.BlogUrl).HasColumnName("blog_url");
            entity.Property(e => e.Brand).HasColumnName("brand");
            entity.Property(e => e.Character).HasColumnName("character");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Material).HasColumnName("material");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Painter).HasColumnName("painter");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Release).HasColumnName("release");
            entity.Property(e => e.Sculptor).HasColumnName("sculptor");
            entity.Property(e => e.Series).HasColumnName("series");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.Url).HasColumnName("url");
        });
    }
}

