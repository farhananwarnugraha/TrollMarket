using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TrollMarketBusiness;
using TrollMarketDataAccess;
using TrollMarketDataAccess.Models;

namespace TrollMarketAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                policy  =>{
                    policy.WithOrigins("http://localhost:8080").AllowAnyHeader().AllowAnyMethod();
            });
        });

        IConfiguration configuration = builder.Configuration;
        IServiceCollection services = builder.Services;
        Depedencies.ConfigurationService(configuration, services);
        services.AddControllers();
        services.AddScoped<ShopService>();
        services.AddScoped<MerchandiseService>();
        services.AddScoped<IProductReopsository, ProductReopsitory>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<CartService>();
        services.AddScoped<IOrderProduct, OrderProductReopsitory>();
        services.AddScoped<ShipperService>();
        services.AddScoped<IShipperRepository, ShipperRepository>();
        services.AddScoped<AuthService>();
        services.AddScoped<ProfileService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options=>{
                options.TokenValidationParameters = new TokenValidationParameters(){
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value)
                    ),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });
        // var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors(MyAllowSpecificOrigins);
        app.MapControllers();

        // app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}
