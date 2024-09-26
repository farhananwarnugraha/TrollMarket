using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TrollMarketAPI.Cart;
using TrollMarketBusiness;
using TrollMarketDataAccess.Models;

namespace TrollMarketAPI;

public class CartService
{
    private readonly IOrderProduct _repository;
    private readonly IUserRepository _userRepository;
    private readonly IProductReopsository _productRepository;
    private readonly IShipperRepository _shipperRepository;
    private readonly ICartRepository _cartRepository;

    public CartService(IOrderProduct repository, IUserRepository userRepository, IProductReopsository productRepository, IShipperRepository shipperRepository, ICartRepository cartRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _productRepository = productRepository;
        _shipperRepository = shipperRepository;
        _cartRepository = cartRepository;
    }

    public CartUpserrtDTO Add(int productId){
        return new CartUpserrtDTO(){
            Shippers = _shipperRepository.Get()
                    .Where(ship => ship.IsService == true)
                    .Select(ship=> new ShipperDTO(){
                            ShipperId = ship.ShipperId,
                            ShipperName = ship.ShipperName
                    }
                    ).ToList(),
            Products = _productRepository.GetProduct(productId)
                        .Where(prod => prod.Productid == productId)
                        .Select(prouct => new ProducctDTO(){
                            Productid = prouct.Productid
                        })
                        .ToList()
        };
    }

    public CartUpserrtDTO Insert(string usernameBuyer, CartUpserrtDTO viewModel){
        var shipper = _shipperRepository.Get(viewModel.ShipperId??0);
        var product = _productRepository.Get(viewModel.Productid??0);
        var model = new OrderProduct(){
            Productid = viewModel.Productid,
            UsernameBuyer = usernameBuyer,
            Quantity = viewModel.Quantity,
            ShipperId = viewModel.ShipperId,
            TotalPrice = (decimal)((product.Price * viewModel.Quantity)+ shipper.Price) 
        };
        var totalCartProduct = _cartRepository.Count(usernameBuyer, product.Productid);
        if(totalCartProduct >0 ){
            var cartProduct = _cartRepository.Get(usernameBuyer, product.Productid);
            if(cartProduct.ShipperId == shipper.ShipperId){
                cartProduct.TotalPrice += (decimal)(product.Price * viewModel.Quantity)!;
                cartProduct.Quantity += viewModel.Quantity;
                _cartRepository.Update(cartProduct);
                return new CartUpserrtDTO(){
                    Productid = cartProduct.Productid,
                    UsernameBuyer = cartProduct.UsernameBuyer,
                    Quantity = cartProduct.Quantity,
                    ShipperId = cartProduct.ShipperId,
                    TotalPrice = cartProduct.TotalPrice
                };
            }else{
                var res = _repository.Insert(model);
                return new CartUpserrtDTO(){
                    Productid = res.Productid,
                    UsernameBuyer = res.UsernameBuyer,
                    Quantity =  res.Quantity,
                    ShipperId = res.ShipperId,
                    TotalPrice = res.TotalPrice
                };
            }
        }
        var result = _repository.Insert(model);
        return new CartUpserrtDTO(){
            Productid = result.Productid,
            UsernameBuyer = result.UsernameBuyer,
            Quantity = result.Quantity,
            ShipperId = result.ShipperId,
            TotalPrice = result.TotalPrice
        };
    }

    public PurcessAllResponseDTO PurcessAll(string usernameBuyer){
        var totalPrices = _repository.TotalPrice(usernameBuyer);
        var waletBalance = _userRepository.Get(usernameBuyer).Balance;
        
        if(waletBalance >= totalPrices){
            var modelView = _repository.Get(usernameBuyer);
            foreach(var item in modelView){
                var product = _productRepository.Get(item.Productid??0);
                if(product.Discontiue == false){
                    item.OrderDate = DateTime.Now;
                    item.UsernameBuyer = usernameBuyer;
                    var seller = _userRepository.Get(product.SellerUsername!);
                    seller.Balance += product.Price;
                    _repository.Update(item);
                    _userRepository.Update(seller);
                }
            }
            var user = _userRepository.Get(usernameBuyer);
            user.Balance = waletBalance - totalPrices;
            _userRepository.Update(user);
            return new PurcessAllResponseDTO(){
                status = 200,
                message = "Purcess All Success"
            };
        }
        return new PurcessAllResponseDTO(){
            status = 400,
            message = "Not enough balance"
        };
    }

    public int Delete(int OrderProductId){
        // var model = _cartRepository.Get(OrderProductId);
        // _cartRepository.Delete(model);
        // return 0;
        try{
            var model = _cartRepository.Get(OrderProductId);
            _cartRepository.Delete(model);
            return 0;
        }catch(Exception exception){
            return 1;
        }
    }
    
}
