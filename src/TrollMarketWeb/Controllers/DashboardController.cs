using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketWeb;

[Authorize]
public class DashboardController : Controller
{
    public IActionResult Index(){
        var user = User.FindFirstValue("username");
        var role = User.Claims.ToList()[1].Value;
        ViewData["Username"] = user;
        ViewData["Role"] = role;
        return View();
    }
}
