using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketWeb;

[Authorize(Roles ="Buyer,Seller")]
public class ProfileController : Controller
{
    private readonly UserService _service;

    public ProfileController(UserService service)
    {
        _service = service;
    }

    // [HttpGet("Profile/{username}")]
    public IActionResult Index(){
        var user = User.FindFirstValue("username");
        var role = User.Claims.ToList()[1].Value;
        ViewData["Username"] = user;
        ViewData["Role"] = role;
        var viewModel = _service.Get(user);
        return View("Index",viewModel);
    }
}
