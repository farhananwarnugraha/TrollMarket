using TrollMarketBusiness;
using TrollMarketDataAccess.Models;
using TrollMarketWeb.ViewModels.Product;

namespace TrollMarketWeb;

public class MerchandiceService
{
    private readonly IProductReopsository _repository;

    public MerchandiceService(IProductReopsository repository)
    {
        _repository = repository;
    }

    public ProductIndexViewModel Get(int pageNumber, int pageSize){
        var model = _repository.GetProducts(pageNumber, pageSize)
            .Select(
                merch => new ProductViewModel(){
                    Productid = merch.Productid,
                    ProductName = merch.ProductName,
                    CatgoryName = merch.CatgoryName,
                    Discontiue = merch.Discontiue==false?"No":"Yes"
                }
            );
        return new ProductIndexViewModel(){
            Merchandise = model.ToList(),
            Paginations = new PaginationViewModel(){
                PageNumber = pageNumber, 
                PageSize = pageSize,
                TotalRows = _repository.Count()
            }
        };
    }

    public ProductUpsertViewModel GetFormInsert(){
        return new ProductUpsertViewModel();
    }

    public void Insert(string username, ProductUpsertViewModel viewModel){
        var model = new Product(){
            ProductName = viewModel.ProductName,
            CatgoryName = viewModel.CatgoryName,
            DescriptionProduct = viewModel.DescriptionProduct,
            Price = viewModel.Price,
            Discontiue = viewModel.Discontiue,
            SellerUsername = username
        };
        _repository.Insert(model);
    }

    public ProductUpsertViewModel Get(int productId){
        var model = _repository.GetDetailProduct(productId);
        return new ProductUpsertViewModel(){
            Productid = model.Productid,
            ProductName = model.ProductName,
            CatgoryName = model.CatgoryName,
            DescriptionProduct = model.DescriptionProduct,
            Price = model.Price,
            Discontiue = model.Discontiue??false
        };
    }

    public void Update(ProductUpsertViewModel model){
        var viewModel = new Product(){
            Productid = model.Productid,
            ProductName = model.ProductName,
            CatgoryName = model.CatgoryName,
            DescriptionProduct = model.DescriptionProduct,
            Price = model.Price,
            Discontiue = model.Discontiue
        };
        _repository.Update(viewModel);
    }

    public void Discontinue(int id){
        var model = _repository.Get(id);
        model.Discontiue = false;
        _repository.Update(model);
    }
}
