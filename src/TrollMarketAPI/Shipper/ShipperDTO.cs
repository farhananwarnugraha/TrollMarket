namespace TrollMarketAPI;

public class ShipperDTO
{
    public int ShipperId { get; set; }
    public string ShipperName { get; set; } = null!;
    public decimal Price { get; set; }
    public bool? IsService { get; set; }
}
