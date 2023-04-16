namespace Figure.WebAPI.Entities;

public class Material
{
    public int Id { get; set; }

    public int FigureId { get; set; }

    public string MaterialType { get; set; } = null!;

    public virtual AnimeFigure AnimeFigure { get; set; } = null!;
}
