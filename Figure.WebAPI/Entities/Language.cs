namespace Figure.WebAPI.Entities;

public class Language
{
    public string LanguageCode { get; set; } = null!;

    public string LanguageName { get; set; } = null!;

    public virtual ICollection<CharacterName> CharacterNames { get; } = new List<CharacterName>();

    public virtual ICollection<FigureName> FigureNames { get; } = new List<FigureName>();

    public virtual ICollection<Measurement> Measurements { get; } = new List<Measurement>();

    public virtual ICollection<Painter> Painters { get; } = new List<Painter>();

    public virtual ICollection<Sculptor> Sculptors { get; } = new List<Sculptor>();

    public virtual ICollection<SeriesName> SeriesNames { get; } = new List<SeriesName>();
}
