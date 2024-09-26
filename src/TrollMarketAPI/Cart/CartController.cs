using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TrollMarketAPI.Cart;

namespace TrollMarketAPI;

[Route("api/v1/trollmarket")]
[ApiController]
[Authorize(Roles ="Buyer")]
public class CartController : ControllerBase
{
    private readonly CartService _service;

    public CartController(CartService service)
    {
        _service = service;
    }


    [HttpGet]
    public IActionResult Add(int productId){
        var shipperdto = _service.Add(productId);
        return Ok(shipperdto);
    }
    // public IActionResult Insert(CartUpserrtDTO model){
    //     var cartResult = _service.Insert("buyer1",model);
    //     return Ok(cartResult);
    // }

    [HttpPost]
    public IActionResult AddCart(CartUpserrtDTO viewModel){
        var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var cart = _service.Insert(user!, viewModel);
        return Ok(cart);
    }

    [HttpPost("purcessall")]
    public IActionResult PurcessAll(PurcessAllDTO viewModel){
        viewModel.usernameBuyer = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var cart = _service.PurcessAll(viewModel.usernameBuyer!);
        return Ok(cart);
    }
    [HttpDelete]
    public IActionResult Delete(int orderProductId){
        var orderDateIsNull = _service.Delete(orderProductId);
        if(orderDateIsNull == 0){
            _service.Delete(orderProductId);
            return Ok("Deleted Sucesedfully");
        }
        else{
            return BadRequest("Delete Failed");
        }
    }
}
