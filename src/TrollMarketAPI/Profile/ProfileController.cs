using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketAPI;
[Route("api/v1/addbalance")]
[ApiController]

public class ProfileController:ControllerBase
{
    private readonly ProfileService _service;

    public ProfileController(ProfileService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Update(string username){
        var user = _service.Get(username);
        return Ok(user);
    }

    [HttpPut]
    [Authorize(Roles ="Buyer")]
    public IActionResult AddBalance(AddBalanceDTO addBalance){
        var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var model  = _service.Update(user,addBalance);
        return Ok(model);
    }
}
