namespace TrollMarketWeb.ViewModels.Product;

public class ProductShopViewModel
{
    public List<ProductViewModel> Products { get; set; }
    public string ProductName { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public PaginationViewModel Paginations { get; set; }
}
