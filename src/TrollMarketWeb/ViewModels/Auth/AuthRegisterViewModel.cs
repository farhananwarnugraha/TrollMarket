using System.ComponentModel.DataAnnotations;

namespace TrollMarketWeb.ViewModels.Auth;

public class AuthRegisterViewModel
{
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Compare("Password")]
    public string PasswordConfirmation { get; set; } =null!;
    [Required]
    public string? FullName { get; set; }
    [Required]
    public string? Address { get; set; }
    public string? Role { get; set; }
    public decimal? Balance { get; set; }
}
