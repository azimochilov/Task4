using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Task4.Data.IRepositories;
using Task4.Domain.Entities;
using Task4.Domain.Enums;
using Task4.Service.DTOs;
using Task4.Service.Interfaces;

namespace Task4.Service.Services;
public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration configuration;
    private readonly IRepository<User> repository;

    public AuthenticationService(IRepository<User> repository, IConfiguration configuration)
    {
        this.configuration = configuration;
        this.repository = repository;
    }

    public async Task<string> AuthenticateAsync(UserForResultDto dto)
    {
        var user = await repository
            .SelectAsync(u => u.Email == dto.Email);

        if (user is null)
            return null;

        if (user.Type == Status.Blocked)
            return "b";
        user.LastLoginTime = DateTime.UtcNow;
        await repository.SaveAsync();
        return GenerateToken(user);
    }

    private string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                 new Claim("Id", user.Id.ToString()),
            }),
            Audience = configuration["JWT:Audience"],
            Issuer = configuration["JWT:Issuer"],
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(configuration["JWT:Expire"])),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
