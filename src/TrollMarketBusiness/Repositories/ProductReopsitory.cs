using Microsoft.EntityFrameworkCore;
using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness;

public class ProductReopsitory : IProductReopsository
{
    private readonly TrollMarketContext _dbContext;

    public ProductReopsitory(TrollMarketContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int Count(string productName, string categoryName, string description)
    {
        return _dbContext.Products 
                .Where(
                product => product.ProductName.ToLower().Contains(productName??"".ToLower()) &&
                product.CatgoryName.ToLower().Contains(categoryName??"".ToLower()) &&
                product.DescriptionProduct.ToLower().Contains(description??"".ToLower()) &&
                product.DeletedProduct == false
            ).Count();
    }

    public int Count()
    {
        return _dbContext.Products.Count();
    }

    public Product Get(int productId)
    {
        return _dbContext.Products
            .Include(prod => prod.SellerUsernameNavigation)
            .Where(prod=>prod.Productid == productId)
            .FirstOrDefault();
    }

    public List<Product> GetBySeller(string sellerUsername)
    {
        return _dbContext.Products
        .Where(prod => prod.SellerUsername == sellerUsername)
        .ToList();
    }

    public Product GetDetailProduct(int productId)
    {
        return _dbContext.Products.Find(productId)?? throw new NullReferenceException("Data Not Found");
    }

    public List<Product> GetProduct(int pageNumber, int pageSize, string productName, string categoryName, string description)
    {
        var model = _dbContext.Products
            .Where(
                product => product.ProductName.ToLower().Contains(productName??"".ToLower()) &&
                product.CatgoryName.ToLower().Contains(categoryName??"".ToLower()) &&
                product.DescriptionProduct.ToLower().Contains(description??"".ToLower()) &&
                product.DeletedProduct == false &&
                product.Discontiue == false
            )
            .Skip((pageNumber-1)*pageSize)
            .Take(pageSize);
        return model.ToList();
    }
    public List<Product> GetProduct(int productId)
    {
        return _dbContext.Products.ToList();
    }

    public List<Product> GetProducts(int pageNumber, int pageSize)
    {
        return _dbContext.Products
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize)
                .ToList();
    }

    public int GetTransaction(int productId)
    {
        return _dbContext.Products
                .Include(prod => prod.OrderProducts)
                .Where(prod => prod.Productid == productId && prod.Discontiue == false)
                .Count();
    }

    public void Insert(Product model)
    {
        _dbContext.Products.Add(model);
        _dbContext.SaveChanges();
    }

    public void Update(Product model)
    {
        _dbContext.Products.Update(model);
        _dbContext.SaveChanges();
    }
}
