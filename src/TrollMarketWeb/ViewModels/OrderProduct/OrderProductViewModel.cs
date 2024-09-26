namespace TrollMarketWeb.ViewModels.OrderProduct;

public class OrderProductViewModel
{
    public int IdOrder { get; set; }
    public string? ProductName { get; set; }
    public string? UsernameBuyer { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public string? OrderDate { get; set; }
    public string? ShipperName { get; set; }
}
