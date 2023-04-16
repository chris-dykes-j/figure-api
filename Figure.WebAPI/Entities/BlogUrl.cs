namespace Figure.WebAPI.Entities;

public partial class BlogUrl
{
    public int Id { get; set; }

    public int FigureId { get; set; }

    public string BlogUrl1 { get; set; } = null!;

    public virtual AnimeFigure AnimeFigure { get; set; } = null!;
}
