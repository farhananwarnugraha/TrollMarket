using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness;

public interface IUserRepository
{
    List<User> Get();
    User Get(string username);
    User Insert(User model);
    User Update(User model);
}
