using Microsoft.AspNetCore.Mvc;

namespace TrollMarketAPI;

[Route("api/v1/auth")]
[ApiController]
public class AuthController:ControllerBase
{ 
    private readonly AuthService _service;

    public AuthController(AuthService service)
    {
        _service = service;
    }

    public IActionResult Login(AuthRequestDTO model){
        try
        {
          var response = _service.GetToken(model);
          return Ok(response);   
        }
        catch (Exception e)
        {
            return Unauthorized(e.Message);
        }
    }
}
