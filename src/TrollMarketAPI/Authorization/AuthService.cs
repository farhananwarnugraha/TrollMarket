using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TrollMarketBusiness;
using TrollMarketDataAccess.Models;

namespace TrollMarketAPI;

public class AuthService
{
    private readonly IUserRepository _userRepositoy;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepositoy, IConfiguration configuration)
    {
        _userRepositoy = userRepositoy;
        _configuration = configuration;
    }

    //create token
    private AuthResponseDTO CreateToken(User model){
        List<Claim> claims = new List<Claim>(){
            new Claim(ClaimTypes.NameIdentifier, model.Username),
            new Claim(ClaimTypes.Role, model.Role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value
            )
        );

        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credential
        );
        var serializeToken = new JwtSecurityTokenHandler().WriteToken(token);
        return new AuthResponseDTO(){
            Username = model.Username,
            Token = serializeToken
        };
    }

    public AuthResponseDTO GetToken(AuthRequestDTO authRequestDTO){
        var model = _userRepositoy.Get(authRequestDTO.Username);
        bool passwordCorrection = BCrypt.Net.BCrypt.Verify(authRequestDTO.Password, model.Password);
        if(passwordCorrection)
            return CreateToken(model);
        throw new LoginUsernamePasswordException("Username and Password Incorrect");
    }
}
