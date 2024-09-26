using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness;

public interface IShipperRepository
{
    List<Shipper> Get();
    // List<Shipper> Get()
    Shipper Get(int shipperId);
    Shipper Insert(Shipper model);
    Shipper Update(Shipper model);
    Shipper Delete(Shipper model);
}
