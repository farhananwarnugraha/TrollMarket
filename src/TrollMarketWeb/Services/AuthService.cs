using System.Security.Claims;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrollMarketBusiness;
using TrollMarketDataAccess.Models;
using TrollMarketWeb.ViewModels.Auth;

namespace TrollMarketWeb;

public class AuthService : Controller
{
    private readonly IUserRepository _repository;

    public AuthService(IUserRepository repository)
    {
        _repository = repository;
    }

    private ClaimsPrincipal GetPrincipal(AuthLoginViewModel viewModel){
        var claims = new List<Claim>(){
            new Claim("username", viewModel.Username),
            new Claim(ClaimTypes.Role, viewModel.Role??"")
        };
        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(identity);
    }

    private AuthenticationTicket GetTicket(ClaimsPrincipal principal){
        AuthenticationProperties authenticationProperties = new AuthenticationProperties(){
            IssuedUtc = DateTime.Now,
            ExpiresUtc = DateTime.Now.AddMinutes(10),
            AllowRefresh = false
        };
        AuthenticationTicket authenticationTicket = new AuthenticationTicket(
            principal, authenticationProperties, CookieAuthenticationDefaults.AuthenticationScheme
        );
        return authenticationTicket;
    }

    public AuthenticationTicket SetLogin(AuthLoginViewModel viewModel){
        var model = _repository.Get(viewModel.Username);
        bool passwordCorection = BCrypt.Net.BCrypt.Verify(viewModel.Password, model.Password);
        if(!passwordCorection || viewModel.Role!=model.Role)
            throw new LoginUsernamePasswordException("Username Or Password Incorrect");
        viewModel = new AuthLoginViewModel(){
            Username = model.Username,
            Password = model.Password,
            Role = model.Role
        };
        ClaimsPrincipal principal = GetPrincipal(viewModel);
        AuthenticationTicket ticket = GetTicket(principal);
        return ticket;
    }

    private List<SelectListItem> AddRols(){
        return new List<SelectListItem>(){
            new SelectListItem(){
                Text = "Admin",
                Value = "Admin"
            },
            new SelectListItem(){
                Text = "Seller",
                Value = "Seller"
            },
            new SelectListItem(){
                Text = "Buyer",
                Value = "Buyer"
            }
        };
    }
    public AuthLoginViewModel GetLogin(){
        return new AuthLoginViewModel(){
            Roles = AddRols()
        };
    }

    //Register
    public AuthRegisterViewModel GetRegister(){
        return new AuthRegisterViewModel();
    }
    public void AddRegister(AuthRegisterViewModel viewModel, string Role){
        var model = new User(){
            Username = viewModel.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(viewModel.Password),
            FullName = viewModel.FullName,
            Address = viewModel.Address,
            Role = Role,
            Balance = 0
        };
        _repository.Insert(model);
    }

}
