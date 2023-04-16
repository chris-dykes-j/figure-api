namespace Figure.WebAPI.Entities;

public class SeriesName
{
    public int FigureId { get; set; }

    public string LanguageCode { get; set; } = null!;

    public string Text { get; set; } = null!;

    public virtual AnimeFigure AnimeFigure { get; set; } = null!;

    public virtual Language LanguageCodeNavigation { get; set; } = null!;
}
