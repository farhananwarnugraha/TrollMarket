using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness;

public interface IOrderProduct
{
    List<OrderProduct> Get(string username);
    OrderProduct Insert(OrderProduct model);
    List<OrderProduct> Get(int pageNumber, int pageSize, string sellerName, string buyerName);
    List<string> GetSellers();
    List<string> GetBuyers();
    int Count();
    decimal TotalPrice(string username);
    OrderProduct Update(OrderProduct model);
}
