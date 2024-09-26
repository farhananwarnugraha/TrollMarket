using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketWeb;

[Authorize(Roles ="Buyer")]
public class ShopController : Controller
{
    private readonly ShopService _service;

    public ShopController(ShopService service)
    {
        _service = service;
    }

    public IActionResult Index(int pageNumber=1, string productName="", string categoryName="", string description=""){
        var user = User.FindFirstValue("username");
        var role = User.Claims.ToList()[1].Value;
        ViewData["Username"] = user;
        ViewData["Role"] = role;
        var viewModel = _service.GetProduct(pageNumber,3,productName,categoryName,description);
        return View(viewModel);
    }
}
