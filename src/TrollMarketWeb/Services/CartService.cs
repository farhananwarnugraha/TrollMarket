using System.Globalization;
using TrollMarketBusiness;
using TrollMarketWeb.ViewModels.Cart;

namespace TrollMarketWeb;

public class CartService
{
    private readonly ICartRepository _repository;

    public CartService(ICartRepository repository)
    {
        _repository = repository;
    }

    public CartIndexViewModel Get(string username){
        System.Console.WriteLine(username);
        var model = _repository.Get(username)
            .Select(
                cart => new CartViewModel(){
                    
                    IdOrder = cart.IdOrder,
                    ProductName = cart.Product.ProductName,
                    Quantity = cart.Quantity,
                    ShipperName = cart.Shipper.ShipperName,
                    SellerName = cart.Product.SellerUsernameNavigation?.FullName,
                    TotalPrice = cart.TotalPrice.ToString("c0", CultureInfo.CreateSpecificCulture("id-ID"))??"-"
                }
            ).ToList();
        return new CartIndexViewModel(){
            Carts = model
        };
    }
}
