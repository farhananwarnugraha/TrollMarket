using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketAPI;

[Route("api/v1/TrollMarket/shop")]
[ApiController]
[Authorize(Roles ="Buyer")]
public class ShopController : ControllerBase
{
    private readonly ShopService _service;

    public ShopController(ShopService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get(int productId){
        var detailProduct = _service.Get(productId);
        return Ok(detailProduct);
    }
}
