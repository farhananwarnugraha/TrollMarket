namespace TrollMarketWeb.ViewModels.Shipper;

public class ShipperViewModel
{
    public int ShipperId { get; set; }
    public string ShipperName { get; set; } = null!;
    public decimal Price { get; set; }
    public string IsServices { get; set; }
}
