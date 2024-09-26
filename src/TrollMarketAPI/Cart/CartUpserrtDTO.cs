namespace TrollMarketAPI;

public class CartUpserrtDTO
{
    public int? Productid { get; set; }
    public string? UsernameBuyer { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public int? ShipperId { get; set; }
    public List<ProducctDTO>? Products { get; set; }
    public List<ShipperDTO>? Shippers { get; set; }
}
