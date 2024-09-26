using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.ViewModels.Product;

namespace TrollMarketWeb;

[Authorize(Roles ="Seller")]
public class MerchandiseController : Controller
{
    private readonly MerchandiceService _service;

    public MerchandiseController(MerchandiceService service)
    {
        _service = service;
    }

    public IActionResult Index(int pageNumber=1, int pageSize =3){
        var user = User.FindFirstValue("username");
        var role = User.Claims.ToList()[1].Value;
        ViewData["Username"] = user;
        ViewData["Role"] = role;
        var viewModel = _service.Get(pageNumber, pageSize);
        return View(viewModel);
    }

    public IActionResult Insert(){
        var user = User.FindFirstValue("username");
        var role = User.Claims.ToList()[1].Value;
        ViewData["Username"] = user;
        ViewData["Role"] = role;
        var viewModel = _service.GetFormInsert();
        return View("InsertMerch", viewModel);
    }

    [HttpPost]
    public IActionResult Insert(ProductUpsertViewModel model){
        if(ModelState.IsValid){
            var user = User.FindFirstValue("username");
            ViewData["Username"]=user;
            _service.Insert(user,model);
            return RedirectToAction("Index");
        }
        var users = User.FindFirstValue("username");
        ViewData["Username"]=users;
        var viewModel = _service.Get(1,3);
        return View("InsertMerch", viewModel);
    }

    [HttpGet]
    public IActionResult Update(int productId){
        var model = _service.Get(productId);
        return View("InsertMerch",model);
    } 

    [HttpPost]
    public IActionResult Update(ProductUpsertViewModel model){
        if(ModelState.IsValid){
            _service.Update(model);
            return RedirectToAction("Index");
        }
        var vm = _service.Get(1,3);
        return View("InsertMerch", vm);
    }
}
