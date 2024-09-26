using System.Globalization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TrollMarketBusiness;

namespace TrollMarketAPI;

public class MerchandiseService
{
    private readonly IProductReopsository _repository;

    public MerchandiseService(IProductReopsository repository)
    {
        _repository = repository;
    }

    public MerchandiseDTO Get(int productId){
        var model = _repository.Get(productId);
        return new MerchandiseDTO(){
            Productid = model.Productid,
            ProductName = model.ProductName,
            CatgoryName = model.CatgoryName,
            DescriptionProduct = model.DescriptionProduct,
            Price = model.Price?.ToString("c", CultureInfo.CreateSpecificCulture("id-ID")),
            Discontiue = model.Discontiue==false?"No":"Yes"
        };
    }
}
