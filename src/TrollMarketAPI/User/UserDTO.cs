namespace TrollMarketAPI;

public class UserDTO
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string Role { get; set; } = null!;
    public decimal? Balance { get; set; }
}
