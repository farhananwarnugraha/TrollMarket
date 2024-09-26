using TrollMarketBusiness;
using TrollMarketDataAccess.Models;

namespace TrollMarketAPI;

public class ProfileService
{
    private readonly IUserRepository _repository;

    public ProfileService(IUserRepository repository)
    {
        _repository = repository;
    }

    public ProfileDTO Get(string username){
        var model = _repository.Get(username);
        return new ProfileDTO(){
            Username = model.Username,
            Balance = model.Balance
        };
    }

    public ProfileDTO Update(string username, AddBalanceDTO balanceDTO){
        var model = _repository.Get(username);
        model.Balance = model.Balance + balanceDTO.Balance;
        _repository.Update(model);
        return new ProfileDTO(){
            Username = model.Username,
            Balance = model.Balance
        };
    }
}
