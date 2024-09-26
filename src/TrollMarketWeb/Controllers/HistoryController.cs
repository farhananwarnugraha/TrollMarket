using System.Runtime.InteropServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketWeb;

[Authorize(Roles = "Admin")]
public class HistoryController : Controller
{
    private readonly HistoryService _service;

    public HistoryController(HistoryService service)
    {
        _service = service;
    }

    public IActionResult Index(int pageNumber=1, int pageSize=5, string sellerName="", string buyerName=""){
        var user = User.FindFirstValue("username");
        var role = User.Claims.ToList()[1].Value;
        ViewData["Username"] = user;
        ViewData["Role"] = role;
        var model = _service.Get(pageNumber, pageSize, sellerName, buyerName);
        return View(model);
    }
}
