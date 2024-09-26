using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness;

public interface IProductReopsository
{
    List<Product> GetProduct(int pageNumber, int pageSize, string productName, string categoryName, string description);
    int Count(string productName, string categoryName, string description);
    List<Product> GetProducts(int pageNumber, int pageSize);
    int Count();
    int GetTransaction(int productId);
    Product Get(int pricuctId);
    Product GetDetailProduct(int productId);
    List<Product> GetProduct(int productId);
    List<Product> GetBySeller(string sellerUsername);
    void Insert(Product model);
    void Update(Product model);
}
