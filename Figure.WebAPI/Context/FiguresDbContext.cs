using Figure.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Figure.WebAPI.Context;

public partial class FiguresDbContext : DbContext
{
    public FiguresDbContext(DbContextOptions<FiguresDbContext> options) : base(options) { }

    public virtual DbSet<BlogUrl> BlogUrls { get; set; } = null!;

    public virtual DbSet<CharacterName> CharacterNames { get; set; } = null!;

    public virtual DbSet<AnimeFigure> Figures { get; set; } = null!;

    public virtual DbSet<FigureName> FigureNames { get; set; } = null!;

    public virtual DbSet<Language> Languages { get; set; } = null!;

    public virtual DbSet<Material> Materials { get; set; } = null!;

    public virtual DbSet<Measurement> Measurements { get; set; } = null!;

    public virtual DbSet<Painter> Painters { get; set; } = null!;

    public virtual DbSet<Price> Prices { get; set; } = null!;

    public virtual DbSet<ReleaseDate> ReleaseDates { get; set; } = null!;

    public virtual DbSet<Sculptor> Sculptors { get; set; } = null!;

    public virtual DbSet<SeriesName> SeriesNames { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogUrl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("blog_url_pkey");

            entity.ToTable("blog_url");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("blog_url");
            entity.Property(e => e.FigureId).HasColumnName("figure_id");

            entity.HasOne(d => d.AnimeFigure).WithMany(p => p.BlogUrls)
                .HasForeignKey(d => d.FigureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blog_url_figure_id_fkey");
        });

        modelBuilder.Entity<CharacterName>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("character_name_pkey");

            entity.ToTable("character_name");

            entity.HasIndex(e => new { e.FigureId, e.LanguageCode, e.Text }, "character_name_figure_id_language_code_text_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FigureId).HasColumnName("figure_id");
            entity.Property(e => e.LanguageCode)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("language_code");
            entity.Property(e => e.Text)
                .HasMaxLength(255)
                .HasColumnName("text");

            entity.HasOne(d => d.AnimeFigure).WithMany(p => p.CharacterNames)
                .HasForeignKey(d => d.FigureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("character_name_figure_id_fkey");

            entity.HasOne(d => d.LanguageCodeNavigation).WithMany(p => p.CharacterNames)
                .HasForeignKey(d => d.LanguageCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("character_name_language_code_fkey");
        });

        modelBuilder.Entity<AnimeFigure>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("figure_pkey");

            entity.ToTable("figure");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Brand)
                .HasMaxLength(255)
                .HasColumnName("brand");
            entity.Property(e => e.OriginUrl)
                .HasMaxLength(255)
                .HasColumnName("origin_url");
            entity.Property(e => e.Scale)
                .HasMaxLength(4)
                .HasColumnName("scale");
        });

        modelBuilder.Entity<FigureName>(entity =>
        {
            entity.HasKey(e => new { e.FigureId, e.LanguageCode }).HasName("figure_name_pkey");

            entity.ToTable("figure_name");

            entity.Property(e => e.FigureId).HasColumnName("figure_id");
            entity.Property(e => e.LanguageCode)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("language_code");
            entity.Property(e => e.Text)
                .HasMaxLength(255)
                .HasColumnName("text");

            entity.HasOne(d => d.AnimeFigure).WithMany(p => p.FigureNames)
                .HasForeignKey(d => d.FigureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("figure_name_figure_id_fkey");

            entity.HasOne(d => d.LanguageCodeNavigation).WithMany(p => p.FigureNames)
                .HasForeignKey(d => d.LanguageCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("figure_name_language_code_fkey");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.LanguageCode).HasName("languages_pkey");

            entity.ToTable("languages");

            entity.Property(e => e.LanguageCode)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("language_code");
            entity.Property(e => e.LanguageName)
                .HasMaxLength(255)
                .HasColumnName("language_name");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("material_pkey");

            entity.ToTable("material");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FigureId).HasColumnName("figure_id");
            entity.Property(e => e.MaterialType)
                .HasMaxLength(255)
                .HasColumnName("material");

            entity.HasOne(d => d.AnimeFigure).WithMany(p => p.Materials)
                .HasForeignKey(d => d.FigureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("material_figure_id_fkey");
        });

        modelBuilder.Entity<Measurement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("measurement_pkey");

            entity.ToTable("measurement");

            entity.HasIndex(e => new { e.FigureId, e.LanguageCode, e.Text }, "measurement_figure_id_language_code_text_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FigureId).HasColumnName("figure_id");
            entity.Property(e => e.LanguageCode)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("language_code");
            entity.Property(e => e.Text)
                .HasMaxLength(255)
                .HasColumnName("text");

            entity.HasOne(d => d.AnimeFigure).WithMany(p => p.Measurements)
                .HasForeignKey(d => d.FigureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("measurement_figure_id_fkey");

            entity.HasOne(d => d.LanguageCodeNavigation).WithMany(p => p.Measurements)
                .HasForeignKey(d => d.LanguageCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("measurement_language_code_fkey");
        });

        modelBuilder.Entity<Painter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("painter_pkey");

            entity.ToTable("painter");

            entity.HasIndex(e => new { e.FigureId, e.LanguageCode, e.Text }, "painter_figure_id_language_code_text_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FigureId).HasColumnName("figure_id");
            entity.Property(e => e.LanguageCode)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("language_code");
            entity.Property(e => e.Text)
                .HasMaxLength(255)
                .HasColumnName("text");

            entity.HasOne(d => d.AnimeFigure).WithMany(p => p.Painters)
                .HasForeignKey(d => d.FigureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("painter_figure_id_fkey");

            entity.HasOne(d => d.LanguageCodeNavigation).WithMany(p => p.Painters)
                .HasForeignKey(d => d.LanguageCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("painter_language_code_fkey");
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("price_pkey");

            entity.ToTable("price");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasDefaultValueSql("'JPY'::bpchar")
                .IsFixedLength()
                .HasColumnName("currency");
            entity.Property(e => e.Edition)
                .HasMaxLength(255)
                .HasColumnName("edition");
            entity.Property(e => e.FigureId).HasColumnName("figure_id");
            entity.Property(e => e.PriceWithTax).HasColumnName("price_with_tax");
            entity.Property(e => e.PriceWithoutTax).HasColumnName("price_without_tax");

            entity.HasOne(d => d.AnimeFigure).WithMany(p => p.Prices)
                .HasForeignKey(d => d.FigureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("price_figure_id_fkey");
        });

        modelBuilder.Entity<ReleaseDate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("release_date_pkey");

            entity.ToTable("release_date");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FigureId).HasColumnName("figure_id");
            entity.Property(e => e.ReleaseMonth).HasColumnName("release_month");
            entity.Property(e => e.ReleaseYear).HasColumnName("release_year");

            entity.HasOne(d => d.AnimeFigure).WithMany(p => p.ReleaseDates)
                .HasForeignKey(d => d.FigureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("release_date_figure_id_fkey");
        });

        modelBuilder.Entity<Sculptor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sculptor_pkey");

            entity.ToTable("sculptor");

            entity.HasIndex(e => new { e.FigureId, e.LanguageCode, e.Text }, "sculptor_figure_id_language_code_text_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FigureId).HasColumnName("figure_id");
            entity.Property(e => e.LanguageCode)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("language_code");
            entity.Property(e => e.Text)
                .HasMaxLength(255)
                .HasColumnName("text");

            entity.HasOne(d => d.AnimeFigure).WithMany(p => p.Sculptors)
                .HasForeignKey(d => d.FigureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sculptor_figure_id_fkey");

            entity.HasOne(d => d.LanguageCodeNavigation).WithMany(p => p.Sculptors)
                .HasForeignKey(d => d.LanguageCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sculptor_language_code_fkey");
        });

        modelBuilder.Entity<SeriesName>(entity =>
        {
            entity.HasKey(e => new { e.FigureId, e.LanguageCode }).HasName("series_name_pkey");

            entity.ToTable("series_name");

            entity.Property(e => e.FigureId).HasColumnName("figure_id");
            entity.Property(e => e.LanguageCode)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("language_code");
            entity.Property(e => e.Text)
                .HasMaxLength(255)
                .HasColumnName("text");

            entity.HasOne(d => d.AnimeFigure).WithMany(p => p.SeriesNames)
                .HasForeignKey(d => d.FigureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("series_name_figure_id_fkey");

            entity.HasOne(d => d.LanguageCodeNavigation).WithMany(p => p.SeriesNames)
                .HasForeignKey(d => d.LanguageCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("series_name_language_code_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
