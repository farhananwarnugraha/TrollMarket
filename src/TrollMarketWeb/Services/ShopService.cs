using System.Globalization;
using TrollMarketBusiness;
using TrollMarketWeb.ViewModels.Product;

namespace TrollMarketWeb;

public class ShopService
{
    private readonly IProductReopsository _repository;

    public ShopService(IProductReopsository repository)
    {
        _repository = repository;
    }

    public ProductShopViewModel GetProduct(int pageNumber, int pageSize,string productName, string categoryName, string description){
        var model = _repository.GetProduct(pageNumber,pageSize,productName,categoryName,description)
            .Select(
                product => new ProductViewModel(){
                    Productid = product.Productid,
                    ProductName = product.ProductName,
                    Price = product.Price?.ToString("c0", CultureInfo.CreateSpecificCulture("id-ID"))
                }
            );
        return new ProductShopViewModel(){
            Products = model.ToList(),
            Paginations = new PaginationViewModel(){
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRows = _repository.Count(productName, categoryName, description)
            },
            ProductName = productName??"",
            CategoryName = categoryName??"",
            Description = categoryName??""
        };
    }
}
