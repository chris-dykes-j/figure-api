namespace Figure.WebAPI.Entities;

public class AnimeFigure
{
    public int Id { get; set; }

    public string? Scale { get; set; }

    public string? Brand { get; set; }

    public string OriginUrl { get; set; } = null!;

    public virtual ICollection<BlogUrl> BlogUrls { get; } = new List<BlogUrl>();

    public virtual ICollection<CharacterName> CharacterNames { get; } = new List<CharacterName>();

    public virtual ICollection<FigureName> FigureNames { get; } = new List<FigureName>();
    
    public virtual ICollection<ImageUrl> ImageUrls { get; } = new List<ImageUrl>();

    public virtual ICollection<Material> Materials { get; } = new List<Material>();

    public virtual ICollection<Measurement> Measurements { get; } = new List<Measurement>();

    public virtual ICollection<Painter> Painters { get; } = new List<Painter>();

    public virtual ICollection<Price> Prices { get; } = new List<Price>();

    public virtual ICollection<ReleaseDate> ReleaseDates { get; } = new List<ReleaseDate>();

    public virtual ICollection<Sculptor> Sculptors { get; } = new List<Sculptor>();

    public virtual ICollection<SeriesName> SeriesNames { get; } = new List<SeriesName>();
}
