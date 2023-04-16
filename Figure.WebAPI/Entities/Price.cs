namespace Figure.WebAPI.Entities;

public class Price
{
    public int Id { get; set; }

    public int FigureId { get; set; }

    public int? PriceWithTax { get; set; }

    public int? PriceWithoutTax { get; set; }

    public string Currency { get; set; } = null!;

    public string? Edition { get; set; }

    public virtual AnimeFigure AnimeFigure { get; set; } = null!;
}
