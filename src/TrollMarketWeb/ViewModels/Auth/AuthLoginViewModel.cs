using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrollMarketWeb.ViewModels.Auth;

public class AuthLoginViewModel
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public List<SelectListItem>? Roles { get; set; }
    public string Role { get; set; } = null!;
}
