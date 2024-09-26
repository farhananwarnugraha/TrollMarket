namespace TrollMarketAPI;

public class CartDTO
{
    public int? IdOrder { get; set; }
    public int? Productid { get; set; }
    public string? UsernameBuyer { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime? OrderDate { get; set; }
    public int? ShipperId { get; set; }
}
