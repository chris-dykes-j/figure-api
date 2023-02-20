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
            entity.HasKey(e => e.Id).HasName("figures_pkey");

            entity.ToTable("figures");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Brand).HasColumnName("brand");
            entity.Property(e => e.Character).HasColumnName("character");
            entity.Property(e => e.JanCode)
                .ValueGeneratedOnAdd()
                .HasColumnName("jancode");
            entity.Property(e => e.Month).HasColumnName("month");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.ProductLine).HasColumnName("productline");
            entity.Property(e => e.ReleasePrice)
                .ValueGeneratedOnAdd()
                .HasColumnName("releaseprice");
            entity.Property(e => e.Sculptor).HasColumnName("sculptor");
            entity.Property(e => e.Series).HasColumnName("series");
            entity.Property(e => e.Year)
                .ValueGeneratedOnAdd()
                .HasColumnName("year");
        });
    }
}

