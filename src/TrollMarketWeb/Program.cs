using Microsoft.AspNetCore.Authentication.Cookies;
using TrollMarketDataAccess;

namespace TrollMarketWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        IServiceCollection services = builder.Services;
        services.AddControllersWithViews();
        Depedencies.ConfigurationService(builder.Configuration, services);
        services.AddBusinessService();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options=>{
                options.Cookie.Name = "AuthCookie";
                options.LoginPath = "/Auth";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.AccessDeniedPath = "/AccessDenied";
            });
        services.AddAuthorization();
        var app = builder.Build();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllerRoute(
            name:"default",
            pattern:"{controller=Auth}/{action=Index}"
        );
        // app.MapGet("/", () => "Hello World!");
        app.UseStaticFiles();
        app.Run();
    }
}
