namespace TrollMarketWeb.ViewModels.History;

public class HistoryViewModel
{
    public int IdOrder { get; set; }
    public string? ProductName { get; set; }
    public string? NameSeller { get; set; }
    public string? NameBauyer { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public string? OrderDate { get; set; }
    public string? ShipperName { get; set; }
}
