namespace TrollMarketWeb.ViewModels.User;

public class UserViewModel
{
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string Role { get; set; } = null!;
    public decimal? Balance { get; set; }
}
