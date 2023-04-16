namespace Figure.WebAPI.Entities;

public partial class ReleaseDate
{
    public int Id { get; set; }

    public int FigureId { get; set; }

    public int ReleaseYear { get; set; }

    public short ReleaseMonth { get; set; }

    public virtual global::Figure.WebAPI.Entities.AnimeFigure AnimeFigure { get; set; } = null!;
}
