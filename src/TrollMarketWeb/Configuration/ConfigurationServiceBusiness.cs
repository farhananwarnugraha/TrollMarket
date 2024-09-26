using TrollMarketBusiness;

namespace TrollMarketWeb;

public static class ConfigurationServiceBusiness
{
    public static IServiceCollection AddBusinessService(this IServiceCollection services){
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<UserService>();
        services.AddScoped<AuthService>();
        services.AddScoped<HistoryService>();
        services.AddScoped<IOrderProduct, OrderProductReopsitory>();
        services.AddScoped<ShopService>();
        services.AddScoped<MerchandiceService>();
        services.AddScoped<IProductReopsository, ProductReopsitory>();
        services.AddScoped<ShipperService>();
        services.AddScoped<IShipperRepository, ShipperRepository>();
        services.AddScoped<CartService>();
        services.AddScoped<ICartRepository, CartRepository>();
        return services;
    }
}
