namespace Figure.WebAPI.Entities;

public class ReleaseDate
{
    public int Id { get; set; }

    public int FigureId { get; set; }

    public int ReleaseYear { get; set; }

    public short ReleaseMonth { get; set; }

    public virtual AnimeFigure AnimeFigure { get; set; } = null!;
}
