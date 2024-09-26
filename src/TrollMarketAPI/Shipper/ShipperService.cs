using TrollMarketBusiness;
using TrollMarketDataAccess.Models;

namespace TrollMarketAPI;

public class ShipperService
{
    private readonly IShipperRepository _repository;

    public ShipperService(IShipperRepository repository)
    {
        _repository = repository;
    }

    public ShipperDTO Insert(ShipperDTO shipperDTO){
        var model = new Shipper(){
            ShipperName = shipperDTO.ShipperName,
            Price = shipperDTO.Price,
            IsService = shipperDTO.IsService
        };
        var result = _repository.Update(model);
        return new ShipperDTO(){
            ShipperName = result.ShipperName,
            Price = result.Price,
            IsService = result.IsService
        };
    }

    public ShipperDTO Get(int shipperId){
        var model = _repository.Get(shipperId);
        return new ShipperDTO(){
            ShipperId = model.ShipperId,
            ShipperName = model.ShipperName,
            Price = model.Price,
            IsService = model.IsService
        };
    }

    public ShipperDTO Update(ShipperDTO shipperDTO){
        var model = new Shipper(){
            ShipperId = shipperDTO.ShipperId,
            ShipperName = shipperDTO.ShipperName,
            Price = shipperDTO.Price,
            IsService = shipperDTO.IsService
        };
        var result = _repository.Update(model);
        return new ShipperDTO(){
            ShipperId = result.ShipperId,
            ShipperName = result.ShipperName,
            Price = result.Price,
            IsService = result.IsService
        };
    }

    public int Delete(int shipperId){
        try{
            var model = _repository.Get(shipperId);
            _repository.Delete(model);
            return 0;
        }   
        catch(Exception exception){
            return 1;
        }
    }
}
