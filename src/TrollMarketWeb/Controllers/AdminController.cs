using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.ViewModels.Auth;

namespace TrollMarketWeb;

[Authorize(Roles ="Admin")]
public class AdminController : Controller
{
    private readonly AuthService _service;

    public AdminController(AuthService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Index(){
        var user = User.FindFirstValue("username");
        var role = User.Claims.ToList()[1].Value;
        ViewData["Username"] = user;
        ViewData["Role"] = role;
        var viewModel = _service.GetRegister();
        return View("Index", viewModel);
    }

    [HttpPost]
    public IActionResult Index(AuthRegisterViewModel viewModel){
        if(ModelState.IsValid){
            var user = User.FindFirstValue("username");
            var role = User.Claims.ToList()[1].Value;
            ViewData["Username"] = user;
            ViewData["Role"] = role;
            _service.AddRegister(viewModel, "Admin");
            return View("index");
        }
        var users = User.FindFirstValue("username");
        var roles = User.Claims.ToList()[1].Value;
        ViewData["Username"] = users;
        ViewData["Role"] = roles;
        var vm = _service.GetRegister();
        return View("Index", vm);
    }
}
