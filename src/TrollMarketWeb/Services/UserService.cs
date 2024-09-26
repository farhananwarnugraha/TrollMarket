using TrollMarketBusiness;
using TrollMarketWeb.ViewModels.OrderProduct;
using TrollMarketWeb.ViewModels.User;

namespace TrollMarketWeb;

public class UserService
{
    private readonly IUserRepository _repository;
    private readonly IOrderProduct _repositoryOrder;

    public UserService(IUserRepository repository, IOrderProduct repositoryOrder)
    {
        _repository = repository;
        _repositoryOrder = repositoryOrder;
    }

    public UserIndexViewModel Get(string username){
        var model = _repository.Get(username);
        var transaction = _repositoryOrder.Get(username)
            .Select(
                order => new OrderProductViewModel(){
                    OrderDate = order.OrderDate?.ToString("dd/MM/yyyy"),
                    ProductName = order.Product?.ProductName,
                    Quantity = order.Quantity,
                    ShipperName = order.Shipper?.ShipperName,
                    TotalPrice = order.TotalPrice
                }
            );
        return new UserIndexViewModel(){
            Transaction = transaction.ToList(),
            User = new UserViewModel(){
                FullName = model.FullName,
                Role = model.Role,
                Address = model.Address,
                Balance = model.Balance
            }
        };
    }
}
