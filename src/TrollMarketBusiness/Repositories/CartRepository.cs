using Microsoft.EntityFrameworkCore;
using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness;

public class CartRepository : ICartRepository
{
    private readonly TrollMarketContext _dbContext;

    public CartRepository(TrollMarketContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int Count(string username, int productId)
    {
        return _dbContext.OrderProducts
            .Where(
                cart => cart.UsernameBuyer == username &&
                cart.Productid == productId && 
                cart.OrderDate == null
            ).Count();
    }

    public OrderProduct Delete(OrderProduct model)
    {
        _dbContext.OrderProducts.Remove(model);
        _dbContext.SaveChanges();
        return model;
    }

    public List<OrderProduct> Get(string username)
    {
        var model = _dbContext.OrderProducts
                    .Where(
                        cart=>cart.UsernameBuyer == username &&
                        cart.OrderDate == null
                    )
                    .Include(cart=>cart.Product)
                        .ThenInclude(prod => prod.SellerUsernameNavigation)
                    .Include(cart => cart.Shipper);
        return model.ToList();
    }

    public OrderProduct Get(string username, int productId)
    {
        return _dbContext.OrderProducts
        .Where(
            cart => cart.UsernameBuyer == username &&
            cart.Productid == productId &&
            cart.OrderDate == null
        ).FirstOrDefault()?? throw new Exception("Product not found");
    }

    public OrderProduct Get(int orderProductId)
    {
        return _dbContext.OrderProducts.Find(orderProductId)?? throw new Exception("Product not found");
    }

    public OrderProduct Insert(OrderProduct model){
        _dbContext.OrderProducts.Add(model);
        _dbContext.SaveChanges();
        return model;
    }

    public OrderProduct Update(OrderProduct model)
    {
        _dbContext.OrderProducts.Update(model);
        _dbContext.SaveChanges();
        return model;
    }
}
