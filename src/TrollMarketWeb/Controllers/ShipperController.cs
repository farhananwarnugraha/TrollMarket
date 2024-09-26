using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketWeb;

[Authorize(Roles ="Admin")]
public class ShipperController : Controller
{
    private readonly ShipperService _service;

    public ShipperController(ShipperService service)
    {
        _service = service;
    }

    public IActionResult Index(){
        var user = User.FindFirstValue("username");
        var role = User.Claims.ToList()[1].Value;
        ViewData["Username"] = user;
        ViewData["Role"] = role;
        var viewModel = _service.Get();
        return View(viewModel);
    }
}
