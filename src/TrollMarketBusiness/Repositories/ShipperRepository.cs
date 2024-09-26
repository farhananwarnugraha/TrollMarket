using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness;

public class ShipperRepository : IShipperRepository
{
    private readonly TrollMarketContext _dbContext;

    public ShipperRepository(TrollMarketContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Shipper Delete(Shipper model)
    {
        _dbContext.Shippers.Remove(model);
        _dbContext.SaveChanges();
        return model;
    }

    public List<Shipper> Get()
    {
        return _dbContext.Shippers.ToList();
    }

    public Shipper Get(int shipperId)
    {
        return _dbContext.Shippers.Find(shipperId)?? throw new NullReferenceException("Data Shipper Not Found");
    }

    public Shipper Insert(Shipper model)
    {
        _dbContext.Shippers.Add(model);
        _dbContext.SaveChanges();
        return model;
    }

    public Shipper Update(Shipper model)
    {
        _dbContext.Shippers.Update(model);
        _dbContext.SaveChanges();
        return model;
    }
}
