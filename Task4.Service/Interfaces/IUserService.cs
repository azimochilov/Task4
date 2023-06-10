using Task4.Service.DTOs;

namespace Task4.Service.Interfaces;
public interface IUserService
{
    Task<UserForResultDto> AddAsync(UserForCreationDto userForCreationDto);
    Task<bool> DeleteAsync(long id);
    Task<UserForResultDto> BlockAsync(long id);
    Task<UserForResultDto> UnBlockAsync(long id);
    Task<IEnumerable<UserForResultDto>> GetAllAsync();

}
