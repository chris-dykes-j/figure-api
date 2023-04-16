namespace Figure.WebAPI.Entities;

public class BlogUrl
{
    public int Id { get; set; }

    public int FigureId { get; set; }

    public string Url { get; set; } = null!;

    public virtual AnimeFigure AnimeFigure { get; set; } = null!;
}
