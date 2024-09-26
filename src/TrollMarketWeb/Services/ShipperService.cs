using TrollMarketBusiness;
using TrollMarketWeb.ViewModels;
using TrollMarketWeb.ViewModels.Shipper;

namespace TrollMarketWeb;

public class ShipperService
{
    private readonly IShipperRepository _repository;

    public ShipperService(IShipperRepository repository)
    {
        _repository = repository;
    }

    public ShipperIndexViewModel Get(){
        var model =  _repository.Get()
            .Select(
                shipper=> new ShipperViewModel(){
                    ShipperId = shipper.ShipperId,
                    ShipperName = shipper.ShipperName,
                    Price = shipper.Price,
                    IsServices = shipper.IsService==false?"No":"Yes"
                }
            );
        return new ShipperIndexViewModel(){
            Shippers = model.ToList()
        };
    }
}
