using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketAPI;
[Route("api/v1/trollmarket/shipment")]
[ApiController]
[Authorize(Roles ="Admin")]
public class ShipperController : ControllerBase
{
    private readonly ShipperService _service;

    public ShipperController(ShipperService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Insert(ShipperDTO shipperDTO){
        shipperDTO = _service.Insert(shipperDTO);
        return Ok(shipperDTO);
    }

    [HttpGet]
    public IActionResult Update(int shipperId){
        var shipperById = _service.Get(shipperId);
        return Ok(shipperById); 
    }

    [HttpPut]
    public IActionResult Update(ShipperDTO shipperDTO){
        shipperDTO = _service.Update(shipperDTO);
        return Ok(shipperDTO);
    }
    
    [HttpDelete]
    public IActionResult Delete(int shipperId){
        var totalTrnasaction = _service.Delete(shipperId);
        if(totalTrnasaction == 0)
            return Ok("Succes Deleted");
        return BadRequest("Deleted Failed");
    }
}
