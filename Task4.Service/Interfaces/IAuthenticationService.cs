using Task4.Service.DTOs;

namespace Task4.Service.Interfaces;
public interface IAuthenticationService
{
    Task<string> AuthenticateAsync(UserForResultDto user);
}
