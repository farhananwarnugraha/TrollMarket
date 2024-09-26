namespace TrollMarketWeb.ViewModels.Cart;

public class CartViewModel
{
    public int IdOrder { get; set; }
    public string? ProductName { get; set; }
    public string? SellerName { get; set; }
    public int Quantity { get; set; }
    public string? TotalPrice { get; set; }
    public string? OrderDate { get; set; }
    public string? ShipperName { get; set; }
}
