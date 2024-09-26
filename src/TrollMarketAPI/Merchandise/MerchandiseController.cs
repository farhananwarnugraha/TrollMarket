using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketAPI;

[Route("api/v1/merchandise")]
[ApiController]
[Authorize(Roles ="Seller")]
public class MerchandiseController:ControllerBase
{
    private readonly MerchandiseService _service;

    public MerchandiseController(MerchandiseService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get(int productId){
        var model = _service.Get(productId);
        return Ok(model);
    }
}
