using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
    }

    public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                ClockSkew = TimeSpan.Zero
            };
        });
    }

}
