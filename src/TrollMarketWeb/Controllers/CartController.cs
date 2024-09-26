using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketWeb;
[Authorize(Roles ="Buyer")]
public class CartController:Controller
{
    private readonly CartService _service;

    public CartController(CartService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Index(){
        var user = User.FindFirstValue("username");
        var role = User.Claims.ToList()[1].Value;
        System.Console.WriteLine(user);
        ViewData["Username"] = user;
        ViewData["Role"] = role;
        var viewModel = _service.Get(user);
        return View("Index", viewModel);
    }
}
