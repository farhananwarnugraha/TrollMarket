using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness;

public class UserRepository : IUserRepository
{
    private readonly TrollMarketContext _dbContext;

    public UserRepository(TrollMarketContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<User> Get()
    {
        return _dbContext.Users.ToList();
    }

    public User Get(string username)
    {
        return _dbContext.Users.Find(username) ?? throw new NullReferenceException("user not found");
    }

    public User Insert(User model)
    {
        _dbContext.Users.Add(model);
        _dbContext.SaveChanges();
        return model;
    }

    public User Update(User model)
    {
        _dbContext.Users.Update(model);
        _dbContext.SaveChanges();
        return model;
    }
}
