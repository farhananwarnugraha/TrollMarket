using Microsoft.EntityFrameworkCore;
using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness;

public class OrderProductReopsitory : IOrderProduct
{
    private readonly TrollMarketContext _dbContext;

    public OrderProductReopsitory(TrollMarketContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int Count()
    {
        return _dbContext.OrderProducts
        .Where(
            op => op.OrderDate != null
        ).Count();
    }

    public List<OrderProduct> Get(string username)
        {
            var model = _dbContext.OrderProducts
                .Where(
                    op => op.UsernameBuyer == username ||
                    op.Product!.SellerUsername == username &&
                    op.OrderDate !=null
                )
                .Include(
                    op => op.Product
                )
                    .ThenInclude(p => p!.SellerUsernameNavigation)
                .Include(op => op.Shipper);
            return model.ToList();
        }

    public List<OrderProduct> Get(int pageNumber, int pageSize, string sellerName, string buyerName)
    {
        var model = _dbContext.OrderProducts
            .Where(
                op => op.Product.SellerUsernameNavigation.FullName.Contains(sellerName??"".ToLower()) &&
                op.UsernameBuyerNavigation.FullName.Contains(buyerName??"".ToLower()) &&
                op.OrderDate != null
            )
            .Include(op => op.Product)
                .ThenInclude(p => p.SellerUsernameNavigation)
            .Include(op => op.UsernameBuyerNavigation)
            .Include(op => op.Shipper)
            .Skip((pageNumber-1)*pageSize)
            .Take(pageSize);
        return model.ToList();
    }

    public List<string> GetBuyers()
    {
        return _dbContext.OrderProducts
            .Select(
                op=> op.UsernameBuyerNavigation.FullName??""
            )
            .Distinct()
            .ToList();
    }


    public List<string> GetSellers()
    {
        return _dbContext.OrderProducts
            .Select(
                op=> op.Product!.SellerUsernameNavigation!.FullName??""
            )
            .Distinct()
            .ToList();
    }

    public OrderProduct Insert(OrderProduct model)
    {
        _dbContext.OrderProducts.Add(model);
        _dbContext.SaveChanges();
        return model;
    }

    public decimal TotalPrice(string username)
    {
        return _dbContext.OrderProducts
            .Where(
                op => op.UsernameBuyer == username
            )
            .Sum(op => op.TotalPrice);
    }

    public OrderProduct Update(OrderProduct model)
    {
        _dbContext.Update(model);
        _dbContext.SaveChanges();
        return model;
    }
}
