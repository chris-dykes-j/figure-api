namespace Figure.WebAPI.Entities;

public partial class Material
{
    public int Id { get; set; }

    public int FigureId { get; set; }

    public string Material1 { get; set; } = null!;

    public virtual global::Figure.WebAPI.Entities.AnimeFigure AnimeFigure { get; set; } = null!;
}
