namespace TrollMarketBusiness;

public class LoginUsernamePasswordException : Exception
{
    public LoginUsernamePasswordException(string? message) : base(message)
    {
    }
}
