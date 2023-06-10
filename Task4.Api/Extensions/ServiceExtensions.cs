using Task4.Data.IRepositories;
using Task4.Data.Repositories;
using Task4.Domain.Entities;
using Task4.Service.Interfaces;
using Task4.Service.Services;

namespace Task4.Api.Extensions;
public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IUserService, UserService>();        
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
    }


}
