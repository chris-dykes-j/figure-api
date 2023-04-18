namespace Figure.WebAPI.Utilities;

public class Release
{
    public string Edition { get; private set; }
    public int? PriceWithTax { get; private set; }
    public int? PriceWithoutTax { get; private set; }

    public Release(string edition, int? priceWithTax, int? priceWithoutTax)
    {
        Edition = edition;
        PriceWithTax = priceWithTax;
        PriceWithoutTax = priceWithoutTax;
    }
}