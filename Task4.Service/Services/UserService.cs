using AutoMapper;
using Task4.Data.IRepositories;
using Task4.Domain.Entities;
using Task4.Service.DTOs;
using Task4.Service.Exceptions;
using Task4.Service.Interfaces;

namespace Task4.Service.Services;
public class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IRepository<User> repository;

    public UserService(IMapper mapper, IRepository<User> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }
    public async Task<UserForResultDto> AddAsync(UserForCreationDto userForCreationDto)
    {
        var exsistEntity = await repository.SelectAsync(x => x.Email == userForCreationDto.Email && !x.IsDeleted);
        if(exsistEntity is null)
        {
            throw new TaskException(409, "User already exsist!");
        }

        var mapped = mapper.Map<User>(userForCreationDto);
        var addedModel = await repository.InsertAsync(mapped);
        await repository.SaveAsync();
        
        return mapper.Map<UserForResultDto>(addedModel);
    }

    public Task<UserForResultDto> BlockAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<UserForResultDto> UnBlockAsync(long id)
    {
        throw new NotImplementedException();
    }
}
