using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrollMarketWeb.ViewModels.Auth;

namespace TrollMarketWeb;

public class AuthController : Controller
{
    private readonly AuthService _service;

    public AuthController(AuthService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Index(){
        var vm = _service.GetLogin();
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Index(AuthLoginViewModel viewModel){
        if(ModelState.IsValid){
            try
            {
                var ticket = _service.SetLogin(viewModel);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ticket.Principal,
                    ticket.Properties
                );
                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
            }
        }
        var vm = _service.GetLogin();
        return View(vm);
    }

    public async Task<IActionResult> Logout(){
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index","Auth");
    }

    [HttpGet]
    public IActionResult RegisBuyer(){
        if(User?.Identity?.IsAuthenticated??false)
            return RedirectToAction("Index");
        var viewModel = _service.GetRegister();
        return View("BuyerRegister", viewModel);
    }

    [HttpPost]
    public IActionResult RegisBuyer(AuthRegisterViewModel viewModel){
        if(ModelState.IsValid){
            _service.AddRegister(viewModel, "Buyer");
            return View("Index");
        }
        var vm = _service.GetRegister();
        return View("BuyerRegister", vm);
    }

    [HttpGet]
    public IActionResult RegisSeller(){
        if(User?.Identity?.IsAuthenticated??false)
            return RedirectToAction("Index");
        var viewModel = _service.GetRegister();
        return View("SellerRegister", viewModel);
    }

    [HttpPost]
    public IActionResult RegisSeller(AuthRegisterViewModel viewModel){
        if(ModelState.IsValid){
            _service.AddRegister(viewModel, "Seller");
            return View("Index");
        }
        var vm = _service.GetRegister();
        return View("SellerRegister", vm);
    }
    
    [HttpGet("AccessDenied")]
    public IActionResult AccessDenied(){
        return View();
    }
}
