using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness;

public interface ICartRepository
{
    List<OrderProduct> Get(string username);
    OrderProduct Get(int orderProductId);
    int Count(string username, int productId);
    OrderProduct Get(string username, int productId);
    OrderProduct Insert(OrderProduct model);
    OrderProduct Update(OrderProduct model);
    OrderProduct Delete(OrderProduct model);

}
