using TrollMarketWeb.ViewModels.OrderProduct;

namespace TrollMarketWeb.ViewModels.User;

public class UserIndexViewModel
{
    public UserViewModel User { get; set; }
    public List<OrderProductViewModel> Transaction { get; set; }
    public decimal Balance { get{
        decimal balance = 50000;
        return balance;
    } }
}
