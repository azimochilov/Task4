namespace Task4.Service.Interfaces;
public interface IAuthorizationService
{
    Task<bool> AuthorizeAsync();
}
