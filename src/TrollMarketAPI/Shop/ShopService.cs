using System.Globalization;
using TrollMarketBusiness;

namespace TrollMarketAPI;

public class ShopService
{
    private readonly IProductReopsository _repository;

    public ShopService(IProductReopsository repository)
    {
        _repository = repository;
    }

    public ShopDTO Get(int ProductId){
        var model = _repository.Get(ProductId);
        return new ShopDTO(){
            Productid = model.Productid,
            ProductName = model.ProductName,
            CatgoryName = model.CatgoryName,
            DescriptionProduct = model.DescriptionProduct,
            Price = model.Price?.ToString("c", CultureInfo.CreateSpecificCulture("Id-ID"))??"-",
            SellerUsername = model.SellerUsernameNavigation.FullName
        };
    }
}
